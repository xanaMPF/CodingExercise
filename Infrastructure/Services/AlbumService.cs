using CodingExercise.Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRecordApiClient _albumRecordClient;
        public AlbumService(IAlbumRecordApiClient albumRecordClient)
        {
            _albumRecordClient = albumRecordClient ?? throw new ArgumentNullException(nameof(albumRecordClient));
        }

        public async Task<IEnumerable<AlbumExtended>> GetAllAsync()
        {
            Task<IEnumerable<Album>> albumsTask;
            Task<IEnumerable<Photo>> photosTask;

            albumsTask = _albumRecordClient.GetAlbums();
            photosTask = _albumRecordClient.GetPhotos();

            await Task.WhenAll(albumsTask, photosTask);

            return MergePhotosIntoAlbums(albumsTask.Result, photosTask.Result);
        }

        public async Task<IEnumerable<AlbumExtended>> GetByUserAsync(int userId)
        {
            Task<IEnumerable<Album>> albumsTask;
            Task<IEnumerable<Photo>> photosTask;

            albumsTask = _albumRecordClient.GetAlbumsByUserId(userId);
            photosTask = _albumRecordClient.GetPhotos();

            await Task.WhenAll(albumsTask, photosTask);

            return MergePhotosIntoAlbums(albumsTask.Result, photosTask.Result);
        }

        private IEnumerable<AlbumExtended> MergePhotosIntoAlbums(IEnumerable<Album> albums, IEnumerable<Photo> photos)
        {
            List<AlbumExtended> mappedAlbums = new List<AlbumExtended>();

            foreach (var album in albums)
            {
                var photosOfAlbum = photos?.Where(photo => photo.AlbumId == album.Id);
                var extendedAlbum = new AlbumExtended(album, photosOfAlbum);
                mappedAlbums.Add(extendedAlbum);
            }

            return mappedAlbums;
        }
    }
}
