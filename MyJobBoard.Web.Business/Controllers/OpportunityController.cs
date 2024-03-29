﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyJobBoard.Application;
using MyJobBoard.Application.Features.Documents.Commands;
using MyJobBoard.Application.Features.Documents.Queries.MyJobBoard.Application.Features.Opportunities.Queries;
using MyJobBoard.Application.Features.Opportunities.Commands;
using MyJobBoard.Application.Features.Opportunities.Queries;
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
    public class OpportunitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OpportunitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Récupérer l'ensembles des opportunités d'un utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(PartialResponse<IEnumerable<OpportunityDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<IEnumerable<OpportunityDto>>))]
        public async Task<ActionResult> GetOpportunities([FromQuery] GetOpportunitiesQueryInputs query)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("GetOpportunities requires an auth user"); ;

            RequestResult<List<Opportunity>> result = await _mediator.Send(new GetOpportunitiesQuery(userId, query.State, query.StartDate, query.Range, query.Sort, query.Desc));
            return result.MapRequestResult(successDto: result.Data?.Select(o => new OpportunityDto(o)));
        }

        /// <summary>
        /// Création d'une ressource opportunité
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NoContent, Type = typeof(Response<IEnumerable<OpportunityDto>>))]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(PartialResponse<IEnumerable<OpportunityDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OpportunityDto))]
        public async Task<ActionResult> CreateOpportunity([FromBody] CreateOpportunityInput query)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("GetOpportunities requires an auth user"); ;

            RequestResult<Opportunity> result = await _mediator.Send(new CreateOpportunityCommand(
                userId,
                query.CompanyId,
                query.Location,
                query.RoleTitle,
                query.RemoteCondition,
                query.Industry,
                query.IndicativeSalaryRange == null ? null : new IndicativeSalaryRange()
                {
                    MinSalary = query.IndicativeSalaryRange.MinSalary,
                    MaxSalary = query.IndicativeSalaryRange.MaxSalary,
                    Periodicity = query.IndicativeSalaryRange.Periodicity
                },
                query.FreeNotes,
                query.InterlocutorId
                ));
            return result.MapRequestResult(successDto: result.Data == null ? null : new OpportunityDto(result.Data));
        }

        /// <summary>
        /// Récupérer une opportunité par son identifiant
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<OpportunityDto>))]
        public async Task<ActionResult> GetOpportunity([FromRoute] Guid id)
        {
            RequestResult<Opportunity?> result = await _mediator.Send(new GetOpportunityByIdQuery(id));
            return result.MapRequestResult(successDto: new OpportunityDto(result.Data!));
        }

        /// <summary>
        /// Supprimer une opportunité
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteOpportunity([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteOpportunityCommand(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }

            return NoContent();
        }

        /// <summary>
        /// mise à jour d'une opportunité
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OpportunityDto))]
        public async Task<ActionResult> UpdateOpportunity([FromRoute] Guid id, [FromBody] UpdateOpportunityInput input)
        {
            RequestResult<Opportunity> result;
            try
            {
                result = await _mediator.Send(input.toBusinessCommand(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }

            return result.MapRequestResult(successDto: result.Data != null ? new OpportunityDto(result.Data) : null);
        }


        /// <summary>
        /// Ajoute un interlocutor à une opportunité
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/interlocutors/{interlocutorId}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OpportunityDto))]
        public async Task<ActionResult> AddInterlocutor([FromRoute] Guid id, [FromRoute] Guid interlocutorId)
        {
            RequestResult<Opportunity> result;
            try
            {
                result = await _mediator.Send(new OpportunityAddInterlocutorCommand(id, interlocutorId));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }

            return result.MapRequestResult(successDto: result.Data != null ? new OpportunityDto(result.Data) : null);
        }

        /// <summary>
        /// Ajoute un interlocutor à une opportunité
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}/interlocutors/{interlocutorId}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OpportunityDto))]
        public async Task<ActionResult> RemoveInterlocutor([FromRoute] Guid id, [FromRoute] Guid interlocutorId)
        {
            RequestResult<Opportunity> result;
            try
            {
                result = await _mediator.Send(new OpportunityRemoveInterlocutorCommand(id, interlocutorId));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }

            return result.MapRequestResult(successDto: result.Data != null ? new OpportunityDto(result.Data) : null);
        }


        /// <summary>
        /// Ajoute un step à une opportunité
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/steps")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OpportunityDto))]
        public async Task<ActionResult> CreateStep([FromRoute] Guid id, [FromBody] AddOpportunityStepInputs input)
        {
            RequestResult<Opportunity> result = await _mediator.Send(input.ToBusinessCommand(id));
            return result.MapRequestResult(successDto: result.Data != null ? new OpportunityDto(result.Data) : null);
        }

        /// <summary>
        /// Met à jour un step dans une opportunité
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/steps/{stepId}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OpportunityDto))]
        public async Task<ActionResult> UpdateStep([FromRoute] Guid id, [FromRoute] Guid stepId, [FromBody] UpdateOpportunityStepInputs input)
        {
            RequestResult<Opportunity> result = await _mediator.Send(input.ToBusinessCommand(id,stepId));
            return result.MapRequestResult(successDto: result.Data != null ? new OpportunityDto(result.Data) : null);
        }

        /// <summary>
        /// Supprime un step à une opportunité
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}/steps/{stepId}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OpportunityDto))]
        public async Task<ActionResult> RemoveSTep([FromRoute] Guid id, [FromRoute] Guid stepId)
        {
            RequestResult<Opportunity> result = await _mediator.Send(new OpportunityRemoveOpportunityStepCommand(id, stepId));

            return result.MapRequestResult(successDto: result.Data != null ? new OpportunityDto(result.Data) : null);
        }
    }




}

