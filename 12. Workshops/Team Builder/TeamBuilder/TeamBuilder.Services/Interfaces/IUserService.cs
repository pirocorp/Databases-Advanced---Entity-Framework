namespace TeamBuilder.Services.Interfaces
{
    using Models;
    using Models.Enums;

    public interface IUserService : IGenericService<User>
    {
        User RegisterUser(string username, string password, string firstName, string lastName, int age, Gender gender);

        User GetUserByUsername(string username);

        User GetUserByCredentials(string username, string password);

        void DeleteUser(User user);
    }
}