using api.Helpers.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(role => role.Id);

            builder.HasMany(role => role.PermissionEntityList)
                .WithMany(permission => permission.RoleEntityList)
                .UsingEntity<RolePermissionEntity>
                (
                    r => r.HasOne<PermissionEntity>().WithMany().HasForeignKey(r => r.PermissionEntityId),
                    l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(l => l.RoleEntityId)
                );

            IEnumerable<RoleEntity> roleList = Enum.GetValues<RoleEnum>().Select(roleEnum => new RoleEntity
                {
                    Id = Convert.ToInt32(roleEnum),
                    Name = roleEnum.ToString(),
                });

            builder.HasData(roleList);
        }
    }
}