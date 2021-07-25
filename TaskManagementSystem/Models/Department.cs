using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class Department
    {
        [Required]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Please Enter Department Name")]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        //Navigational Property
        public ICollection<Employee> Employees { get; set; }
        
    }
}
