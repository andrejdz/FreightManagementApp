using System.Globalization;
using System.Windows.Controls;

namespace FreightManagement.ProjectValidationRule
{
    public class ShortValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(!short.TryParse((string)value, out convertedValue))
            {
                return new ValidationResult(false, $"Must be an integer number!");
            }
            if(convertedValue > short.MaxValue && convertedValue <= 0)
            {
                return new ValidationResult(false, $"Value must be grater than 0" +
                    $" and less than {short.MaxValue}");
            }

            return ValidationResult.ValidResult;
        }

        private short convertedValue;
    }
}
