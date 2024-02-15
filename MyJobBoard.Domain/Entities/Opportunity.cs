using MyJobBoard.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyJobBoard.Domain.Entities
{
    public class Opportunity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string RoleTitle { get; set; }
        public IndicativeSalaryRange? IndicativeSalaryRange { get; set; }
        public string? Location { get; set; }
        public RemoteCondition? RemoteCondition { get; set; }
        public string? Industry { get; set; }
        public Guid? CompanyId { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public OpportunityState State { get; set; }
        

        // Navigation properties
        public Company? Company { get; set; }
        public ICollection<OpportunityStep> OpportunitySteps { get; set; }

        public ICollection<Interlocutor>? Interlocutors { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }

    [Owned]
    public class IndicativeSalaryRange
    {
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public SalaryPeriodicity Periodicity { get; set; }
    }
}
