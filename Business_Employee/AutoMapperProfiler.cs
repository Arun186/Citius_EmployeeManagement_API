using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Repository_Employee.Dtos;
using Models_Employee;

namespace Business_Employee
{
    class AutoMapperProfiler:Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<Employee_Data, Employees>().ReverseMap();
            CreateMap<Employee_Data, Employees>().ReverseMap().ForPath(source => source.RoleName, destination => destination.MapFrom(src => src.RoleNavigation.Rolename));
        }
    }
}
