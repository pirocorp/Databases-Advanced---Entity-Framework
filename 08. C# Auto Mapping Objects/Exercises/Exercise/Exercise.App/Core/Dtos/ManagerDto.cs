namespace Exercise.App.Core.Dtos
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        public ManagerDto()
        {
            this.EmployeesDtos = new List<EmployeeDto>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeesCount => this.EmployeesDtos.Count;

        public ICollection<EmployeeDto> EmployeesDtos { get; set; }
    }
}