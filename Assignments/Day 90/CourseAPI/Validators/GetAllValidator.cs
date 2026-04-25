using CourseAPI.DTO;
using FluentValidation;

namespace CourseAPI.Validators
{
    public class GetAllValidator : AbstractValidator<GetAlldto>
    {
        public GetAllValidator()
        {
        }
    }
}
