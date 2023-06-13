using Gaming.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.MVC.DataAccsess.EntityConfigure;

public class UserConfigure : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x=> x.UserName).IsUnique();
        builder.HasIndex(x=> x.Email).IsUnique();
    }
}
