using Library.Infrastructure.DTO;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Extentions
{
    public static class CacheExtentions
    {
        public static void SetJwt(this IMemoryCache memoryCache, Guid tokenId, JwtDTO jwt)
            => memoryCache.Set(GetKey(tokenId), jwt, TimeSpan.FromSeconds(5));

        public static JwtDTO GetJwt(this IMemoryCache memoryCache, Guid tokenId)
            => memoryCache.Get<JwtDTO>(GetKey(tokenId));

        private static string GetKey(Guid tokenId)
            => $"jwt:{tokenId}";
    }
}
