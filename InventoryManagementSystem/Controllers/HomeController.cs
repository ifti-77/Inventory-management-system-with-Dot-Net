using InventoryManagementSystem.EF;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;


namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly InventoryManagementContext db;


        public HomeController(InventoryManagementContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (UserState.IsLoggedIn) 
            {
                return RedirectToAction("Index", "Dashboard");
            } 
            return View(null);
        }

        [HttpPost]
        public IActionResult Index(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(user => user.Email == login.email);
                if (user == null)
                {
                    ModelState.AddModelError("email", "Invalid email");
                    return View(login);
                }
                else if (user.Password != login.password)
                {
                    ModelState.AddModelError("password", "Invalid password");
                    return View(login);
                }
                else
                {
                    UserState.IsLoggedIn = true;
                    UserState.UserID = user.Id;
                    UserState.UserName = user.Name;
                    UserState.UserRole = user.Role;

                    return RedirectToAction("Index", "Dashboard");
                }
            }

                return View(login);
        }
    }
}
