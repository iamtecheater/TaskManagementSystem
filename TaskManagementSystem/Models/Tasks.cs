using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class Tasks
    {
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Task Start Date.")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter Task Start Time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Enter Due Date For Task")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Enter Task End Time")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Enter Task Title")]
        [StringLength(250)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Task Description")]
        [StringLength(250)]
        public string Description { get; set; }
        public bool IsEnd { get; set; }

        //Navigational Property
        public virtual ICollection<EmpInTask> Employees { get; set; }
    }
}
