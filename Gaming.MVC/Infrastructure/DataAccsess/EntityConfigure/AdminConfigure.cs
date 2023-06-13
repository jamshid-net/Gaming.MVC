using Gaming.MVC.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.MVC.Infrastructure.DataAccsess.EntityConfigure;

public class AdminConfigure : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasIndex(x => x.AdminName).IsUnique();
    }
}
