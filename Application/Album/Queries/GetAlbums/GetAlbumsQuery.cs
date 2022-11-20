using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Common.Interfaces;
using System.Collections.Generic;

namespace CodingExercise.Application.Album.Queries.GetAlbums
{
    public class GetAlbumsQuery : IRequestWrapper<IEnumerable<AlbumDto>>
    {
    }
}
