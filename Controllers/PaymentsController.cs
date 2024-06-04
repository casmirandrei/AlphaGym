using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AlphaGym.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AlphaGym.Controllers
{
    [Authorize(Roles = "administrator")]
    public class PaymentsController : Controller
    {
        private readonly GymContext _context;

        public PaymentsController(GymContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var payments = _context.Payments
                                   .Include(p => p.PurchasedItems)
                                   .ToList();
            return View(payments);
        }

        [HttpPost]
        public IActionResult ValidatePayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentID == id);
            if (payment != null)
            {
                payment.IsPaid = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult InvalidatePayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentID == id);
            if (payment != null)
            {
                payment.IsPaid = false;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentID == id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
