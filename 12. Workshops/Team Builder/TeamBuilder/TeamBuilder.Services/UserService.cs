namespace TeamBuilder.Services
{
    using System.Linq;
    using AutoMapper;

    using Data;
    using Interfaces;
    using Models;
    using Models.Enums;

    public class UserService : GenericService<User>, IUserService
    {
        public UserService(TeamBuilderContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public User RegisterUser(string username, string password, string firstName, string lastName, int age, Gender gender)
        {
            var newUser = new User()
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender,
            };

            this.Add(newUser);
            return newUser;
        }

        public User GetUserByCredentials(string username, string password)
        {
            var user = this.GetAll(u => u.Username == username && 
                                        u.Password == password &&
                                        u.IsDeleted == false)
                .SingleOrDefault();

            return user;
        }

        public User GetUserByUsername(string username)
        {
            var user = this.GetAll(u => u.Username == username)
                .SingleOrDefault();

            return user;
        }

        public void DeleteUser(User user)
        {
            user.IsDeleted = true;
            this.Update(user);
        }
    }
}