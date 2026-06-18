using Microsoft.EntityFrameworkCore;
using FirstSiteShopWithMvc.Models;
using System.Collections;


namespace FirstSiteShopWithMvc.Data;

    public class AppDbContext : DbContext
    {
    public DbSet<Blog> posts { get; set; } = null!;
    public DbSet<Item> item { get; set; } = null!;
    public DbSet<Category> categories { get; set; } = null!;
    public DbSet<Order> orders { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }

