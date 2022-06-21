using System.ComponentModel.DataAnnotations;
using Dependency_CRUD;

namespace MVC_Project.Models
{
    public class Employee
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Salary { get; set; }
        [Required]
        public string Skillset { get; set; }

        //public int empid { get; set; }
       // public string empsalary { get; set; }
        
       // public string empskillset { get; set; }
    }
}
