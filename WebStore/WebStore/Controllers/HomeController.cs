using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<EmployeeView> _employees = new List<EmployeeView>
        {
            new EmployeeView
            {
                Id = 1,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Patronymic = "Ivanovich",
                Age = 22
            },
            new EmployeeView
            {
                Id = 2,
                FirstName = "Vlad",
                LastName = "Petrov",
                Patronymic = "Ivanovich",
                Age = 23
            },
            new EmployeeView
            {
                Id = 3,
                FirstName = "Nikitin",
                LastName = "Nikita",
                Patronymic = "Sergeevich",
                Age = 30
            }
        };

        // GET: Home
        public ActionResult Index()
        {
            //return Content("Hello from first controller!");
            return View(_employees);
        }

        public ActionResult Details(int id)
        {
            return View(_employees.FirstOrDefault(x => x.Id == id));
        }
    }
}