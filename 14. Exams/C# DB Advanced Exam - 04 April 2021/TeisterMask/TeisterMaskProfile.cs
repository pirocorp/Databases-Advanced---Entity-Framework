namespace TeisterMask
{
    using System;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using Common;
    using Data.Models;
    using DataProcessor.ImportDto;

    public class TeisterMaskProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public TeisterMaskProfile()
        {
            this.CreateMap<TaskDto, Task>()
                .ForMember(
                    d => d.OpenDate,
                    opt => opt.MapFrom(s =>
                        DateTime.ParseExact(s.OpenDate, ValidationConstants.DateTimeFormat, CultureInfo.InvariantCulture)))
                .ForMember(
                    d => d.DueDate,
                    opt => opt.MapFrom(s =>
                        DateTime.ParseExact(s.DueDate, ValidationConstants.DateTimeFormat, CultureInfo.InvariantCulture)));

            this.CreateMap<ProjectDto, Project>()
                .ForMember(
                    d => d.OpenDate,
                    opt => opt.MapFrom(s => DateTime.ParseExact(s.OpenDate, ValidationConstants.DateTimeFormat, CultureInfo.InvariantCulture)))
                .ForMember(
                    d => d.DueDate,
                    opt => opt.MapFrom(s => s.DueDate == "" 
                            ? (DateTime?) null 
                            : DateTime.ParseExact(s.DueDate, ValidationConstants.DateTimeFormat, CultureInfo.InvariantCulture)))
                .ForMember(
                    d => d.Tasks,
                    opt => opt.MapFrom(s => s.Tasks));

            this.CreateMap<EmployeeDto, Employee>();

            this.CreateMap<Task, DataProcessor.ExportDto.TaskDto>()
                .ForMember(
                    d => d.Label, 
                    opt => opt.MapFrom(s => s.LabelType.ToString()));

            this.CreateMap<Project, DataProcessor.ExportDto.ProjectDto>()
                .ForMember(
                    d => d.Tasks,
                    opt => opt.MapFrom(s => s.Tasks.OrderBy(t => t.Name)))
                .ForMember(
                    d => d.HasEndDate,
                    opt => opt.MapFrom(s => s.DueDate.HasValue ? "Yes" : "No"));

        }
    }
}
