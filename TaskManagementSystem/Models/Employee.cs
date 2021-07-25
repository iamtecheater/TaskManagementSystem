using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Enter Employee First Name")]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Enter Employee Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter Employee Date of Birth")]
        public DateTime DayOfBirthday { get; set; }
        [Required(ErrorMessage = "Enter Employee Email Address.")]
        [EmailAddress]
        [StringLength(250)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Enter Employee Phone Number.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public bool CheckBoxEmp { get; set; }

        //Navigational Property
        public virtual ICollection<EmpInTask> Task { get; set; }

        public int? DeptID { get; set; }
        public Department Department { get; set; }
    }
}
