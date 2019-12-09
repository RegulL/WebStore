using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeView> GetAll();

        EmployeeView GetById(int id);

        void Commit();

        void AddNew(EmployeeView model);

        void Delete(int id);
    }
}
