using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/employees")]
    [Produces(contentType: ("application/json"))]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeData
    {
        private readonly IEmployeeData _employeeData;
        public EmployeesApiController(IEmployeeData employeeData)
        {
            _employeeData = employeeData ?? throw new ArgumentNullException(nameof(employeeData));
        }

        [HttpPost, ActionName(name: "post")]
        public void AddNew([FromBody]EmployeeView model)
        {
            _employeeData.AddNew(model);
        }

        [NonAction]
        public void Commit()
        {
            
        }

        [HttpDelete(template:"{id}")]
        public void Delete(int id)
        {
            _employeeData.Delete(id);
        }

        [HttpGet, ActionName(name:"get")]
        public IEnumerable<EmployeeView> GetAll()
        {
            return _employeeData.GetAll();
        }

        [HttpGet(template:"{id}"), ActionName(name: "get")]
        public EmployeeView GetById(int id)
        {
            return _employeeData.GetById(id);
        }
        [HttpPut,ActionName("put")]
        public EmployeeView UpdateEmployee(int id, [FromBody]EmployeeView entity)
        {
            return _employeeData.UpdateEmployee(id, entity);
        }
    }
}