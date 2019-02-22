namespace PetClinic.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Dtos.Export;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals
                .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(a => new
                {
                    OwnerName = a.Passport.OwnerName,
                    AnimalName = a.Name,
                    Age = a.Age,
                    SerialNumber = a.PassportSerialNumber,
                    RegisteredOn = a.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                })
                .OrderBy(a => a.Age)
                .ThenBy(a => a.SerialNumber)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(animals);
            return jsonString;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context.Procedures
                .OrderBy(p => p.DateTime)
                .ThenBy(p => p.Animal.PassportSerialNumber)
                .Select(p => new ExportProcedureDto
                {
                    Passport = p.Animal.PassportSerialNumber,
                    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                    DateTime = p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = p.ProcedureAnimalAids.Select(a => new ExportAnimalAidDto
                    {
                        Name = a.AnimalAid.Name,
                        Price = a.AnimalAid.Price
                    })
                    .ToArray(),
                    TotalPrice = p.ProcedureAnimalAids.Sum(a => a.AnimalAid.Price)
                })
                .ToArray();

            var result = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ExportProcedureDto[]), new XmlRootAttribute("Procedures"));
            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
            serializer.Serialize(new StringWriter(result), procedures, xmlNamespaces);

            return result.ToString().Trim();
        }
    }
}
