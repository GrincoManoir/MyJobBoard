using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Application.Services;
using MyJobBoard.Domain.Entities;

namespace MyJobBoard.Application.Features.Opportunities.Commands
{
    public class OpportunityRemoveInterlocutorCommand(Guid opportunityId, Guid interlocutorId) : IRequest<RequestResult<Opportunity>>
    {
        public Guid OpportunityId { get; set; } = opportunityId;
        public Guid InterlocutorId { get; set; } = interlocutorId;
    }

    public class OpportunityRemoveInterlocutorCommandHandler(IOpportunityService opportunityService, IInterlocutorService interlocutorService) : IRequestHandler<OpportunityRemoveInterlocutorCommand, RequestResult<Opportunity>>
    {
        private readonly IOpportunityService _opportunityService = opportunityService;
        private readonly IInterlocutorService _interlocutorService = interlocutorService;
        public async Task<RequestResult<Opportunity>> Handle(OpportunityRemoveInterlocutorCommand request, CancellationToken cancellationToken)
        {
            Opportunity? opportunity = await _opportunityService.GetOpportunityById(request.OpportunityId);
            if(opportunity  == null)
            {
                return RequestResult<Opportunity>.Failure("Opportunity not found", ApplicationResult.NOT_FOUND);  
            }

            Interlocutor? interlocutor = await _interlocutorService.GetInterlocutorById(request.InterlocutorId);
            if(interlocutor == null)
            {
                return RequestResult<Opportunity>.Failure("Interlocutor not found", ApplicationResult.NOT_FOUND);
            }

            bool interlocutorBelongOpportunity = opportunity.Interlocutors.Remove(interlocutor);
            if(!interlocutorBelongOpportunity)
            {
                return RequestResult<Opportunity>.Failure("interlocutor not linked to this opportunity", ApplicationResult.BAD_REQUEST);
            }
            try
            {
                await _opportunityService.UpdateOpportunity(opportunity);
            }
            catch (Exception e)
            {
                return RequestResult<Opportunity>.Failure(e.InnerException?.Message?? e.Message, ApplicationResult.BAD_REQUEST);
            }
            return RequestResult<Opportunity>.Success(data: opportunity);
        }
    }
}
