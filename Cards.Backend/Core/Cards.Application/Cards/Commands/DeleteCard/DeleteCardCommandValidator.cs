using System;
using FluentValidation;

namespace Cards.Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
    {
        public DeleteCardCommandValidator() 
        {
            RuleFor(deleteCardCommand
                => deleteCardCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteCardCommand
                => deleteCardCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
