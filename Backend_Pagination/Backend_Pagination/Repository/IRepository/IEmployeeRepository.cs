using Backend_Pagination.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Pagination.Repository.IRepository
{
   public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        List<Employee> GetEmployeesByPage(int pageNumber, int pageSize);
        Task<Employee> GetEmployeeById(int id);
        List<Employee> GetFilteredData(string filterParam, string filterBy);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        public void DeleteEmployee(int id);


    }
}
