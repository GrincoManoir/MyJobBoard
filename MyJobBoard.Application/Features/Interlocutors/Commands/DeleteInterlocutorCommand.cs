using MediatR;
using MyJobBoard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Interlocutors.Commands
{
    public class DeleteInterlocutorCommand : IRequest
    {
        public Guid InterlocutorId { get; set; }
        public DeleteInterlocutorCommand(Guid interlocutorId)
        {
            InterlocutorId = interlocutorId;
        }
    }
    public class DeleteInterlocutorCommandHandler : IRequestHandler<DeleteInterlocutorCommand>
    {
        private readonly IInterlocutorService _interlocutorService;
        public DeleteInterlocutorCommandHandler(IInterlocutorService interlocutorService)
        {
            _interlocutorService = interlocutorService;
        }
        public async Task Handle(DeleteInterlocutorCommand request, CancellationToken cancellationToken)
        {
            await _interlocutorService.DeleteInterlocutor(request.InterlocutorId);
        }
    }
}
