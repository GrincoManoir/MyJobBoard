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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        public CompanyService(ICompanyRepository repo)
        {

            _repo = repo;
        }

        public async Task<List<Company>> GetCompaniesAsync(string userId, string? range, string? sort, bool? desc)
        {
            IEnumerable<Company> documents = await _repo.GetCompaniesAsync(userId, range, sort, desc);
            return documents.ToList();
        }

        public async Task<Company?> GetCompanyById(Guid companyId)
        {
            return await _repo.GetCompanyById(companyId);


        }

        public async Task CreateCompany(Company company)
        {
            await _repo.CreateCompany(company);
        }

        public async Task DeleteCompany(Guid companyId)
        {
            await _repo.DeleteCompany(companyId);
        }

        public async Task UpdateCompany(Company company)
        {
            await _repo.UpdateCompany(company);
        }
    }
}
