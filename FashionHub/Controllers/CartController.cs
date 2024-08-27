using FashionHub.Data;
using FashionHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionHub.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToCart(int itemId, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page
                return Redirect("/Identity/Account/Login");
            }

            string userId = User.Identity.Name;

            var cart = _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Item)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                // Create a new cart if it doesn't exist
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ItemId == itemId);
            if (cartItem != null)
            {
                // Update the quantity if the item already exists in the cart
                cartItem.Quantity += quantity;
            }
            else
            {
                // Add a new item to the cart if it doesn't exist
                var item = GetItemFromDatabase(itemId); // Retrieve item details from the database
                if (item != null)
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ItemId = item.ItemId,
                        Item = item,
                        Quantity = quantity,
                        Cart = cart
                    });
                }
            }

            // Save changes to the database
            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

       
        private Item GetItemFromDatabase(int itemId)
        {
            return _context.Items.Find(itemId);
        }

      
        public IActionResult Cart()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            string userId = User.Identity.Name;
            var cart = _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Item)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                // If no cart found, return an empty list or a suitable view
                return View(new List<CartItem>());
            }

            return View(cart.CartItems);
        }


        [HttpPost]
        public IActionResult RemoveFromCart(int itemId)
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
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ItemId == itemId);
                if (cartItem != null)
                {
                    // Remove the item from the cart's item list
                    cart.CartItems.Remove(cartItem);

                    // Remove the cart item from the database
                    _context.CartItems.Remove(cartItem);

                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Cart");
        }

    }
}
