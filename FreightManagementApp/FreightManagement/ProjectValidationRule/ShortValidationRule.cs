using NLog;
using System.Globalization;
using System.Windows.Controls;

namespace FreightManagement.ProjectValidationRule
{
    public class ShortValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(!short.TryParse((string)value, out _convertedValue))
            {
                _logger.Warn("Must be an integer number!");
                return new ValidationResult(false, "Must be an integer number!");
            }
            if(_convertedValue > short.MaxValue && _convertedValue <= 0)
            {
                _logger.Warn($"Value must be grater than 0" +
                    $" and less than {short.MaxValue}");
                return new ValidationResult(false, $"Value must be grater than 0" +
                    $" and less than {short.MaxValue}");
            }

            return ValidationResult.ValidResult;
        }

        private short _convertedValue;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
    }
}
