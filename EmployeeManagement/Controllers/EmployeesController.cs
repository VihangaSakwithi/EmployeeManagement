using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DBContext empDBContext;

        public EmployeesController(DBContext empDBContext)
        {
            this.empDBContext = empDBContext;
        }

        [HttpGet]
        public IActionResult index()
        {
            var employees = empDBContext.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEmployeeView addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
            };

            empDBContext.Employees.Add(employee);
            empDBContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View(Guid id)
        {
            var employee = empDBContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {

                var ViewModel = new UpdateEmployee()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };

                return View(ViewModel);
            }
                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult View(UpdateEmployee model)
        {
            var employee = empDBContext.Employees.Find(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                empDBContext.SaveChanges();

                return RedirectToAction("Index");
            }
                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(UpdateEmployee model)
        {
            var employee = empDBContext.Employees.Find(model.Id);

            if (employee != null)
            {
                empDBContext.Employees.Remove(employee);
                empDBContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
