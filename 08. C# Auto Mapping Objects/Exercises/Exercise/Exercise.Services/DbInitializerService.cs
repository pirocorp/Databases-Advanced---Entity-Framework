﻿namespace Exercise.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data;

    public class DbInitializerService : IDbInitializerService
    {
        private readonly ExerciseContext context;

        public DbInitializerService(ExerciseContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}
