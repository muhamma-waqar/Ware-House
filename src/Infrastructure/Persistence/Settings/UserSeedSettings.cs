using Infrastructure.Common.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Settings
{
    class UserSeedSettings
    {
        [MemberNotNullWhen(true, nameof(DefaultUsername), nameof(DefaultPassword))]

        public bool SeedDefaultUser { get; init; }
        [RequiredIf(nameof(SeedDefaultUser),true)]
        public string? DefaultUsername { get; init; }
        [RequiredIf(nameof(SeedDefaultUser), true)]
        public string? DefaultPassword { get; init;}
        public string DefaultEmail { get; init; }
    }
}
