namespace PetStore
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;
    using Services.Implementations;

    public static class StartUp
    {
        public static void Main()
        {
            using var data = new PetStoreDbContext();

        }

        private static void SeedData(PetStoreDbContext data)
        {
            for (var i = 0; i < 10; i++)
            {
                var breed = new Breed
                {
                    Name = "Breed " + i,
                };

                data.Breeds.Add(breed);
            }

            data.SaveChanges();

            for (var i = 0; i < 30; i++)
            {
                var category = new Category
                {
                    Name = "Category " + i,
                    Description = "Category Description " + i,
                };

                data.Categories.Add(category);
            }

            data.SaveChanges();

            for (var i = 0; i < 100; i++)
            {
                var categoryId = data.Categories
                    .OrderBy(c => Guid.NewGuid())
                    .Select(c => c.Id)
                    .FirstOrDefault();

                var breedId = data.Breeds
                    .OrderBy(c => Guid.NewGuid())
                    .Select(c => c.Id)
                    .FirstOrDefault();

                var pet = new Pet
                {
                    DateOfBirth = DateTime.UtcNow.AddDays(-60),
                    Price = 50 + i,
                    Gender = (Gender) (i % 2),
                    Description = "Some random description " + i,
                    CategoryId = categoryId,
                    BreedId = breedId
                };

                data.Pets.Add(pet);
            }

            data.SaveChanges();
        }
    }
}
