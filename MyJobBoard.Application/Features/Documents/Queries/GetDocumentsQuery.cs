using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Documents.Queries
{
    using global::MyJobBoard.Application.Interfaces;
    using global::MyJobBoard.Domain.Entities;
    using global::MyJobBoard.Domain.Enums;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;

    namespace MyJobBoard.Application.Features.Documents.Queries
    {
        public class GetDocumentsQuery : IRequest<RequestResult<List<Document>>>
        {
            public string UserId { get; }
            public EDocumentType? Type { get; set; }
            public DateTime? StartDate { get; set; }
            public string? Range { get; set; }
            public string? Sort { get; set; }
            public bool? Desc {get; set; }

            public GetDocumentsQuery(string userId, EDocumentType? type,DateTime? startDate, string? range, string? sort, bool? desc)
            {
                UserId = userId;
                Type= type;
                StartDate = startDate;
                Range = range;
                Sort = sort;
                Desc = desc;
            }
        }




        public class GetDocumentsByUserIdQueryHandler : IRequestHandler<GetDocumentsQuery, RequestResult<List<Document>>>
        {

            private readonly IDocumentService _documentService;
            public GetDocumentsByUserIdQueryHandler(IDocumentService documentService)
            {
                    _documentService = documentService;
            }
            public async Task<RequestResult<List<Document>>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
            {
                List<Document> documents; 
                try
                {
                    documents = await _documentService.GetDocumentsAsync(request.UserId, request.Type, request.StartDate, request.Range, request.Sort, request.Desc);
                }
                catch (Exception ex)
                {
                    return RequestResult<List<Document>>.Failure(ex.Message);
                }
                if(documents == null || documents.Count == 0)
                {
                    return RequestResult<List<Document>>.Success();
                }
                if (!string.IsNullOrEmpty(request.Range))
                {
                    return RequestResult<List<Document>>.Success(documents, ApplicationResult.PARTIAL);
                }
                return RequestResult<List<Document>>.Success(documents);
            }
        }


    }

}
