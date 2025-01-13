using InstitudeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagementSystem.Controllers
{
   
    public class UserController : Controller
    {
        private readonly TeknowellContext context;
        public UserController(TeknowellContext context)
        {
            this.context = context;
        }
        
        public IActionResult Dashboard()
        {
            var totalEqn=context.Inquiries.Count();
            ViewData["TotalInquiry"]=totalEqn;

            var totalAdd = context.Addmissions.Count();
            ViewData["TotalAdd"] = totalAdd;

            if (HttpContext.Session.GetString("Usersession") != null)
            {
                ViewBag.mysession = HttpContext.Session.GetString("Usersession").ToString();

            }
            else
            {
                return RedirectToAction("Login","Home");
            }
            return View();
        }


        public async Task<IActionResult> Table(string searchString)
        {
            // Start with the base query
            var entities = from e in context.Inquiries
                           select e;

            // Apply the search filter if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                entities = entities.Where(e => e.Name.Contains(searchString) ||
                                                e.City.Contains(searchString) ||
                                                e.Course.Contains(searchString));
            }

            // Include related data for the CourseNavigation
            entities = entities.Include(a => a.CourseNavigation);

            // Sort by Id in descending order and get the results
            var sortedAdmissions = await entities.OrderByDescending(a => a.Id).ToListAsync();

            // Return the sorted admissions to the view
            return View(sortedAdmissions);
        }




        public IActionResult Insert()//page create page
        {
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert( Inquiry inquiry)
        {
            if (ModelState.IsValid)
            {
                await context.Inquiries.AddAsync(inquiry);
                await context.SaveChangesAsync(); //call to save changes
                TempData["Insert"] = "Data Inserted Successfully..";
                return RedirectToAction("Table", "User");
            }
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            return View(inquiry);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Inquiries == null) 
            {
                return NotFound();
            }
            var stdData = await context.Inquiries.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }

            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Inquiry inquiry)
        {
            if (id != inquiry.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Inquiries.Update(inquiry);
                await context.SaveChangesAsync();
                TempData["Update"] = "Data Updated Successfully..";
                return RedirectToAction("Table", "User");
            }
            ViewBag.Courses = new SelectList(context.Courses, "Name", "Name");
            return View(inquiry);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Inquiries == null)
            {
                return NotFound();
            }
            var stdData = await context.Inquiries.FirstOrDefaultAsync(x => x.Id == id);
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
            var stdData = await context.Inquiries.FindAsync(id);
            if (stdData != null)
            {
                context.Inquiries.Remove(stdData);
            }
            await context.SaveChangesAsync();
            TempData["deleted"] = "Data Deleted Successfully..";
            return RedirectToAction("Table", "User");
        }


        //public async Task<IActionResult> Admit(int id)
        //{
        //    // Fetch the inquiry by ID
        //    var inquiry = context.Inquiries.FirstOrDefault(i => i.Id == id);
        //    if (inquiry == null)
        //    {
        //        TempData["Error"] = "Inquiry not found.";
        //        return RedirectToAction("Table","User");
        //    }

        //    // Transfer data to the Admission table
        //    var admission = new Addmission
        //    {
        //        Name = inquiry.Name,
        //        Email = inquiry.Email,
        //        Gender = inquiry.Gender,
        //        Course = inquiry.Course,
        //        ContactNo=inquiry.Contact
                
        //    };

        //    context.Addmissions.Add(admission);

        //    // Optionally delete the inquiry after transferring
        //    //context.Inquiries.Remove(inquiry);

        //    //await context.Addmissions.SaveChanges();
        //    TempData["Success"] = "Inquiry successfully admitted.";
        //    return RedirectToAction("Table","User");
        //}



    }
}
