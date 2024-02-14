using MyJobBoard.Domain.Entities;

namespace MyJobBoard.Web.Business.DTO
{
    public class InterlocutorDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LinkedinProfile { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public Guid? OpportunityId { get; set; }

        public InterlocutorDto(Interlocutor interlocutor)
        {
            Id = interlocutor.Id;
            FirstName = interlocutor.FirstName;
            LastName = interlocutor.LastName;
            LinkedinProfile = interlocutor.LinkedinProfile;
            Email = interlocutor.Email;
            Phone = interlocutor.Phone;
            OpportunityId = interlocutor.OpportunityId;
        }
    }
}