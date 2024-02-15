using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class CreateCompanyInput
    {
        public string Name { get; set; }
        public string[]? Websites { get; set; }
        public string[]? SocialNetworks { get; set; }
    }
}
