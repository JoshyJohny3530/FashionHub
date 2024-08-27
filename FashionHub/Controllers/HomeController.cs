using FashionHub.Data;
using FashionHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FashionHub.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        
    }
}
