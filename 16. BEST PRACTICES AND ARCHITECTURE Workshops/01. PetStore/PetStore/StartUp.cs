namespace PetStore
{
    using Data;
    using Services.Implementations;

    public static class StartUp
    {
        public static void Main()
        {
            using var data = new PetStoreDbContext();

            //var brandService = new BrandService(data);

            //var brandWithToys = brandService.FindByIdWithToys(1);

            var userService = new UserService(data);
            var foodService = new FoodService(data, userService);

            
        }
    }
}
