using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository_Employee.Dtos;


namespace Repository_Employee.Interfaces
{
   public interface IEmployee
    {
        Task<IEnumerable<Dtos.Employees>> GetAll();

        Task<Dtos.Employees> GetEmployeeId(int id);

        Task<int> AddEmployee(Dtos.Employees employee);

        Task<bool> RemoveEmployee(int empid);

        Task<int> EditEmployee(Dtos.Employees employee);
    }
}
