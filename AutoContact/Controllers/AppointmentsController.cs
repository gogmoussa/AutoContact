using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoContact.Models;
using AutoContact.Helpers;
using System.Security.Claims;

namespace AutoContact.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AutoContactContext _context;

        public AppointmentsController(AutoContactContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            ViewData["Events"] = JSONHelper.GetAppointmentListJSONString(_context.Appointments.ToList());
            var autoContactContext = _context.Appointments.Include(a => a.Car);
            return View(await autoContactContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Car)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["Clients"] = new SelectList(_context.Clients, "ClientId", "FirstName");
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "Model");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentDate,AppointmentStartTime,BookedAtTime,Message,BookingEmployeeId,ClientId,Car")] Appointment appointment)
        {
            if (ModelState.IsValid)  
            {
                if (User.FindFirstValue(ClaimTypes.Role) == "Client")
                    appointment.ClientId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                appointment.BookedAtTime = DateTime.Now;
                appointment.BookingEmployeeId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                appointment.Car.Vin ??= "N/A";
                _context.Add(appointment);
                _context.Add(appointment.Car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "Model", appointment.CarId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.Include(a => a.Car).FirstOrDefaultAsync(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["Clients"] = new SelectList(_context.Clients, "ClientId", "FirstName", appointment.ClientId);
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "Model", appointment.CarId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("AppointmentId,AppointmentDate,AppointmentStartTime,BookedAtTime,Message,BookingEmployeeId,ClientId,Car")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment.Car);                    
                    _context.Update(appointment);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["Clients"] = new SelectList(_context.Clients, "ClientId", "FirstName", appointment.ClientId);
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "Model", appointment.CarId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Car)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(long id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
