using Gaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Application.Common.Interfaces;

public interface IApplicationDbContext
{


  
    public DbSet<Product> Products { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Order> Orders { get; }
    public DbSet<Cart> Carts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
