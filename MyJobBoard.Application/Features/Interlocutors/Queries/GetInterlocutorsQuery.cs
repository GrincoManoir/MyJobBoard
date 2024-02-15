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

    namespace MyJobBoard.Application.Features.Interlocutors.Queries
    {
        public class GetInterlocutorsQuery(string userId, string? range, string? sort, bool? desc) : IRequest<RequestResult<List<Interlocutor>>>
        {
            public string UserId { get; } = userId;
            public string? Range { get; set; } = range;
            public string? Sort { get; set; } = sort;
            public bool? Desc {get; set; } = desc;
        }




        public class GetInterlocutorsByUserIdQueryHandler : IRequestHandler<GetInterlocutorsQuery, RequestResult<List<Interlocutor>>>
        {

            private readonly IInterlocutorService _interlocutorService;
            public GetInterlocutorsByUserIdQueryHandler(IInterlocutorService interlocutorService)
            {
                    _interlocutorService = interlocutorService;
            }
            public async Task<RequestResult<List<Interlocutor>>> Handle(GetInterlocutorsQuery request, CancellationToken cancellationToken)
            {
                List<Interlocutor> interlocutors; 
                try
                {
                    interlocutors = await _interlocutorService.GetInterlocutorsAsync(request.UserId, request.Range, request.Sort, request.Desc);
                }
                catch (Exception ex)
                {
                    return RequestResult<List<Interlocutor>>.Failure(ex.Message);
                }
                if(interlocutors == null || interlocutors.Count == 0)
                {
                    return RequestResult<List<Interlocutor>>.Success();
                }
                if (!string.IsNullOrEmpty(request.Range))
                {
                    return RequestResult<List<Interlocutor>>.Success(interlocutors, ApplicationResult.PARTIAL);
                }
                return RequestResult<List<Interlocutor>>.Success(interlocutors);
            }
        }


    }

}
