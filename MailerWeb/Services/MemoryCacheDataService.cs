﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MailerWeb.Services
{
    public class MemoryCacheDataService : IMemoryCacheDataService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _options;

        public MemoryCacheDataService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(600)
            };
        }

        public void Set(object key, object value, MemoryCacheEntryOptions options)
        {
            _memoryCache.Set(key, value, options);
        }

        public void Set(object key, object value)
        {
            _memoryCache.Set(key, value, _options);
        }

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
        }


        public bool TryGetValue(object key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
