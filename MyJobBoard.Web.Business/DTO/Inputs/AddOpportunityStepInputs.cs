using MyJobBoard.Application.Features.Opportunities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class AddOpportunityStepInputs
    {
        public DateTime? DueDate { get; set; }
        public string FreeNotes { get; set; }
    }

    public static class AddOpportunityStepInputsExtensions
    {
        public static OpportunityAddOpportunityStepCommand ToBusinessCommand(this AddOpportunityStepInputs inputs, Guid opportunityId)
        {
            return new OpportunityAddOpportunityStepCommand(opportunityId, inputs.DueDate, inputs.FreeNotes);
        }
    }
}
