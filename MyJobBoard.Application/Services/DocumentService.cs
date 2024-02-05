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
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repo;
        public DocumentService(IDocumentRepository repo)
        {

            _repo = repo;
        }

        public async Task<List<Document>> GetDocumentsAsync(string userId, Domain.Enums.EDocumentType? type, DateTime? startDate, string? range, string? sort, bool? desc)
        {
            IEnumerable<Document> documents = await _repo.GetDocumentsAsync(userId, type, startDate, range, sort, desc);
            return documents.ToList();
        }

        public async Task<Document?> GetDocumentById(Guid documentId)
        {
            return await _repo.GetDocumentById(documentId);


        }

        public async Task<Guid> CreateDocument(Document document)
        {
            return await _repo.CreateDocument(document);
        }

        public async Task DeleteDocument(Guid documentId)
        {
            await _repo.DeleteDocument(documentId);
        }
    }
}
