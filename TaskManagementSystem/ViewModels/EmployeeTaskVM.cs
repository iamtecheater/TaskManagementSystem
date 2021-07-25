using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class EmployeeTaskVM
    {
        // Zadanie
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = " Enter Task Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter Task Start Time")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Enter Task Due Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Enter Task End Time")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Enter Task Title")]
        [StringLength(250)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Task Description")]
        [StringLength(250)]
        public string Description { get; set; }
        public bool IsEnd { get; set; }

        //Employee
        [Required]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Enter Employee First Name")]
        [StringLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Employee Surname")]
        [StringLength(250)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter Employee Date Of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime DayOfBirthday { get; set; }
        [Required(ErrorMessage = "Enter Employee Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Enter Employee Phone Number")]
        [RegularExpression("^(\\+[0 - 9]{11})$")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
