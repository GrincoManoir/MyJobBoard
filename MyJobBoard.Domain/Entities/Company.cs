using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string[]? Websites { get; set; }
        public string[]? SocialNetworks { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
