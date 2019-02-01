namespace Exercise.App.Core.Controllers
{
    using System;
    using System.Linq;
    using AutoMapper;

    using Contracts;
    using Data;
    using Dtos;
    using Microsoft.EntityFrameworkCore;

    public class ManagerController : IManagerController
    {
        private readonly ExerciseContext context;
        private readonly IMapper mapper;

        public ManagerController(ExerciseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void SetManager(int employeeId, int managerId)
        {
            var employee = this.context.Employees.Find(employeeId);

            var manager = this.context.Employees.Find(managerId);

            if (employee == null || manager == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Manager = manager;
            this.context.SaveChanges();
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