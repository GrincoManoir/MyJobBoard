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
    public class OpportunityStepService : IOpportunityStepService
    {
        private readonly IOpportunityStepRepository _repo;
        public OpportunityStepService(IOpportunityStepRepository repo)
        {

            _repo = repo;
        }

        public async Task<List<OpportunityStep>> GetOpportunityStepsAsync(string userId, string? range, string? sort, bool? desc)
        {
            IEnumerable<OpportunityStep> documents = await _repo.GetOpportunityStepsAsync(userId, range, sort, desc);
            return documents.ToList();
        }

        public async Task<OpportunityStep?> GetOpportunityStepById(Guid opportunityStepId)
        {
            return await _repo.GetOpportunityStepById(opportunityStepId);


        }

        public async Task CreateOpportunityStep(OpportunityStep opportunityStep)
        {
            await _repo.CreateOpportunityStep(opportunityStep);
        }

        public async Task DeleteOpportunityStep(Guid opportunityStepId)
        {
            await _repo.DeleteOpportunityStep(opportunityStepId);
        }

        public async Task UpdateOpportunityStep(OpportunityStep opportunityStep)
        {
            await _repo.UpdateOpportunityStep(opportunityStep);
        }
    }
}
