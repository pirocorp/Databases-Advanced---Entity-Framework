namespace MusicHub.DataProcessor.ExportDtos
{
    public class AlbumsByProducerExportDto
    {
        public string AlbumName { get; set; }

        public string ReleaseDate { get; set; }

        public string ProducerName { get; set; }

        public SongAlbumByProducerExportDto[] Songs { get; set; }

        public string AlbumPrice { get; set; }
    }
}
