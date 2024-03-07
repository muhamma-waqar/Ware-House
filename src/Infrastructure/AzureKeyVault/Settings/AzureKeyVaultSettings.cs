using Infrastructure.Common.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AzureKeyVault.Settings
{
    internal class AzureKeyVaultSettings
    {
        [RequiredIf(nameof(AddToConfiguration), true)]
        public string? ServiceUrl { get; init; }

        [MemberNotNullWhen(true, nameof(ServiceUrl))]
        public bool AddToConfiguration { get; init; }
    }
}
