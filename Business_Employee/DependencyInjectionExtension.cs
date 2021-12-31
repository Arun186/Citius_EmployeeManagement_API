using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Repository_Employee.Implementations;
using Repository_Employee.Interfaces;

namespace Business_Employee
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterBusinessDependencies(this IServiceCollection services)
        {
            services.AddTransient<IEmployee,EmployeeRepository>();
            return services;
        }
    }
}
