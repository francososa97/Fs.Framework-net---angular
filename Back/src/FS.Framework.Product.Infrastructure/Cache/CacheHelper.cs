﻿using FS.Framework.Product.Application.Interfaces.Cache;
using FS.Framework.Product.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace FS.Framework.Product.Infrastructure.Cache;

public class CacheHelper : ICacheHelper
{
    private readonly IMemoryCache _cache;
    private const int DefaultTtlSeconds = 60;

    public CacheHelper(IMemoryCache cache)
    {
        _cache = cache;
    }

    private static MemoryCacheEntryOptions BuildCacheOptions(int ttlSeconds)
    {
        return new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ttlSeconds),
            SlidingExpiration = TimeSpan.FromSeconds(ttlSeconds / 2)
        };
    }

    public async Task<IEnumerable<ProductModel>> GetOrSetProductsAsync(Func<Task<IEnumerable<ProductModel>>> factory)
    {
        const string key = "products:all";
        if (_cache.TryGetValue(key, out var obj) && obj is IEnumerable<ProductModel> cached)
            return cached;

        var value = await factory();
        _cache.Set(key, value, BuildCacheOptions(DefaultTtlSeconds));
        return value;
    }

    public void RemoveProductsCache()
    {
        _cache.Remove("products:all");
    }

}
