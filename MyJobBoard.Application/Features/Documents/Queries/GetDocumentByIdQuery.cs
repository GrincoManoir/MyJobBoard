using MediatR;
using MyJobBoard.Application.Features.Documents.Queries.MyJobBoard.Application.Features.Documents.Queries;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Documents.Queries
{
    public class GetDocumentByIdQuery : IRequest<RequestResult<Document?>>
    {
        public string UserId { get; }
        public Guid DocumentId { get; }

        public GetDocumentByIdQuery(string userId, Guid documentId)
        {
            UserId = userId;
            DocumentId = documentId;
        }


        public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, RequestResult<Document?>>
        {

            private readonly IDocumentService _documentService;
            public GetDocumentByIdQueryHandler(IDocumentService documentService)
            {
                _documentService = documentService;
            }

            public async Task<RequestResult<Document?>> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
            {
                Document? document;
                try
                {
                    document = await _documentService.GetDocumentById(request.DocumentId);
                }
                catch (Exception ex)
                {
                    return RequestResult<Document?>.Failure(ex.Message);
                }
                if (document == null)
                {
                    return RequestResult<Document?>.Failure("Document not found",ApplicationResult.NOT_FOUND);
                }
                return RequestResult<Document?>.Success(document, ApplicationResult.OK);
            }
                


        }
    }
}
