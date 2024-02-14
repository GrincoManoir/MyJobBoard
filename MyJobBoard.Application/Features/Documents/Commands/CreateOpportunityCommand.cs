using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System.Collections.ObjectModel;

namespace MyJobBoard.Application.Features.Documents.Commands
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

        public CreateOpportunityCommand(string userId, Guid? companyId, string? location, string roleTitle, RemoteCondition? remoteCondition, string? industry, IndicativeSalaryRange? indicativeSalaryRange, string? freeNotes)
        {
            UserId = userId;
            CompanyId = companyId;
            Location = location;
            RoleTitle = roleTitle;
            RemoteCondition = remoteCondition;
            Industry = industry;
            IndicativeSalaryRange = indicativeSalaryRange;
            FreeNotes = freeNotes;

        }
    }

    public class CreateOpportunityCommandHandler : IRequestHandler<CreateOpportunityCommand, RequestResult<Opportunity>>
    {
        private readonly IOpportunityService _opportunityService;

        public CreateOpportunityCommandHandler(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

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
            if (!String.IsNullOrWhiteSpace(request.FreeNotes))
            {
                opportunity.OpportunitySteps.Add(new OpportunityStep
                {
                    State = OpportunityState.APPLIED,
                    CreationDate = DateTime.UtcNow,
                    FreeNotes = request.FreeNotes
                });
            }

            try
            { 
                 await _opportunityService.CreateOpportunity(opportunity);
            }
            catch (Exception ex)
            {
                return RequestResult<Opportunity>.Failure(ex.Message);
            }

            return RequestResult<Opportunity>.Success(data : opportunity, ApplicationResult.OK);
        }
    }
}
