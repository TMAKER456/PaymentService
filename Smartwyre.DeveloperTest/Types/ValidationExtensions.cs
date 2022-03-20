using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smartwyre.DeveloperTest.Types
{
    public static class ValidationExtensions
    {
        public static bool IsValid<T>(this T validateableObject) => validateableObject.IsValid(out _);

        public static bool IsValid<T>(this T validateableObject, out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            if (validateableObject is not null)
            {
                var validationContext = new ValidationContext(validateableObject, null, null);
                bool isValid = Validator.TryValidateObject(validateableObject, validationContext, validationResults);
                return isValid;
            }
            else
            {
                validationResults.Add(new ValidationResult("Null object"));
                return false;
            }
        }
    }
}
