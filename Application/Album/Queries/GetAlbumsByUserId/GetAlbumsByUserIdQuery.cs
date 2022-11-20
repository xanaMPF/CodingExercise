using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Common.Interfaces;
using System.Collections.Generic;

namespace CodingExercise.Application.Album.Queries.GetAlbumsByUserId
{
    public class GetAlbumsByUserIdQuery : IRequestWrapper<IEnumerable<AlbumDto>>
    {
        public GetAlbumsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; private set; }
    }
}
