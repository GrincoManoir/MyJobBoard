using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Documents.Queries
{
    using global::MyJobBoard.Application.Interfaces;
    using global::MyJobBoard.Domain.Entities;
    using global::MyJobBoard.Domain.Enums;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;

    namespace MyJobBoard.Application.Features.Companies.Queries
    {
        public class GetCompaniesQuery(string userId, string? range, string? sort, bool? desc) : IRequest<RequestResult<List<Company>>>
        {
            public string UserId { get; set; } = userId;
            public string? Range { get; set; } = range;
            public string? Sort { get; set; } = sort;
            public bool? Desc {get; set; } = desc;

        }




        public class GetCompaniesByUserIdQueryHandler : IRequestHandler<GetCompaniesQuery, RequestResult<List<Company>>>
        {

            private readonly ICompanyService _companyService;
            public GetCompaniesByUserIdQueryHandler(ICompanyService companyService)
            {
                    _companyService = companyService;
            }
            public async Task<RequestResult<List<Company>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
            {
                List<Company> companies; 
                try
                {
                    companies = await _companyService.GetCompaniesAsync(request.UserId, request.Range, request.Sort, request.Desc);
                }
                catch (Exception ex)
                {
                    return RequestResult<List<Company>>.Failure(ex.Message);
                }
                if(companies == null || companies.Count == 0)
                {
                    return RequestResult<List<Company>>.Success();
                }
                if (!string.IsNullOrEmpty(request.Range))
                {
                    return RequestResult<List<Company>>.Success(companies, ApplicationResult.PARTIAL);
                }
                return RequestResult<List<Company>>.Success(companies);
            }
        }


    }

}
