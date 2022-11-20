﻿using AutoMapper;
using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodingExercise.Application.Album.Queries.GetAlbum
{
    public class GetAlbumQueryHandler : IRequestHandlerWrapper<GetAlbumQuery, IEnumerable<AlbumDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAlbumService _albumService;

        public GetAlbumQueryHandler(IMapper mapper, IAlbumService albumService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
        }

        public async Task<IEnumerable<AlbumDto>> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
        {   
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var albuns = await _albumService.GetAllAsync();

            return _mapper.Map<IEnumerable<AlbumDto>>(albuns);
        }
    }
}
