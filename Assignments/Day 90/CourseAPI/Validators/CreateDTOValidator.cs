using CourseAPI.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CourseAPI.Validators
{
    public class CreateDTOValidator : AbstractValidator<CreateDto>
    {
        public CreateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Summary)
                .NotEmpty();
        }
    }
}