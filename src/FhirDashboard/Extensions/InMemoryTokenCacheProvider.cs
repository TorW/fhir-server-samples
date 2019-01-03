// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;

namespace Microsoft.AspNetCore.Authentication
{
    /// <summary>
    /// Provides an implementation of <see cref="ITokenCacheProvider"/> for a cookie based token cache implementation
    /// </summary>
    public class InMemoryTokenCacheProvider : ITokenCacheProvider
    {
        private InMemoryTokenCacheHelper helper;
        private IMemoryCache memoryCache;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache"></param>
        public InMemoryTokenCacheProvider(IMemoryCache cache)
        {
            memoryCache = cache;
        }

        /// <summary>
        /// Get an MSAL.NET Token cache from the HttpContext, and possibly the AuthenticationProperties and Cookies sign-in scheme
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <param name="authenticationProperties">Authentication properties</param>
        /// <param name="signInScheme">Sign-in scheme</param>
        /// <returns>A token cache to use in the application</returns>
        public TokenCache GetCache(HttpContext httpContext, ClaimsPrincipal claimsPrincipal, AuthenticationProperties authenticationProperties, string signInScheme)
        {
            string userId = claimsPrincipal.GetMsalAccountId();
            helper = new InMemoryTokenCacheHelper(userId, httpContext, memoryCache);
            return helper.GetMsalCacheInstance();
        }
    }
}
