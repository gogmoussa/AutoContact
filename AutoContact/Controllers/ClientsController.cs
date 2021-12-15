using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoContact.Models;
using AutoContact.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace AutoContact.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AutoContactContext _context;

        public ClientsController(AutoContactContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.Include(c => c.Address).ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Client model = new Client();
            model.Address = new Address();
            return View(model);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ClientId,FirstName,LastName,DriverLicence,BirthDate,AddressId,Email,PhoneNum,HashPass,HashSalt,Address,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(client.Password) || client.Password.Length < 6)
                {
                    ModelState.AddModelError("Password", "Password has to be atleast 6 characters long!");
                    return View(client);
                }
                client.HashSalt = Crypto.generateSalt();
                client.HashPass = Crypto.hashPassword(client.Password, client.HashSalt);
                if (string.IsNullOrEmpty(client.Address.UnitNum))
                    client.Address.UnitNum = "";

                _context.Add(client);
                await _context.SaveChangesAsync();

                AccessLevel clientAccessLevel = new AccessLevel();
                clientAccessLevel.AccessLevel1 = "Client";
                clientAccessLevel.ClientId = client.ClientId;

                _context.Add(clientAccessLevel);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(c => c.Address).FirstOrDefaultAsync(c => c.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ClientId,FirstName,LastName,DriverLicence,BirthDate,AddressId,Email,PhoneNum,HashPass,HashSalt,Password")] Client client, [Bind("AddressId,StreetNum,UnitNum,StreetName,CityName,ProvinceName,Country")] Address address)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.HashSalt = Crypto.generateSalt();
                    client.HashPass = Crypto.hashPassword(client.HashPass, client.HashSalt);
                    _context.Update(client);

                    if (address.UnitNum == null)
                        address.UnitNum = " ";

                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(long id)
        {
            return _context.Clients.Any(c => c.ClientId == id);
        }
    }
}

