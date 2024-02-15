using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Application.Services;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyJobBoard.Application.Features.Interlocutors.Commands
{
    public class CreateInterlocutorCommand(string userId,string? firstname, string? lastName, string? linkedinProfile, string? email, string? phone, Guid? opportunityId) : IRequest<RequestResult<Interlocutor>>
    {
        public string userId { get; set; } = userId;
        public string? FirstName { get; set; } = firstname;
        public string? LastName { get; set; } = lastName;
        public string? LinkedinProfile { get; set; } = linkedinProfile;
        public string? Email { get; set; } = email;
        public string? Phone { get; set; } = phone;
        public Guid? OpportunityId { get; set; } =  opportunityId;


    }

    public class CreateInterlocutorCommandHandler(IInterlocutorService interlocutorService, IOpportunityService opportunityService) : IRequestHandler<CreateInterlocutorCommand, RequestResult<Interlocutor>>
    {
        private readonly IInterlocutorService _interlocutorService = interlocutorService;
        private readonly IOpportunityService _opportunityService = opportunityService;

        public async Task<RequestResult<Interlocutor>> Handle(CreateInterlocutorCommand request, CancellationToken cancellationToken)
        {
            var opportunities = new Collection<Opportunity>();
            Opportunity? opportunity;
            if(request.OpportunityId.HasValue)
            {
                opportunity = await _opportunityService.GetOpportunityById(request.OpportunityId.Value);
                if(opportunity == null)
                {
                    return RequestResult<Interlocutor>.Failure("Opportunity Not Found",ApplicationResult.NOT_FOUND);
                }
                opportunities.Add(opportunity);
            }
            var interlocutor = new Interlocutor
            {
                Id = Guid.NewGuid(),
                UserId = request.userId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                LinkedinProfile = request.LinkedinProfile,
                Opportunities = opportunities,
                Phone = request.Phone
            };

            try
            {
                await _interlocutorService.CreateInterlocutor(interlocutor);
            }
            catch (Exception ex)
            {
                return RequestResult<Interlocutor>.Failure(ex.Message);
            }

            return RequestResult<Interlocutor>.Success(data: interlocutor, ApplicationResult.OK);
        }
    }
}
