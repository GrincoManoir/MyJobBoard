using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO
{
    public class OpportunityDto
    {
        public Guid Id { get; set; }
        public string RoleTitle { get; set; }
        public CompanyDto? Company { get; set; }
        public OpportunityState State { get; set; }
        public List<OpportunityStepDto> OpportunitySteps { get; set; }
        public List<InterlocutorDto> Interlocutors { get; set; }
        public RemoteCondition? RemoteCondition { get; set; }
        public string? Location { get; set; }
        public string? Industry { get; set; }
        public IndicativeSalaryRange? IndicativeSalaryRange { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        
        

        public OpportunityDto(Opportunity opportunity)
        {
            Id = opportunity.Id;
            RoleTitle = opportunity.RoleTitle;
            Company = opportunity.Company !=null ? new CompanyDto(opportunity.Company) : null;
            StartDate = opportunity.StartDate;
            LastUpdateDate = opportunity.LastUpdateDate;
            State = opportunity.State;
            RemoteCondition = opportunity.RemoteCondition;
            Location = opportunity.Location;
            Industry = opportunity.Industry;
            IndicativeSalaryRange = opportunity.IndicativeSalaryRange == null ? null : opportunity.IndicativeSalaryRange;


            Interlocutors = opportunity.Interlocutors?.Select(i => new InterlocutorDto(i)).ToList() ?? new List<InterlocutorDto>();
            OpportunitySteps = opportunity.OpportunitySteps?.Select(s => new OpportunityStepDto(s)).ToList() ?? new List<OpportunityStepDto>();
        }
    }
}
