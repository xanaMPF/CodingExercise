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
        public static AlbumExtended extended_album2_1Photos = new AlbumExtended(album_2, new List<Photo> { photo_200_album_2 });

        public static AlbumExtended extended_album1_noPhotos = new AlbumExtended(album_1, new List<Photo> { });

        public static PhotoDto photo100Dto = new PhotoDto { Id = 100, Title = "Photo100" };
        public static PhotoDto photo200Dto = new PhotoDto { Id = 200, Title = "Photo200" };
        public static PhotoDto photo201Dto = new PhotoDto { Id = 201, Title = "Photo201" };

        public static AlbumDto albumDto_1_2Photos = new AlbumDto() { Id = 1, Title = "Title1", UserId = 10, Photos = new List<PhotoDto> { photo100Dto, photo200Dto } };
        public static AlbumDto albumDto_1_0Photos = new AlbumDto() { Id = 1, Title = "Title1", UserId = 10, Photos = new List<PhotoDto> {} };

        public static AlbumDto albumDto_2 = new AlbumDto() { Id = 2, Title = "Title2", UserId = 20, Photos = new List<PhotoDto> { photo200Dto, photo201Dto } };

        public static GetAlbumsByUserIdQuery getAlbumsByUserIdQueryValid = new GetAlbumsByUserIdQuery(10);

        public static IEnumerable<object[]> AlbumsPhotosUser10ValidTestData() => new List<object[]>
        {
            new object[] {
                new List<AlbumDto> { albumDto_1_2Photos, },
                new List<AlbumExtended> { extended_album1_1Photo }
            },
            new object[] {
                new List<AlbumDto> { albumDto_2, albumDto_1_2Photos },
                new List<AlbumExtended> { extended_album2_2Photos, extended_album1_1Photo }
            },
            new object[] {
                new List<AlbumDto> { albumDto_1_0Photos },
                new List<AlbumExtended> { extended_album1_noPhotos }
            }
        };

        public static IEnumerable<object[]> AlbumsPhotosNoUser10ValidTestData() => new List<object[]>
        {
            new object[] {
                new List<AlbumDto> { },
                new List<AlbumExtended> { }
            }
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
        [MemberData(nameof(AlbumsPhotosUser10ValidTestData))]
        public async Task Handle_ReturnsAlbumsForUser_WhenServiceReturnsValidAlbumsForUser(List<AlbumDto> expectedAlbumDtos, List<AlbumExtended> albumExtendeds)
        {
            var albumServiceMock = new Mock<IAlbumService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            albumServiceMock.Setup(m => m.GetByUserAsync(10)).ReturnsAsync(() => albumExtendeds);

            var handler = new GetAlbumsByIdQueryHandler(mapper, albumServiceMock.Object);

            var result = await handler.Handle(getAlbumsByUserIdQueryValid, CancellationToken.None);
            result.Should().BeEquivalentTo(expectedAlbumDtos);
        }

        [Theory(DisplayName = "Handle Returns empty if Album service does not return objects for that user")]
        [MemberData(nameof(AlbumsPhotosNoUser10ValidTestData))]
        public async Task Handle_ReturnsNoAlbumsForUser_WhenServiceDoesNotReturnsValidAlbumsForUser(List<AlbumDto> expectedAlbumDtos, List<AlbumExtended> receivedAlbumExtended)
        {
            var albumServiceMock = new Mock<IAlbumService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            albumServiceMock.Setup(m => m.GetByUserAsync(10)).ReturnsAsync(() => receivedAlbumExtended);

            var handler = new GetAlbumsByIdQueryHandler(mapper, albumServiceMock.Object);

            var result = await handler.Handle(getAlbumsByUserIdQueryValid, CancellationToken.None);
            result.Should().BeEquivalentTo(expectedAlbumDtos);
        }
    }
}
