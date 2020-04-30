namespace PetStore.Services
{
    using System.Collections.Generic;

    using Models.Category;

    public interface ICategoryService
    {
        int Create(string name, string description);

        int Create(CreateCategoryServiceModel model);

        bool Exists(int categoryId);

        bool Exists(string name);

        IEnumerable<CategoryListingServiceModel> All();

        CategoryListingServiceModel GetById(int id);

        void Edit(CategoryListingServiceModel model);

        bool Remove(int id);
    }
}
