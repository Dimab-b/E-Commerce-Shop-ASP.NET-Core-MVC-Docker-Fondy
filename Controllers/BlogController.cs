using Microsoft.AspNetCore.Mvc;
using FirstSiteShopWithMvc.Data;
using FirstSiteShopWithMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstSiteShopWithMvc.Controllers
{
    public class BlogController : Controller
    {

        private readonly AppDbContext _context;

        public BlogController (AppDbContext context) => _context = context;
     

        public async Task<ActionResult> Index()
        {
            List<Blog> blogs = await _context.posts.ToListAsync();
            return View(blogs);
        }
    }
}
