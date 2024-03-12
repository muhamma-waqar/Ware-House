using Infrastructure.Authentication.Core.Model;
using Infrastructure.Authentication.External.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.External.Services
{
    public interface IExternalSignInService
    {
        Task<(MySingInResult result, SingInData? data)> SingInExternal(ExternalAuthenticaitonProvider provider, string idToken);
    }
}
