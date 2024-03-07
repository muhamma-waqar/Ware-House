using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.External.Model
{
    public class ExternalUserData
    {
        public string Email { get; init; } = null!;
        public bool EmailVerified { get; init; }
        public string FullName { get; init; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
    }
}
