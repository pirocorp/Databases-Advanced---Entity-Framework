namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Dtos;
    using Models.Enums;
    using Services.Contracts;
    using Utilities;

    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly ITagService tagService;
        private readonly IAlbumTagService albumTagService;
        private readonly IUserSessionService userSessionService;
        private readonly IAlbumRoleService albumRoleService;

        public AddTagToCommand(IAlbumService albumService, ITagService tagService, IAlbumTagService albumTagService, 
            IUserSessionService userSessionService, IAlbumRoleService albumRoleService)
        {
            this.albumService = albumService;
            this.tagService = tagService;
            this.albumTagService = albumTagService;
            this.userSessionService = userSessionService;
            this.albumRoleService = albumRoleService;
        }

        //AddTagTo <albumName> <tag>
        public string Execute(string[] args)
        {
            var albumName = args[0];
            var tagName = args[1];

            if (!this.userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumExists = this.albumService.Exists(albumName);
            var tagExists = this.tagService.Exists(tagName.ValidateOrTransform());

            if (!tagExists || !albumExists)
            {
                throw new ArgumentException($"Either tag or album do not exist!");
            }

            var album = this.albumService.ByName<AlbumDto>(albumName);
            var tag = this.tagService.ByName<TagDto>(tagName.ValidateOrTransform());

            var tagId = tag.Id;
            var albumId = album.Id;

            var currentUserAlbumRole = this.albumRoleService.GetAlbumRole(albumId, this.userSessionService.User.Id);

            if (currentUserAlbumRole.Role != Role.Owner)
            {
                throw new InvalidOperationException("Permission Denied! You are not the owner of the album!");
            }

            this.albumTagService.AddTagTo(albumId, tagId);
            return $"Tag {tag.Name} added to {album.Name}!";
        }
    }
}
