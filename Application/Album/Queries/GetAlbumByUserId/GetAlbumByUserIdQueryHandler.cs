using AutoMapper;
using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Album.Queries.GetSettingById;
using CodingExercise.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodingExercise.Application.Album.Queries.GetAlbumByUserId
{
    public class GetSettingByIdQueryHandler : IRequestHandlerWrapper<GetAlbumByUserIdQuery, IEnumerable<AlbumDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAlbumService _albumService;

        public GetSettingByIdQueryHandler(IMapper mapper, IAlbumService albumService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
        }

        public async Task<IEnumerable<AlbumDto>> Handle(GetAlbumByUserIdQuery request, CancellationToken cancellationToken)
        {
            var albuns = await _albumService.GetAllAsync();
            var albunsByUser = albuns.Where(albuns => albuns.UserId == request.UserId);
            return _mapper.Map<IEnumerable<AlbumDto>>(albunsByUser);
        }
    }
}
