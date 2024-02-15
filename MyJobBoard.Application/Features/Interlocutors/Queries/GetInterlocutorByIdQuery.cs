using MediatR;
using MyJobBoard.Application.Features.Documents.Queries;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Interlocutors.Queries
{
    public class GetInterlocutorByIdQuery : IRequest<RequestResult<Interlocutor?>>
    {
        public Guid InterlocutorId { get; }

        public GetInterlocutorByIdQuery(Guid interlocutorId)
        {
            InterlocutorId = interlocutorId;
        }
    }

    public class GetInterlocutorByIdQueryHandler : IRequestHandler<GetInterlocutorByIdQuery, RequestResult<Interlocutor?>>
    {
        private readonly IInterlocutorService _interlocutorService;
        public GetInterlocutorByIdQueryHandler(IInterlocutorService interlocutorService)
        {
            _interlocutorService = interlocutorService;
        }

        public async Task<RequestResult<Interlocutor?>> Handle(GetInterlocutorByIdQuery request, CancellationToken cancellationToken)
        {
            Interlocutor? interlocutor;
            try
            {
                interlocutor = await _interlocutorService.GetInterlocutorById(request.InterlocutorId);
            }
            catch (Exception ex)
            {
                return RequestResult<Interlocutor?>.Failure(ex.Message);
            }
            if (interlocutor == null)
            {
                return RequestResult<Interlocutor?>.Failure("Interlocutor not found", ApplicationResult.NOT_FOUND);
            }
            return RequestResult<Interlocutor?>.Success(interlocutor, ApplicationResult.OK);
        }
    }
}
