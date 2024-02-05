using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Guid> CreateDocument(Document document);
        Task DeleteDocument(Guid documentId);
        Task<Document?> GetDocumentById(Guid documentId);
        Task<IEnumerable<Document>> GetDocumentsAsync(string userId, EDocumentType? documentType, DateTime? startDate, string? range, string? sort, bool? desc);
    }
}
