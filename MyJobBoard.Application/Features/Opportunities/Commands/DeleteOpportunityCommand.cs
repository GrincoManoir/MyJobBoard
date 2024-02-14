using MediatR;
using MyJobBoard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Opportunities.Commands
{
    public class DeleteOpportunityCommand : IRequest
    {
        public Guid OpportunityId { get; set; }
        public DeleteOpportunityCommand(Guid opportunityId)
        {
            OpportunityId = opportunityId;
        }
    }
    public class DeleteOpportunityCommandHandler : IRequestHandler<DeleteOpportunityCommand>
    {
        private readonly IOpportunityService _opportunityService;
        public DeleteOpportunityCommandHandler(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }
        public async Task Handle(DeleteOpportunityCommand request, CancellationToken cancellationToken)
        {
            await _opportunityService.DeleteOpportunity(request.OpportunityId);
        }
    }
}
