using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class EmpInTask
    {
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int TaskID { get; set; }
        public Tasks Task { get; set; }
    }
}
