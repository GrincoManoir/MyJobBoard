using MyJobBoard.Domain.Entities;

namespace MyJobBoard.Web.Business.DTO
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<string>? Websites { get; set; }
        public ICollection<string>? SocialNetworks { get; set; }


        public CompanyDto(Company company)
        {
            Id = company.Id;
            Name = company.Name;
            Websites = company.Websites;
            SocialNetworks = company.SocialNetworks;
        }
    }
}