using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Repository_Employee.Dtos;
using Repository_Employee.Interfaces;

namespace Repository_Employee.Implementations
{
    public class EmployeeRepository : IEmployee
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;

        public EmployeeRepository()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }
        public async Task<int> AddEmployee(Dtos.Employees employee)
        {
            try
            {
                employee.CreatedAt = DateTime.Now;
                employee.Doj = DateTime.Now;
                _unitOfWork.EmployeeRepository.Insert(employee);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            return await _unitOfWork.Save();

        }

        public async Task<int> EditEmployee(Dtos.Employees employee)
        {
            employee.ModifiedAt = DateTime.Now;
            _unitOfWork.EmployeeRepository.Update(employee);
            return await _unitOfWork.Save();
            
        }

        public async Task<IEnumerable<Dtos.Employees>> GetAll()
        {
            IEnumerable<Dtos.Employees> employee = await _unitOfWork.EmployeeRepository.GetAll(d => d.RoleNavigation);
            //IEnumerable<Employees> employee = await _unitOfWork.EmployeeRepository.GetAll();
            return employee;
        }

        public async Task<Dtos.Employees> GetEmployeeId(int id)
        {
            var empid = await _unitOfWork.EmployeeRepository.GetByID(id);
            if(empid!=null)
            {
                return empid;
            }
            return null;
        }

        public async Task<bool> RemoveEmployee(int empid)
        {
            bool result = false;
            if (empid > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var emp = await _unitOfWork.EmployeeRepository.GetByID(empid);
                    if (emp != null)
                    {
                        _unitOfWork.EmployeeRepository.Delete(empid);
                        _unitOfWork.Save();
                        scope.Complete();
                        result = true;

                    }
                }
            }
            return result;
        }
    }
}

