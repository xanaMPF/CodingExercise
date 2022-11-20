using AutoMapper;
using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodingExercise.Application.Album.Queries.GetAlbumsByUserId
{
    public class GetAlbumsByIdQueryHandler : IRequestHandlerWrapper<GetAlbumsByUserIdQuery, IEnumerable<AlbumDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAlbumService _albumService;

        public GetAlbumsByIdQueryHandler(IMapper mapper, IAlbumService albumService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
        }

        public async Task<IEnumerable<AlbumDto>> Handle(GetAlbumsByUserIdQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var albums = await _albumService.GetAllAsync();
            var albumsByUser = albums.Where(album => album.UserId == request.UserId);
            return _mapper.Map<IEnumerable<AlbumDto>>(albumsByUser);
        }
    }
}
