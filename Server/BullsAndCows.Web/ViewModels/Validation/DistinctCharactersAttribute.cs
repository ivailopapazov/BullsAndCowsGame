namespace BullsAndCows.Web.ViewModels.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class DistinctCharactersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string characters = (string)validationContext
                .ObjectType
                .GetProperty(validationContext.MemberName)
                .GetValue(validationContext.ObjectInstance);

            bool isValid = characters.Distinct().Count() == characters.Length;

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("All characters must be distinct!");
            }
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var characters = (string)value;
                var isValid = characters.Distinct().Count() == characters.Length;

                return isValid;
            }

            return base.IsValid(value);
        }
    }
}