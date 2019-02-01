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
            this.CreateMap<Employee, ManagerDto>()
                .ForMember(d => d.EmployeesDtos, s => 
                    s.MapFrom(m => m.ManagedEmployees))
                .ReverseMap();
        }
    }
}