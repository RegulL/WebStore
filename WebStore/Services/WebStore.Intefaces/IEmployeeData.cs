using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.ViewModels;

namespace WebStore.Interfaces
{
    public interface IEmployeeData
    {
        IEnumerable<EmployeeView> GetAll();

        EmployeeView GetById(int id);

        void Commit();

        void AddNew(EmployeeView model);

        void Delete(int id);

        EmployeeView UpdateEmployee(int id, EmployeeView entity);
    }
}
