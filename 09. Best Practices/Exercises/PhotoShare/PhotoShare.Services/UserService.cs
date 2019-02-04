namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using Contracts;
    using Data;
    using Models;

    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;
        private readonly IUserSessionService userSessionService;

        public UserService(PhotoShareContext context, IUserSessionService userSessionService)
	    {
	        this.context = context;
	        this.userSessionService = userSessionService;
	    }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

	    public TModel ByUsername<TModel>(string username)
	        => this.By<TModel>(u => u.Username == username).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<User>(id) != null;

        public bool Exists(string name)
            => this.ByUsername<User>(name) != null;

        public void ChangePassword(int userId, string password)
        {
            var user = this.ById<User>(userId);
            user.Password = password;
            this.context.SaveChanges();
        }

        public void Delete(string username)
        {
            var user = this.context.Users.Single(u => u.Username == username);
            user.IsDeleted = true;
            this.context.SaveChanges();
        }

        public User Register(string username, string password, string email)
	    {
	        var user = new User()
	        {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false
	        };

	        this.context.Add(user);
	        this.context.SaveChanges();
	        return user;
	    }

        public User Login(string username, string password)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Username == username &&
                                                              u.Password == password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            this.userSessionService.User = user;
            user.LastTimeLoggedIn = DateTime.Now;
            this.context.SaveChanges();
            return user;
        }

        public string Logout()
        {
            var username = this.userSessionService.User.Username;
            this.userSessionService.User = null;

            return username;
        }

        public Friendship AddFriend(int userId, int friendId)
	    {
	        var friendship = new Friendship()
	        {
                UserId = userId,
                FriendId = friendId
	        };

	        this.context.Friendships.Add(friendship);
	        this.context.SaveChanges();
	        return friendship;
	    }

	    public Friendship AcceptFriend(int userId, int friendId)
	    {
	        var friendship = new Friendship()
	        {
	            UserId = userId,
	            FriendId = friendId
	        };

	        this.context.Friendships.Add(friendship);
	        this.context.SaveChanges();
	        return friendship;
        }

	    public void SetBornTown(int userId, int townId)
	    {
	        var user = this.ById<User>(userId);
	        user.BornTownId = townId;
	        this.context.SaveChanges();
	    }

	    public void SetCurrentTown(int userId, int townId)
	    {
	        var user = this.ById<User>(userId);
	        user.CurrentTownId = townId;
	        this.context.SaveChanges();
        }

	    private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
	        => this.context.Users
	            .Where(predicate)
	            .AsQueryable()
	            .ProjectTo<TModel>();
    }
}