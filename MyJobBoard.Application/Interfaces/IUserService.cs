using Microsoft.AspNetCore.Identity;
using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUser(ApplicationUser user, string password);
        Task<ApplicationUser?> GetUserById(Guid userId);
        Task<ApplicationUser?> GetUserByEmail(string email);
    }
}
