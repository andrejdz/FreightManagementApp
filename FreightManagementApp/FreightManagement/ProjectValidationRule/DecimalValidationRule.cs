using System.Globalization;
using System.Windows.Controls;

namespace FreightManagement.ProjectValidationRule
{
    public class DecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(!decimal.TryParse((string)value, NumberStyles.AllowDecimalPoint,
                null, out convertedValue))
            {
                return new ValidationResult(false, $"Must be decimal number!");
            }
            if(convertedValue > decimal.MaxValue && convertedValue <= decimal.Zero)
            {
                return new ValidationResult(false, $"Value must be grater than {decimal.Zero}" +
                    $" and less than {decimal.MaxValue}");
            }

            return ValidationResult.ValidResult;
        }

        private decimal convertedValue;
    }
}
