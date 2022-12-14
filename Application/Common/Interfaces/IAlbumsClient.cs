using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExercise.Application.Common.Interfaces
{
    public interface IAlbumsClient
    {
        [Get("/albums")]
        Task<IEnumerable<Domain.Entities.Album>> GetAlbums();

        [Get("/albums")]
        Task<IEnumerable<Domain.Entities.Album>> GetAlbumsByUserId([Query]int userId);

        [Get("/photos")]
        Task<IEnumerable<Domain.Entities.Photo>> GetPhotos();


    }
}
