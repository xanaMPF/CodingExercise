using AutoMapper;
using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Album.Queries.GetAlbums;
using CodingExercise.Application.Album.Queries.GetAlbumsByUserId;
using CodingExercise.Application.Common.Interfaces;
using CodingExercise.Application.Common.Mappings;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CodingExerciseTests.Queries.GetAlbumsByUserId
{
    public class GetAlbumsByUserIdHandlerTest
    {
        public static Album album_1 = new Album { Id = 1, Title = "Title1", UserId = 10 };
        public static Album album_2 = new Album { Id = 2, Title = "Title2", UserId = 20 };
        public static Photo photo_100_album_1 = new Photo { AlbumId = 1, Id = 100, Title = "Photo100" };
        public static Photo photo_200_album_2 = new Photo { AlbumId = 2, Id = 200, Title = "Photo200" };
        public static Photo photo_201_album_2 = new Photo { AlbumId = 2, Id = 201, Title = "Photo201" };

        public static AlbumExtended extended_album1_1Photo = new AlbumExtended(album_1, new List<Photo> { photo_100_album_1, photo_200_album_2 });
        public static AlbumExtended extended_album2_2Photos = new AlbumExtended(album_2, new List<Photo> { photo_200_album_2, photo_201_album_2 });

        public static AlbumExtended extended_album1_noPhotos = new AlbumExtended(album_1, new List<Photo> { });

        public static PhotoDto photoDto = new PhotoDto { Id = 100, Title = "Photo100" };
        public static PhotoDto photo2Dto = new PhotoDto { Id = 200, Title = "Photo200" };

        public static AlbumDto albumDto_1 = new AlbumDto() { Id = 1, Title = "Title1", UserId = 10, Photos = new List<PhotoDto> { photoDto, photo2Dto } };
        public static AlbumDto albumDto_1_NoPhotos = new AlbumDto() { Id = 1, Title = "Title1", UserId = 10, Photos = new List<PhotoDto> { } };
        public static AlbumDto albumDto_2 = new AlbumDto() { Id = 2, Title = "Title2", UserId = 20, Photos = new List<PhotoDto> { photoDto } };

        public static GetAlbumsByUserIdQuery getAlbumsByUserIdQueryValid = new GetAlbumsByUserIdQuery(10);
        public static GetAlbumsByUserIdQuery getAlbumsByUserIdQueryInValidNegative = new GetAlbumsByUserIdQuery(-2);
        public static GetAlbumsByUserIdQuery getAlbumsByUserIdQueryInValidZero = new GetAlbumsByUserIdQuery(0);

        public static IEnumerable<object[]> AlbunsPhotosUser10ValidTestData() => new List<object[]>
        {
            new object[] {
                new List<AlbumDto> { albumDto_1, },
                new List<AlbumExtended> { extended_album1_1Photo }
            },
            new object[] {
                new List<AlbumDto> { albumDto_1, },
                new List<AlbumExtended> { extended_album1_1Photo, extended_album2_2Photos }
            }
        };

        public static IEnumerable<object[]> AlbunsPhotosNoUser10ValidTestData() => new List<object[]>
        {
            new object[] {
                new List<AlbumDto> { },
                new List<AlbumExtended> { }
            },
            new object[] {
                new List<AlbumDto> {  },
                new List<AlbumExtended> { extended_album2_2Photos }
            },
        };

        [Fact(DisplayName = "Handle Throws ArgumentNullException if no query is passed")]
        public async Task Handle_ThrowsArgumentNullException_WhenNoQueryIsPassed()
        {
            var albumServiceMock = new Mock<IAlbumService>();
            var mapperMock = new Mock<IMapper>();

            var albumService = new GetAlbumsByIdQueryHandler(mapperMock.Object, albumServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => albumService.Handle(null, CancellationToken.None));
        }

        [Theory(DisplayName = "Handle Returns valid objects for specific user if there are Albums for that user")]
        [MemberData(nameof(AlbunsPhotosUser10ValidTestData))]
        public async Task Handle_ReturnsAlbunsForUser_WhenServiceReturnsValidAlbunsForUser(List<AlbumDto> expectedAlbumDtos, List<AlbumExtended> albumExtendeds)
        {
            var albumServiceMock = new Mock<IAlbumService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            albumServiceMock.Setup(m => m.GetAllAsync()).ReturnsAsync(() => albumExtendeds);

            var handler = new GetAlbumsByIdQueryHandler(mapper, albumServiceMock.Object);

            var result = await handler.Handle(getAlbumsByUserIdQueryValid, CancellationToken.None);
            result.Should().BeEquivalentTo(expectedAlbumDtos);
        }

        [Theory(DisplayName = "Handle Returns empty if Album service does not return objects for that user")]
        [MemberData(nameof(AlbunsPhotosNoUser10ValidTestData))]
        public async Task Handle_ReturnsNoAlbunsForUser_WhenServiceDoesNotReturnsValidAlbunsForUser(List<AlbumDto> expectedAlbumDtos, List<AlbumExtended> albumExtendeds)
        {
            var albumServiceMock = new Mock<IAlbumService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            albumServiceMock.Setup(m => m.GetAllAsync()).ReturnsAsync(() => albumExtendeds);

            var handler = new GetAlbumsByIdQueryHandler(mapper, albumServiceMock.Object);

            var result = await handler.Handle(getAlbumsByUserIdQueryValid, CancellationToken.None);
            result.Should().BeEquivalentTo(expectedAlbumDtos);
        }
    }
}
