namespace Exercise.App.Core
{
    using AutoMapper;

    using Dtos;
    using Models;

    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            this.CreateMap<Employee, EmployeeDto>().ReverseMap();
            this.CreateMap<Employee, EmployeePersonalInfoDto>();
            this.CreateMap<Employee, EmployeeSetAddressDto>();
            this.CreateMap<Employee, EmployeeSetBirthdayDto>();
            this.CreateMap<Employee, EmployeesOlderThanDto>()
                .ForMember(d => d.ManagerLastName, 
                           s => s.MapFrom(m => m.Manager.LastName));
            this.CreateMap<Employee, SetManagerDto>()
                .ForMember(d => d.ManagerFirstName,
                           s => s.MapFrom(m => m.Manager.FirstName))
                .ForMember(d => d.ManagerLastName,
                           s => s.MapFrom(m => m.Manager.LastName));
            this.CreateMap<Employee, ManagerDto>()
                .ForMember(d => d.EmployeesDtos, 
                           s => s.MapFrom(m => m.ManagedEmployees))
                .ReverseMap();
        }
    }
}