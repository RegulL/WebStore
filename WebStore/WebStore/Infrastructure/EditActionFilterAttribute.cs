using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Infrastructure
{
    public class EditActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
             //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var model = (EmployeeView)context.ActionArguments["model"];

            if (model.FirstName == null ||
               model.LastName == null ||
               model.Patronymic == null)
            {
                throw new ArgumentNullException();
            }

            if (model.Age < 18) { throw new ArgumentException(); }
                
        }
    }
}
