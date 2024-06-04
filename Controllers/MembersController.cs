using System.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlphaGym.Models;
using Microsoft.AspNetCore.Authorization;
using AlphaGym.Services;


namespace AlphaGym.Controllers
{
    [Authorize (Roles ="administrator")]
    public class MembersController : BaseController
    {
        private readonly GymContext _context;

        public MembersController(CartService cartService, GymContext context) : base(cartService)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index(string SearchString)
        {

            //return View(await _context.Members.ToListAsync());

            ViewData["CurrentFilter"] = SearchString;
            


            var members = from m in _context.Members 
                          select m;
            if(!String.IsNullOrEmpty(SearchString))
            {
                members= members.Where(m => m.Name.Contains(SearchString));

            }
            

            return View(await members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,SubscriptionType,MonthlySubscription,SubscriptionExpiryDate")] Member member, DateTime subscriptionStartDate, int subscriptionMonths, string SubscriptionType)
        {
            if (ModelState.IsValid)
            {
                // set subscription date
                member.SubscriptionStartDate = subscriptionStartDate;
                //  calculate the total months for subscription
                int totalDaysToAdd = subscriptionMonths * 30;
                // automatically set subscription expiry date
                member.SubscriptionExpiryDate = subscriptionStartDate.AddDays(totalDaysToAdd);

                // Add the member to the database
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { monthlySubscription = member.MonthlySubscription });
            }
            return View(member);
        }
        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            
           
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,SubscriptionType,MonthlySubscription,SubscriptionExpiryDate")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Retrieve subscription start date and subscription months from the existing member object
                DateTime subscriptionStartDate = member.SubscriptionExpiryDate; // Assuming you have this property in Member class
                int subscriptionMonths = member.MonthlySubscription; // Assuming MonthlySubscription represents the number of months

                // Calculate the total days to add based on the number of months
                int totalDaysToAdd = subscriptionMonths * 30;

                // Add the calculated days to the subscription start date to get the new expiration date
                member.SubscriptionExpiryDate = subscriptionStartDate.AddDays(totalDaysToAdd);

                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
