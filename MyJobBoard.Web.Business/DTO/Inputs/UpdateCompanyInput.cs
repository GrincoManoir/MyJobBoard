using MyJobBoard.Application.Features.Companies.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class UpdateCompanyInput
    {
        public string? Name { get; set; }
        public string[]? Websites { get; set; }
        public string[]? SocialNetworks { get; set; }
    }

    public static class UpdateCompanyInputExtensions
    {
        public static UpdateCompanyCommand ToUpdateCompanyCommand(this UpdateCompanyInput input, Guid id)
        {
            return new UpdateCompanyCommand(id, input.Name, input.Websites, input.SocialNetworks);
        }
    }
}
