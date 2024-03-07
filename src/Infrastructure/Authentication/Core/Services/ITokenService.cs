using Infrastructure.Authentication.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Core.Services
{
    public interface ITokenService
    {
        TokenModel CreateAuthenticationToken(string  UserId, string uniqueName, IEnumerable<(string claimType, string claimValue)>? customClaim = null);
    }
}
