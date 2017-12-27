using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using NLog;

namespace FreightManagement.ProjectValidationRule
{
    public class StringEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string convertedValue = (string)value;
            if(convertedValue == null)
            {
                _logger.Info("Value equals null!");
                return new ValidationResult(false, "Value equals null!");
            }
            if(convertedValue.Count() > 20)
            {
                _logger.Info("Length of value grater than 20 character!");
                return new ValidationResult(false, "Length of value grater than 20 character!");
            }

            return ValidationResult.ValidResult;
        }

        private static Logger _logger = LogManager.GetCurrentClassLogger();
    }
}
