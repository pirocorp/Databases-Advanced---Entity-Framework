namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Models.Enums;
    using Services.Contracts;
    using Utilities;


    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;
        private readonly IUserSessionService userSessionService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService, IUserSessionService userSessionService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
            this.userSessionService = userSessionService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            var username = data[0];
            var albumTitle = data[1];
            var colorName = data[2];
            var tags = data.Skip(3).ToArray();

            if (!this.userSessionService.IsLoggedIn() || 
                this.userSessionService.User.Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumExists = this.albumService.Exists(albumTitle);

            if (albumExists)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            var colorIsValid = Enum.TryParse<Color>(colorName, out var color);

            if (!colorIsValid)
            {
                throw new ArgumentException($"Color {colorName} not found!");
            }

            for (var i = 0; i < tags.Length; i++)
            {
                var tag = tags[i] = tags[i].ValidateOrTransform();
                var tagExists = this.tagService.Exists(tag);

                if (!tagExists)
                {
                    throw new ArgumentException($"Invalid tags!");
                }
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;
            this.albumService.Create(userId, albumTitle, colorName, tags);
            return $"Album {albumTitle} successfully created!";
        }
    }
}
