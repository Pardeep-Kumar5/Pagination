using Backend_Pagination.Model;
using Backend_Pagination.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Pagination.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionstring;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("Constr");
        }

        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using (var command = new SqlCommand("EmployeeStoredProcedure", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //command.Parameters.AddWithValue("@Emp_Id", employee.Id);
                    command.Parameters.AddWithValue("@Emp_Name", employee.Name);
                    command.Parameters.AddWithValue("@Emp_Address", employee.Address);
                    command.Parameters.AddWithValue("@Emp_Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Action", "INSERT");

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Employee> GetFilteredData(string filterParam, string filterBy)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                var command = new SqlCommand("GetFilteredData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@filterParam", filterParam);
                command.Parameters.AddWithValue("@filterBy", filterBy);

                var reader = command.ExecuteReader();
                var employees = new List<Employee>();

                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        Id = (int)reader["Emp_Id"],
                        Name = (string)reader["Emp_Name"],
                        Address =(string)reader["Emp_Address"],
                        Salary = (int)reader["Emp_Salary"]
                    };
                    employees.Add(employee);
                }
                return employees;
            }
        }
    public void DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using (var command = new SqlCommand("EmployeeStoredProcedure", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Emp_Id", id);
                    command.Parameters.AddWithValue("@Action", "DELETE");
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
           var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using (var command = new SqlCommand("EmployeeStoredProcedure", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", "SELECT_ALL");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                Id = (int)reader["Emp_Id"],
                                Name = reader["Emp_Name"].ToString(),
                                Address = reader["Emp_Address"].ToString(),
                                Salary = (int)reader["Emp_Salary"]
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            return employees;
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                using (var command = new SqlCommand("EmployeeStoredProcedure", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Emp_Id", id);
                    command.Parameters.AddWithValue("@Action", "SELECT");

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var employee = new Employee()
                        {
                            Id = (int)reader["Emp_Id"],
                            Name = reader["Emp_Name"].ToString(),
                            Address = reader["Emp_Address"].ToString(),
                            Salary = (int)reader["Emp_Salary"],
                        };
                        return employee;
                    }
                }
            }
            return null;
        }
        public List<Employee> GetEmployeesByPage(int pageNumber, int pageSize)
        {
             var employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetEmployeesByPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PageNumber", pageNumber);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                Id = (int)reader["Emp_Id"],
                                Name = (string)reader["Emp_Name"],
                                Address = (string)reader["Emp_Address"],
                                Salary = (int)reader["Emp_Salary"]
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            return employees;
        }
        public void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using (var command = new SqlCommand("EmployeeStoredProcedure", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Emp_Id", employee.Id);
                    command.Parameters.AddWithValue("@Emp_Name", employee.Name);
                    command.Parameters.AddWithValue("@Emp_Address", employee.Address);
                    command.Parameters.AddWithValue("@Emp_Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Action", "UPDATE");

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
