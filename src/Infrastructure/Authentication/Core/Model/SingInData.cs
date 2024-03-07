using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Core.Model
{
    public record SingInData
    {
        public TokenModel Token { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!; 

        public bool IsExternalLogin => !string.IsNullOrEmpty(ExternalAuthenticationProvider);
        public string? ExternalAuthenticationProvider { get; init; }

    }
}
