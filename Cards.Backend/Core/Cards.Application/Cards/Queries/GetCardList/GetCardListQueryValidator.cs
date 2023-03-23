using System;
using FluentValidation;

namespace Cards.Application.Cards.Queries.GetCardList
{
    public class GetCardListQueryValidator : AbstractValidator<GetCardListQuery>
    {
        public GetCardListQueryValidator() 
        {
            RuleFor(cardList => cardList.UserId).NotEqual(Guid.Empty);
        }
    }
}
