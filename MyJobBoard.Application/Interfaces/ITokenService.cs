using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Interfaces
{
    public interface ITokenService
    {
        void AddToBlacklist(string token);
        public bool IsTokenBlacklisted(string token);
    }
}
