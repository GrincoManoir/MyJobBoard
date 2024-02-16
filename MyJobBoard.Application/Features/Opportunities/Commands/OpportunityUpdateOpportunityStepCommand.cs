using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Opportunities.Commands
{
    public class OpportunityUpdateOpportunityStepCommand(Guid opportunityId,Guid opportunityStepId, DateTime? dueDate, string freeNotes, OpportunityState state) : IRequest<RequestResult<Opportunity>>
    {
        public Guid OpportunityId { get; set; } = opportunityId;
        public Guid OpportunityStepId { get; set; } = opportunityStepId;
        public OpportunityState State { get; set; } = state;
        public DateTime? DueDate { get; set; } = dueDate;
        public string FreeNotes { get; set; } = freeNotes;
    }

    public class OpportunityUpdateOpportunityStepCommandHandler(IOpportunityService opportunityService): IRequestHandler<OpportunityUpdateOpportunityStepCommand, RequestResult<Opportunity>>
    {
        private readonly IOpportunityService _opportunityService = opportunityService;


        public async Task<RequestResult<Opportunity>> Handle(OpportunityUpdateOpportunityStepCommand request, CancellationToken cancellationToken)
        {
            Opportunity? opportunity = await _opportunityService.GetOpportunityById(request.OpportunityId);
            if (opportunity == null)
            {
                return RequestResult<Opportunity>.Failure("Opportunity not found", ApplicationResult.NOT_FOUND);
            }

            var opportunityStep = opportunity.OpportunitySteps.SingleOrDefault(s => s.Id == request.OpportunityStepId);
            if (opportunityStep == null)
            {
                return RequestResult<Opportunity>.Failure("OpportunityStep not found", ApplicationResult.NOT_FOUND);
            }

            UpdateStep(request, opportunityStep);

            try
            {
                await _opportunityService.UpdateOpportunity(opportunity);
            }
            catch (Exception ex)
            {
                return RequestResult<Opportunity>.Failure(ex.InnerException?.Message ?? ex.Message, ApplicationResult.BAD_REQUEST);
            }

            return RequestResult<Opportunity>.Success(data: opportunity);
        }

        private static void UpdateStep(OpportunityUpdateOpportunityStepCommand request, OpportunityStep opportunityStep)
        {
            opportunityStep.DueDate = request.DueDate;
            opportunityStep.FreeNotes = request.FreeNotes;
            opportunityStep.State = request.State;
        }
    }
}
