using System.ComponentModel.DataAnnotations;

namespace ContainerStar.API.Validation
{
    public class RequiredAttribute: System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = base.IsValid(value, validationContext);
            
            if(result != null)
                result.ErrorMessage = "required";

            return result;
        }
    }
}
