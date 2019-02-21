namespace ACTester.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;

    using AcTester.Data.Interfaces;
    using AcTester.Helpers.Enumerations;
    using AcTester.Helpers.Utilities;
    using AcTester.Models;
    using Interfaces;
    using ViewModels;

    public class AirConditionerTesterSystem : IAirConditionerTesterSystem
    {
        private readonly IUnitOfWork database;

        public AirConditionerTesterSystem(IUnitOfWork database)
        {
            this.database = database;
        }

        public string RegisterStationaryAirConditioner(string manufacturer, string model, string energyEfficiencyRating, int powerUsage)
        {
            EnergyEfficiencyRating rating;
            try
            {
                rating =
                    (EnergyEfficiencyRating)Enum.Parse(typeof(EnergyEfficiencyRating), energyEfficiencyRating);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(Constants.IncorrectEnergyEfficiencyRating, ex);
            }

            AirConditioner airConditioner = new StationaryAirConditioner
            {
                PowerUsage = powerUsage,
                Manufacturer = manufacturer,
                Model = model,
                RequiredEnergyEfficiencyRating = rating
            };

            this.database.AirConditionersRepo.Add(airConditioner);
            this.database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage)
        {
            AirConditioner airConditioner = new CarAirConditioner
            {
                Manufacturer = manufacturer,
                Model = model,
                VolumeCovered = volumeCoverage
            };

            this.database.AirConditionersRepo.Add(airConditioner);
            this.database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string RegisterPlaneAirConditioner(string manufacturer, string model, int volumeCoverage, int electricityUsed)
        {
            AirConditioner airConditioner = new PlaneAirConditioner
            {
                Model = model,
                Manufacturer = manufacturer,
                VolumeCovered = volumeCoverage,
                ElectricityUsed = electricityUsed
            };

            this.database.AirConditionersRepo.Add(airConditioner);
            this.database.Save();
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string TestAirConditioner(string manufacturer, string model)
        {
            var airConditioner = this.GetAirConditionerByManufacturerAndModel(manufacturer, model);
            var air = Mapper.Map<AirConditionerDto>(airConditioner);

            air.Model = airConditioner.Model;
            air.Manufacturer = airConditioner.Manufacturer;

            var mark = air.Test() ? Mark.Passed : Mark.Failed;

            this.database.ReportsRepo.Add(new Report
            {
                Manufacturer = manufacturer,
                Model = model,
                Mark = mark
            });

            this.database.Save();
            return string.Format(Constants.TestAirConditioner, model, manufacturer);
        }

        public string FindAirConditioner(string manufacturer, string model)
        {
            var airConditioner = this.GetAirConditionerByManufacturerAndModel(manufacturer, model);
            var aidDto = Mapper.Map<AirConditionerDto>(airConditioner);
            return aidDto.ToString();
        }

        public string FindReport(string manufacturer, string model)
        {
            var report = this.GetReportByManufacturerAndModel(manufacturer, model);
            var reportDto = Mapper.Map<ReportDto>(report);

            return reportDto.ToString();
        }

        public string FindAllReportsByManufacturer(string manufacturer)
        {
            var reports = this.GetReportsByManufacturer(manufacturer);
            if (reports.Count == 0)
            {
                return Constants.NoReports;
            }


            IList<ReportDto> reportDtos = new List<ReportDto>();
            foreach (var report in reports)
            {
                reportDtos.Add(Mapper.Map<ReportDto>(report));
            }

            reportDtos = reportDtos.OrderBy(x => x.Model).ToList();
            var reportsPrint = new StringBuilder();
            reportsPrint.AppendLine(string.Format("Reports from {0}:", manufacturer));
            reportsPrint.Append(string.Join(Environment.NewLine, reportDtos));
            return reportsPrint.ToString();
        }

        public string Status()
        {
            var reports = this.database.ReportsRepo.Count();

            double airConditioners = this.database.AirConditionersRepo.Count();
            if (reports == 0)
            {
                return string.Format(Constants.Status, 0);
            }

            var percent = reports / airConditioners;
            percent = percent * 100;
            return string.Format(Constants.Status, percent);
        }

        private AirConditioner GetAirConditionerByManufacturerAndModel(string manufacturer, string model)
        {
            return this.database.AirConditionersRepo.First(air => air.Manufacturer == manufacturer && air.Model == model);
        }

        private Report GetReportByManufacturerAndModel(string manufacturer, string model)
        {
            return this.database.ReportsRepo.First(air => air.Manufacturer == manufacturer && air.Model == model);
        }

        private IList<Report> GetReportsByManufacturer(string manufacturer)
        {
            return this.database.ReportsRepo.GetAll(report => report.Manufacturer == manufacturer).ToList();
        }
    }
}
