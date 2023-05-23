using Backend_Pagination.Model;
using Backend_Pagination.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Pagination.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository )
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult GetEmployeesByPage(int pageNumber, int pageSize)
        {
            List<Employee> employees = _employeeRepository.GetEmployeesByPage(pageNumber, pageSize);

            return Ok(employees);
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        [HttpPost]
        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }
        [HttpPut]
        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }
        [HttpGet("FilterData")]
        public IActionResult GetFilter (string FilterData,string filterBy)
        {
            var FilterName = _employeeRepository.GetFilteredData(FilterData, filterBy);
            return Ok(FilterName);
        }
        [HttpDelete]
        public void DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
        }
    }
}
