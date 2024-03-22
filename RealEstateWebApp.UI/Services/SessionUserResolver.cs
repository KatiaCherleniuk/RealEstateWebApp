using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using RealEstateWebApp.Business.Identity;
using Microsoft.AspNetCore.Http;

namespace RealEstateWebApp.UI.Services
{
    public class SessionUserResolver : ISessionUserResolver
    {
        public bool IsAuthorized => _currentUser != null;
        public SessionUserModel User => _currentUser;

        private readonly SessionUserModel _currentUser;

        public SessionUserResolver(IHttpContextAccessor httpContextAccessor)
        {
            var httpUser = httpContextAccessor.HttpContext?.User;
            if (httpUser == null || !httpUser.Identity.IsAuthenticated)
                return;
            var claims = httpContextAccessor.HttpContext?.User.Claims;
            _currentUser = new SessionUserModel()
            {
                Id = int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
                Name = claims.First(c => c.Type == ClaimTypes.Name).Value,
                RoleName = claims.First(c => c.Type == ClaimTypes.Role).Value,
                Email = claims.First(c => c.Type == ClaimTypes.Email).Value
            };
        }
    }
}