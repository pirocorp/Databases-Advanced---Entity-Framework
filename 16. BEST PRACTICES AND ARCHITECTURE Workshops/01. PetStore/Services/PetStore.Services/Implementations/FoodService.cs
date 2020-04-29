namespace PetStore.Services.Implementations
{
    using System;

    using Data.Models;
    using Models.Food;

    public class FoodService : IFoodService
    {
        public void BuyFromDistributor(string name, double weight, decimal price, 
            DateTime expirationDate, string brandName, string categoryName)
        {
            var food = new Food
            {
                Name = name,
                Weight = weight,
                Price = price,
                ExpirationDate = expirationDate,
            };
        }

        public void BuyFromDistributor(AddingFoodServiceModel model)
        {
            throw new NotImplementedException();
        }
    }
}
