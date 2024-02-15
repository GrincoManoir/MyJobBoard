using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Companies.Commands
{
    public class UpdateCompanyCommand(Guid id, string name, string[]? websites, string[]? socialNetworks) : IRequest<RequestResult<Company>>
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string[]? Websites { get; set; } = websites;
        public string[]? SocialNetworks { get; set; } = socialNetworks;
    }



    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, RequestResult<Company>>
    {
        private readonly ICompanyService _companyService;
        public UpdateCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<RequestResult<Company>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _companyService.GetCompanyById(request.Id);
            if (company == null)
            {
                return RequestResult<Company>.Failure("Company not found", ApplicationResult.NOT_FOUND);
            }

            request.SetAttachedEntitiesNewProperties(company);
            try
            {
                await _companyService.UpdateCompany(company);
            }
            catch (Exception e)
            {
                return RequestResult<Company>.Failure(e.Message, ApplicationResult.BAD_REQUEST);
            }

            return RequestResult<Company>.Success(data: company);
        }
    }

    public static class UpdateCompanyCommandExtensions
    {
        public static void SetAttachedEntitiesNewProperties(this UpdateCompanyCommand request, Company entity)
        {
            entity.Name = request.Name;
            entity.Websites = request.Websites;
            entity.SocialNetworks = request.SocialNetworks;
        }
    }
}




