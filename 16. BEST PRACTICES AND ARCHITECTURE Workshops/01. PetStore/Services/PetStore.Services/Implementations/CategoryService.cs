namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Models;
    using Models.Category;
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

        public int Create(CreateCategoryServiceModel model)
        {
            return this.Create(model.Name, model.Description);
        }

        public bool Exists(int categoryId)
        {
            return this._data.Categories.Any(c => c.Id == categoryId);
        }

        public bool Exists(string name)
        {
            return this._data.Categories.Any(c => c.Name == name);
        }

        public IEnumerable<CategoryListingServiceModel> All()
        {
            return this._data
                .Categories
                .Select(c => new CategoryListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();
        }

        public CategoryListingServiceModel GetById(int id)
        {
            return this._data.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstOrDefault();
        }

        public void Edit(CategoryListingServiceModel model)
        {
            var category = this._data.Categories
                .Find(model.Id);

            category.Name = model.Name;
            category.Description = model.Description;

            this._data.SaveChanges();
        }

        public bool Remove(int id)
        {
            var category = this._data
                .Categories
                .Find(id);

            if (category == null)
            {
                return false;
            }

            this._data.Categories.Remove(category);

            try
            {
                var deletedEntitiesCount = this._data.SaveChanges();

                if (deletedEntitiesCount == 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
