using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyJobBoard.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public override bool TwoFactorEnabled => false;
         
    }
}
