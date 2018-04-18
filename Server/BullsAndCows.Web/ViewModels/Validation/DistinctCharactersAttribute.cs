namespace BullsAndCows.Web.ViewModels.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class DistinctCharactersAttribute : ValidationAttribute
    {
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