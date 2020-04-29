namespace PetStore.Services
{
    using System;

    using Models.Food;

    public interface IFoodService
    {
        void BuyFromDistributor(string name, double weight, decimal price,
            DateTime expirationDate, string brandName, string categoryName);

        void BuyFromDistributor(AddingFoodServiceModel model);
    }
}
