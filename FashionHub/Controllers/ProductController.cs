using FashionHub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FashionHub.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId = 0)
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.selectedCategory=categoryId.ToString();
            var products = categoryId != 0
                ? _context.Items.Where(i => i.CategoryId == categoryId).ToList()
                : _context.Items.ToList();

            return View(products);
        }


        public IActionResult Details(int id)
        {
            var product = _context.Items.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int itemId, int quantity)
        {
            // Logic to add item to cart
            return RedirectToAction("Cart", "Cart");
        }
    }
}
