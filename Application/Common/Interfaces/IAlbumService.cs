using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExercise.Application.Common.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumExtended>> GetAllAsync();

        Task<IEnumerable<AlbumExtended>> GetByUserAsync(int userId);
    }
}
