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
    public class InterlocutorService : IInterlocutorService
    {
        private readonly IInterlocutorRepository _repo;
        public InterlocutorService(IInterlocutorRepository repo)
        {

            _repo = repo;
        }

        public async Task<List<Interlocutor>> GetInterlocutorsAsync(string userId, string? range, string? sort, bool? desc)
        {
            IEnumerable<Interlocutor> documents = await _repo.GetInterlocutorsAsync(userId, range, sort, desc);
            return documents.ToList();
        }

        public async Task<Interlocutor?> GetInterlocutorById(Guid interlocutorId)
        {
            return await _repo.GetInterlocutorById(interlocutorId);


        }

        public async Task CreateInterlocutor(Interlocutor interlocutor)
        {
            await _repo.CreateInterlocutor(interlocutor);
        }

        public async Task DeleteInterlocutor(Guid interlocutorId)
        {
            await _repo.DeleteInterlocutor(interlocutorId);
        }

        public async Task UpdateInterlocutor(Interlocutor interlocutor)
        {
            await _repo.UpdateInterlocutor(interlocutor);
        }
    }
}
