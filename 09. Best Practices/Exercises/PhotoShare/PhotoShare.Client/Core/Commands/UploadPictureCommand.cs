namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Dtos;
    using Contracts;
    using Models.Enums;
    using Services.Contracts;

    public class UploadPictureCommand : ICommand
    {
        private readonly IPictureService pictureService;
        private readonly IAlbumService albumService;
        private readonly IUserSessionService userSessionService;
        private readonly IAlbumRoleService albumRoleService;

        public UploadPictureCommand(IPictureService pictureService, IAlbumService albumService, IUserSessionService userSessionService, IAlbumRoleService albumRoleService)
        {
            this.pictureService = pictureService;
            this.albumService = albumService;
            this.userSessionService = userSessionService;
            this.albumRoleService = albumRoleService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            var albumName = data[0];
            var pictureTitle = data[1];
            var path = data[2];

            if (!this.userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumExists = this.albumService.Exists(albumName);

            if (!albumExists)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            var albumId = this.albumService.ByName<AlbumDto>(albumName).Id;

            var albumRoleForCurrentUser = this.albumRoleService.GetAlbumRole(albumId, this.userSessionService.User.Id);

            if (albumRoleForCurrentUser.Role != Role.Owner)
            {
                throw new InvalidOperationException("Permission Denied! You are not the owner of the album!");
            }

            var picture = this.pictureService.Create(albumId, pictureTitle, path);

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
