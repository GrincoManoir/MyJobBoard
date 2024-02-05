// MyJobBoard.Infrastructure/Persistence/DocumentRepository.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;

public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
{
    private readonly MyJobBoardBusinessDbContext _context;

    public DocumentRepository(MyJobBoardBusinessDbContext context) : base(context)

    {
        _context = context;
    }

    public async Task<IEnumerable<Document>> GetDocumentsAsync(string userId, EDocumentType? documentType, DateTime? startDate, string? range, string? sort,bool? desc )
    {
        var documentQuery = base.GetQueryableItems(new Dictionary<string, object> { { "UserId", userId }, { "Type", documentType } }, range, sort, desc);
        if (startDate.HasValue)
        {
            documentQuery = documentQuery.Where(d => d.UploadedDate >= startDate);
        }
        return  await documentQuery.AsNoTracking().ToListAsync();
    }


    public async Task<Document?> GetDocumentById(Guid documentId)
    {
        return await _context.Documents.SingleOrDefaultAsync(c => c.Id == documentId);
    }



    public async Task<Guid> CreateDocument(Document document)
    {
        document.Id = Guid.NewGuid();

        _= await _context.Documents.AddAsync(document);
        await _context.SaveChangesAsync();
        return document.Id;

    }

    public async Task DeleteDocument(Guid documentId)
    {
        _ = await _context.Documents.Where(d => d.Id == documentId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
}
