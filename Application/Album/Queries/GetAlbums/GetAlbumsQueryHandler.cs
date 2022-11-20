using AutoMapper;
using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodingExercise.Application.Album.Queries.GetAlbums
{
    public class GetAlbumsQueryHandler : IRequestHandlerWrapper<GetAlbumsQuery, IEnumerable<AlbumDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAlbumService _albumService;

        public GetAlbumsQueryHandler(IMapper mapper, IAlbumService albumService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
        }

        public async Task<IEnumerable<AlbumDto>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
        {   
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var albuns = await _albumService.GetAllAsync();

            return _mapper.Map<IEnumerable<AlbumDto>>(albuns);
        }
    }
}
