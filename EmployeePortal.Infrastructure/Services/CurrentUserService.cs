using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EmployeePortal.Infrastructure.Services
{
    public class CurrentUser
    {
        internal CurrentUser(string id, string userName)
        {
            Id = id;
            UserName = userName;
        }
        public string Id { get; }
        public string UserName { get; }
    }

    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string FullNameClaim = "name";

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser? Provide()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            
            var claims = claimsPrincipal.Claims.ToArray();
            var currentUser = new CurrentUser
            (
                id: GetValueOrEmptyString(claims, ClaimTypes.NameIdentifier),
                userName: GetValueOrEmptyString(claims, FullNameClaim)
            );
            return currentUser;
        }

        private static string GetValueOrEmptyString(IEnumerable<Claim> claims, string claimName)
        {
            var claim = claims.FirstOrDefault(c => c.Type.Equals(claimName, StringComparison.InvariantCultureIgnoreCase));
            return claim == null 
                ? string.Empty 
                : claim.Value;
        }
    }
}
