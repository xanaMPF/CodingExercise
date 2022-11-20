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
        private readonly IAlbumRecordApiClient _placeholderClient;
        public AlbumService(IAlbumRecordApiClient placeholderClient)
        {
            _placeholderClient = placeholderClient ?? throw new ArgumentNullException(nameof(placeholderClient));
        }

        public async Task<IEnumerable<AlbumExtended>> GetAllAsync()
        {
            List<AlbumExtended> albuns = new List<AlbumExtended>();
            Task<IEnumerable<Album>> albumsTask;
            Task<IEnumerable<Photo>> photosTask;

            albumsTask = _placeholderClient.GetAlbums();
            photosTask = _placeholderClient.GetPhotos();

            Task.WaitAll(albumsTask, photosTask);

            foreach (var album in albumsTask.Result)
            {
                var photosOfAlbum = photosTask.Result?.Where(photo => photo.AlbumId == album.Id);
                var extendedAlbum = new AlbumExtended(album, photosOfAlbum);
                albuns.Add(extendedAlbum);
            }

            return albuns;
        }
    }
}
