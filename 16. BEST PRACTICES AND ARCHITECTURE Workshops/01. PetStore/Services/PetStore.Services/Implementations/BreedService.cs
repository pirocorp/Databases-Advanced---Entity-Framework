namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;
    using static Data.Models.DataValidation;

    public class BreedService : IBreedService
    {
        private readonly PetStoreDbContext _data;

        public BreedService(PetStoreDbContext data)
        {
            this._data = data;
        }

        public int Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name cannot be empty.");
            }

            if (name.Length > NameMaxLength)
            {
                throw new InvalidOperationException($"Breed name cannot be more than ${NameMaxLength} characters.");
            }

            if (this._data.Breeds.Any(br => br.Name == name))
            {
                throw new InvalidOperationException($"Breed {name} already exists.");
            }

            var breed = new Breed()
            {
                Name = name
            };

            this._data.Breeds.Add(breed);
            this._data.SaveChanges();

            return breed.Id;
        }

        public bool Exists(int breedId)
        {
            return this._data.Breeds.Any(x => x.Id == breedId);
        }
    }
}
