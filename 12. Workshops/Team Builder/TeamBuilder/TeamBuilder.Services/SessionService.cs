namespace TeamBuilder.Services
{
    using System;

    using Interfaces;
    using Models;
    using Utils;

    public class SessionService : ISessionService
    {
        private User currentUser;

        public SessionService()
        {
        }

        public void Authorize()
        {
            if (this.currentUser == null)
            {
                throw new InvalidOperationException(Constants.LoginFirst);
            }
        }

        public void Login(User user)
        {
            if (this.currentUser != null)
            {
                throw new InvalidOperationException(Constants.LogoutFirst);
            }

            this.currentUser = user;
        }

        public void Logout()
        {
            if (this.currentUser == null)
            {
                throw new InvalidOperationException(Constants.LoginFirst);
            }

            this.currentUser = null;
        }

        public bool IsAuthenticated()
        {
            return this.currentUser != null;
        }

        public User GetCurrentUser()
        {
            if (this.currentUser == null)
            {
                throw new InvalidOperationException(Constants.LoginFirst);
            }

            return this.currentUser;
        }
    }
}