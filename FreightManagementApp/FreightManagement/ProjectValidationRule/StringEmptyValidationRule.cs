using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace FreightManagement.ProjectValidationRule
{
    public class StringEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string convertedValue = (string)value;
            if(convertedValue == null)
            {
                return new ValidationResult(false, $"Value equals null!");
            }
            if(convertedValue.Count() > 20)
            {
                return new ValidationResult(false, $"Length of value grater than 20 character!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
