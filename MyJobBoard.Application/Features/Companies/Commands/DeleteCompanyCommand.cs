using MediatR;
using MyJobBoard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public DeleteCompanyCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyService _companyService;
        public DeleteCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            await _companyService.DeleteCompany(request.CompanyId);
        }
    }
}
