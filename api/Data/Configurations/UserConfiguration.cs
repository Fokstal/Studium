using api.Helpers.Enums;
using api.Models;
using api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(user => user.Id);

            builder.HasMany(user => user.RoleList)
                .WithMany(role => role.UserList)
                .UsingEntity<UserRoleEntity>
                (
                    l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(l => l.RoleId),
                    r => r.HasOne<UserEntity>().WithMany().HasForeignKey(r => r.UserId)
                );

            IEnumerable<UserEntity> userList = Enum.GetValues<RoleEnum>().Select(roleEnum => new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Login = roleEnum.ToString().ToLower(),
                    PasswordHash = StringHasher.Generate(GenerateCorrectPasswordByLine(roleEnum.ToString().ToLower())),
                });

            builder.HasData(userList);
        }

        private static string GenerateCorrectPasswordByLine(string line)
        {
            if (line.Length > 9) return line;

            line += line;

            return GenerateCorrectPasswordByLine(line);
        }
    }
}