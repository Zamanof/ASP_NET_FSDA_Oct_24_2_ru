using ASP_NET_04._Products_MVC.Data;
using ASP_NET_04._Products_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP_NET_04._Products_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsMVCContext _context;

        public ProductsController(ProductsMVCContext context)
        {
            _context = context;
        }

        // Get
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // Get: Products/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var product = await _context
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return NotFound();
            return View(product);
        }
    }
}


/*
 GetAll 
        GET -> html
 Create
        GET -> html
        POST
*/