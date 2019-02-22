namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using Data;
    using Dtos.Import;
    using Models;

    public class Deserializer
    {
        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var animalAidDtos = (ImportAnimalAidDto[])JsonConvert.DeserializeObject(jsonString, typeof(ImportAnimalAidDto[]));
            var animalAids = new List<AnimalAid>();
            var result = new StringBuilder();

            foreach (var animalAidDto in animalAidDtos)
            {
                if (!IsValid(animalAidDto))
                {
                    result.AppendLine($"Error: Invalid data.");
                    continue;
                }

                if (context.AnimalAids.Any(x => x.Name == animalAidDto.Name) ||
                    animalAids.Any(x => x.Name == animalAidDto.Name))
                {
                    result.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var animalAid = new AnimalAid()
                {
                    Name = animalAidDto.Name,
                    Price = animalAidDto.Price
                };

                animalAids.Add(animalAid);
                result.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();
            return result.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var result = new StringBuilder();
            var animalDtos = (ImportAnimalDto[]) JsonConvert.DeserializeObject(jsonString, typeof(ImportAnimalDto[]));
            var animals = new List<Animal>();

            foreach (var animalDto in animalDtos)
            {
                if (!IsValid(animalDto) ||
                    !IsValid(animalDto.Passport))
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                var passportIsValid = ValidatePassport(context, animalDto.Passport);

                if (!passportIsValid)
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (animals.Any(a => a.Passport.SerialNumber == animalDto.Passport.SerialNumber))
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                var currentPassport = new Passport
                {
                    SerialNumber = animalDto.Passport.SerialNumber,
                    OwnerName = animalDto.Passport.OwnerName,
                    OwnerPhoneNumber = animalDto.Passport.OwnerPhoneNumber,
                    RegistrationDate = DateTime.ParseExact(animalDto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                };

                var currentAnimal = new Animal()
                {
                    Name = animalDto.Name,
                    Type = animalDto.Type,
                    Age = animalDto.Age,
                    Passport = currentPassport
                };

                animals.Add(currentAnimal);
                result.AppendLine($"Record {currentAnimal.Name} Passport №: {currentAnimal.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.SaveChanges();
            return result.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportVetDto[]), new XmlRootAttribute("Vets"));
            var vetDtos = (ImportVetDto[]) serializer.Deserialize(new StringReader(xmlString));

            var result = new StringBuilder();
            var vets = new List<Vet>();

            foreach (var vetDto in vetDtos)
            {
                if (!IsValid(vetDto))
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (!ValidatePhoneNumber(vetDto.PhoneNumber))
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (context.Vets.Any(v => v.PhoneNumber == vetDto.PhoneNumber) ||
                    vets.Any(v => v.PhoneNumber == vetDto.PhoneNumber))
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                var vet = new Vet
                {
                    Name = vetDto.Name,
                    Profession = vetDto.Profession,
                    Age = vetDto.Age,
                    PhoneNumber = vetDto.PhoneNumber
                };

                vets.Add(vet);
                result.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();
            return result.ToString().Trim();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportProcedureDto[]), new XmlRootAttribute("Procedures"));
            var procedureDtos = (ImportProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            var result = new StringBuilder();
            var procedures = new List<Procedure>();

            foreach (var procedureDto in procedureDtos)
            {
                var vet = context.Vets.FirstOrDefault(v => v.Name == procedureDto.Vet);

                if (vet == null)
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                var animal = context.Animals.SingleOrDefault(a => a.Passport.SerialNumber == procedureDto.Animal);

                if (animal == null)
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (!ValidateDate(procedureDto.DateTime))
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                var date = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                var animalAids = GetAnimalAids(context, procedureDto);

                if (animalAids == null)
                {
                    result.AppendLine("Error: Invalid data.");
                    continue;
                }

                var procedure = new Procedure()
                {
                    Vet = vet,
                    Animal = animal,
                    DateTime = date,
                };

                var procedureAnimalAids = new List<ProcedureAnimalAid>();

                foreach (var animalAid in animalAids)
                {
                    var currentProcedureAnimalAid = new ProcedureAnimalAid()
                    {
                        Procedure = procedure,
                        AnimalAid = animalAid
                    };

                    procedureAnimalAids.Add(currentProcedureAnimalAid);
                }

                procedure.ProcedureAnimalAids = procedureAnimalAids;
                procedures.Add(procedure);
                result.AppendLine("Record successfully imported.");
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();
            return result.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validatorContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validatorContext, validationResults, true);
        }

        private static bool ValidatePassport(PetClinicContext context, ImportAnimalPassportDto animalDtoPassport)
        {
            var passportSerialNumber = animalDtoPassport.SerialNumber;

            if (context.Passports.Any(p => p.SerialNumber == passportSerialNumber))
            {
                return false;
            }

            if (!ValidateSerialNumber(animalDtoPassport.SerialNumber))
            {
                return false;
            }

            if (!ValidatePhoneNumber(animalDtoPassport.OwnerPhoneNumber))
            {
                return false;
            }

            if (!ValidateDate(animalDtoPassport.RegistrationDate))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateSerialNumber(string serialNumber)
        {
            var validStringLength = serialNumber.Length == 10;
            var startsWith7Letters = serialNumber.Take(7).All(char.IsLetter);
            var endsWith3Digits = serialNumber.Skip(7).Take(3).All(char.IsDigit);

            return validStringLength && startsWith7Letters && endsWith3Digits;
        }

        private static bool ValidatePhoneNumber(string ownerPhoneNumber)
        {
            if (ownerPhoneNumber.Length == 10)
            {
                var firstDigitIsZero = ownerPhoneNumber[0] == '0';
                var allDigits = ownerPhoneNumber.All(char.IsDigit);

                return firstDigitIsZero && allDigits;
            }

            if (ownerPhoneNumber.Length == 13)
            {
                var startsWith = ownerPhoneNumber.StartsWith("+359");
                var followedBy9Digits = ownerPhoneNumber.Skip(4).Take(9).All(char.IsDigit);

                return startsWith && followedBy9Digits;
            }

            return false;
        }

        private static bool ValidateDate(string registrationDate)
        {
            var isParsed = DateTime.TryParseExact(registrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);

            return isParsed;
        }

        private static IEnumerable<AnimalAid> GetAnimalAids(PetClinicContext context, ImportProcedureDto procedureDto)
        {
            var animalAids = new List<AnimalAid>();

            foreach (var animalAidDto in procedureDto.AnimalAids)
            {
                var currentAnimalAid = context.AnimalAids.FirstOrDefault(a => a.Name == animalAidDto.Name);

                if (currentAnimalAid == null)
                {
                    return null;
                }

                if (animalAids.Any(a => a.Name == animalAidDto.Name))
                {
                    return null;
                }

                animalAids.Add(currentAnimalAid);
            }

            return animalAids;
        }
    }
}