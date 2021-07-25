using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class TaskWithEmpsList
    {
        //Tasks
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Task Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter Task Start Time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Enter Task Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Enter Task End Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Enter Task Title")]
        [StringLength(250)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Task Description")]
        [StringLength(250)]
        public string Description { get; set; }
        public bool IsEnd { get; set; }

        // Employee/s Assigned To Task/s
        public List<Employee> EmployeesList { get; set; }
    }
}
