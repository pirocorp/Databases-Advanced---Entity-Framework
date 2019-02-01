﻿namespace Exercise.App.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    using Dtos;

    public interface IEmployeeController
    {
        void AddEmployee(EmployeeDto employeeDto);

        void SetBirthday(int employeeId, DateTime date);

        void SetAddress(int employeeId, string address);

        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId);

        List<EmployeesOlderThanDto> GetListEmployeesOlderThan(int age);
    }
}
