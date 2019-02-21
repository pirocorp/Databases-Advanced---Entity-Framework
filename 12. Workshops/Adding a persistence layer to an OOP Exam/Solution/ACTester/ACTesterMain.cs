namespace ACTester
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AcTester.Models;
    using AutoMapper;
    using Core;
    using global::Ninject;
    using Interfaces;
    using ViewModels;

    public class AcTesterMain
    {
        public static void Main()
        {
            ConfigureMapper();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var manager = kernel.Get<IActionManager>();
            var userInterface = kernel.Get<IUserInterface>();
            var engine = new Engine(manager, userInterface);
            engine.Run();
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Report, ReportDto>();
                expression.CreateMap<AirConditioner, AirConditionerDto>()
                    .Include<StationaryAirConditioner, StationaryAirConditionerDto>()
                    .Include<VehicleAirConditioner, VehicleAirConditionerDto>()
                    .Include<PlaneAirConditioner, PlaneAirConditionerDto>()
                    .Include<CarAirConditioner, CarAirConditionerDto>();

                var types = Assembly.Load("AcTester.Models")
                                       .GetTypes()
                                       .Where(type => type.IsSubclassOf(typeof(AirConditioner)))
                                       .ToArray();

                foreach (var type in types)
                {
                    expression.CreateMap(type, Type.GetType("ACTester.ViewModels." + type.Name + "Dto"));
                }
            });
        }
    }
}
