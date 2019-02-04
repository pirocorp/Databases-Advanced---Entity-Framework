namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;

    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name)
            => this.By<TModel>(u => u.Name == name).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<Album>(id) != null;

        public bool Exists(string name)
            => this.ByName<Album>(name) != null;

        public Album Create(int userId, string albumTitle, string bgColor, string[] tags)
        {
            var backgroundColor = Enum.Parse<Color>(bgColor, true);

            var album = new Album()
            {
                Name = albumTitle,
                BackgroundColor = backgroundColor,
            };

            this.context.Albums.Add(album);
            this.context.SaveChanges();

            var albumRole = new AlbumRole()
            {
                UserId = userId,
                Album = album
            };

            this.context.AlbumRoles.Add(albumRole);
            this.context.SaveChanges();

            foreach (var tag in tags)
            {
                var currentTagId = this.context.Tags.FirstOrDefault(x => x.Name == tag).Id;

                var albumTag = new AlbumTag()
                {
                    Album = album,
                    TagId = currentTagId
                };

                this.context.AlbumTags.Add(albumTag);
            }

            this.context.SaveChanges();
            return album;
        }

        private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate)
            => this.context.Albums
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}
