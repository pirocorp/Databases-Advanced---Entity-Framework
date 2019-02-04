namespace PhotoShare.Services
{
    using Contracts;
    using Models;

    public class UserSessionService : IUserSessionService
    {
        public User User { get; set; }

        public bool IsLoggedIn() => this.User != null;
    }
}