using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Common.Interfaces;
using System.Collections.Generic;

namespace CodingExercise.Application.Album.Queries.GetSettingById
{
    public class GetAlbumByUserIdQuery : IRequestWrapper<IEnumerable<AlbumDto>>
    {
        public int UserId { get; set; }
    }
}
