namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using Data;
    using User = Data.Models.User;
    using static Data.Models.DataValidation;
    using static Data.Models.DataValidation.User;

    public class UserService : IUserService
    {
        private readonly PetStoreDbContext _data;

        public UserService(PetStoreDbContext data)
        {
            this._data = data;
        }

        public int Register(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name cannot be empty.");
            }

            if (name.Length > NameMaxLength)
            {
                throw new InvalidOperationException($"Brand name cannot be more than ${NameMaxLength} characters.");
            }

            if (email.Length > EmailMaxLength)
            {
                throw new InvalidOperationException($"Email {email} already exists.");
            }

            if (this._data.Users.Any(u => u.Email == email))
            {
                throw new InvalidOperationException($"Email {email} already exists.");
            }

            var user = new User
            {
                Name = name,
                Email = email
            };

            this._data.Users.Add(user);
            this._data.SaveChanges();

            return user.Id;
        }

        public bool Exists(int userId)
        {
            return this._data.Users.Any(u => u.Id == userId);
        }
    }
}
