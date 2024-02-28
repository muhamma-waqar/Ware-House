using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Validation
{
    public class RequiredIfAttribute : RequiredAttribute
    {
        private readonly string _otherPropertyName;
        private readonly bool _otherPropertyValue;

        public RequiredIfAttribute(string otherPropertyName, bool otherPropertyValue)
        {
            _otherPropertyName = otherPropertyName;
            _otherPropertyValue = otherPropertyValue;
            AllowEmptyStrings = false;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext context)
        {
            if (IsValidationRequired(context))
                return base.IsValid(value, context);
            return ValidationResult.Success;
        }

        private bool IsValidationRequired(ValidationContext context)
        {
            var otherProperty = context.ObjectInstance.GetType().GetProperty(_otherPropertyName);

            if (otherProperty is null)
                throw new ArgumentException($"The specified property '{_otherPropertyName}' is not found on the validiton object.");
            if (otherProperty.PropertyType != typeof(bool) && otherProperty.PropertyType != typeof(bool?))
                throw new ArgumentException($"The specified property '{_otherPropertyName}' on the validation object must be of type bool.");

            var ifConditionSatified = otherProperty.GetValue(context.ObjectInstance) as bool? == _otherPropertyValue;

            return ifConditionSatified;
        }
    }
}
