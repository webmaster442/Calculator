//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Calculator.Configuration.Validators;

internal sealed class FolderExists : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string str)
        {
            if (string.IsNullOrEmpty(str))
                return new ValidationResult($"{validationContext.MemberName} can't be null or empty");

            if (!Directory.Exists(str))
                return new ValidationResult($"{validationContext.MemberName} contains a Directory name that doesn't exist: {str}");

            return ValidationResult.Success;
        }
        return new ValidationResult($"{validationContext.MemberName} is not a string type");
    }
}
