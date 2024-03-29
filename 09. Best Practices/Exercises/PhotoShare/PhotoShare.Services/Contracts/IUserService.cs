﻿namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IUserService
    {
        TModel ById<TModel>(int id);

        TModel ByUsername<TModel>(string username);

        bool Exists(int id);

        bool Exists(string name);

        User Register(string username, string password, string email);

        User Login(string username, string password);

        string Logout();

        void Delete(string username);

        Friendship AddFriend(int userId, int friendId);

        Friendship AcceptFriend(int userId, int friendId);

        void ChangePassword(int userId, string password);

        void SetBornTown(int userId, int townId);

        void SetCurrentTown(int userId, int townId);
    }
}
