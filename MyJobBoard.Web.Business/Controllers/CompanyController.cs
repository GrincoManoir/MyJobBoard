using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyJobBoard.Application;
using MyJobBoard.Application.Features.Documents.Commands;
using MyJobBoard.Application.Features.Documents.Queries.MyJobBoard.Application.Features.Companies.Queries;
using MyJobBoard.Application.Features.Companies.Commands;
using MyJobBoard.Application.Features.Companies.Queries;
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
    public class CompaniesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Récupérer l'ensembles des companies d'un utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(PartialResponse<IEnumerable<CompanyDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<IEnumerable<CompanyDto>>))]
        public async Task<ActionResult> GetCompanies([FromQuery] GetCompaniesQueryInputs query)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("GetCompanies requires an auth user"); ;

            RequestResult<List<Company>> result = await _mediator.Send(new GetCompaniesQuery(userId, query.Range, query.Sort, query.Desc));
            return result.MapRequestResult(successDto: result.Data?.Select(o => new CompanyDto(o)));
        }

        /// <summary>
        /// Création d'une ressource company
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NoContent, Type = typeof(Response<IEnumerable<CompanyDto>>))]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(PartialResponse<IEnumerable<CompanyDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
        public async Task<ActionResult> CreateCompany([FromBody] CreateCompanyInput query)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("GetCompanies requires an auth user"); ;

            RequestResult<Company> result = await _mediator.Send(new CreateCompanyCommand(
                userId,
                query.Name,
                query.Websites,
                query.SocialNetworks
                ));
            return result.MapRequestResult(successDto: result.Data == null ? null : new CompanyDto(result.Data));
        }

        /// <summary>
        /// Récupérer une company par son identifiant
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<CompanyDto>))]
        public async Task<ActionResult> GetCompany([FromRoute] Guid id)
        {
            RequestResult<Company?> result = await _mediator.Send(new GetCompanyByIdQuery(id));
            return result.MapRequestResult(successDto: result.Data == null ? null : new CompanyDto(result.Data!));
        }

        /// <summary>
        /// Supprimer une company
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteCompany([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteCompanyCommand(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }
           
           return NoContent();
        }

        /// <summary>
        /// mise à jour d'une company
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
        public async Task<ActionResult> UpdateCompany([FromRoute] Guid id, [FromBody] UpdateCompanyInput input)
        {
            RequestResult<Company> result;
            try
            {
                result = await _mediator.Send(input.ToUpdateCompanyCommand(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse(e.Message));
            }
           
            return result.MapRequestResult(successDto: result.Data != null ? new CompanyDto(result.Data) : null);
        }
    }

    
}

