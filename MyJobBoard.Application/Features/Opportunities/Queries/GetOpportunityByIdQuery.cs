using MediatR;
using MyJobBoard.Application.Features.Documents.Queries;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Opportunities.Queries
{
    public class GetOpportunityByIdQuery : IRequest<RequestResult<Opportunity?>>
    {
        public Guid OpportunityId { get; }

        public GetOpportunityByIdQuery(Guid opportunityId)
        {
            OpportunityId = opportunityId;
        }
    }

    public class GetOpportunityByIdQueryHandler : IRequestHandler<GetOpportunityByIdQuery, RequestResult<Opportunity?>>
    {
        private readonly IOpportunityService _opportunityService;
        public GetOpportunityByIdQueryHandler(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        public async Task<RequestResult<Opportunity?>> Handle(GetOpportunityByIdQuery request, CancellationToken cancellationToken)
        {
            Opportunity? opportunity;
            try
            {
                opportunity = await _opportunityService.GetOpportunityById(request.OpportunityId);
            }
            catch (Exception ex)
            {
                return RequestResult<Opportunity?>.Failure(ex.Message);
            }
            if (opportunity == null)
            {
                return RequestResult<Opportunity?>.Failure("Opportunity not found", ApplicationResult.NOT_FOUND);
            }
            return RequestResult<Opportunity?>.Success(opportunity, ApplicationResult.OK);
        }
    }
}
