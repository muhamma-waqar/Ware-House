using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Mass
{
    public record Mass
    {
        [Required]
        public int Id { get; set; }
        public float Value { get; set; }

        [Required]
        public MassUnit Unit { get; set; } = default!;
        private Mass() { }

        public Mass(float value, MassUnit unit) 
        {
            if(value < 0) throw new ArgumentException("Value cannot be negative", nameof(value));

            Value = value;
            Unit = unit;
        }

        public Mass ConvertTo(MassUnit newUnit)
        {
            if (newUnit == Unit) return this;
            return new Mass(value: Value* Unit.ConversionRateToGram / newUnit.ConversionRateToGram, unit:newUnit);
        }

        public override string ToString() => $"{Value:n} {Unit.Symbol}";
    }
}
