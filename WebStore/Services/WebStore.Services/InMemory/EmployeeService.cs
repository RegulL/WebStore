using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Services.InMemory
{
    public class EmployeeService : IEmployeeData
    {
        public readonly List<EmployeeView> _employees;

        public EmployeeService()
        {
            _employees = new List<EmployeeView>
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
        }

        public void AddNew(EmployeeView model)
        {
            if (model == null)
            {
                new ArgumentNullException(nameof(model));
            }
            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
        }

        public void Commit()
        {
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp == null)
                return;
            _employees.Remove(emp);
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public EmployeeView UpdateEmployee(int id, EmployeeView entity)
        {
            if (entity == null) 
            {
                new ArgumentNullException(nameof(entity));
            }

            var employee = _employees.FirstOrDefault(e => e.Id == entity.Id);

            if (employee == null) 
            {
                new InvalidOperationException("Do not find...");
            }


            employee.Age = entity.Age;
            employee.FirstName = entity.FirstName;
            employee.LastName = entity.LastName;
            employee.Patronymic = entity.Patronymic;
            
            
            return employee;
        }
    }
}
