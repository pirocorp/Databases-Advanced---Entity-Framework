namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;

    public class PetService : IPetService
    {
        private readonly PetStoreDbContext _data;
        private readonly IBreedService _breedService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public PetService(PetStoreDbContext data, 
            IBreedService breedService, ICategoryService categoryService, 
            IUserService userService)
        {
            this._data = data;
            this._breedService = breedService;
            this._categoryService = categoryService;
            this._userService = userService;
        }

        public void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, 
            string description, int breedId, int categoryId)
        {
            if (price < 0)
            {
                throw new InvalidOperationException("Price cannot be negative");
            }

            if (!this._breedService.Exists(breedId))
            {
                throw new InvalidOperationException("Breed not found.");
            }

            if (!this._categoryService.Exists(categoryId))
            {
                throw new InvalidOperationException("Category not found.");
            }

            var pet = new Pet
            {
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Price = price,
                Description = description,
                BreedId = breedId,
                CategoryId = categoryId,
            };

            this._data.Pets.Add(pet);
            this._data.SaveChanges();
        }

        public bool Exists(int petId)
        {
            return this._data.Pets
                .Any(p => p.Id == petId);
        }

        public void SellPet(int petId, int userId)
        {
            if (!this.Exists(petId))
            {
                throw new InvalidOperationException("Pet not found");
            }

            if (!this._userService.Exists(userId))
            {
                throw new InvalidOperationException("User not found");
            }

            var pet = this._data.Pets
                .First(p => p.Id == petId);

            if (pet.OrderId != null)
            {
                throw new InvalidOperationException("Pet is already sold");
            }

            var order = new Order()
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Done,
                UserId = userId
            };

            pet.Order = order;

            this._data.Orders.Add(order);

            this._data.SaveChanges();
        }
    }
}
