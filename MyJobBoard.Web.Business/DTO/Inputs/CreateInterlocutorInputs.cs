using MyJobBoard.Application.Features.Interlocutors.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class CreateInterlocutorInputs
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LinkedinProfile { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? OpportunityId { get; set; }
    }

    public static class CreateInterlocutorInputsExtensions
    {
        public static CreateInterlocutorCommand ToBusinessCommand(this CreateInterlocutorInputs input, string userId)
        {
            return new CreateInterlocutorCommand(
                userId : userId,
                firstname : input.FirstName,
                lastName : input.LastName,
                linkedinProfile : input.LinkedinProfile,
                email : input.Email,
                phone : input.Phone,
                opportunityId : input.OpportunityId
            );
        }
    }
}
