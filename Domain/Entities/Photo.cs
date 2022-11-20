using System;

namespace Domain.Entities
{
    public class Photo
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri? Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
