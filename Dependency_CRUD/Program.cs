using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Dependency_CRUD
{
   
    public class Program
    {
        public static string connectionstring = @"Data Source=DESKTOP-3C9L1B1\SQLEXPRESS;Initial Catalog=SQLDB;Integrated Security=True";
        //public static string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["Dependency_CRUD.Properties.Settings.SQLDBConnectionString"].ConnectionString;
        static void Main(string[] args)
        {
            int empid;
            IService _emptype;
        again:
            Console.WriteLine("Do you want to insert,update or delete employee details?Select Option \n1-insert \n2-Update \n3-Delete");
            int FPtype;
            try
            {
                FPtype = int.Parse(Console.ReadLine());
            }
            catch
            {
                //Console.WriteLine("Please enter valid option to proceed.");
                FPtype = 0;
            }
            
            //switch(FPtype)
            //{
            //    case 1:
            //        _emptype = new CreateEmployee();
            //        break;
            //    case 2:
            //        _emptype = new UpdateEmployee(1);
            //        break;
            //    case 3:
            //        _emptype = new DeleteEmployee(1);
            //        break;
            //        default:
            //        _emptype = null;
            //        Console.WriteLine("Please enter valid option to proceed.");
            //        goto again;
            //        break;
            //}

            
            //BusinessLogicService objBusinessLogicService = new BusinessLogicService(_emptype);
                        
            Console.ReadKey();
        }

    }

    public class BusinessLogicService
    {
        private IService iservice;

        public BusinessLogicService(IService iservice)
        {
            this.iservice = iservice;
            this.iservice.modifydata();
            
        }
    }

    public interface IService
    {
        void modifydata();
       
    }

    public class CreateEmployee : IService
    {
        int empid;
        int salary;
        string skillset;
        public CreateEmployee(int empid, int salary, string skillset)
        {
            this.empid = empid;
            this.salary = salary;
            this.skillset = skillset;
        }
        SqlConnection sqlConnection = new SqlConnection(Program.connectionstring);
        
        public void modifydata()
        {
            //Console.WriteLine("Enter your employee ID");
            //int emp_id = int.Parse(Console.ReadLine());
            string selectquery = "SELECT employee_id FROM EMPLOYEE WHERE employee_id=" + empid;
            SqlCommand cmd1 = new SqlCommand(selectquery, sqlConnection);
            sqlConnection.Open();
            SqlDataReader dataReader = cmd1.ExecuteReader();
            
            if (dataReader.HasRows)
            {
                Console.WriteLine("Employee ID already exist.");
                dataReader.Close();
                sqlConnection.Close();
            }
            else
            {
                dataReader.Close();
                //Console.WriteLine("Enter your salary");
                //int emp_salary = int.Parse(Console.ReadLine());
                //Console.WriteLine("enter your Skillsets");
                //string emp_skills = Console.ReadLine();
                string insertquery = "INSERT INTO EMPLOYEE(employee_id,employee_salary,employee_skillset)VALUES(" + empid + "," + salary + ",'" + skillset + "')";
                SqlCommand cmd = new SqlCommand(insertquery, sqlConnection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Row inserted successfully!!!");
                sqlConnection.Close();
            }
        }

    }
    public class UpdateEmployee : IService
    {
        int empid;
        int salary;
        string skillset;
        public UpdateEmployee(int empid, int salary, string skillset)
        {
            this.empid = empid;
            this.salary = salary;
            this.skillset = skillset;
        }
        SqlConnection sqlConnection = new SqlConnection(Program.connectionstring);
        public void modifydata()
        {
            //Console.WriteLine("Enter your existing employee ID that you would like to update");
           // int emp_id = int.Parse(Console.ReadLine());
            string selectquery = "SELECT employee_id FROM EMPLOYEE WHERE employee_id=" + empid;
            SqlCommand cmd1 = new SqlCommand(selectquery, sqlConnection);
            sqlConnection.Open();
            SqlDataReader dataReader = cmd1.ExecuteReader();
            if (!dataReader.HasRows)
            {
                Console.WriteLine("Employee ID does not exist.");
                dataReader.Close();
                sqlConnection.Close();
            }
            else
            {
                dataReader.Close();
                string updatequery = "UPDATE EMPLOYEE SET employee_salary=" + salary + ",employee_skillset='" + skillset + "' where employee_id=" + empid + " ";
                SqlCommand cmd = new SqlCommand(updatequery, sqlConnection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Row Updated successfully!!!");
                sqlConnection.Close();
            }
        }
    }
    public class DeleteEmployee : IService
    {
        int empid;
        public DeleteEmployee(int empid)
        {
            this.empid = empid;
        }
        SqlConnection sqlConnection = new SqlConnection(Program.connectionstring);
        public void modifydata()
        {
           // Console.WriteLine("Enter your existing employee ID that you would like to delete");
            //int emp_id = int.Parse(Console.ReadLine());
            string selectquery = "SELECT employee_id FROM EMPLOYEE WHERE employee_id=" + empid;
            SqlCommand cmd1 = new SqlCommand(selectquery, sqlConnection);
            sqlConnection.Open();
            SqlDataReader dataReader = cmd1.ExecuteReader();
            if (!dataReader.HasRows)
            {
                Console.WriteLine("Employee ID does not exist.");
                dataReader.Close();
                sqlConnection.Close();
            }
            else
            {
                dataReader.Close();
                string deletequery = "DELETE FROM EMPLOYEE where employee_id=" + empid + " ";
                SqlCommand cmd = new SqlCommand(deletequery, sqlConnection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Row deleted successfully!!!");
                sqlConnection.Close();
            }
        }
    }
}
