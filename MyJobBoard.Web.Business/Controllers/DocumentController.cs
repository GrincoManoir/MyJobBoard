using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyJobBoard.Application;
using MyJobBoard.Application.Features.Documents.Commands;
using MyJobBoard.Application.Features.Documents.Queries;
using MyJobBoard.Application.Features.Documents.Queries.MyJobBoard.Application.Features.Documents.Queries;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using MyJobBoard.Web.Business.DTO;
using MyJobBoard.Web.Business.DTO.Inputs;
using MyJobBoard.Web.Common;
using MyJobBoard.Web.Common.Dto;
using System.Net;
using System.Security.Claims;

/// <summary>
/// 
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;
    /// <summary>
    /// API pour intéragir avec la ressource Document
    /// </summary>
    /// <param name="mediator"></param>
    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpGet()]
    /// <summary>
    /// Récupérer l'ensemble des documents, avec la possibilité de filtrer
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet()]
    [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(PartialResponse<IEnumerable<DocumentDto>>))]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<IEnumerable<DocumentDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NoContent, Type = typeof(Response<IEnumerable<DocumentDto>>))]
    public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments([FromQuery] GetDocumentsQueryInputs query)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("GetDocuments requires an auth user"); ;
        
        RequestResult<List<Document>> result = await _mediator.Send(new GetDocumentsQuery(userId, query.Type, query.StartDate, query.Range, query.Sort, query.Desc));
        return result.MapRequestResult(successDto: result.Data?.Select(d => new DocumentDto(d)));
    }

    /// <summary>
    /// Upload un document
    /// </summary>
    /// <param name="file"></param>
    /// <param name="documentType"></param>
    /// <param name="customName"></param>
    /// <returns></returns>
    [HttpPost("upload")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UploadDocumentDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Upload([FromForm] IFormFile file, [FromForm] EDocumentType documentType, [FromForm] string? customName = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Upload requires an auth user");

        var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var command = new UploadDocumentCommand
        {
            FileContent = memoryStream.ToArray(),
            FileContentType = file.ContentType,
            Name = customName ?? file.FileName,
            UserId = userId,
            DocumentType = documentType
        };

        RequestResult<Guid> result = await _mediator.Send(command);
        return result.MapRequestResult(successDto: new { DocumentId = result.Data });
    }

    /// <summary>
    /// Download un document
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    [HttpGet("{id}/download")]
    public async Task<ActionResult> Download([FromRoute]Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Download requires an auth user");
        var query = new GetDocumentByIdQuery(userId, id);

        RequestResult<Document?> result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            var document = result.Data;
            return File(document!.Content, document.ContentType, document.Name);
        }
        
        return result!.MapRequestResult<Document,DocumentDto?>(null);

        
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Download requires an auth user");
        var query = new DeleteDocumentCommand()
        {
            DocumentId = id
        };

        await _mediator.Send(query);

        return NoContent();
    }
}
