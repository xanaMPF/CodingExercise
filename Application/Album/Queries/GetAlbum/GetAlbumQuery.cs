using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Common.Interfaces;
using System.Collections.Generic;

namespace CodingExercise.Application.Album.Queries.GetAlbum
{
    public class GetAlbumQuery : IRequestWrapper<IEnumerable<AlbumDto>>
    {
    }
}
