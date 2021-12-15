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
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly AutoContactContext _context;

        public EmployeesController(AutoContactContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var autoContactContext = _context.Employees.Include(e => e.Address).Include(e => e.ManagerNavigation);
            return View(await autoContactContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> MechanicDetails(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Address)
                .Include(e => e.ManagerNavigation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Address)
                .Include(e => e.ManagerNavigation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Employee model = new Employee();
            model.Address = new Address();
            model.EmployeeAccessLevel = new AccessLevel();
            model.AllEmployees = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.FirstName} {e.LastName}"
            }).ToList();
            return View(model);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,AddressId,Email,PhoneNum,EmployeeSin,Manager,HireDate,Password,EmployeeAccessLevel,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(employee.Password) || employee.Password.Length < 6)
                {
                    ModelState.AddModelError("Password", "Password has to be atleast 6 characters long!");
                    return View(employee);
                }
                employee.HashSalt = Crypto.generateSalt();
                employee.HashPass = Crypto.hashPassword(employee.Password, employee.HashSalt);
                if (string.IsNullOrEmpty(employee.Address.UnitNum))
                    employee.Address.UnitNum = "";

                _context.Add(employee);
                await _context.SaveChangesAsync();

                employee.EmployeeAccessLevel.EmployeeId = employee.EmployeeId;
                _context.Add(employee.EmployeeAccessLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(x => x.Address).Include(y => y.AccessLevels).FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EmployeeId,FirstName,LastName,AddressId,Email,PhoneNum,EmployeeSin,Manager,HireDate,TerminationDate,TerminationReason,Password,HashPass,HashSalt")] Employee employee, [Bind("AddressId,StreetNum,UnitNum,StreetName,CityName,ProvinceName,Country")] Address address)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(employee.Password))
                    {
                        if (employee.Password.Length >= 6)
                        {
                            employee.HashSalt = Crypto.generateSalt();
                            employee.HashPass = Crypto.hashPassword(employee.Password, employee.HashSalt);
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Password has to be atleast 6 characters long!");
                            return View(employee);
                        }
                    }

                    _context.Update(employee);
                    //await _context.SaveChangesAsync();

                    if (address.UnitNum == null)
                        address.UnitNum = " ";

                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> MechanicEdit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(x => x.Address).FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MechanicEdit(long id, [Bind("EmployeeId,FirstName,LastName,AddressId,Email,PhoneNum,EmployeeSin,Manager,HireDate,TerminationDate,TerminationReason,HashPass,HashSalt")] Employee employee, [Bind("AddressId,StreetNum,UnitNum,StreetName,CityName,ProvinceName,Country")] Address address)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    //await _context.SaveChangesAsync();

                    if (address.UnitNum == null)
                        address.UnitNum = " ";

                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(nameof(MechanicDetails), employee);
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
               .Include(e => e.Address)
               .Include(e => e.ManagerNavigation)
               .Include(y => y.AccessLevels)
               .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var employee = await _context.Employees.Include(e => e.Address)
               .Include(e => e.ManagerNavigation)
               .Include(y => y.AccessLevels).FirstOrDefaultAsync(e => e.EmployeeId == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
