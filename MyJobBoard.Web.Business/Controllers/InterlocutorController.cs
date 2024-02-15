using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyJobBoard.Application;
using MyJobBoard.Application.Features.Documents.Queries.MyJobBoard.Application.Features.Interlocutors.Queries;
using MyJobBoard.Application.Features.Interlocutors.Commands;
using MyJobBoard.Application.Features.Interlocutors.Queries;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Web.Business.DTO;
using MyJobBoard.Web.Business.DTO.Inputs;
using MyJobBoard.Web.Common;
using MyJobBoard.Web.Common.Dto;
using System.Net;
using System.Security.Claims;

namespace MyJobBoard.Web.Business.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InterlocutorsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Récupérer l'ensembles des interlocutors d'un utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(PartialResponse<IEnumerable<InterlocutorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<IEnumerable<InterlocutorDto>>))]
        public async Task<ActionResult> GetInterlocutors([FromQuery] GetInterlocutorsQueryInputs query)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("GetInterlocutors requires an auth user"); ;

            RequestResult<List<Interlocutor>> result = await _mediator.Send(new GetInterlocutorsQuery(userId, query.Range, query.Sort, query.Desc));
            return result.MapRequestResult(successDto: result.Data?.Select(o => new InterlocutorDto(o)));
        }

        /// <summary>
        /// Création d'une ressource Interlocutor
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NoContent, Type = typeof(Response<IEnumerable<InterlocutorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(PartialResponse<IEnumerable<InterlocutorDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InterlocutorDto))]
        public async Task<ActionResult> CreateInterlocutor([FromBody] CreateInterlocutorInputs query)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("GetInterlocutors requires an auth user"); ;

            RequestResult<Interlocutor> result = await _mediator.Send(query.ToBusinessCommand(userId));
               
            return result.MapRequestResult(successDto: result.Data == null ? null : new InterlocutorDto(result.Data));
        }

        /// <summary>
        /// Récupérer un interlocutor par son identifiant
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<InterlocutorDto>))]
        public async Task<ActionResult> GetInterlocutor([FromRoute] Guid id)
        {
            RequestResult<Interlocutor?> result = await _mediator.Send(new GetInterlocutorByIdQuery(id));
            return result.MapRequestResult(successDto: result.Data == null ? null : new InterlocutorDto(result.Data!));
        }

        /// <summary>
        /// Supprimer un interlocutor
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteInterlocutor([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteInterlocutorCommand(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }
           
           return NoContent();
        }

        /// <summary>
        /// mise à jour d'un interlocutor
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(InterlocutorDto))]
        public async Task<ActionResult> UpdateInterlocutor([FromRoute] Guid id,[FromBody] UpdateInterlocutorInput input)
        {
            RequestResult<Interlocutor> result;
            try
            {
                result = await _mediator.Send(input.ToBusinessCommand(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }
           
            return result.MapRequestResult(successDto: result.Data != null ? new InterlocutorDto(result.Data) : null);
        }
    }

    
}

