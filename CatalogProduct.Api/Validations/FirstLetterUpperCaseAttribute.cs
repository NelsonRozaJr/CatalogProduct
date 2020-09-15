using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CatalogProduct.Api.Validations
{
    public class FirstLetterUpperCaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string productName = value?.ToString();

            if (string.IsNullOrWhiteSpace(productName))
            {
                return ValidationResult.Success;
            }

            string firstLetter = productName[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula.");
            }

            return ValidationResult.Success;
        }
    }
}