using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    /// <summary>
    /// Employee controller
    /// </summary>
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Current database context.
        /// </summary>
        private readonly LibContext _context;

        /// <summary>
        /// Creates a new instance of <see cref="EmployeeController"/>.
        /// </summary>
        /// <param name="context">Current database context</param>
        public EmployeeController(LibContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Index controller action.
        /// </summary>
        /// <returns>Employee index view</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        /// <summary>
        /// Details controller actions.
        /// </summary>
        /// <param name="id"><see cref="Employee"/> id</param>
        /// <returns>Details view for the specified employee if it exists</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        /// <summary>
        /// Create GET controller action for an employee.
        /// </summary>
        /// <returns>The view for creating a new employee</returns>
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create POST action for saving a new employee.
        /// </summary>
        /// <param name="employee">The employee to create.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,BirthDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        /// <summary>
        /// Edit GET controller action for editing an employee.
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <returns>View for editing the employee.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        /// <summary>
        /// Edit POST controller action.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="employee">Updated employee data</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,BirthDate")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        /// <summary>
        /// Delete controller action.
        /// </summary>
        /// <param name="id">Employee id to delete.</param>
        /// <returns>View for deleting an employee.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        /// <summary>
        /// Delete POST controller action for finally removing the employee.
        /// </summary>
        /// <param name="id">Employee id to delete.</param>
        /// <returns>Redirect to Index action.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks whether the employee still exists.
        /// </summary>
        /// <param name="id">Id of the <see cref="Employee"/>.</param>
        /// <returns>Whether the specified employee exists.</returns>
        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
