using AutoMapper;
using CodingExercise.Application.Common.Mappings;
using Domain.Entities;
using System;

namespace CodingExercise.Application.Album.Dto
{
    public class PhotoDto : IMapFrom <Photo>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
        public Uri ThumbnailUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Photo, PhotoDto>();
        }
    }
}
