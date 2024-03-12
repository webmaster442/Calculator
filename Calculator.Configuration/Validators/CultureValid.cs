//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Calculator.Configuration.Validators;
internal sealed class CultureValid : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string str)
        {
            if (string.IsNullOrEmpty(str))
                return new ValidationResult($"{validationContext.MemberName} can't be null or empty");

            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            bool isValid = cultures.Any(x => x.Name == str
                                     || x.ThreeLetterISOLanguageName == str);

            if (!isValid)
                return new ValidationResult($"{validationContext.MemberName} contains an invalid laguage value: {str}");

            return ValidationResult.Success;
        }
        return new ValidationResult($"{validationContext.MemberName} is not a string type");
    }
}
