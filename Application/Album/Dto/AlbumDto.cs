﻿using AutoMapper;
using CodingExercise.Application.Common.Mappings;
using System.Collections.Generic;

namespace CodingExercise.Application.Album.Dto
{
    public class AlbumDto : IMapFrom<Domain.Entities.AlbumExtended>
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<PhotoDto> Photos { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.AlbumExtended, AlbumDto>();
        }
    }
}
