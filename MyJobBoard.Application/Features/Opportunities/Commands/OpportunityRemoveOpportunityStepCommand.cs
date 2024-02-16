using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Opportunities.Commands
{
    public class OpportunityRemoveOpportunityStepCommand(Guid opportunityId, Guid opportunityStepId) : IRequest<RequestResult<Opportunity>>
    {
        public Guid OpportunityId { get; set; } = opportunityId;
        public Guid OpportunityStepId { get; set; } = opportunityStepId;
    }

    public class OpportunityRemoveOpportunityStepCommandHandler(IOpportunityService opportunityService) : IRequestHandler<OpportunityRemoveOpportunityStepCommand, RequestResult<Opportunity>>
    {
        private readonly IOpportunityService _opportunityService = opportunityService;


        public async Task<RequestResult<Opportunity>> Handle(OpportunityRemoveOpportunityStepCommand request, CancellationToken cancellationToken)
        {
            Opportunity? opportunity = await _opportunityService.GetOpportunityById(request.OpportunityId);
            if (opportunity == null)
            {
                return RequestResult<Opportunity>.Failure("Opportunity not found", ApplicationResult.NOT_FOUND);
            }

            OpportunityStep? step = opportunity.OpportunitySteps.SingleOrDefault(s => s.Id == request.OpportunityStepId);

            if (step == null || !opportunity.OpportunitySteps.Remove(step))
            {
                return RequestResult<Opportunity>.Failure("Step not found", ApplicationResult.NOT_FOUND);
            }

            try
            {
                await _opportunityService.UpdateOpportunity(opportunity);
            }
            catch (Exception ex)
            {
                return RequestResult<Opportunity>.Failure(ex.InnerException?.Message ?? ex.Message, ApplicationResult.BAD_REQUEST);
            }

            return RequestResult<Opportunity>.Success(data :opportunity);
        }
    }
}
