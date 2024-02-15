// MyJobBoard.Infrastructure/Persistence/DocumentRepository.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;

public class OpportunityStepRepository : GenericRepository<OpportunityStep>, IOpportunityStepRepository
{
    private readonly MyJobBoardBusinessDbContext _context;

    public OpportunityStepRepository(MyJobBoardBusinessDbContext context) : base(context)

    {
        _context = context;
    }

    public async Task<List<OpportunityStep>> GetOpportunityStepsAsync(string userId, string? range, string? sort,bool? desc )
    {
        var opportunityStepQuery = base.GetQueryableItems(new Dictionary<string, object> { { "UserId", userId } }, range, sort, desc);     
        return  await opportunityStepQuery.AsNoTracking().ToListAsync();
    }


    public async Task<OpportunityStep?> GetOpportunityStepById(Guid opportunityStepId)
    {
        var opportunityStep =  await _context.OpportunitySteps.SingleOrDefaultAsync(c => c.Id == opportunityStepId);
        return opportunityStep;
    }



    public async Task CreateOpportunityStep(OpportunityStep opportunityStep)
    {
        opportunityStep.Id = Guid.NewGuid();

        _= await _context.OpportunitySteps.AddAsync(opportunityStep);
        await _context.SaveChangesAsync();            
    }

    public async Task DeleteOpportunityStep(Guid opportunityStepId)
    {
        _ = await _context.OpportunitySteps.Where(d => d.Id == opportunityStepId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOpportunityStep(OpportunityStep opportunityStep)
    {
        _context.OpportunitySteps.Update(opportunityStep);
        await _context.SaveChangesAsync();
    }
}
