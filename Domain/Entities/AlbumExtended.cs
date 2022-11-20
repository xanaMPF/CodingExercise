using System.Collections.Generic;

namespace Domain.Entities
{
    public class AlbumExtended : Album
    {
        public AlbumExtended()
        { }

        public AlbumExtended(Album album, IEnumerable<Photo> photos)
        {
            Id = album.Id;
            Title = album.Title;
            UserId = album.UserId;
            Photos = photos;
        }

        public IEnumerable<Photo> Photos { get; private set; }
    }
}
