using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;

namespace MyJobBoard.Application.Features.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<RequestResult<Company?>>
    {
        public Guid CompanyId { get; }

        public GetCompanyByIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, RequestResult<Company?>>
    {
        private readonly ICompanyService _companyService;
        public GetCompanyByIdQueryHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<RequestResult<Company?>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            Company? company;
            try
            {
                company = await _companyService.GetCompanyById(request.CompanyId);
            }
            catch (Exception ex)
            {
                return RequestResult<Company?>.Failure(ex.Message);
            }
            if (company == null)
            {
                return RequestResult<Company?>.Failure("Company not found", ApplicationResult.NOT_FOUND);
            }
            return RequestResult<Company?>.Success(company, ApplicationResult.OK);
        }
    }
}
