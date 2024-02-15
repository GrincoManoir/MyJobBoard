using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface IInterlocutorRepository
    {
        Task<List<Interlocutor>> GetInterlocutorsAsync(string userId, string? range, string? sort, bool? desc);
        Task<Interlocutor?> GetInterlocutorById(Guid interlocutorId);
        Task CreateInterlocutor(Interlocutor interlocutor);
        Task DeleteInterlocutor(Guid interlocutorId);
        Task UpdateInterlocutor(Interlocutor interlocutor);
    }
}
