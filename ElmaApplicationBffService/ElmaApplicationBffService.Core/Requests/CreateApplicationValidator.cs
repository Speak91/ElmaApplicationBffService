using FluentValidation;

namespace ElmaApplicationBffService.Core.Requests
{
    public class CreateApplicationValidator : AbstractValidator<CreateApplication>
    {
        public CreateApplicationValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(250).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(250).NotEmpty();
            RuleFor(x => x.FatherName).MaximumLength(250).NotEmpty();
            RuleFor(x => x.Citizenship).NotEmpty().WithMessage("Не указано гражданство");
           
        }
    }
}
