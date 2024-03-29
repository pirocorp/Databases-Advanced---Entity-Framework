﻿namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IAlbumRoleService
    {
        AlbumRole PublishAlbumRole(int albumId, int userId, string role);

        AlbumRole GetAlbumRole(int albumId, int userId);
    }
}
