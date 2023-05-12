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
        Task<Employee> GetEmployeeById(int id);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        public void DeleteEmployee(int id);


    }
}
