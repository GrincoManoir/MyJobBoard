using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Documents.Queries
{
    using global::MyJobBoard.Application.Interfaces;
    using global::MyJobBoard.Domain.Entities;
    using global::MyJobBoard.Domain.Enums;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;

    namespace MyJobBoard.Application.Features.Opportunities.Queries
    {
        public class GetOpportunitiesQuery : IRequest<RequestResult<List<Opportunity>>>
        {
            public string UserId { get; }
            public OpportunityState? State { get; set; }
            public DateTime? StartDate { get; set; }
            public string? Range { get; set; }
            public string? Sort { get; set; }
            public bool? Desc {get; set; }

            public GetOpportunitiesQuery(string userId, OpportunityState? state,DateTime? startDate, string? range, string? sort, bool? desc)
            {
                UserId = userId;
                State = state;
                StartDate = startDate;
                Range = range;
                Sort = sort;
                Desc = desc;
            }
        }




        public class GetOpportunitiesByUserIdQueryHandler : IRequestHandler<GetOpportunitiesQuery, RequestResult<List<Opportunity>>>
        {

            private readonly IOpportunityService _opportunityService;
            public GetOpportunitiesByUserIdQueryHandler(IOpportunityService opportunityService)
            {
                    _opportunityService = opportunityService;
            }
            public async Task<RequestResult<List<Opportunity>>> Handle(GetOpportunitiesQuery request, CancellationToken cancellationToken)
            {
                List<Opportunity> opportunities; 
                try
                {
                    opportunities = await _opportunityService.GetOpportunitiesAsync(request.UserId, request.State, request.StartDate, request.Range, request.Sort, request.Desc);
                }
                catch (Exception ex)
                {
                    return RequestResult<List<Opportunity>>.Failure(ex.Message);
                }
                if(opportunities == null || opportunities.Count == 0)
                {
                    return RequestResult<List<Opportunity>>.Success();
                }
                if (!string.IsNullOrEmpty(request.Range))
                {
                    return RequestResult<List<Opportunity>>.Success(opportunities, ApplicationResult.PARTIAL);
                }
                return RequestResult<List<Opportunity>>.Success(opportunities);
            }
        }


    }

}
