using CodingExercise.Application.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<IAlbumRecordApiClient, AlbumRecordApiClient>();
            return services;
        }    
    }
}
