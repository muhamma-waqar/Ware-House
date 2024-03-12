﻿using Application.Common.Dependencies.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPI.Authentication.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private const string DefaultNonUserMoniker = "System";
        private const string UknownUserMoniker = "Anonymous";

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            if(httpContextAccessor.HttpContext == null)
            {
                UserId = DefaultNonUserMoniker;
            }
            else
            {
                UserId = httpContextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
            }
        }

        public string UserId { get; }
    }
}
