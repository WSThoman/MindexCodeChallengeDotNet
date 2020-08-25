﻿using challenge.Models;

namespace challenge.Services
{
    public interface IEmployeeService
    {
        Employee GetById(string id);

        Employee Create(Employee employee);

        Employee Replace(Employee originalEmployee, Employee newEmployee);
    }
}
