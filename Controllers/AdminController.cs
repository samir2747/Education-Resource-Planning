using InstitudeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly TeknowellContext context;
        public AdminController(TeknowellContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Admission(DateTime? startDate, DateTime? endDate, string searchString)
        {
            // Start with the basic query
            var admissions = context.Addmissions.AsQueryable();

            // Filter for the date range
            if (startDate.HasValue && endDate.HasValue)
            {
                admissions = admissions.Where(a => a.AddmissionDate >= startDate.Value && a.AddmissionDate <= endDate.Value);
            }

            // Filter for search term (Name, Course, etc.)
            if (!string.IsNullOrEmpty(searchString))
            {
                admissions = admissions.Where(a =>
                    a.Name.Contains(searchString) || // Example: Replace with your actual field
                    a.Course.Contains(searchString) ||// Example: Assuming you want to search by course name
                    a.City.Contains(searchString) // Example: Assuming you want to search by city
                );
            }

            // Include related CourseNavigation if needed
            admissions = admissions.Include(a => a.CourseNavigation);

            // Apply sorting by descending Id
            var sortedAdmissions = await admissions.OrderByDescending(a => a.Id).ToListAsync();

            // Return the sorted and filtered admissions to the view
            ViewData["SearchString"] = searchString; // Pass the search string back to the view
            ViewData["startDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["endDate"] = endDate?.ToString("yyyy-MM-dd");

            return View(sortedAdmissions);
        }





        [HttpGet("Admin/addd/{id}")]
        public async Task<IActionResult> Addd(int id)
        {
            var inquiry = await context.Inquiries.FindAsync(id);
            if (inquiry == null)
            {
                return NotFound();
            }

            var admission = new Addmission
            {
                Name = inquiry.Name,
                Gender = inquiry.Gender,
                Dob = inquiry.Dob,
                City = inquiry.City,
                ContactNo=inquiry.Contact,
                Qualification = inquiry.Qualification,
                CurrentAddress=inquiry.Address,
                Email = inquiry.Email,
                InquiryId = inquiry.Id
            };

            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
           

            return View(admission);
            

        }
        [HttpPost]
        public async Task<IActionResult> Addd(Addmission admission)
        {
            if (ModelState.IsValid)
            {
               
                admission.Id = 0; 
                await context.Addmissions.AddAsync(admission); 
                await context.SaveChangesAsync();

                TempData["Insert"] = "Data Inserted Successfully.";
                return RedirectToAction("Admission", "Admin"); 
            }
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");

            return View(admission); 
        }

        public IActionResult Add()//page create page
        {
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            //        var courses = context.Courses  // Assuming you have a Course model
            //.Select(c => new
            //{
            //    c.Name,
            //    DisplayText = $"{c.Name} ({c.Duration})"
            //})
            //.ToList();

            //        ViewBag.Courses = new SelectList(courses, "Name", "DisplayText");



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Addmission admission)
        {
            if (ModelState.IsValid)
            {
                await context.Addmissions.AddAsync(admission);
                await context.SaveChangesAsync(); //call to save changes
                TempData["Insert"] = "Data Inserted Successfully..";
                return RedirectToAction("Admission", "Admin");
            }
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            return View(admission);
        }
 

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Addmissions == null)
            {
                return NotFound();
            }
            var stdData = await context.Addmissions.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Addmission admission)
        {
            if (id != admission.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Addmissions.Update(admission);
                await context.SaveChangesAsync();
                TempData["Update"] = "Data Updated Successfully..";
                return RedirectToAction("Admission", "Admin");
            }
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            return View(admission);
        }
        public async Task<IActionResult> Details(int? id)//page create page
        {
            if (id == null || context.Addmissions == null)
            {
                return NotFound();
            }
            var stdData = await context.Addmissions.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Addmissions == null)
            {
                return NotFound();
            }
            var stdData = await context.Addmissions.FirstOrDefaultAsync(x => x.Id == id);
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
            var stdData = await context.Addmissions.FindAsync(id);
            if (stdData != null)
            {
                context.Addmissions.Remove(stdData);
            }
            await context.SaveChangesAsync();
            TempData["deleted"] = "Data Deleted Successfully..";
            return RedirectToAction("Admission", "Admin");
        }

    }
}
