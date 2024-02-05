using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Documents.Commands
{
    public class UploadDocumentCommand : IRequest<RequestResult<Guid>>
    {
        public string? UserId { get; set; }
        public string Name { get; set; }
        public EDocumentType DocumentType { get; set; }
        public byte[] FileContent { get; set; }
        public string FileContentType { get; set; }

    }



    public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, RequestResult<Guid>>
    {
        private readonly IDocumentService _documentService;

        public UploadDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<RequestResult<Guid>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = new Document
            {
                UserId = request.UserId,
                Name = request.Name,
                ContentType = request.FileContentType,
                Content = request.FileContent,
                Type = request.DocumentType,
                UploadedDate = DateTime.UtcNow
            };

            Guid id;
            try
            {
                id = await _documentService.CreateDocument(document);
            }
            catch (Exception ex)
            {
                return RequestResult<Guid>.Failure(ex.Message);
            }

            return RequestResult<Guid>.Success(id, ApplicationResult.OK);
        }
    }
}
