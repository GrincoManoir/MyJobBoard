using MyJobBoard.Application.Services;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface IOpportunityRepository
    {
        Task<List<Opportunity>> GetOpportunitiesAsync(string userId, OpportunityState? state, DateTime? startDate, string? range, string? sort, bool? desc);
        Task<Opportunity?> GetOpportunityById(Guid opportunityId);
        Task CreateOpportunity(Opportunity opportunity);
        Task DeleteOpportunity(Guid opportunityId);
        Task UpdateOpportunity(Opportunity opportunity);
    }
}
