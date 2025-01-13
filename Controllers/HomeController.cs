using InstitudeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InstitudeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeknowellContext context;

        public HomeController(TeknowellContext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Usersession") != null)
            {
                return RedirectToAction("Dashboard","User");
            }
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Registration u1)
        {
            var myUser = context.Registrations.Where(x => x.Email == u1.Email && x.Password == u1.Password).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetString("Usersession",myUser.Name);
                return RedirectToAction("Dashboard","User");
            }
            else
            {
                ViewBag.Message = "Login Failled...";
            }
            return View();
        }
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Registration register)
        {
            if (ModelState.IsValid)
            {
                await context.Registrations.AddAsync(register);
                await context.SaveChangesAsync();
                TempData["success"] = "Registered Successfully...";
                return RedirectToAction("Login");
            }
            return View(register);
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("Usersession") != null)
            {
                HttpContext.Session.Remove("Usersession");
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //public IActionResult Reset()
        //{
        //    ModelState.Clear();
        //    return RedirectToAction("Login","Home");
        //}
        public IActionResult Privacy()
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
