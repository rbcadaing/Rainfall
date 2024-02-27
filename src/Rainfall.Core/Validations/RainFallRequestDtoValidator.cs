using FluentValidation;
using Rainfall.Web.Model;

namespace Rainfall.Core.Validations
{
    public class RainFallRequestDtoValidator : AbstractValidator<Web.Model.RainFallRequestDto>
    {
        public RainFallRequestDtoValidator()
        {
            RuleFor(r => r._view)
                .Must(v =>
                 {
                     if (v == null || v == string.Empty || v.ToLower() == "full")
                     {
                         return true;
                     }
                     else return false;
                 }).WithMessage("allowed values are either empty string or 'full'");
            RuleFor(r => r._limit).NotEmpty().Must(isPositiveNumber).WithMessage("Allowed values are positive numbers only");
            RuleFor(r => r._offset).NotEmpty().Must(isPositiveNumber).WithMessage("Allowed values are positive numbers only");
        }

        public static bool isPositiveNumber(string value)
        {
            int _ = 0;
            var _val = int.TryParse(value, out _);
            if (_val == false)
            {
                return false;
            }
            else
            {
                if (_ >= 0)
                { 
                    return true; 
                }
                else
                { 
                    return false; 
                }
            }
        }
    }
}
