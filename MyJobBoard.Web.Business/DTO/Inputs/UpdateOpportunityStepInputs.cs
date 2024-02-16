using MyJobBoard.Application.Features.Opportunities.Commands;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class UpdateOpportunityStepInputs
    {
        public DateTime? DueDate { get; set; }
        public string FreeNotes { get; set; }
        public OpportunityState State { get; set; }
    }

    public static class UpdateOpportunityStepInputsExtensions
    {
        public static OpportunityUpdateOpportunityStepCommand ToBusinessCommand(this UpdateOpportunityStepInputs inputs, Guid opportunityId, Guid opportunityStepId)
        {
            return new OpportunityUpdateOpportunityStepCommand(opportunityId, opportunityStepId, inputs.DueDate, inputs.FreeNotes, inputs.State);
        }
    }
}
