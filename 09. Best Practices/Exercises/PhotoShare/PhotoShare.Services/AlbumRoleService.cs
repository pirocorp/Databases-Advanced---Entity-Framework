namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Models;
    using Models.Enums;
    using Data;
    using Contracts;

    public class AlbumRoleService : IAlbumRoleService
    {
        private readonly PhotoShareContext context;

        public AlbumRoleService(PhotoShareContext context)
        {
            this.context = context;
        }

        public AlbumRole PublishAlbumRole(int albumId, int userId, string role)
        {
            var roleAsEnum = Enum.Parse<Role>(role);

            var albumRole = new AlbumRole()
            {
                AlbumId = albumId,
                UserId = userId,
                Role = roleAsEnum
            };

            this.context.AlbumRoles.Add(albumRole);
            this.context.SaveChanges();
            return albumRole;
        }

        public AlbumRole GetAlbumRole(int albumId, int userId)
        {
            var albumRole = this.context.AlbumRoles.FirstOrDefault(ar => ar.AlbumId == albumId && ar.UserId == userId);

            return albumRole;
        }
    }
}
