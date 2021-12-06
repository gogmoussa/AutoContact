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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AutoContactApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AutoContactContext _context;

        public HomeController(ILogger<HomeController> logger, AutoContactContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AdminDashboard", new { id = User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
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
        public IActionResult MechanicMechanic()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult AdminMechanic()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AdminLogin(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AdminDashboard", new { id = User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // POST: Home/AdminLogin
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(string email, string password, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    var employee = await (from emp in _context.Employees
                                          where emp.Email == email
                                          select new { EmployeeId = emp.EmployeeId, Email = emp.Email, Password = emp.HashPass, Salt = emp.HashSalt }).FirstOrDefaultAsync();

                    if (employee != null && Crypto.hashPassword(password, employee.Salt).Equals(employee.Password))
                    {
                        var role = _context.AccessLevels.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId)?.AccessLevel1;
                        HttpContext.Session.SetString("email", email);
                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, email),
                                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
                                new Claim(ClaimTypes.Role, role),
                            };


                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            //AllowRefresh = <bool>,
                            // Refreshing the authentication session should be allowed.

                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                            // The time at which the authentication ticket expires. A 
                            // value set here overrides the ExpireTimeSpan option of 
                            // CookieAuthenticationOptions set with AddCookie.

                            //IsPersistent = true,
                            // Whether the authentication session is persisted across 
                            // multiple requests. When used with cookies, controls
                            // whether the cookie's lifetime is absolute (matching the
                            // lifetime of the authentication ticket) or session-based.

                            //IssuedUtc = <DateTimeOffset>,
                            // The time at which the authentication ticket was issued.

                            //RedirectUri = <string>
                            // The full path or absolute URI to be used as an http 
                            // redirect response value.
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        return RedirectToAction("AdminDashboard", new { id = employee.EmployeeId });
                    }
                }
            }
            ModelState.AddModelError("Error", "Invalid Login");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("email");
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(AdminLogin));
        }

        public async Task<IActionResult> AdminDashboard(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
