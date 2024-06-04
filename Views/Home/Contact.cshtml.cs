using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlphaGym.Views.Home
{
    public class ContactModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Contact Us";
        }
    }
}
