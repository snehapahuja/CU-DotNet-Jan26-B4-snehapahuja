using CourseAPI.DTO;
using FluentValidation;

namespace CourseAPI.Validators
{
    public class UpdateValidator : AbstractValidator<UpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Price).GreaterThan(0);

            RuleFor(x => x.Summary).NotEmpty();
        }
    }
}
