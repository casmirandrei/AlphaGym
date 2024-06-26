﻿using AlphaGym.Models;
using AlphaGym.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlphaGym.Controllers
{
    public class AboutUs : BaseController
    {
        public AboutUs(CartService cartService) : base(cartService)
        {
        }

        public IActionResult Index()
        {
            var model = new AboutUsViewModel
            {
                Tagline = "Where Strength Meets Passion",
                Description = "Welcome to Alpha Gym – your dedicated partner on the journey to a healthier, stronger, and more empowered you. At Alpha Gym, we're not just a gym; we're a community that believes in the transformative power of fitness."
            };
            return View(model);
        }
    }
}
