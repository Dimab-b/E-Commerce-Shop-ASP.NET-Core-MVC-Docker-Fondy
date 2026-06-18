using FirstSiteShopWithMvc.Areas.Identity.Data;
using FirstSiteShopWithMvc.Data;
using FirstSiteShopWithMvc.Models;
using FirstSiteShopWithMvc.Services;
using FirstSiteShopWithMvc.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SKit.LiqPay.SDK;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(9, 0, 0))
    ));
builder.Services.AddDbContext<ApplicationUserContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(9, 0, 0))
    ));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationUserContext>();

builder.Services.AddScoped<IPaymentService, FondyPayService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseSession();
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    int retryCount = 6; 
    while (retryCount > 0)
    {
        try
        {
            var context = services.GetRequiredService<AppDbContext>(); 

            
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Set<Category>().Any())
            {
               
                var catShoes = new Category { Name = "Кроси" };
                var catHoodies = new Category { Name = "Худі" };

                context.Set<Category>().AddRange(catShoes, catHoodies);
                context.SaveChanges(); 

                
                var initialItems = new List<Item>
        {
            new Item
            {
                Name = "Nike air max 95",
                Price = 6800,
                Description = "Кроси для темщіків",
                FullText = "Cool shoes",
                Image = "shoes95.png",
                CategoryId = catShoes.Id
            },
            new Item
            {
                Name = "Represent",
                Price = 4550,
                Description = "Худі базове чорне",
                FullText = "Minimalistic hoodie with brand-name",
                Image = "represent.png",
                CategoryId = catHoodies.Id
            },
            new Item
            {
                Name = "Nike air force 1",
                Price = 4400,
                Description = "Кросівки білі",
                FullText = "White shoes",
                Image = "force.png",
                CategoryId = catShoes.Id
            }
        };

                context.Set<Item>().AddRange(initialItems);
                context.SaveChanges(); 

                logger.LogInformation("База даних успішно наповнена оригінальними товарами!");
            }
            break; 
        }
        catch (Exception ex)
        {
            retryCount--;
            logger.LogWarning("База даних ще не готова. Очікуємо 5 секунд... Спроб залишилось: {Count}", retryCount);
            System.Threading.Thread.Sleep(5000); 

            if (retryCount == 0)
            {
                logger.LogError(ex, "Не вдалося накотити міграції. База даних недоступна.");
                throw;
            }
        }
    }
}

app.Run();
app.Run();
