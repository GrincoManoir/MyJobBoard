using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Domain.Entities
{
    public class Interlocutor
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LinkedinProfile { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<Opportunity>? Opportunities { get; set; }
       

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
