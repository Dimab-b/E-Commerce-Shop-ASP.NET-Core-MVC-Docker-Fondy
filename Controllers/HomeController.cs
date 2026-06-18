using FirstSiteShopWithMvc.Data;
using FirstSiteShopWithMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FirstSiteShopWithMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        { 
            List<Item> items = await _context.item.ToListAsync(); 
            return View(items);
        }

        public IActionResult About()
        {
            return View();
        }


        public async Task<ActionResult> Product(int id)
        {
            Item item = await _context.item.FindAsync(id) ?? new Item();
            return View(item);
        }

        public async Task<ActionResult> Shoes()
        {
            List<Item> items = await _context.item.Where(el => el.CategoryId == 1).ToListAsync();
            return View(items);
        }


        public async Task<ActionResult> Hoodies()
        {
            List<Item> items = await _context.item.Where(el => el.CategoryId == 3).ToListAsync();
            return View(items);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
