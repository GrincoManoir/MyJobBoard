// MyJobBoard.Infrastructure/Persistence/DocumentRepository.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;

public class InterlocutorRepository : GenericRepository<Interlocutor>, IInterlocutorRepository
{
    private readonly MyJobBoardBusinessDbContext _context;

    public InterlocutorRepository(MyJobBoardBusinessDbContext context) : base(context)

    {
        _context = context;
    }

    public async Task<List<Interlocutor>> GetInterlocutorsAsync(string userId, string? range, string? sort,bool? desc )
    {
        var interlocutorQuery = base.GetQueryableItems(new Dictionary<string, object> { { "UserId", userId } }, range, sort, desc);     
        return  await interlocutorQuery.AsNoTracking().ToListAsync();
    }


    public async Task<Interlocutor?> GetInterlocutorById(Guid interlocutorId)
    {
        var interlocutor =  await _context.Interlocutors.SingleOrDefaultAsync(c => c.Id == interlocutorId);
        return interlocutor;
    }



    public async Task CreateInterlocutor(Interlocutor interlocutor)
    {
        interlocutor.Id = Guid.NewGuid();
        _= await _context.Interlocutors.AddAsync(interlocutor);
        await _context.SaveChangesAsync();            
    }

    public async Task DeleteInterlocutor(Guid interlocutorId)
    {
        _ = await _context.Interlocutors.Where(d => d.Id == interlocutorId).ExecuteDeleteAsync();
        await OnInterlocutorDelete(interlocutorId);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateInterlocutor(Interlocutor interlocutor)
    {
        _context.Interlocutors.Update(interlocutor);
        await _context.SaveChangesAsync();
    }

    private async Task OnInterlocutorDelete(Guid interlocutorId)
    {
        await _context.OpportunityInterlocutors.Where(io => io.InterlocutorId == interlocutorId).ExecuteDeleteAsync();
    }
}
