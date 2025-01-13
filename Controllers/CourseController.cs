using InstitudeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly TeknowellContext context;
        public CourseController(TeknowellContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> CourseTable()
        {
            var stdData = await context.Courses.ToArrayAsync();
            return View(stdData);
        }
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(string name, string[] frontEnd, string[] backEnd, string[] dataBaseLanguage, string duration, int courseFees, string courseFormat, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || courseFees <= 0 || string.IsNullOrWhiteSpace(duration) || string.IsNullOrWhiteSpace(courseFormat))
            {
                ModelState.AddModelError("", "Name, Duration, Course Fees, and Course Format are required.");
                return View();
            }

            // Save data to the database 
            var courseForm = new Course 
            {
                Name = name,
                FrontEnd = string.Join(",", frontEnd.Where(x => x != "false")), // Convert array to comma-separated string
                BackEnd = string.Join(",", backEnd.Where(x => x != "false")), // Convert array to comma-separated string
                DataBaseLanguage = string.Join(",", dataBaseLanguage.Where(x => x != "false")), // Convert array to comma-separated string
                Duration = duration,
                CourseFees= courseFees,
                CourseFormat = courseFormat,
                Description = description
               
            };

           await context.Courses.AddAsync(courseForm);
           await context.SaveChangesAsync(); 

            return RedirectToAction("CourseTable", "Course");
        }




        public async Task<IActionResult> Details(int? id)//page create page
        {
            if (id == null || context.Courses == null)
            {
                return NotFound();
            }
            var stdData = await context.Courses.FirstOrDefaultAsync(x => x.Cid == id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Courses == null)
            {
                return NotFound();
            }
            var stdData = await context.Courses.FirstOrDefaultAsync(x => x.Cid == id);
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
            var stdData = await context.Courses.FindAsync(id);
            if (stdData != null)
            {
                context.Courses.Remove(stdData);
            }
            await context.SaveChangesAsync();
            TempData["deleted"] = "Data Deleted Successfully..";
            return RedirectToAction("CourseTable", "Course");
        }




    }
}
