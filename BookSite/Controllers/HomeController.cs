using BookSite.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BookSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Products.ToList());
        }
    }
}
