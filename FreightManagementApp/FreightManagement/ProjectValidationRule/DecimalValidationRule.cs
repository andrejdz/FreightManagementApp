using NLog;
using System.Globalization;
using System.Windows.Controls;

namespace FreightManagement.ProjectValidationRule
{
    public class DecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(!decimal.TryParse((string)value, NumberStyles.AllowDecimalPoint,
                null, out _convertedValue))
            {
                _logger.Warn("Must be decimal number!");
                return new ValidationResult(false, "Must be decimal number!");
            }
            if(_convertedValue > decimal.MaxValue && _convertedValue <= decimal.Zero)
            {
                _logger.Warn($"Value must be grater than {decimal.Zero}" +
                    $" and less than {decimal.MaxValue}");
                return new ValidationResult(false, $"Value must be grater than {decimal.Zero}" +
                    $" and less than {decimal.MaxValue}");
            }

            return ValidationResult.ValidResult;
        }

        private decimal _convertedValue;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
    }
}
