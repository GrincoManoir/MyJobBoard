using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyJobBoard.Domain.Entities
{
    public class OpportunityInterlocutor
    {
        public Guid Id { get; set; }
        public Guid OpportunityId { get; set; }
        public Guid InterlocutorId { get; set; }

        public Opportunity? Opportunity { get; set; }
        public Interlocutor? Interlocutor { get; set; }
    }
}
