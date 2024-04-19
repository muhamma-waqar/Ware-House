using Infrastructure.Common.Validation;

namespace WebAPI.Logging.Settings
{
    public class LogglySettings
    {
        [Required]
        [MemberNotNullWhen(true,nameof(CustomerToken))]
        public bool? WriteToLoggly { get; init; }

        [RequiredIf(nameof(WriteToLoggly), true)]
        public string? CustomerToken { get; set; }
    }
}
