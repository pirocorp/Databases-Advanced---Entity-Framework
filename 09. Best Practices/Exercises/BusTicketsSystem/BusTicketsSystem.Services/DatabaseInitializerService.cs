namespace BusTicketsSystem.Services
{
    using System.Linq;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly BusTicketsContext context;

        public DatabaseInitializerService(BusTicketsContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.Migrate();

            var databaseIsEmpty = !this.context.Customers.Any();

            if (databaseIsEmpty)
            {
                this.SeedDatabase();
            }
        }

        private void SeedDatabase()
        {
            this.SeedCountries();
            this.SeedTowns();
            this.SeedBusCompanies();
            this.SeedBusStations();
            this.SeedCustomers();
            this.SeedBankAccounts();
        }

        private void SeedBankAccounts()
        {
            var bankAccounts = new[]
            {
                new BankAccount{AccountNumber = "BG4396UBBS800265465464", Balance = 1250.00M, CustomerId = 1}, 
                new BankAccount{AccountNumber = "BG98RFSINB464564646645", Balance = 125550.00M, CustomerId = 1}, 
                new BankAccount{AccountNumber = "BG231BDFSDF45656456231", Balance = 250.00M, CustomerId = 3}, 
                new BankAccount{AccountNumber = "BG4396UBBS800265465464", Balance = 356, CustomerId = 3}, 
                new BankAccount{AccountNumber = "BGBHKLLJ54646546466412", Balance = 640M, CustomerId = 2}, 
                new BankAccount{AccountNumber = "BG332MKMKM555234646464", Balance = 313.33M, CustomerId = 5},
                new BankAccount{AccountNumber = "SWZ55563JDKS2313548632", Balance = 6546.45M, CustomerId = 4},
            };

            this.context.BankAccounts.AddRange(bankAccounts);
            this.context.SaveChanges();
        }

        private void SeedCustomers()
        {
            var customers = new[]
            {
                new Customer{FirstName = "Pesho", LastName = "Petkov", Gender = Gender.Male, HomeTownId = 1}, 
                new Customer{FirstName = "Galin", LastName = "Hristov", Gender = Gender.Male, HomeTownId = 1}, 
                new Customer{FirstName = "Qsen", LastName = "Peshev", Gender = Gender.Male, HomeTownId = 1}, 
                new Customer{FirstName = "Stefan", LastName = "Stoev", Gender = Gender.Male, HomeTownId = 1}, 
                new Customer{FirstName = "Kalin", LastName = "Hristov", Gender = Gender.Male, HomeTownId = 2}, 
                new Customer{FirstName = "Cvetan", LastName = "Polev", Gender = Gender.Male, HomeTownId = 2}, 
                new Customer{FirstName = "Toni", LastName = "Kishev", Gender = Gender.Male, HomeTownId = 1}, 
                new Customer{FirstName = "Georgi", LastName = "Geshev", Gender = Gender.Male, HomeTownId = 1}, 
                new Customer{FirstName = "Petar", LastName = "Stoev", Gender = Gender.Male, HomeTownId = 1}, 
                new Customer{FirstName = "Grigor", LastName = "Malinov", Gender = Gender.Male, HomeTownId = 3}, 
                new Customer{FirstName = "Vasil", LastName = "Stoev", Gender = Gender.Male, HomeTownId = 3}, 
                new Customer{FirstName = "Maq", LastName = "Gesheva", Gender = Gender.Female, HomeTownId = 4}, 
                new Customer{FirstName = "Viktorq", LastName = "Lesnata", Gender = Gender.Female, HomeTownId = 4}, 
                new Customer{FirstName = "Kostadinka", LastName = "Sivata", Gender = Gender.Female, HomeTownId = 4}, 
                new Customer{FirstName = "Zdravko", LastName = "Zdravkov", Gender = Gender.Male, HomeTownId = 4}, 
                new Customer{FirstName = "Stela", LastName = "Andonova", Gender = Gender.Female, HomeTownId = 14}, 
                new Customer{FirstName = "Daniela", LastName = "Stefanova", Gender = Gender.Female, HomeTownId = 14}, 
                new Customer{FirstName = "Kalina", LastName = "Pencheva", Gender = Gender.Female, HomeTownId = 14}, 
                new Customer{FirstName = "Plamen", LastName = "Rosinov", Gender = Gender.Male, HomeTownId = 14}, 
            };

            this.context.Customers.AddRange(customers);
            this.context.SaveChanges();
        }

        private void SeedBusStations()
        {
            var busStations = new[]
            {
                new BusStation {Name = "Central Station", TownId = 1},
                new BusStation {Name = "Train Station", TownId = 1},
                new BusStation {Name = "Centrum", TownId = 1},
                new BusStation {Name = "NDK", TownId = 1},
                new BusStation {Name = "Train Station", TownId = 2},
                new BusStation {Name = "Train Station", TownId = 3},
                new BusStation {Name = "South Station", TownId = 4},
                new BusStation {Name = "West Station", TownId = 4},
                new BusStation {Name = "North Station", TownId = 4},
                new BusStation {Name = "Central Station", TownId = 4},
                new BusStation {Name = "Central Station", TownId = 5},
                new BusStation {Name = "Train Station", TownId = 6},
                new BusStation {Name = "JFK", TownId = 7},
                new BusStation {Name = "San Remo", TownId = 7},
                new BusStation {Name = "Central Gare", TownId = 7},
                new BusStation {Name = "New Ark", TownId = 7},
                new BusStation {Name = "LAX", TownId = 8},
                new BusStation {Name = "Hollywood", TownId = 8},
                new BusStation {Name = "San Antonio", TownId = 8},
                new BusStation {Name = "BS Club", TownId = 9},
                new BusStation {Name = "Elisabeth Tower", TownId = 10},
                new BusStation {Name = "St. Patric", TownId = 10},
                new BusStation {Name = "Waterloo", TownId = 10},
                new BusStation {Name = "Big Ben", TownId = 10},
                new BusStation {Name = "Trafalgar", TownId = 10},
                new BusStation {Name = "Central Station", TownId = 11},
                new BusStation {Name = "Pier 12", TownId = 11},
                new BusStation {Name = "Central Station", TownId = 12},
                new BusStation {Name = "MU", TownId = 12},
                new BusStation {Name = "Central Station", TownId = 12},
                new BusStation {Name = "Das Kampf", TownId = 13},
                new BusStation {Name = "Mein Kampf", TownId = 13},
                new BusStation {Name = "Das Goolag", TownId = 13},
                new BusStation {Name = "Germania", TownId = 13},
                new BusStation {Name = "Wall", TownId = 14},
                new BusStation {Name = "East Berlin", TownId = 14},
                new BusStation {Name = "West Berlin", TownId = 14},
                new BusStation {Name = "Gay Town", TownId = 14},
                new BusStation {Name = "Sex Town", TownId = 14},
                new BusStation {Name = "Central Station", TownId = 15},
            };

            this.context.BusStations.AddRange(busStations);
            this.context.SaveChanges();
        }

        private void SeedBusCompanies()
        {
            var busCompanies = new[]
            {
                new BusCompany {Name = "Ivkoni", CountryId = 1}, 
                new BusCompany {Name = "Etap", CountryId = 1}, 
                new BusCompany {Name = "Biomet", CountryId = 1}, 
                new BusCompany {Name = "Greedy", CountryId = 2}, 
                new BusCompany {Name = "Trans Greedy", CountryId = 2}, 
                new BusCompany {Name = "May We Go", CountryId = 2}, 
                new BusCompany {Name = "Come in", CountryId = 2}, 
                new BusCompany {Name = "Get On", CountryId = 2}, 
                new BusCompany {Name = "Voyage", CountryId = 2}, 
                new BusCompany {Name = "Virgin", CountryId = 3}, 
                new BusCompany {Name = "AA", CountryId = 4}, 
                new BusCompany {Name = "Deltha", CountryId = 4}, 
                new BusCompany {Name = "Southern Service", CountryId = 4}, 
                new BusCompany {Name = "DB", CountryId = 5}, 
                new BusCompany {Name = "DAB", CountryId = 5},
            };

            this.context.BusCompanies.AddRange(busCompanies);
            this.context.SaveChanges();
        }

        private void SeedTowns()
        {
            var towns = new[]
            {
                new Town {Name = "Sofia", CountryId = 1},
                new Town {Name = "Varna", CountryId = 1},
                new Town {Name = "Shumen", CountryId = 1},
                new Town {Name = "Brussels", CountryId = 2},
                new Town {Name = "Antwerpen", CountryId = 2},
                new Town {Name = "Bruges", CountryId = 2},
                new Town {Name = "New York", CountryId = 3},
                new Town {Name = "Los Angeles", CountryId = 3},
                new Town {Name = "Boston", CountryId = 3},
                new Town {Name = "London", CountryId = 4},
                new Town {Name = "Brighton", CountryId = 4},
                new Town {Name = "Manchester", CountryId = 4},
                new Town {Name = "Munich", CountryId = 5},
                new Town {Name = "Berlin", CountryId = 5},
                new Town {Name = "Dresden", CountryId = 5},
            };

            this.context.Towns.AddRange(towns);
            this.context.SaveChanges();
        }

        private void SeedCountries()
        {
            var countries = new[]
            {
                new Country{Name = "Bulgaria"},
                new Country{Name = "Belgium"},
                new Country{Name = "USA"},
                new Country{Name = "UK"},
                new Country{Name = "Germany"},
            };

            this.context.Countries.AddRange(countries);
            this.context.SaveChanges();
        }
    }
}