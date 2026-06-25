using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Services.Contract
{
    public interface  ICacheService
    {
        Task CacheResponseAsync(string key, object value, TimeSpan timetoLive);

        Task<string?> GetCachedResponseAsync(string key);

        Task RemoveByPatternAsync(string pattern);

    }
}
