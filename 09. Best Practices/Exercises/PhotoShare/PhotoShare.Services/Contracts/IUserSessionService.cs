namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IUserSessionService
    {
        User User { get; set; }

        bool IsLoggedIn();
    }
}