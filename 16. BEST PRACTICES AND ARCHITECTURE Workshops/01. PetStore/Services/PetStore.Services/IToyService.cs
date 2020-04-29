namespace PetStore.Services
{
    using Models.Toy;

    public interface IToyService
    {
        void BuyFromDistributor(string name, string description,
            decimal distributorPrice, decimal profit, int brandId,
            int categoryId);

        void BuyFromDistributor(AddingToyServiceModel model);

        bool Exists(int toyId);

        void SellToyToUser(int toyId, int userId);
    }
}
