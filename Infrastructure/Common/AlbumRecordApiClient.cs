using CodingExercise.Application.Common.Exceptions;
using CodingExercise.Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class AlbumRecordApiClient : IAlbumRecordApiClient
    {
        private readonly IAlbumsClient _albumsClient;
        private readonly ILogger<AlbumRecordApiClient> _logger;

        public AlbumRecordApiClient(IAlbumsClient albumsClient, ILogger<AlbumRecordApiClient> logger)
        {
            _albumsClient = albumsClient ?? throw new ArgumentNullException(nameof(albumsClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            try
            {
                return await _albumsClient.GetAlbums();
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get Albums from source", e);
                throw new AlbumRecordsException();
            }
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            try
            {
                return await _albumsClient.GetPhotos();
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to get Photos from source", e);
                throw new PhotoRecordsException();
            }
        }
    }
}
