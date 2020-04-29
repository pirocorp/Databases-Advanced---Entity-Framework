namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;
    using static Data.Models.DataValidation;

    public class CategoryService : ICategoryService
    {
        private readonly PetStoreDbContext _data;

        public CategoryService(PetStoreDbContext data)
        {
            this._data = data;
        }

        public int Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name cannot be empty.");
            }

            if (name.Length > NameMaxLength)
            {
                throw new InvalidOperationException($"Category name cannot be more than ${NameMaxLength} characters.");
            }

            if (this._data.Categories.Any(c => c.Name == name))
            {
                throw new InvalidOperationException($"Category {name} already exists.");
            }

            var category = new Category()
            {
                Name = name,
                Description = description
            };

            this._data.Categories.Add(category);
            this._data.SaveChanges();

            return category.Id;
        }

        public bool Exists(int categoryId)
        {
            return this._data.Categories.Any(c => c.Id == categoryId);
        }
    }
}
