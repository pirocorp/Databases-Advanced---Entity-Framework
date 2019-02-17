namespace TeamBuilder.Services.Interfaces
{
    using Models;

    public interface ISessionService
    {
        void Login(User user);

        void Logout();

        void Authorize();

        bool IsAuthenticated();

        User GetCurrentUser();
    }
}