using System;
using FluentValidation;

namespace Cards.Application.Cards.Queries.GetCardInfo
{
    public class GetCardInfoQueryValidator : AbstractValidator<GetCardInfoQuery>
    {
        public GetCardInfoQueryValidator() 
        {
            RuleFor(card => card.Id).NotEqual(Guid.Empty);
            RuleFor(card => card.UserId).NotEqual(Guid.Empty);
        }
    }
}
