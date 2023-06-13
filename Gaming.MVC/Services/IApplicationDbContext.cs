using Gaming.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.Services;

public interface IApplicationDbContext
{
    public DbSet<Admin> Admins { get; }

    public DbSet<User> Users { get; }
    public DbSet<Product> Products { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Order> Orders { get; }
    public DbSet<Cart> Carts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
}
