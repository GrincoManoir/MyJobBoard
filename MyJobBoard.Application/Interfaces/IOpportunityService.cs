using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;

namespace MyJobBoard.Application.Interfaces
{
    public interface IOpportunityService
    {
        Task<List<Opportunity>> GetOpportunitiesAsync(string userId, OpportunityState? state, DateTime? startDate, string? range, string? sort, bool? desc);
        Task<Opportunity?> GetOpportunityById(Guid opportunityId);
        Task CreateOpportunity(Opportunity opportunity);
        Task DeleteOpportunity(Guid opportunityId);
        Task UpdateOpportunity(Opportunity opportunity);
    }
}