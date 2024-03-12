using Infrastructure.Authentication.External.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.External.Services
{
    public interface IExternalAuthenticationVerifier
    {
        Task<(bool success, ExternalUserData? userData)> Verify(ExternalAuthenticaitonProvider provider, string idToken);
    }
}
