using CodingExercise.Application.Common.Exceptions;
using CodingExercise.Application.Common.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodingExerciseTests.Services.AlbumServiceTest
{
    public class AlbumServiceTest
    {
        public static Album album_1 = new Album { Id = 1, Title = "Title1", UserId = 10 };
        public static Album album_2 = new Album { Id = 2, Title = "Title2", UserId = 20 };
        public static Photo photo_100_album_1 = new Photo { AlbumId = 1, Id = 100, Title = "Photo100"};
        public static Photo photo_200_album_2 = new Photo { AlbumId = 2, Id = 200, Title = "Photo200"};
        public static Photo photo_201_album_2 = new Photo { AlbumId = 2, Id = 201, Title = "Photo201"};

        public static AlbumExtended extended_album1_1Photo = new AlbumExtended (album_1, new List<Photo> { photo_100_album_1 });
        public static AlbumExtended extended_album2_2Photos = new AlbumExtended(album_2, new List<Photo> { photo_200_album_2, photo_201_album_2 });
        public static AlbumExtended extended_album1_noPhotos = new AlbumExtended(album_1, new List<Photo> { });

        public static IEnumerable<object[]> AlbunsPhotosValidTestData () => new List<object[]>
        {
            new object[] { 
                new List<Album> { album_1 },
                new List<Photo> { photo_100_album_1},
                new List<AlbumExtended> { extended_album1_1Photo }
            },
            new object[] {
                new List<Album> { album_2 },
                new List<Photo> { photo_200_album_2, photo_201_album_2},
                new List<AlbumExtended> { extended_album2_2Photos }
            },
        };

        public static IEnumerable<object[]> AlbunsNoPhotosValidTestData() => new List<object[]>
        {
            new object[] {
                new List<Album> { album_1 },
                null,
                new List<AlbumExtended> { extended_album1_noPhotos }
            },
            new object[] {
                new List<Album> { album_1 },
                new List<Photo> { },
                new List<AlbumExtended> { extended_album1_noPhotos }
            },
           new object[] {
                new List<Album> { album_1 },
                new List<Photo> { photo_200_album_2, photo_201_album_2},
                new List<AlbumExtended> { extended_album1_noPhotos }
            },
        };

        [Fact(DisplayName = "GetAllAsync Returns empty list if Album/Photo APIs return empty lists")]
        public async Task GetAllAsync_EmptyResultFromApi_ReturnsEmptyResults()
        {
            var partyServiceMock = new Mock<IAlbumRecordApiClient>();
            partyServiceMock.Setup(m => m.GetAlbums()).ReturnsAsync(() => new List<Album>());
            partyServiceMock.Setup(m => m.GetPhotos()).ReturnsAsync(() => new List<Photo>());

            var albumService = new AlbumService(partyServiceMock.Object);
            var result = await albumService.GetAllAsync();
            var expected = new List<AlbumExtended>();
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "GetAllAsync Throws dedicated exception if Album API fails with exception")]
        public async Task GetAllAsync_AlbunsApiFails_ThrowsException()
        {
            var partyServiceMock = new Mock<IAlbumRecordApiClient>();
            partyServiceMock.Setup(m => m.GetAlbums()).Throws(() => new AlbumRecordsException());
            partyServiceMock.Setup(m => m.GetPhotos()).ReturnsAsync(() => new List<Photo>());

            var albumService = new AlbumService(partyServiceMock.Object);
            await Assert.ThrowsAsync<AlbumRecordsException>(() => albumService.GetAllAsync());
        }

        [Fact(DisplayName = "GetAllAsync Throws dedicated exception if Photo API fails with exception")]
        public async Task GetAllAsync_PhotosApiFails_ThrowsException()
        {
            var partyServiceMock = new Mock<IAlbumRecordApiClient>();
            partyServiceMock.Setup(m => m.GetAlbums()).ReturnsAsync(() => new List<Album>());
            partyServiceMock.Setup(m => m.GetPhotos()).Throws(() => new PhotoRecordsException());

            var albumService = new AlbumService(partyServiceMock.Object);
            await Assert.ThrowsAsync<PhotoRecordsException>(() => albumService.GetAllAsync());
        }

        [Theory(DisplayName = "GetAllAsync Returns valid objects if Album/Photo Api returns valid objects ")]
        [MemberData(nameof(AlbunsPhotosValidTestData))]
        public async Task GetAllAsync_ExpectedAlbunsPhotos_ReturnsValidAlbuns(List<Album> albums, List<Photo> photos, List<AlbumExtended> albumsExtended)
        {
            var partyServiceMock = new Mock<IAlbumRecordApiClient>();
            partyServiceMock.Setup(m => m.GetAlbums()).ReturnsAsync(() => albums);
            partyServiceMock.Setup(m => m.GetPhotos()).ReturnsAsync(() => photos);

            var albumService = new AlbumService(partyServiceMock.Object);
            var result = await albumService.GetAllAsync();
            albumsExtended.Should().BeEquivalentTo(result);
        }

        [Theory(DisplayName = "GetAllAsync Returns albuns with empty photo list with Photo Api doesnt return photos for the albuns")]
        [MemberData(nameof(AlbunsNoPhotosValidTestData))]
        public async Task GetAllAsync_ExpectedAlbunsWithNoPhotos_ReturnsValidAlbunsWithNoPhotos(List<Album> albums, List<Photo> photos, List<AlbumExtended> albumsExtended)
        {
            var partyServiceMock = new Mock<IAlbumRecordApiClient>();
            partyServiceMock.Setup(m => m.GetAlbums()).ReturnsAsync(() => albums);
            partyServiceMock.Setup(m => m.GetPhotos()).ReturnsAsync(() => photos);

            var albumService = new AlbumService(partyServiceMock.Object);
            var result = await albumService.GetAllAsync();
            albumsExtended.Should().BeEquivalentTo(result);
        }

    }
}
