using System;
using FluentValidation;

namespace Cards.Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator() 
        {
            RuleFor(updateCardCommand 
                => updateCardCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateCardCommand 
                => updateCardCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateCardCommand
                => updateCardCommand.Title).NotEmpty().MaximumLength(250);
        }
    }
}
