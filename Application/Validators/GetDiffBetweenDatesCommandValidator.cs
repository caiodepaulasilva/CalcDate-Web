using FluentValidation;
using Application.Commands;

namespace Application.Validators
{
    public class GetDiffBetweenDatesCommandValidator : AbstractValidator<GetDiffBetweenDatesCommand>
    {
        public GetDiffBetweenDatesCommandValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("EndDate is required.");
        }
    }
}
