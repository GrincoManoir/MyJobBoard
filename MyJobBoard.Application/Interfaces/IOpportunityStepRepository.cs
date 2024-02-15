using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface IOpportunityStepRepository
    {
        Task<List<OpportunityStep>> GetOpportunityStepsAsync(string userId, string? range, string? sort, bool? desc);
        Task<OpportunityStep?> GetOpportunityStepById(Guid opportunityStepId);
        Task CreateOpportunityStep(OpportunityStep opportunityStep);
        Task DeleteOpportunityStep(Guid opportunityStepId);
        Task UpdateOpportunityStep(OpportunityStep opportunityStep);
    }
}
