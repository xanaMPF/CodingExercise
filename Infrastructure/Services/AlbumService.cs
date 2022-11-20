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
        private readonly IPlaceholderClient _placeholderClient;
        public AlbumService(IPlaceholderClient placeholderClient)
        {
            _placeholderClient = placeholderClient ?? throw new ArgumentNullException(nameof(placeholderClient));
        }

        public async Task<IEnumerable<AlbumExtended>> GetAllAsync()
        {
            List<AlbumExtended> albuns = new List<AlbumExtended>();
            var albunsTask = _placeholderClient.GetAlbums();
            var photosTask = _placeholderClient.GetPhotos();

            Task.WaitAll(albunsTask, photosTask);

            foreach(var album in albunsTask.Result)
            {
                var photosOfAlbum = photosTask.Result.Where(photo => photo.AlbumId == album.Id);
                var extendedAlbum = new AlbumExtended(album, photosOfAlbum);
                albuns.Add(extendedAlbum);
            }

            return albuns;
        }
    }
}
