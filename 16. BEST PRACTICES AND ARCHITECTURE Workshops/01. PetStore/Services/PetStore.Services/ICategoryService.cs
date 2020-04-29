namespace PetStore.Services
{
    public interface ICategoryService
    {
        int Create(string name, string description);

        bool Exists(int categoryId);
    }
}
