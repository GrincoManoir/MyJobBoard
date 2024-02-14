using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Domain.Entities
{
    public class Interview : OpportunityStep
    {
        List<Interlocutor> Interviewers { get; set; }
    }

    public class TechnicalInterview : Interview
    {
        public string? TechnicalTestUrl { get; set; }
    }

    public class HRInterview : Interview
    {
        public RemoteCondition? DiscussedRemote { get; set; }
        public int? NegociatedMinSalary { get; set; }
        public int? NegociatedMaxSalary { get; set; }
    }
}
