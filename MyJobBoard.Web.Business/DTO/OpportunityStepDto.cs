using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System.Diagnostics;

namespace MyJobBoard.Web.Business.DTO
{
    public class OpportunityStepDto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public OpportunityState State { get; set; }
        public DateTime? DueDate { get; set; }
        public string? FreeNotes { get; set; }

        public OpportunityStepDto(OpportunityStep opportunityStep)
        {
            Id = opportunityStep.Id;
            CreationDate = opportunityStep.CreationDate;
            DueDate = opportunityStep.DueDate;
            FreeNotes = opportunityStep.FreeNotes;
            State = opportunityStep.State;
        }
    }
}