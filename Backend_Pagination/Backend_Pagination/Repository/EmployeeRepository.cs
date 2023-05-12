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
