﻿using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace FreightManagement.ProjectValidationRule
{
    public class String20CharValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string convertedValue = (string)value;
            if(string.IsNullOrEmpty(convertedValue) || string.IsNullOrWhiteSpace(convertedValue))
            {
                return new ValidationResult(false, $"Field empty or contains only whitespaces!");
            }
            if(convertedValue.Count() > 20)
            {
                return new ValidationResult(false, $"Length of value grater than 20 character!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
