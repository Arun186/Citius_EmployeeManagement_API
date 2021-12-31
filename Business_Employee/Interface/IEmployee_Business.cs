using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models_Employee;

namespace Business_Employee.Interface
{
    public interface IEmployee_Business
    {
        Task<IEnumerable<Employee_Data>> GetAll();

        Task<Employee_Data> GetEmployeeId(int id);

        Task<int> AddEmployee(Employee_Data employee);

        Task<bool> RemoveEmployee(int empid);

        Task<int> EditEmployee(Employee_Data employee);
    }
}
