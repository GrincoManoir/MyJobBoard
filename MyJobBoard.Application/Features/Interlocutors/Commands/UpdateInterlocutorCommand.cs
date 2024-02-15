using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Interlocutors.Commands
{
    public class UpdateInterlocutorCommand(Guid id,string? firstname, string? lastName, string? linkedinProfile, string? email, string? phone) : IRequest<RequestResult<Interlocutor>>
    {
        public Guid Id { get; set; } = id;
        public string? FirstName { get; set; } = firstname;
        public string? LastName { get; set; } = lastName;
        public string? LinkedinProfile { get; set; } = linkedinProfile;
        public string? Email { get; set; } = email;
        public string? Phone { get; set; } = phone;
    }



    public class UpdateInterlocutorCommandHandler : IRequestHandler<UpdateInterlocutorCommand, RequestResult<Interlocutor>>
    {
        private readonly IInterlocutorService _interlocutorService;
        public UpdateInterlocutorCommandHandler(IInterlocutorService interlocutorService)
        {
            _interlocutorService = interlocutorService;
        }

        public async Task<RequestResult<Interlocutor>> Handle(UpdateInterlocutorCommand request, CancellationToken cancellationToken)
        {
            Interlocutor? interlocutor = await _interlocutorService.GetInterlocutorById(request.Id);
            if (interlocutor == null)
            {
                return RequestResult<Interlocutor>.Failure("Interlocutor not found", ApplicationResult.NOT_FOUND);
            }

            request.SetAttachedEntitiesNewProperties(interlocutor);
            try
            {
                await _interlocutorService.UpdateInterlocutor(interlocutor);
            }
            catch (Exception e)
            {
                return RequestResult<Interlocutor>.Failure(e.Message, ApplicationResult.BAD_REQUEST);
            }

            return RequestResult<Interlocutor>.Success(data: interlocutor);
        }
    }

    public static class UpdateInterlocutorCommandExtensions
    {
        public static void SetAttachedEntitiesNewProperties(this UpdateInterlocutorCommand request, Interlocutor entity)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.LinkedinProfile = request.LinkedinProfile;
            entity.Email = request.Email;
            entity.Phone = request.Phone;
        }
    }
}




