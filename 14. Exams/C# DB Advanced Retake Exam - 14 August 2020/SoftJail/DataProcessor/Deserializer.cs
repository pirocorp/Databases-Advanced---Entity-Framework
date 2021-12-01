namespace SoftJail.DataProcessor
{
    using System;
    using Data;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        private const string DepartmentSuccessMessage = "Imported {0} with {1} cells";

        private const string PrisonerSuccessMessage = "Imported {0} {1} years old";

        private const string OfficerSuccessMessage = "Imported {0} ({1} prisoners)";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var output = new StringBuilder();

            var departments = JsonConvert.DeserializeObject<DepartmentImportDto[]>(jsonString);

            foreach (var departmentDto in departments)
            {
                if (!IsValid(departmentDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessDepartment(departmentDto, output, context);
            }

            return output.ToString();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var output = new StringBuilder();

            var prisoners = JsonConvert.DeserializeObject<PrisonerImportDto[]>(jsonString);

            foreach (var prisonerDto in prisoners)
            {
                if (!IsValid(prisonerDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessPrisoner(prisonerDto, output, context);
            }

            return output.ToString();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var output = new StringBuilder();

            var officerImportDtos = DeserializeXml<OfficerImportDto>("Officers", xmlString);

            foreach (var officerDto in officerImportDtos)
            {
                if (!IsValid(officerDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessOfficer(officerDto, output, context);
            }

            return output.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }

        private static IEnumerable<TDto> DeserializeXml<TDto>(string rootAttribute, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var userDtos = (TDto[])serializer.Deserialize(new StringReader(inputXml));
            return userDtos;
        }

        private static void ProcessDepartment(DepartmentImportDto departmentDto, StringBuilder output, SoftJailDbContext context)
        {
            if (departmentDto.Cells.Length < 1)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var cells = new List<Cell>();

            foreach (var cellDto in departmentDto.Cells)
            {
                if (!IsValid(cellDto))
                {
                    output.AppendLine(ErrorMessage);
                    return;
                }

                cells.Add(new Cell()
                {
                    CellNumber = cellDto.CellNumber,
                    HasWindow = cellDto.HasWindow,
                });
            }

            var department = new Department()
            {
                Name = departmentDto.Name
            };

            context.Add(department);
            context.SaveChanges();

            cells.ForEach(c => c.DepartmentId = department.Id);

            department.Cells = cells;
            context.SaveChanges();

            output.AppendLine(string.Format(DepartmentSuccessMessage, department.Name, cells.Count));

        }
        
        private static void ProcessPrisoner(PrisonerImportDto prisonerDto, StringBuilder output, SoftJailDbContext context)
        {
            if (prisonerDto.Bail.HasValue && prisonerDto.Bail.Value < 0)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var validIncarcerationDate = DateTime.TryParseExact(
                prisonerDto.IncarcerationDate,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var incarcerationDate);

            var releaseDate = (DateTime?)null;
            var validReleaseDate = true;

            if (!string.IsNullOrWhiteSpace(prisonerDto.ReleaseDate))
            {
                validReleaseDate = DateTime.TryParseExact(
                    prisonerDto.ReleaseDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate);

                releaseDate = parsedDate;
            }

            if (!validIncarcerationDate || !validReleaseDate)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var mails = new List<Mail>();

            foreach (var mailDto in prisonerDto.Mails)
            {
                if (!IsValid(mailDto))
                {
                    output.AppendLine(ErrorMessage);
                    return;
                }

                mails.Add(new Mail()
                {
                    Description = mailDto.Description,
                    Sender = mailDto.Sender,
                    Address = mailDto.Address
                });
            }

            var prisoner = new Prisoner()
            {
                FullName = prisonerDto.FullName,
                Nickname = prisonerDto.Nickname,
                Age = prisonerDto.Age,
                IncarcerationDate = incarcerationDate,
                ReleaseDate = releaseDate,
                Bail = prisonerDto.Bail,
                CellId = prisonerDto.CellId,
            };

            context.Add(prisoner);
            context.SaveChanges();

            mails.ForEach(m => m.PrisonerId = prisoner.Id);
            prisoner.Mails = mails;
            context.SaveChanges();

            output.AppendLine(string.Format(PrisonerSuccessMessage, prisoner.FullName, prisoner.Age));
        }

        private static void ProcessOfficer(OfficerImportDto officerDto, StringBuilder output, SoftJailDbContext context)
        {
            if (officerDto.Money < 0)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var validPosition = Enum.TryParse<Position>(officerDto.Position, out var position);
            var validWeapon = Enum.TryParse<Weapon>(officerDto.Weapon, out var weapon);

            if (!validPosition || !validWeapon)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            //if (!context.Departments.Any(d => d.Id == officerDto.DepartmentId))
            //{
            //    output.AppendLine(ErrorMessage);
            //    return;
            //}

            var officerPrisoners = officerDto.Prisoners
                .Select(p => new OfficerPrisoner()
                {
                    PrisonerId = p.Id
                })
                .ToList();

            var officer = new Officer()
            {
                FullName = officerDto.Name,
                Salary = officerDto.Money,
                Position = position,
                Weapon = weapon,
                DepartmentId = officerDto.DepartmentId,
                OfficerPrisoners = officerPrisoners
            };

            output.AppendLine(string.Format(OfficerSuccessMessage, officer.FullName, officerPrisoners.Count));
            context.Add(officer);
            context.SaveChanges();
        }
    }
}