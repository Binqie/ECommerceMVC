using ECommerceMVC.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Data;

public class ApplicationContext : DbContext
{
    private DbSet<User> Users { get; set; } = null!;
    private DbSet<Order> Orders { get; set; } = null!;
    private DbSet<Product> Products { get; set; } = null!;
    private DbSet<Cart> Carts { get; set; } = null!;
    private DbSet<Category> Categories { get; set; } = null!;
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}