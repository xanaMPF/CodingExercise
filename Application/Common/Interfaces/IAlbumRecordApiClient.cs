using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExercise.Application.Common.Interfaces
{
    public interface IAlbumRecordApiClient
    {
        Task<IEnumerable<Domain.Entities.Album>> GetAlbums();

        Task<IEnumerable<Domain.Entities.Photo>> GetPhotos();
    }
}
