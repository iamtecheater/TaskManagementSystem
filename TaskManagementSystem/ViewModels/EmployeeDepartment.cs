using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class EmployeeDepartment
    {
        //Department
        [Required]
        public int? DepartmentID { get; set; }
        public string DeptName { get; set; }

        public List<Department> departmentsEDVM { get; set; }

        //Employee
        [Required]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Enter Employee First Name")]
        [StringLength(250)]
        public string EmplName { get; set; }
        [Required(ErrorMessage = "Enter Employee Surname")]
        [StringLength(250)]
        public string EmplSurname { get; set; }
        [Required(ErrorMessage = "Enter Emplyee Date Of Birth")]
        public DateTime DayOfBirthday { get; set; }
        [Required(ErrorMessage = "Enter Employee Email Address")]
        [StringLength(250)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Enter Employee Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
