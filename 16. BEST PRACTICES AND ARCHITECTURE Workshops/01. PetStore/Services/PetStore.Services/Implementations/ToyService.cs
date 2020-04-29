namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;
    using Models.Toy;
    using static Data.Models.DataValidation;

    public class ToyService : IToyService
    {
        private readonly PetStoreDbContext _data;
        private readonly IUserService _userService;

        public ToyService(PetStoreDbContext data, IUserService userService)
        {
            this._data = data;
            this._userService = userService;
        }

        public void BuyFromDistributor(string name, string description, 
            decimal distributorPrice, decimal profit, int brandId,
            int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name cannot be empty.");
            }

            if (name.Length > NameMaxLength)
            {
                throw new InvalidOperationException($"Toy name cannot be more than ${NameMaxLength} characters.");
            }

            if (profit < 0)
            {
                throw new InvalidOperationException("Profit must be positive.");
            }

            var toy = new Toy
            {
                Name = name,
                Description = description,
                DistributorPrice = distributorPrice,
                Price = distributorPrice + profit,
                BrandId = brandId,
                CategoryId = categoryId
            };

            this._data.Toys.Add(toy);
            this._data.SaveChanges();
        }

        public void BuyFromDistributor(AddingToyServiceModel model)
        {
            this.BuyFromDistributor(model.Name, model.Description, 
                model.Price, model.Profit, model.BrandId, model.CategoryId);
        }

        public bool Exists(int toyId)
        {
            return this._data.Toys.Any(t => t.Id == toyId);
        }

        public void SellToyToUser(int toyId, int userId)
        {
            if (!this.Exists(toyId))
            {
                throw new InvalidOperationException($"Toy with id {toyId} doesn't exists.");
            }

            if (!this._userService.Exists(userId))
            {
                throw new InvalidOperationException($"User with id {userId} doesn't exists.");
            }

            var order = new Order
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Done,
                UserId = userId,
            };

            var toyOrder = new ToyOrder
            {
                ToyId = toyId,
                Order = order,
            };

            this._data.Orders.Add(order);
            this._data.ToyOrders.Add(toyOrder);

            this._data.SaveChanges();
        }
    }
}
