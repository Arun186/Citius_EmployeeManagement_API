using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_Employee.Interface;
using Business_Employee.Implementations;
using Models_Employee;
using Microsoft.AspNetCore.Authorization;

namespace Employee_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee_Business _iemployee;


        public EmployeeController(IEmployee_Business employee_Business)
        {
            _iemployee = employee_Business;
        }
        [Authorize(Roles = "Admin,Physician,Nurse")]
        [HttpPost]
        public async Task<int> AddEmployee(Employee_Data employee)
        {
            int result;
            try
            {
                 result = await _iemployee.AddEmployee(employee);
            }
            catch (Exception ex)
            {
                return 400;//BadRequest(ex);
            }
            return result;
        }

        //[HttpPut("{id}")]
        //public async Task<int> UpdateEmployee(Employee_Data employee,int id)
        //{
        //    int result;
        //    try
        //    {
        //        employee.Id = id;
        //        result = await _iemployee.EditEmployee(employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        return 400;//BadRequest(ex);
        //    }
        //    return result;
        //}

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Physician,Nurse")]
        public async Task<int> UpdateEmployee(Employee_Data employee, int id)
        {
            int result;
            try
            {
                employee.Id = id;
                result = await _iemployee.EditEmployee(employee);
            }
            catch (Exception ex)
            {
                return 400;//BadRequest(ex);
            }
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Physician,Nurse")]
        public async Task<IEnumerable<Employee_Data>> GetAll()
        {
            var details = await _iemployee.GetAll();

            return (details);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Physician,Nurse")]
        public async Task<ActionResult<Employee_Data>> GetEmployeeId(int id)
        {
            var details = await _iemployee.GetEmployeeId(id);

            return (details);
        }

    }
}
