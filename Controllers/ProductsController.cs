using Microsoft.AspNetCore.Mvc;
using AlphaGym.Models;
using AlphaGym.Services;
using System.Linq;
using AlphaGym.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AlphaGym.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly GymContext _context;

        public ProductsController(GymContext context, CartService cartService) : base(cartService)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View(products);
        }

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            TempData["Message"] = "Added to cart";
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            var cartItems = _cartService.GetCartItems();
            ViewBag.CartTotal = _cartService.GetCartTotal().ToString("C");
            return View(cartItems);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            var model = new CheckoutViewModel
            {
                Email = User.Identity.Name
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cartItems = _cartService.GetCartItems();

                // Save the payment information
                var payment = new Payment
                {
                    UserID = User.Identity.Name,
                    UserName = User.Identity.Name, // Replace with actual user name if available
                    PhoneNumber = model.PhoneNumber,
                    CardNumber = model.CardNumber,
                    PaymentDate = DateTime.Now,
                    Amount = (double)_cartService.GetCartTotal(), // Explicitly cast decimal to double
                    IsPaid = false,
                    PurchasedItems = cartItems.Select(item => new PurchasedItem
                    {
                        ProductName = item.Product.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.Product.UnitPrice // Explicitly cast decimal to double
                    }).ToList()
                };

                _context.Payments.Add(payment);
                _context.SaveChanges();

                // Clear the cart
                _cartService.ClearCart();

                TempData["Message"] = "Checkout successful!";
                return RedirectToAction("Index");
            }

            // If the model state is invalid, return the view with the current model to display validation errors
            return View(model);
        }

        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Cart");
        }
    }
}
