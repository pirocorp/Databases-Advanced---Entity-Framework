namespace PetStore.Services
{
    using System;

    using Models.Food;

    public interface IFoodService
    {
        void BuyFromDistributor(string name, double weight, decimal price,
            decimal profit, DateTime expirationDate, int brandId, int categoryId);

        void BuyFromDistributor(AddingFoodServiceModel model);

        bool Exists(int foodId);

        void SellFoodToUser(int foodId, int userId);
    }
}
