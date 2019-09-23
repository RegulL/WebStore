using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
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
    }
}
