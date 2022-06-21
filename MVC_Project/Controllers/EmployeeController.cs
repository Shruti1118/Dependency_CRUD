using Microsoft.AspNetCore.Mvc;
using Dependency_CRUD;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class EmployeeController : Controller
    {
        
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader dr;
        SqlConnection sqlConnection = new SqlConnection();
        List<Employee>  employees = new List<Employee>();
        
        public IActionResult Employee()
        {
            sqlConnection.ConnectionString = MVC_Project.Properties.Resources.ConnectionString;
            
            FetchData();
            return View(employees);
        }
        private void FetchData()
        {
            try
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT TOP (1000) [employee_id],[employee_salary],[employee_skillset] FROM[SQLDB].[dbo].[EMPLOYEE]";
                dr= sqlCommand.ExecuteReader();
                
                while(dr.Read())
                {
                    employees.Add(new Employee() { Id = dr["employee_id"].ToString(),
                        Salary = dr["employee_salary"].ToString(),
                        Skillset= dr["employee_skillset"].ToString()

                    });
                }

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IActionResult AddEmployee(int empid2, int empsalary2, string empskillset2)
        {
            IService _emptype = new CreateEmployee(empid2, empsalary2, empskillset2);
            BusinessLogicService objBusinessLogicService = new BusinessLogicService(_emptype);
            return View();
        }

        public IActionResult UpdateEmployee(int empid1,int empsalary,string empskillset)
        {
            IService _emptype = new UpdateEmployee(empid1, empsalary, empskillset);
            BusinessLogicService objBusinessLogicService = new BusinessLogicService(_emptype);
            return View();
        }
      
        
        public IActionResult DeleteEmployee(string empid)
        {
            int id =Convert.ToInt32(empid);
            IService _emptype = new DeleteEmployee(id);
            BusinessLogicService objBusinessLogicService = new BusinessLogicService(_emptype);
            return View();
        }

    }
}
