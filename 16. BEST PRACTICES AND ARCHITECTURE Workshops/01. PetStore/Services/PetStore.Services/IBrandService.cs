namespace PetStore.Services
{
    using System.Collections.Generic;
    using Models.Brand;

    public interface IBrandService
    {
        int Create(string name);

        int? GetBrandIdByName(string name);

        IEnumerable<BrandListingServiceModel> SearchByName(string name);

        BrandWithToysServiceModel FindByIdWithToys(int id);
    }
}
