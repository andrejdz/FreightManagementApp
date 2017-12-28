using NLog;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace FreightManagement.ProjectValidationRule
{
    public class String50CharValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string convertedValue = (string)value;
            if(string.IsNullOrEmpty(convertedValue) || string.IsNullOrWhiteSpace(convertedValue))
            {
                _logger.Warn("Field empty or contains only whitespaces!");
                return new ValidationResult(false, "Field empty or contains only whitespaces!");
            }
            if(convertedValue.Count() > 50)
            {
                _logger.Warn("Length of value grater than 50 character!");
                return new ValidationResult(false, "Length of value grater than 50 character!");
            }

            return ValidationResult.ValidResult;
        }

        private static Logger _logger = LogManager.GetCurrentClassLogger();
    }
}
