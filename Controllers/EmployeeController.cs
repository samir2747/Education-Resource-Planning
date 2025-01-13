using InstitudeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly TeknowellContext context;
        public EmployeeController(TeknowellContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Employee()
        {
            // Fetch the employees and order them by descending Eid
            var data = await context.Employees
                .OrderByDescending(x => x.Eid)
                .ToListAsync();

            // Return the sorted data to the view
            return View(data);
        }


        public IActionResult AddEmp()//page create page
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmp(Employee emp)
        {
            if (ModelState.IsValid)
            {
                await context.Employees.AddAsync(emp);
                await context.SaveChangesAsync(); //call to save changes
                TempData["Insert"] = "Data Inserted Successfully..";
                return RedirectToAction("Employee", "Employee");
            }
            return View(emp);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Employees == null)
            {
                return NotFound();
            }
            var stdData = await context.Employees.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Employee employee)
        {
            if (id != employee.Eid)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
                TempData["Update"] = "Data Updated Successfully..";
                return RedirectToAction("Employee", "Employee");
            }
            return View(employee);
        }
        public async Task<IActionResult> Details(int? id)//page create page
        {
            if (id == null || context.Employees == null)
            {
                return NotFound();
            }
            var stdData = await context.Employees.FirstOrDefaultAsync(x => x.Eid == id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Employees == null)
            {
                return NotFound();
            }
            var stdData = await context.Employees.FirstOrDefaultAsync(x => x.Eid == id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//Protect Application From Atackers Or Hackers
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var stdData = await context.Employees.FindAsync(id);
            if (stdData != null)
            {
                context.Employees.Remove(stdData);
            }
            await context.SaveChangesAsync();
            TempData["deleted"] = "Data Deleted Successfully..";
            return RedirectToAction("Employee", "Employee");
        }
    }
}
