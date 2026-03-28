using InventoryManagementSystem.EF;
using InventoryManagementSystem.EF.Tables;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly InventoryManagementContext db;

        public DashboardController(InventoryManagementContext db) {
                        this.db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {

            if (UserState.IsLoggedIn == true)
            {
                if ( UserState.UserRole == "manager")
                {
                    return RedirectToAction("Manager");
                }
                else if (UserState.UserRole == "employee")
                {
                    return RedirectToAction("Employee");
                }
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Manager()
        {
            ViewBag.UserName = UserState.UserName;
            return View(); 
        }

        [HttpGet]
        public IActionResult Employee() 
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            UserState.IsLoggedIn = false;
            UserState.UserID = 0;
            UserState.UserName = string.Empty;
            UserState.UserRole = string.Empty;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            if(UserState.IsLoggedIn == true && UserState.UserRole == "manager")
            {
                var products = db.Products.ToList();

                return View(products);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            if (UserState.IsLoggedIn == true && UserState.UserRole == "manager")
            {
                return View(null);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel product)
        {
            if (UserState.IsLoggedIn == true && UserState.UserRole == "manager")
            {
                if (ModelState.IsValid) 
                {
                    var newProduct = new Product{
                        Name = product.Name,
                        Sku = product.Sku,
                        Quantity = product.Quantity,
                        Price = product.Price
                    };
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                    return RedirectToAction("ProductList");
                }
                return View(product);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            if (UserState.IsLoggedIn == true && UserState.UserRole == "manager")
            {
                var product = (from p in db.Products where p.Id== id select p).SingleOrDefault();
                return View(product);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductModel product)
        {
            if (UserState.IsLoggedIn == true && UserState.UserRole == "manager")
            {
                if (ModelState.IsValid)
                {
                    var currentProduct = db.Products.Where(p => p.Id == product.Id)
                                                     .ExecuteUpdate(p => p.SetProperty(p => p.Name, product.Name)
                                                                           .SetProperty(p => p.Quantity, product.Quantity)
                                                                           .SetProperty(p => p.Price, product.Price)
                                                                           .SetProperty(p => p.Sku, product.Sku));

                    return RedirectToAction("ProductList");
                }
                return View(product);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (UserState.IsLoggedIn == true && UserState.UserRole == "manager")
            {
                var product = (from p in db.Products where p.Id == id select p).ExecuteDelete();
                return RedirectToAction("ProductList");
            }
            return RedirectToAction("Index", "Dashboard");
        }

    }
}
