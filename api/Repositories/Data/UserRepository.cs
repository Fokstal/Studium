using api.Data;
using api.Models;
using api.Model.DTO;
using api.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using api.Services;

namespace api.Repositories.Data
{
    public class UserRepository(AppDbContext db) : DataRepositoryBase<UserEntity, RegisterUserDTO>(db)
    {
        public async override Task<IEnumerable<UserEntity>> GetListAsync()
        {
            IEnumerable<UserEntity> userList = await 
                _db.User
                .Select(userDb => new UserEntity()
                {
                    Id = userDb.Id,
                    Login = userDb.Login,
                    FirstName = userDb.FirstName,
                    MiddleName = userDb.MiddleName,
                    LastName = userDb.LastName,
                    PasswordHash = userDb.PasswordHash,
                    DateCreated = userDb.DateCreated,
                    RoleList = userDb.RoleList.Select(roleDb => new RoleEntity { Id = roleDb.Id, Name = roleDb.Name }).ToList(),
                })
                .ToListAsync();

            return userList;
        }

        public override Task<UserEntity?> GetAsync(int id) => throw new NotImplementedException();

        public async Task<UserEntity?> GetAsync(Guid id)
        {
            UserEntity? user = await _db.User.FirstOrDefaultAsync(userDb => userDb.Id == id);

            return user;
        }

        public async Task<UserEntity?> GetNoTrackingAsync(Guid id)
        {
            UserEntity? user = await _db.User.AsNoTracking().FirstOrDefaultAsync(userDb => userDb.Id == id);

            return user;
        }

        public async Task<UserEntity?> GetAsync(string login)
        {
            UserEntity? user = await _db.User.FirstOrDefaultAsync(userDb => userDb.Login.ToLower() == login.ToLower());

            return user;
        }

        public override async Task AddAsync(UserEntity user)
        {
            await _db.User.AddAsync(user);

            await _db.SaveChangesAsync();
        }

        public override UserEntity Create(RegisterUserDTO userDTO)
        {
            return new()
            {
                Id = userDTO.Id,
                Login = userDTO.Login,
                FirstName = userDTO.FirstName,
                MiddleName = userDTO.MiddleName,
                LastName = userDTO.LastName,
                PasswordHash = StringHasher.Generate(userDTO.Password),
                DateCreated = DateTime.Now,
                RoleList = GetRolesEntityByEnum(userDTO.RoleList),
            };
        }

        public override Task UpdateAsync(UserEntity valueToUpdate, RegisterUserDTO valueDTO) => throw new NotImplementedException();

        public async Task<HashSet<PermissionEnum>> GetPermissionListAsync(Guid id)
        {
            List<RoleEntity>[] roleList = await
                _db.User
                    .AsNoTracking()
                    .Include(user => user.RoleList)
                    .ThenInclude(role => role.PermissionList)
                    .Where(user => user.Id == id)
                    .Select(user => user.RoleList)
                    .ToArrayAsync();

            return roleList
                .SelectMany(role => role)
                .SelectMany(role => role.PermissionList)
                .Select(permission => (PermissionEnum)permission.Id)
                .ToHashSet();
        }

        public async Task<HashSet<RoleEnum>> GetRoleListAsync(Guid id)
        {
            List<RoleEntity>[] roleList = await
                _db.User
                    .AsNoTracking()
                    .Include(user => user.RoleList)
                    .ThenInclude(role => role.PermissionList)
                    .Where(user => user.Id == id)
                    .Select(user => user.RoleList)
                    .ToArrayAsync();

            return roleList
                .SelectMany(role => role)
                .Select(role => (RoleEnum)role.Id)
                .ToHashSet();
        }

        public List<RoleEntity> GetRolesEntityByEnum(RoleEnum[] roleEnumList)
        {
            List<RoleEntity> roleEntityList = new();

            foreach (RoleEnum roleEnum in roleEnumList)
            {
                roleEntityList.Add
                (
                    _db.Role
                    .SingleOrDefault(roleDb => roleDb.Name == roleEnum.ToString())
                    ?? throw new Exception("RoleDb and RoleEnum is not Equal!")
                );
            }

            return roleEntityList;
        }
    }
}