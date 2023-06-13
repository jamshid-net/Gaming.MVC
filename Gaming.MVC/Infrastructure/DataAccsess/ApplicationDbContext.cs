﻿using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gaming.MVC.Infrastructure.DataAccsess;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    public DbSet<Admin> Admins { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}