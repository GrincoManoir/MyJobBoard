using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MyJobBoard.Application.Interfaces;

namespace MyJobBoard.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly MemoryCache _cache;
        public TokenService(IConfiguration configuration)
        {
            _cache = new MemoryCache(new MemoryCacheOptions()
            {
                ExpirationScanFrequency = TimeSpan.FromMinutes(5),
            });
        }
        public void AddToBlacklist(string token)
        {
            var options = new MemoryCacheEntryOptions();

            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(key: token, value: token, options);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _cache.TryGetValue(key: token, out _);
        }

    }
}
