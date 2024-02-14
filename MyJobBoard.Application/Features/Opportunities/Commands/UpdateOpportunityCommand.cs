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
    public class UpdateOpportunityCommand() : IRequest<RequestResult<Opportunity>>
    {
        public Guid Id { get; set; }
        public string RoleTitle { get; set; }
        public IndicativeSalaryRange? IndicativeSalaryRange { get; set; }
        public string? Location { get; set; }
        public RemoteCondition? RemoteCondition { get; set; }
        public string? Industry { get; set; }
        public Guid? CompanyId { get; set; }
        public OpportunityState State { get; set; }
    }



    public class UpdateOpportunityCommandHandler : IRequestHandler<UpdateOpportunityCommand, RequestResult<Opportunity>>
    {
        private readonly IOpportunityService _opportunityService;
        public UpdateOpportunityCommandHandler(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        public async Task<RequestResult<Opportunity>> Handle(UpdateOpportunityCommand request, CancellationToken cancellationToken)
        {
            Opportunity? opportunity = await _opportunityService.GetOpportunityById(request.Id);
            if (opportunity == null)
            {
                return RequestResult<Opportunity>.Failure("Opportunity not found", ApplicationResult.NOT_FOUND);
            }

            request.SetAttachedEntitiesNewProperties(opportunity);
            try
            {
                await _opportunityService.UpdateOpportunity(opportunity);
            }
            catch (Exception e)
            {
                return RequestResult<Opportunity>.Failure(e.Message, ApplicationResult.BAD_REQUEST);
            }

            return RequestResult<Opportunity>.Success(data: opportunity);
        }
    }

    public static class UpdateOpportunityCommandExtensions
    {
        public static void SetAttachedEntitiesNewProperties(this UpdateOpportunityCommand request, Opportunity entity)
        {
            entity.LastUpdateDate = DateTime.UtcNow;
            entity.RoleTitle = request.RoleTitle;
            if (request.IndicativeSalaryRange != null)
            {
                entity.IndicativeSalaryRange = request.IndicativeSalaryRange;
            }
            entity.Location = request.Location;
            entity.RemoteCondition = request.RemoteCondition;
            entity.Industry = request.Industry;
            entity.CompanyId = request.CompanyId;
            entity.State = request.State;
        }
    }
}




