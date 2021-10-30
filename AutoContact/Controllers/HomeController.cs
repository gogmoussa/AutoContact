using AutoContact.Models;
using AutoContactApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoContact.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AutoContactApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AutoContactContext _context;

        public HomeController(ILogger<HomeController> logger, AutoContactContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ScheduleAppointment()
        {
            return View();
        }
        public IActionResult ViewAppointment()
        {
            return View();
        }

        public IActionResult ClientProfile()
        {
            return View();
        }
        public IActionResult MechanicMechanic()
        {
            return View();
        }

        public IActionResult AdminMechanic()
        {
            return View();
        }
        public IActionResult AdminClient()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        // POST: Home/AdminLogin
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    var employee = await (from emp in _context.Employees
                                        where emp.Email == email
                                        select new { Email = emp.Email, Password = emp.HashPass, Salt = emp.HashSalt }).FirstOrDefaultAsync();

                    if (employee != null && Crypto.hashPassword(password, employee.Salt).Equals(employee.Password))
                    {
                        HttpContext.Session.SetString("email", email);
                        return RedirectToAction(nameof(AdminDashboard));
                    }
                }
            }
            ModelState.AddModelError("Error", "Invalid Login");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction(nameof(AdminLogin));
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
