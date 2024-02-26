using FluentValidation;
using Rainfall.Web.Model;

namespace Rainfall.Core.Validations
{
    public class RainFallQueryDtoValidator : AbstractValidator<RainFallRequestDto>
    {
        public RainFallQueryDtoValidator()
        {
            RuleFor(r => r.view).NotNull()
                .Must(v => v?.ToLower() == "full")
                .WithMessage("allowed values are either empty string or 'full'");

            RuleFor(r => r.limit).GreaterThan(0);
            RuleFor(r => r.offset).GreaterThan(0);
        }
    }
}
