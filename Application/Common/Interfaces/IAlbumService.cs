using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExercise.Application.Common.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<Domain.Entities.AlbumExtended>> GetAllAsync();
    }
}
