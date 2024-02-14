// MyJobBoard.Infrastructure/Persistence/DocumentRepository.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;

public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
{
    private readonly MyJobBoardBusinessDbContext _context;

    public OpportunityRepository(MyJobBoardBusinessDbContext context) : base(context)

    {
        _context = context;
    }

    public async Task<List<Opportunity>> GetOpportunitiesAsync(string userId, OpportunityState? state, DateTime? startDate, string? range, string? sort,bool? desc )
    {
        var opportunityQuery = base.GetQueryableItems(new Dictionary<string, object> { { "UserId", userId }, { "State", state } }, range, sort, desc);
        if (startDate.HasValue)
        {
            opportunityQuery = opportunityQuery.Where(d => d.StartDate >= startDate);
        }
        opportunityQuery = opportunityQuery.Include(o => o.Company);
        
        return  await opportunityQuery.AsNoTracking().ToListAsync();
    }


    public async Task<Opportunity?> GetOpportunityById(Guid opportunityId)
    {
        var opportunity =  await _context.Opportunities.SingleOrDefaultAsync(c => c.Id == opportunityId);
        if (opportunity != null)
        {
            await _context.Entry(opportunity)
                .Reference(o => o.Company)
                .LoadAsync();
        }
        return opportunity;
    }



    public async Task CreateOpportunity(Opportunity opportunity)
    {
        opportunity.Id = Guid.NewGuid();

        _= await _context.Opportunities.AddAsync(opportunity);
        await _context.SaveChangesAsync();
      
        await _context.Entry(opportunity)
            .Reference(o => o.Company)
            .LoadAsync();
            
    }

    public async Task DeleteOpportunity(Guid opportunityId)
    {
        _ = await _context.Opportunities.Where(d => d.Id == opportunityId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOpportunity(Opportunity opportunity)
    {
        _context.Opportunities.Update(opportunity);
        await _context.SaveChangesAsync();
        await _context.Entry(opportunity)
           .Reference(o => o.Company)
           .LoadAsync();
    }
}
