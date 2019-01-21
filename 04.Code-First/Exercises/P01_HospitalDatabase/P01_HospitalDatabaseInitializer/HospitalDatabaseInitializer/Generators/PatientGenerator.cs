﻿namespace P01_HospitalDatabase.Generators
{
    using System;
    //using System.IO;

    using Data;
    using Data.Models;

    public class PatientGenerator
    {
        private static Random rnd = new Random();

        public static Patient NewPatient(HospitalContext context)
        {
            var firstName = NameGenerator.FirstName();
            var lastName = NameGenerator.LastName();

            var patient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = EmailGenerator.NewEmail(firstName + lastName),
                Address = AddressGenerator.NewAddress(),
            };

            patient.Visitations = GenerateVisitations(patient);
            patient.Diagnoses = GenerateDiagnoses(patient);

            return patient;
        }

        private static Diagnose[] GenerateDiagnoses(Patient patient)
        {
            var diagnoseNames = new string[] 
            {
                "Limp Scurvy",
                "Fading Infection",
                "Cow Feet",
                "Incurable Ebola",
                "Snake Blight",
                "Spider Asthma",
                "Sinister Body",
                "Spine Diptheria",
                "Pygmy Decay",
                "King's Arthritis",
                "Desert Rash",
                "Deteriorating Salmonella",
                "Shadow Anthrax",
                "Hiccup Meningitis",
                "Fading Depression",
                "Lion Infertility",
                "Wolf Delirium",
                "Humming Measles",
                "Incurable Stomach",
                "Grave Heart",
            };
            //var diagnoseNames = File.ReadAllLines("<INSERT DIR HERE>");

            var diagnoseCount = rnd.Next(1, 4);
            var diagnoses = new Diagnose[diagnoseCount];
            for (var i = 0; i < diagnoseCount; i++)
            {
                var diagnoseName = diagnoseNames[rnd.Next(diagnoseNames.Length)];

                var diagnose = new Diagnose()
                {
                    Name = diagnoseName,
                    Patient = patient
                };

                diagnoses[i] = diagnose;
            }

            return diagnoses;
        }

        private static Visitation[] GenerateVisitations(Patient patient)
        {
            var visitationCount = rnd.Next(1, 5);

            var visitations = new Visitation[visitationCount];

            for (var i = 0; i < visitationCount; i++)
            {
                var visitationDate = RandomDay(2005);

                var visitation = new Visitation()
                {
                    Date = visitationDate,
                    Patient = patient
                };

                visitations[i] = visitation;
            }

            return visitations;
        }

        private static DateTime RandomDay(int startYear)
        {
            var start = new DateTime(startYear, 1, 1);
            var range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}
