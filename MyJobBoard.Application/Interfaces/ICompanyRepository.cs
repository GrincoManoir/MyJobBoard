using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetCompaniesAsync(string userId, string? range, string? sort, bool? desc);
        Task<Company?> GetCompanyById(Guid companyId);
        Task CreateCompany(Company company);
        Task DeleteCompany(Guid companyId);
        Task UpdateCompany(Company company);
    }
}
