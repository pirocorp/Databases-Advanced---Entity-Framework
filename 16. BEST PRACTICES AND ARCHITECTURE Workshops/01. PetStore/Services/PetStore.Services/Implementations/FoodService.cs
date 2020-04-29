namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;
    using Models.Food;
    using static Data.Models.DataValidation;

    public class FoodService : IFoodService
    {
        private readonly PetStoreDbContext _data;
        private readonly IUserService _userService;

        public FoodService(PetStoreDbContext data, IUserService userService)
        {
            this._data = data;
            this._userService = userService;
        }

        public void BuyFromDistributor(string name, double weight, decimal price, 
            decimal profit, DateTime expirationDate, int brandId, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name cannot be empty.");
            }

            if (name.Length > NameMaxLength)
            {
                throw new InvalidOperationException($"Food name cannot be more than ${NameMaxLength} characters.");
            }

            if (profit < 0)
            {
                throw new InvalidOperationException("Profit must be positive.");
            }

            var food = new Food
            {
                Name = name,
                Weight = weight,
                DistributorPrice = price,
                Price = price + profit,
                ExpirationDate = expirationDate,
                BrandId = brandId,
                CategoryId = categoryId,
            };

            this._data.Foods.Add(food);
            this._data.SaveChanges();
        }

        public void BuyFromDistributor(AddingFoodServiceModel model)
        {
            this.BuyFromDistributor(model.Name, model.Weight, model.Price, 
                model.Profit, model.ExpirationDate, model.BrandId, model.CategoryId);
        }

        public bool FoodExists(int foodId)
        {
            return this._data.Foods.Any(f => f.Id == foodId);
        }

        public void SellFoodToUser(int foodId, int userId)
        {
            if (!this.FoodExists(foodId))
            {
                throw new InvalidOperationException($"Food with id {foodId} doesn't exists.");
            }

            if (!this._userService.UserExists(userId))
            {
                throw new InvalidOperationException($"User with id {userId} doesn't exists.");
            }

            var order = new Order()
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Done,
                UserId = userId
            };

            var foodOrder = new FoodOrder()
            {
                FoodId = foodId,
                Order = order
            };

            this._data.Orders.Add(order);
            this._data.FoodOrders.Add(foodOrder);

            this._data.SaveChanges();
        }
    }
}
