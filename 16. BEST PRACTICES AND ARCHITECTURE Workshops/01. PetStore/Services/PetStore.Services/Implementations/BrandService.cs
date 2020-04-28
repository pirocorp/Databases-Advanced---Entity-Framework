namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Data;
    using Data.Models;
    using Models.Brand;
    using static Data.Models.DataValidation;

    public class BrandService : IBrandService
    {
        private readonly PetStoreDbContext _data;

        public BrandService(PetStoreDbContext data)
        {
            this._data = data;
        }

        public int Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name cannot be empty.");
            }

            if (name.Length > NameMaxLength)
            {
                throw new InvalidOperationException($"Brand name cannot be more than ${NameMaxLength} characters.");
            }

            if (this._data.Brands.Any(br => br.Name == name))
            {
                throw new InvalidOperationException($"Brand {name} already exists.");
            }

            var brand = new Brand
            {
                Name = name
            };

            this._data.Brands.Add(brand);
            this._data.SaveChanges();

            return brand.Id;
        }

        public IEnumerable<BrandListingServiceModel> SearchByName(string name)
        {
            return this._data.Brands
                .Where(br => br.Name.ToLower().Contains(name.ToLower()))
                .Select(br => new BrandListingServiceModel
                {
                    Id = br.Id,
                    Name = br.Name,
                })
                .ToList();
        }
    }
}
