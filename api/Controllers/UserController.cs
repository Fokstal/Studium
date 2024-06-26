using api.Data;
using api.Models;
using api.Model.DTO;
using api.Services;
using api.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using api.Extensions;
using api.Helpers.Constants;
using System.Security.Claims;
using api.Helpers.Enums;

using static api.Helpers.Enums.RoleEnum;
using static api.Helpers.Enums.PermissionEnum;

namespace api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController(IConfiguration configuration, AppDbContext db) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserRepository _userRepository = new(db);
        private readonly StudentRepository _studentRepository = new(db);
        private readonly SubjectRepository _subjectRepository = new(db);
        private readonly GroupRepository _groupRepository = new(db);

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequireRoles([Admin, Secretar])]
        [RequirePermissions([ViewUser])]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetListAsync()
            => Ok(await _userRepository.GetListAsync());

        [HttpGet("list-by-session")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetListBySessionAsync()
        {
            IEnumerable<UserEntity> userEntityList = [];

            try
            {
                Guid userIdSession = new HttpContextService(HttpContext).GetUserIdFromCookie();
                HashSet<RoleEnum> roleListUserSession = await _userRepository.GetRoleListAsync(userIdSession);

                if (new Authorizing(_userRepository, HttpContext).IsAdminAndSecretarRole())
                {
                    return Ok(await _userRepository.GetListAsync());
                }

                if (roleListUserSession.Contains(Curator) && !roleListUserSession.Contains(Teacher))
                {
                    userEntityList = userEntityList.Concat(await _userRepository.GetListByCuratorAsync(userIdSession));
                }

                if (roleListUserSession.Contains(Teacher))
                {
                    userEntityList = userEntityList.Concat(await _userRepository.GetListByTeacherAsync(userIdSession));
                }

                if (roleListUserSession.Contains(Student))
                {
                    userEntityList = userEntityList.Concat([await _userRepository.GetListByStudentAsync(userIdSession)])!;
                }
            }
            catch
            {
                return Unauthorized();
            }

            return Ok(userEntityList);
        }

        [HttpGet("list-by-subject/{subjectId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetListAsync(int subjectId)
        {
            if (subjectId < 1) return BadRequest();

            SubjectEntity? subjectEntity = await _subjectRepository.GetAsync(subjectEntityId: subjectId);

            if (subjectEntity is null) return NotFound();

            GroupEntity groupEntity = await
                _groupRepository
                .GetAsync(groupEntityId: subjectEntity.GroupEntityId)
                ?? throw new Exception("Group on Subject in Db is null!");

            Authorizing authorizing = new(_userRepository, HttpContext);

            if (!authorizing.IsAdminAndSecretarRole())
            {
                bool userAccess = await authorizing.RequireOwnerListAccess
                ([
                    new()
                    {
                        IdList = [groupEntity.CuratorId],
                        Role = Curator
                    },
                    new()
                    {
                        IdList = [subjectEntity.TeacherId],
                        Role = Teacher
                    },
                    new()
                    {
                        IdList = groupEntity.StudentEntityList.Select(s => s.Id).ToArray(),
                        Role = Student
                    },
                ]);

                if (userAccess is false) return Forbid();
            }

            List<StudentEntity> studentEntityList = groupEntity.StudentEntityList;

            return Ok(await _userRepository.GetListAsync(studentEntityList: studentEntityList));
        }

        [HttpGet("session")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserEntity>> GetSessionAsync()
        {
            string? token = HttpContext.Request.Cookies[CookieNames.USER_TOKEN];

            if (token is null) return Unauthorized();

            Claim userIdClaim = JwtProvider.GetClaimFromToken(token, CustomClaims.USER_ID);

            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId)) return Unauthorized();

            return Ok(await _userRepository.GetAsync(userEntityId: userId));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequireRoles([Admin, Secretar])]
        [RequirePermissions([RegisterUser])]
        public async Task<ActionResult<RegisterUserDTO>> RegisterAsync([FromBody] RegisterUserDTO userDTO)
        {
            if (await _userRepository.GetAsync(login: userDTO.Login) is not null)
            {
                ModelState.AddModelError("Custom", "User already Exists!");

                return BadRequest(ModelState);
            }

            userDTO.Id = Guid.NewGuid();

            await _userRepository.AddAsync(_userRepository.Create(userDTO));

            return Created("User", userDTO);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDTO userDTO)
        {
            UserEntity? userEntity = await _userRepository.GetAsync(login: userDTO.Login);

            if (userEntity is null) return NotFound();

            if (StringHasher.Verify(userDTO.Password, userEntity.PasswordHash) is false)
                return BadRequest("Password is not valid!");

            HttpContext.Response.Cookies
            .Append(CookieNames.USER_TOKEN, new JwtProvider(_configuration)
            .GenerateToken(userEntity));

            return Ok();
        }

        [HttpPost("logout")]
        public IActionResult LogoutAsync()
        {
            HttpContext.Response.Cookies.Delete(CookieNames.USER_TOKEN);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequirePermissions([EditUser])]
        public async Task<IActionResult> DeleteAsync(Guid userId)
        {
            UserEntity? userEntityToRemove = await _userRepository.GetNoTrackingAsync(userEntityId: userId);

            if (userEntityToRemove is null) return NotFound();

            if (UserService.CheckRoleContains(_userRepository, userEntityToRemove, Student))
            {
                StudentEntity studentToRemove = await
                    _studentRepository
                    .GetAsync(userEntityToRemove.Id)
                    ?? throw new Exception("Student on User is null!");

                await _studentRepository.RemoveAsync(studentToRemove);
            }

            await _userRepository.RemoveAsync(userEntityToRemove);

            return NoContent();
        }
    }
}