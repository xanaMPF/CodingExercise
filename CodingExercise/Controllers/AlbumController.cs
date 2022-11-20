using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Album.Queries.GetAlbum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CodingExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ApiControllerBase
    {

        private readonly ILogger<AlbumController> _logger;

        public AlbumController(ILogger<AlbumController> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }


        [HttpGet]
        public async Task<ActionResult<AlbumDto>> Get([FromQuery] GetAlbumQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
