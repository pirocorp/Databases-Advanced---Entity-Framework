namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Dtos;
    using Models.Enums;
    using Services.Contracts;

    public class ShareAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly IAlbumRoleService albumRoleService;
        private readonly IUserSessionService userSessionService;

        public ShareAlbumCommand(IAlbumService albumService, IUserService userService, IAlbumRoleService albumRoleService, IUserSessionService userSessionService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.albumRoleService = albumRoleService;
            this.userSessionService = userSessionService;
        }
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            var albumId = int.Parse(data[0]);
            var username = data[1];
            var role = data[2];
            var parsed = Enum.TryParse<Role>(role, out var permission);
            
            if (!this.userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var currentUserRole = this.albumRoleService.GetAlbumRole(albumId, this.userSessionService.User.Id);

            if (currentUserRole.Role != Role.Owner)
            {
                throw new InvalidOperationException("Permission Denied! You are not the owner of the album!");
            }

            if (!parsed)
            {
                throw new ArgumentException($"Permission must be either “Owner” or “Viewer”!");
            }

            var userExists = this.userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var albumExists = this.albumService.Exists(albumId);

            if (!albumExists)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;
            var albumName = this.albumService.ById<AlbumDto>(albumId).Name;
            this.albumRoleService.PublishAlbumRole(albumId, userId, role);
            return $"Username {username} added to album {albumName} ({permission.ToString()})";
        }
    }
}
