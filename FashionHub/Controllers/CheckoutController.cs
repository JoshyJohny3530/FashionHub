using FashionHub.Data;
using FashionHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FashionHub.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Sample order items (this would normally come from the cart)
            var orderItems = new List<OrderItem>
            {
                new OrderItem { ItemName = "T-Shirt", Price = 25.00m, Quantity = 2 },
                new OrderItem { ItemName = "Jeans", Price = 50.00m, Quantity = 1 }
            };

            var viewModel = new CheckoutViewModel
            {
                OrderItems = orderItems,
                TotalAmount = 100.00m // This would be calculated based on the cart items
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the payment and order (this would include integration with a payment gateway)
                // Save the order to the database

                return RedirectToAction("Success");
            }

            // If the model is invalid, return the same view with validation errors
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            string userId = User.Identity.Name;

            var cart = _context.Carts.Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart != null)
            {

                _context.CartItems.RemoveRange(cart.CartItems);


                _context.SaveChanges();
            }

            return RedirectToAction("Confirm");
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }
    }
}