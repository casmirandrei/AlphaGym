using AlphaGym.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using AlphaGym.Services;

namespace AlphaGym.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(CartService cartService) : base(cartService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact Us";
            return View();
        }

     
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
    }
}