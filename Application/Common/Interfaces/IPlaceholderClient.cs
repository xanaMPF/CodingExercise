using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExercise.Application.Common.Interfaces
{
    public interface IPlaceholderClient
    {
        [Get("/albums")]
        Task<IEnumerable<Domain.Entities.Album>> GetAlbums();

        [Get("/photos")]
        Task<IEnumerable<Domain.Entities.Photo>> GetPhotos();
    }
}
