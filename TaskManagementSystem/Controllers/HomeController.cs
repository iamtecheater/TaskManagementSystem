using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        private static bool _sortStatus;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
    }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Main(string SearchString)
        {
            _sortStatus = true;
            var tasks = _context.Task.ToList();

            // Search by task title
            if (!String.IsNullOrEmpty(SearchString))
            {
                tasks = tasks.Where(t => t.Title.ToLower().Contains(SearchString.ToLower())).ToList();
            }
            return View(tasks);
        }

        [HttpGet]
        public ViewResult AddTask()
        {
            var empl = _context.Employees.ToList();
            TaskWithEmpsList twel = new TaskWithEmpsList();
            twel.EmployeesList = empl;
            twel.StartDate = DateTime.Now;
            twel.StartTime = DateTime.Now;
            twel.EndDate = DateTime.Now;
            twel.EndTime = DateTime.Now;
            return View(twel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddTask([Bind("ID, StartDate, StartTime, EndDate, EndTime, Title, Description, IsEnd, EmployeesList")] TaskWithEmpsList taskEmp)
        {
            taskEmp.IsEnd = false;
            EmpInTask eit;
            Tasks task = new Tasks();
            Employee empl;

            task.ID = taskEmp.ID;
            task.StartTime = taskEmp.StartTime;
            task.StartDate = taskEmp.StartDate;
            task.EndTime = taskEmp.EndTime;
            task.EndDate = taskEmp.EndDate;
            task.Title = taskEmp.Title;
            task.Description = taskEmp.Description;
            task.IsEnd = taskEmp.IsEnd;

            if (ModelState.IsValid)
            {
                foreach (var emp in taskEmp.EmployeesList.Where(e => e.CheckBoxEmp == true))
                {
                    empl = new Employee
                    {
                        EmployeeID = emp.EmployeeID,
                        Name = emp.Name,
                        Surname = emp.Surname,
                        DayOfBirthday = emp.DayOfBirthday,
                        EmailAddress = emp.EmailAddress,
                        PhoneNumber = emp.PhoneNumber,
                        DeptID = emp.DeptID
                    };

                    eit = new EmpInTask
                    {
                        Task = task,
                        // Add employee ID
                        // When the employee already exists and is not added in the task.
                        // Then this works
                        //eit.Employee = empl;
                        EmployeeID = empl.EmployeeID
                    };
                    _context.TaskInTasks.Add(eit);
                }
                _context.SaveChanges();
                return RedirectToAction("Main");
            }
            return View(taskEmp);

        }

        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            Tasks task = _context.Task.Find(id);
            _context.Task.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }

        [HttpGet]
        public IActionResult DetailTask(int id)
        {
            var task = _context.Task.Include(z => z.Employees).ThenInclude(e => e.Employee).Where(z => z.ID == id).FirstOrDefault();
            TaskWithEmpsList twel = new TaskWithEmpsList
            {
                ID = task.ID,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Title = task.Title,
                Description = task.Description,
                EmployeesList = new List<Employee>()
            };
            foreach (EmpInTask item in task.Employees)
            {
                twel.EmployeesList.Add(item.Employee);
            }
            return View(twel);
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            Tasks task = _context.Task.Find(id);
            List<EmpInTask> eitList = _context.TaskInTasks.Where(f => f.TaskID == id).ToList();
            TaskWithEmpsList twel = new TaskWithEmpsList();
            List<Employee> empList = _context.Employees.ToList();

            foreach (var empInTask in eitList)
            {
                foreach (var emp in empList)
                {
                    if (empInTask.EmployeeID == emp.EmployeeID)
                    {
                        emp.CheckBoxEmp = true;
                    }
                }
            }

            twel.ID = task.ID;
            twel.StartDate = task.StartDate;
            twel.StartTime = task.StartTime;
            twel.EndDate = task.EndDate;
            twel.EndTime = task.EndTime;
            twel.Title = task.Title;
            twel.Description = task.Description;
            twel.IsEnd = task.IsEnd;
            twel.EmployeesList = empList;

            return View(twel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditTask(int id, [Bind("ID, StartDate, StartTime, EndDate, EndTime, Title, Description, IsEnd, EmployeesList")] TaskWithEmpsList taskEmp)
        {
            taskEmp.IsEnd = false;
            EmpInTask eit;
            Tasks task = new();
            Employee empl;

            task.ID = taskEmp.ID;
            task.StartTime = taskEmp.StartTime;
            task.StartDate = taskEmp.StartDate;
            task.EndTime = taskEmp.EndTime;
            task.EndDate = taskEmp.EndDate;
            task.Title = taskEmp.Title;
            task.Description = taskEmp.Description;
            task.IsEnd = taskEmp.IsEnd;

            foreach (var emp in taskEmp.EmployeesList.Where(e => e.CheckBoxEmp == true))
            {
                empl = new Employee
                {
                    EmployeeID = emp.EmployeeID,
                    Name = emp.Name,
                    Surname = emp.Surname,
                    DayOfBirthday = emp.DayOfBirthday,
                    EmailAddress = emp.EmailAddress,
                    PhoneNumber = emp.PhoneNumber,
                    DeptID = emp.DeptID
                };

                eit = new EmpInTask
                {
                    TaskID = task.ID,
                    // Add employee ID
                    // When the employee already exists and is not added in the task.
                    // Then this works
                    //eit.Employee = empl;
                    EmployeeID = empl.EmployeeID
                };
                if (TaskEmp(eit.EmployeeID, eit.TaskID) == false)
                {
                    _context.TaskInTasks.Add(eit);
                    _context.SaveChanges();
                }
            }

            foreach (var emp in taskEmp.EmployeesList.Where(e => e.CheckBoxEmp == false))
            {
                eit = new EmpInTask
                {
                    TaskID = task.ID,
                    EmployeeID = emp.EmployeeID
                };
                if (TaskEmp(eit.EmployeeID, eit.TaskID) == true)
                {
                    _context.TaskInTasks.Remove(eit);
                    _context.SaveChanges();
                }
            }

            if (id != task.ID)
            {
                return Content("ID does not exist");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Task.Update(task);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExist(task.ID))
                    {
                        return Content("Task does not exist");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Main));
            }
            return View(taskEmp);
        }

        [HttpGet]
        public IActionResult EndTask(int id)
        {
            Tasks task = _context.Task.Find(id);

            task.IsEnd = true;
            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Main));
        }

        [HttpGet]
        public IActionResult ResponeTask(int id)
        {
            Tasks task = _context.Task.Find(id);

            task.IsEnd = false;
            _context.Update(task);
            _context.SaveChanges();
            return RedirectToAction(nameof(Main));
        }

        private bool TaskExist(int id)
        {
            return _context.Task.Any(t => t.ID == id);
        }

        private bool TaskEmp(int EmplID, int TaskID)
        {
            return _context.TaskInTasks.Any(t => t.EmployeeID == EmplID && t.TaskID == TaskID);
        }

        [HttpGet]
        public ViewResult SortTask(string id)
        {
            var tasks = _context.Task.ToList();

            // Order by title
            if (_sortStatus != false && id == "Title")
            {
                tasks = tasks.OrderBy(t => t.Title).ToList();
                _sortStatus = false;
            }
            // Order by title desc
            else if (_sortStatus != true && id == "Title")
            {
                tasks = tasks.OrderByDescending(t => t.Title).ToList();
                _sortStatus = true;
            }

            // Order by start date
            if (_sortStatus != true && id == "StartDate")
            {
                tasks = tasks.OrderBy(s => s.StartTime).ToList();
                tasks = tasks.OrderBy(s => s.StartDate).ToList();
                _sortStatus = true;
            }
            // Order by start date desc
            else if (_sortStatus != false && id == "StartDate")
            {
                tasks = tasks.OrderByDescending(s => s.StartTime).ToList();
                tasks = tasks.OrderByDescending(s => s.StartDate).ToList();
                _sortStatus = false;
            }

            // Order by end date
            if (_sortStatus != true && id == "EndDate")
            {
                tasks = tasks.OrderBy(s => s.EndTime).ToList();
                tasks = tasks.OrderBy(s => s.EndDate).ToList();
                _sortStatus = true;
            }
            // Order by end date desc
            else if (_sortStatus != false && id == "EndDate")
            {
                tasks = tasks.OrderByDescending(s => s.EndTime).ToList();
                tasks = tasks.OrderByDescending(s => s.EndDate).ToList();
                _sortStatus = false;
            }

            return View("Main", tasks);
        }

        private DataTable GetDataTable()
        {
            var tasks = _context.Task;

            // Creating data table
            DataTable dt = new DataTable
            {

                // Setting table name
                TableName = "TaskTable"
            };

            // Add columns
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("DateStart", typeof(DateTime));
            dt.Columns.Add("TimeStart", typeof(DateTime));
            dt.Columns.Add("DateEnd", typeof(DateTime));
            dt.Columns.Add("TimeEnd", typeof(DateTime));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Description", typeof(string));

            // Add rows in data table
            foreach (var zadanie in tasks)
            {
                dt.Rows.Add(zadanie.ID, zadanie.StartDate, zadanie.StartTime, zadanie.EndDate, zadanie.EndTime, zadanie.Title, zadanie.Description);
            }

            dt.AcceptChanges();

            return dt;
        }

        public IActionResult WriteDataToExcel()
        {
            DataTable dt = GetDataTable();

            // Name of file
            string fileName = "Sample.xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                // Add data table to workbook
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    // Return xlsx Excel file
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }

        }
    }
}
