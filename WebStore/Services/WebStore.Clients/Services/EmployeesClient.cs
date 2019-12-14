using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;
using System.Net.Http;

namespace WebStore.Clients.Services
{
    public class EmployeesClient : BaseClient, IEmployeeData
    {

        public EmployeesClient(IConfiguration configuration) : base(configuration)
        {
        }
        protected override string ServiceAddress { get; } = "api/employees";

        public void AddNew(EmployeeView model)
        {
            string url = $"{ServiceAddress}";
            Post<EmployeeView>(url, value: model);
        }

        public void Commit()
        {
        }

        public void Delete(int id)
        {
            string url = $"{ServiceAddress}/{id}";
            Delete(url);
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            string url = $"{ServiceAddress}";
            return Get<List<EmployeeView>>(url);
        }

        public EmployeeView GetById(int id)
        {
            string url = $"{ServiceAddress}/{id}";
            return Get<EmployeeView>(url);
        }

        public EmployeeView UpdateEmployee(int id, EmployeeView entity)
        {
            string url = $"{ServiceAddress}/{id}";
            var result = Put(url, value: entity);
            return result.Content.ReadAsAsync<EmployeeView>().Result;
        }
    }
}
