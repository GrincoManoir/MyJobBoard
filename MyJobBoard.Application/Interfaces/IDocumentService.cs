using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<Guid> CreateDocument(Document document);
        Task DeleteDocument(Guid documentId);
        Task<Document?> GetDocumentById(Guid documentId);
        Task<List<Document>> GetDocumentsAsync(string userId, Domain.Enums.EDocumentType? type, DateTime? startDate, string? range, string? sort, bool? desc);
    }
}
