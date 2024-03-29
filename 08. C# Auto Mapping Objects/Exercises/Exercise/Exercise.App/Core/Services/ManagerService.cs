﻿namespace Exercise.App.Core.Services
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using Data;
    using Dtos;
    using Microsoft.EntityFrameworkCore;

    public class ManagerService : IManagerService
    {
        private readonly ExerciseContext context;
        private readonly IMapper mapper;

        public ManagerService(ExerciseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public SetManagerDto SetManager(int employeeId, int managerId)
        {
            var employee = this.context.Employees.Find(employeeId);

            var manager = this.context.Employees.Find(managerId);

            if (employee == null || manager == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Manager = manager;
            this.context.SaveChanges();

            return this.mapper.Map<SetManagerDto>(employee);
        }

        public ManagerDto GetManagerInfo(int employeeId)
        {
            var employee = this.context.Employees
                .Include(x => x.ManagedEmployees)
                .SingleOrDefault(x => x.Id == employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            var employeeDto = this.mapper.Map<ManagerDto>(employee);

            return employeeDto;
        }
    }
}