using api.Data;
using api.Models;
using api.Model.DTO;
using api.Helpers.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Data
{
    public class UserRepository(AppDbContext db) : DataRepositoryBase<UserEntity, RegisterUserDTO>(db)
    {
        public async Task<UserEntity?> GetAsync(string login)
        {
            UserEntity? user = await _db.User.FirstOrDefaultAsync(userDb => userDb.Login.ToLower() == login.ToLower());

            return user;
        }

        public override async Task AddAsync(UserEntity user)
        {
            RoleEntity role =
                await _db.Role
                    .SingleOrDefaultAsync(roleDb => roleDb.Id == Convert.ToInt32(RoleEnum.Student))
                    ?? throw new InvalidOperationException();

            user.RoleList.Add(role);

            await _db.User.AddAsync(user);

            await _db.SaveChangesAsync();
        }

        public override Task UpdateAsync(UserEntity valueToUpdate, RegisterUserDTO valueDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<HashSet<PermissionEnum>> GetPermissionListAsync(int id)
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

        public async Task<HashSet<RoleEnum>> GetRoleListAsync(int id)
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
                .Select(role => (RoleEnum) role.Id)
                .ToHashSet();
        }
    }
}