using Microsoft.AspNetCore.Mvc;
using AlphaGym.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlphaGym.Controllers
{
    public class BaseController : Controller
    {
        protected readonly CartService _cartService;

        public BaseController(CartService cartService)
        {
            _cartService = cartService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.CartCount = _cartService.GetCartCount();
            base.OnActionExecuting(context);
        }
    }
}
