using Domain;
using FluentValidation;

namespace Application.Activities
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            this.RuleFor(x => x.Title)
                .NotEmpty();
            this.RuleFor(x => x.Description)
                .NotEmpty();
            this.RuleFor(x => x.Date)
                .NotEmpty();
            this.RuleFor(x => x.City)
                .NotEmpty();
            this.RuleFor(x => x.Category)
                .NotEmpty();
            this.RuleFor(x => x.Venue)
                .NotEmpty();
        }
    }
}