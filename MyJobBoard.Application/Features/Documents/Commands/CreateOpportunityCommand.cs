using MediatR;
using MyJobBoard.Application.Features.Interlocutors.Queries;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System.Collections.ObjectModel;

namespace MyJobBoard.Application.Features.Opportunities.Commands
{
    public class CreateOpportunityCommand : IRequest<RequestResult<Opportunity>>
    {
        public string UserId { get; set; }
        public Guid? CompanyId { get; set; }
        public string? Location { get; set; }
        public string RoleTitle { get; set; }
        public RemoteCondition? RemoteCondition { get; set; }
        public string? Industry { get; set; }
        public IndicativeSalaryRange? IndicativeSalaryRange { get; set; }
        public string? FreeNotes { get; set; }
        public Guid? InterlocutorId { get; set; }

        public CreateOpportunityCommand(string userId, Guid? companyId, string? location, string roleTitle, RemoteCondition? remoteCondition, string? industry, IndicativeSalaryRange? indicativeSalaryRange, string? freeNotes, Guid? interlocutorId)
        {
            UserId = userId;
            CompanyId = companyId;
            Location = location;
            RoleTitle = roleTitle;
            RemoteCondition = remoteCondition;
            Industry = industry;
            IndicativeSalaryRange = indicativeSalaryRange;
            FreeNotes = freeNotes;
            InterlocutorId = interlocutorId;

        }
    }

    public class CreateOpportunityCommandHandler(IOpportunityService opportunityService, IInterlocutorService interlocutorService) : IRequestHandler<CreateOpportunityCommand, RequestResult<Opportunity>>
    {
        private readonly IOpportunityService _opportunityService = opportunityService;
        private readonly IInterlocutorService _interlocutorService = interlocutorService;

        public async Task<RequestResult<Opportunity>> Handle(CreateOpportunityCommand request, CancellationToken cancellationToken)
        {
            var opportunity = new Opportunity
            {
                UserId = request.UserId,
                CompanyId = request.CompanyId,
                Location = request.Location,
                RoleTitle = request.RoleTitle,
                RemoteCondition = request.RemoteCondition,
                Industry = request.Industry,
                IndicativeSalaryRange = request.IndicativeSalaryRange,
                StartDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow,
                State = OpportunityState.APPLIED,
                OpportunitySteps = new Collection<OpportunityStep>()

            };
            if (!string.IsNullOrWhiteSpace(request.FreeNotes))
            {
                opportunity.OpportunitySteps.Add(new OpportunityStep
                {
                    State = OpportunityState.APPLIED,
                    CreationDate = DateTime.UtcNow,
                    FreeNotes = request.FreeNotes
                });
            }

            if(request.InterlocutorId.HasValue && Guid.Empty != request.InterlocutorId)
            {
                var interlocutor = await _interlocutorService.GetInterlocutorById(request.InterlocutorId.Value);
                if(interlocutor == null)
                {
                    return RequestResult<Opportunity>.Failure("Interlocutor Not Found", ApplicationResult.NOT_FOUND);
                }
                opportunity.Interlocutors = new Collection<Interlocutor> { interlocutor };
            }

            try
            {
                await _opportunityService.CreateOpportunity(opportunity);
            }
            catch (Exception ex)
            {
                return RequestResult<Opportunity>.Failure(ex.Message);
            }

            return RequestResult<Opportunity>.Success(data: opportunity, ApplicationResult.OK);
        }
    }
}
