using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business_Employee.Interface;
using Models_Employee;
using Repository_Employee.Interfaces;
using Repository_Employee.Dtos;

using AutoMapper;

namespace Business_Employee.Implementations
{
    public class Employee_Business_Repository : IEmployee_Business
    {
        private readonly IEmployee _iemployee;
        private readonly IMapper _imapper;

        public Employee_Business_Repository(IEmployee employee, IMapper mapper)
        {
            _iemployee = employee;
            _imapper = mapper;

        }
        public async Task<int> AddEmployee(Employee_Data employee)
        {
            var emp = _imapper.Map<Employees>(employee);
            int id = await _iemployee.AddEmployee(emp);
            return id;

        }

        public async Task<int> EditEmployee(Employee_Data employee)
        {
            var emp = _imapper.Map<Employees>(employee);
            int details = await _iemployee.EditEmployee(emp);
            return details;
        }

        public async Task<IEnumerable<Employee_Data>> GetAll()
        {
            var details = await _iemployee.GetAll();
            return _imapper.Map<IEnumerable<Employee_Data>>(details);
        }

        public async Task<Employee_Data> GetEmployeeId(int id)
        {
            var  empDetails = await _iemployee.GetEmployeeId(id);
            return _imapper.Map<Employee_Data>(empDetails);
        }

        public async Task<bool> RemoveEmployee(int empid)
        {
            var removePatient = await _iemployee.RemoveEmployee(empid);
            return removePatient;
        }
    }
}
