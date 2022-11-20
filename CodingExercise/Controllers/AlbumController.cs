using CodingExercise.Application.Album.Dto;
using CodingExercise.Application.Album.Queries.GetAlbums;
using CodingExercise.Application.Album.Queries.GetAlbumsByUserId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingExercise.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AlbumController : ApiControllerBase
    {       

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> Get()
        {
            return Ok(await Mediator.Send(new GetAlbumsQuery()));
        }

        [HttpGet("byUser/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetByUserId(int userId)
        {
            return Ok(await Mediator.Send(new GetAlbumsByUserIdQuery(userId)));
        }
    }
}
