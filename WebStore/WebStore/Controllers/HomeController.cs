using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}