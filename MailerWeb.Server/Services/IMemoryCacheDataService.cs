using Microsoft.Extensions.Caching.Memory;

namespace MailerWeb.Server.Services
{
    public interface IMemoryCacheDataService
    {
        void Set(object key, object value);
        void Set(object key, object value, MemoryCacheEntryOptions options);
        void Remove(object key);
        bool TryGetValue(object key, out object value);
    }
}