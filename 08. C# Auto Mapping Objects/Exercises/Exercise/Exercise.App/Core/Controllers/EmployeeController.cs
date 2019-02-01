namespace Exercise.App.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Contracts;
    using Data;
    using Dtos;
    using Models;

    public class EmployeeController : IEmployeeController
    {
        private readonly ExerciseContext context;
        private readonly IMapper mapper;

        public EmployeeController(ExerciseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = this.mapper.Map<Employee>(employeeDto);
            this.context.Employees.Add(employee);
            this.context.SaveChanges();
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = this.context.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Address = address;
            this.context.SaveChanges();
        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            var employee = this.context.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Birthday = date;
            this.context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
            var employee = this.context.Employees
                .Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            var employeeDto = this.mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId)
        {
            var employee = this.context.Employees
                .Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            var employeeDto = this.mapper.Map<EmployeePersonalInfoDto>(employee);

            return employeeDto;
        }

        public List<EmployeesOlderThanDto> GetListEmployeesOlderThan(int age)
        {
            var employeesOlderThanDtos = new List<EmployeesOlderThanDto>();

            var employeesOlderThan = this.context.Employees
                .Where(e => e.Birthday.HasValue)
                .Where(e => (DateTime.Now.Year - e.Birthday.Value.Year) > age)
                .ToArray();

            foreach (var employee in employeesOlderThan)
            {
                var employeesOlderThanDto = this.mapper.Map<EmployeesOlderThanDto>(employee);
                employeesOlderThanDtos.Add(employeesOlderThanDto);
            }

            return employeesOlderThanDtos;
        }

        //Generic Version of both methods up
        public T GetDto<T>(int employeeId)
        {
            var employee = this.context.Employees
                .Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            var employeeDto = this.mapper.Map<T>(employee);

            return employeeDto;
        }
    }
}