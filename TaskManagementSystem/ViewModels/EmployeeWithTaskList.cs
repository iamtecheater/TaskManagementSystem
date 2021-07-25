using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class EmployeeWithTaskList
    {
        // Employee
        [Required]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Enter Employee First Name")]
        [StringLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Employee Surname")]
        [StringLength(250)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter Employee Date Of Birth")]
        public DateTime DayOfBirthday { get; set; }
        [Required(ErrorMessage = "Enter Employee Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Enter Employee Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        //Employee Department
        [Required]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Enter Department Name")]
        [StringLength(250)]
        public string DeptName { get; set; }

        //Task Assigned to Employee
        public List<Tasks> TaskList { get; set; }
    }
}
