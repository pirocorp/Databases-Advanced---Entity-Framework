namespace PetStore.Services
{
    using System;

    using Models.Food;

    public interface IFoodService
    {
        void BuyFromDistributor(string name, double weight, decimal price,
            decimal profit, DateTime expirationDate, int brandId, int categoryId);

        void BuyFromDistributor(AddingFoodServiceModel model);

        bool FoodExists(int foodId);

        void SellFoodToUser(int foodId, int userId);
    }
}
