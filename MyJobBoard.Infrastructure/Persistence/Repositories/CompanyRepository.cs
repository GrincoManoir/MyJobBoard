// MyJobBoard.Infrastructure/Persistence/DocumentRepository.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    private readonly MyJobBoardBusinessDbContext _context;

    public CompanyRepository(MyJobBoardBusinessDbContext context) : base(context)

    {
        _context = context;
    }

    public async Task<List<Company>> GetCompaniesAsync(string userId, string? range, string? sort,bool? desc )
    {
        var companyQuery = base.GetQueryableItems(new Dictionary<string, object> { { "UserId", userId } }, range, sort, desc);     
        return  await companyQuery.AsNoTracking().ToListAsync();
    }


    public async Task<Company?> GetCompanyById(Guid companyId)
    {
        var company =  await _context.Companies.SingleOrDefaultAsync(c => c.Id == companyId);
        return company;
    }



    public async Task CreateCompany(Company company)
    {
        company.Id = Guid.NewGuid();

        _= await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();            
    }

    public async Task DeleteCompany(Guid companyId)
    {
        _ = await _context.Companies.Where(d => d.Id == companyId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCompany(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
    }
}
