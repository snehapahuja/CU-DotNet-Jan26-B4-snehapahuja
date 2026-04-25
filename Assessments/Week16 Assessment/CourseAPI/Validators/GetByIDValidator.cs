using CourseAPI.DTO;
using FluentValidation;

namespace CourseAPI.Validators
{
    public class GetByIDValidator : AbstractValidator<GetByIDDto>
    {
        public GetByIDValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
