//using Backend_Pagination.Model;
//using Backend_Pagination.Repository.IRepository;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Backend_Pagination.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TestController : ControllerBase
//    {
//        private readonly IEmployeeRepository _employeeRepository;
//        public TestController(IEmployeeRepository employeeRepository)
//        {
//            _employeeRepository = employeeRepository;
//        }
//        [HttpGet]
//        public IEnumerable<Employee> GetAllEmployees()
//        {
//           return _employeeRepository.GetAllEmployees();
//        }
//    }
//}
