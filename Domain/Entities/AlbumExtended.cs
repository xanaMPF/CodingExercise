using System.Collections.Generic;

namespace Domain.Entities
{
    public class AlbumExtended : Album
    {
        public AlbumExtended(Album album, IEnumerable<Photo> photos)
        {
            Id = album.Id;
            Title = album.Title;
            UserId = album.UserId;
            Photos = photos ?? new List<Photo>();
        }

        public IEnumerable<Photo> Photos { get; private set; }
    }
}
