using System;
using FluentValidation;

namespace Cards.Application.Cards.Commands.CreateCard
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(createCardCommand =>
                createCardCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createCardCommand =>
                createCardCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
