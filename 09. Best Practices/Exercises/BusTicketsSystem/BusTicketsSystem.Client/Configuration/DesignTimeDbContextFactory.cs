﻿namespace BusTicketsSystem.Client.Configuration
{
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    using Data;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BusTicketsContext>
    {
        public BusTicketsContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BusTicketsContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new BusTicketsContext(builder.Options);
        }
    }
}