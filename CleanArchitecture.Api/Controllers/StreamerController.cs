using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]

    public class StreamerController : ControllerBase
    {
        private IMediator _mediator;

        public StreamerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost(Name ="CreateStreamer")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateStreamer([FromBody]CreateStreamerCommand command)
        {
            return await _mediator.Send(command);

        }


        [HttpPut(Name = "UpdateStreamer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateStreamer([FromBody]UpdateStreamerCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }


        [HttpDelete("{id}",Name = "DeleteStreamer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeletedStreamer(int id)
        {
            var command = new DeleteStreamerCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }

    }
}
