using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyJobBoard.Application.Features.Companies.Commands
{
    public class CreateCompanyCommand(string userId, string name, string[]? websites, string[]? socialNetworks) : IRequest<RequestResult<Company>>
    {
        public string UserId { get; set; } = userId;
        public string Name { get; set; } = name;
        public string[]? Websites { get; set; } = websites;
        public string[]? SocialNetworks { get; set; } = socialNetworks;

       
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, RequestResult<Company>>
    {
        private readonly ICompanyService _companyService;

        public CreateCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<RequestResult<Company>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                Websites = request.Websites,
                SocialNetworks = request.SocialNetworks
            };

            try
            {
                await _companyService.CreateCompany(company);
            }
            catch (Exception ex)
            {
                return RequestResult<Company>.Failure(ex.Message);
            }

            return RequestResult<Company>.Success(data: company, ApplicationResult.OK);
        }
    }
}
