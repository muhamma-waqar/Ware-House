using Infrastructure.Authentication.Core.Model;
using Infrastructure.Authentication.Core.Services;
using Infrastructure.Authentication.External.Exceptions;
using Infrastructure.Authentication.External.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.External.Services
{
    public class ExternalSignInService : IExternalSignInService
    {
        private readonly IExternalAuthenticationVerifier _verifier;
        private readonly ITokenService _tokenService;


        public ExternalSignInService(IExternalAuthenticationVerifier verifier, ITokenService tokenService)
        {
            _verifier = verifier;
            _tokenService = tokenService;
        }

        public async Task<(MySingInResult result, SingInData? data)> SingInExternal(ExternalAuthenticaitonProvider provider, string idToken)
        {
            var (success, userData) = await _verifier.Verify(provider, idToken);
            if (!success)
            {
                return (MySingInResult.Failed, null);
            }

            if(string.IsNullOrWhiteSpace(userData!.Email) || string.IsNullOrWhiteSpace(userData.FullName))
            {
                var missingFields = new List<string>();
                if (string.IsNullOrWhiteSpace(userData.Email)) missingFields.Add(nameof(ExternalUserData.Email));
                if (string.IsNullOrWhiteSpace(userData.FullName)) missingFields.Add(nameof(ExternalUserData.FullName));

                throw new ExternalAuthenticaitonInfoException(
                missingFields: missingFields,
                receivedData: userData
            );
            }


            var token = _tokenService.CreateAuthenticationToken(UserId: $"ext: {provider}:{userData.Email}",
                uniqueName: $"{userData.FullName} ({provider})");

            return (result: MySingInResult.Success,
                data: new SingInData()
                {
                    ExternalAuthenticationProvider = provider.ToString(),
                    Username = userData.FullName,
                    Email = userData.Email,
                    Token = token,
                });
        }
    }
}
