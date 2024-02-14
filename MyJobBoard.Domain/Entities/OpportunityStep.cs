using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Domain.Entities
{
    public class OpportunityStep
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? FreeNotes { get; set; }
        public OpportunityState State { get; set; }
        public Guid OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
    }
}
