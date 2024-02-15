using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO
{
    public class InterlocutorOpportunityDto
    {
        public Guid Id { get; set; }
        public string RoleTitle { get; set; }
        public CompanyDto? Company { get; set; }
        public OpportunityState State { get; set; }
        public RemoteCondition? RemoteCondition { get; set; }
        public string? Location { get; set; }
        public string? Industry { get; set; }

        public InterlocutorOpportunityDto(Opportunity opportunity)
        {
            Id = opportunity.Id;
            RoleTitle = opportunity.RoleTitle;
            Company = opportunity.Company !=null ? new CompanyDto(opportunity.Company) : null;
            State = opportunity.State;
            RemoteCondition = opportunity.RemoteCondition;
            Location = opportunity.Location;
            Industry = opportunity.Industry;
        }
    }
}
