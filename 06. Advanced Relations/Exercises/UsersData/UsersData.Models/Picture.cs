namespace UsersData.Models
{
    using System.Collections.Generic;

    public class Picture
    {
        public int PictureId { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Path { get; set; }

        public ICollection<AlbumPicture> Albums { get; set; }
    }
}