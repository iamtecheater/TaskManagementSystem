using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class DepartmentEmployeesListViewModels
    {
        //Department
        [Required]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Enter Department Name")]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        // List Of Employees
        public List<Employee> EmployeesList { get; set; }
    }
}
