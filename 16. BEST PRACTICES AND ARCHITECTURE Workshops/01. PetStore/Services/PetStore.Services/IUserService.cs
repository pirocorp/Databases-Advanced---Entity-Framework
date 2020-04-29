namespace PetStore.Services
{
    public interface IUserService
    {
        int Register(string name, string email);

        bool Exists(int userId);
    }
}
