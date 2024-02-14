using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Services
{
    public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _repo;
        public OpportunityService(IOpportunityRepository repo)
        {

            _repo = repo;
        }

        public async Task<List<Opportunity>> GetOpportunitiesAsync(string userId, OpportunityState? state, DateTime? startDate, string? range, string? sort, bool? desc)
        {
            IEnumerable<Opportunity> documents = await _repo.GetOpportunitiesAsync(userId, state, startDate, range, sort, desc);
            return documents.ToList();
        }

        public async Task<Opportunity?> GetOpportunityById(Guid opportunityId)
        {
            return await _repo.GetOpportunityById(opportunityId);


        }

        public async Task CreateOpportunity(Opportunity opportunity)
        {
            await _repo.CreateOpportunity(opportunity);
        }

        public async Task DeleteOpportunity(Guid opportunityId)
        {
            await _repo.DeleteOpportunity(opportunityId);
        }

        public async Task UpdateOpportunity(Opportunity opportunity)
        {
            await _repo.UpdateOpportunity(opportunity);
        }
    }
}
