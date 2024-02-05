using MediatR;
using MyJobBoard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Documents.Commands
{
    public class DeleteDocumentCommand : IRequest
    {
        public Guid DocumentId { get; set; }
    }


    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IDocumentService _documentService;
        public DeleteDocumentCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
            
        }
        public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.DeleteDocument(request.DocumentId);
        }
    }
}
