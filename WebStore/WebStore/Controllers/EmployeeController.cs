using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using WebStore.Infrastructure;

namespace WebStore.Controllers
{
    [Route(template:"users")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET: Home
        [Route(template:"all")]
        public ActionResult Index()
        {
            //return Content("Hello from first controller!");
            return View(_employeeService.GetAll());
        }

        [Route(template:"{id}")]
        public ActionResult Details(int id)
        {
            return View(_employeeService.GetById(id));
        }

        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeView());
            EmployeeView model = _employeeService.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(EmployeeView model)
        {
            if (model.Age < 18 || model.Age > 100)
            {
                ModelState.AddModelError(key: "Age", errorMessage: "Age is incorrect");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id > 0)
            {
                var dbItem = _employeeService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();

                dbItem.FirstName = model.FirstName;
                dbItem.LastName = model.LastName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
            }
            else
            {
                _employeeService.AddNew(model);
            }
            _employeeService.Commit();

            return RedirectToAction(nameof(Index));
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}