using MediatR;
using System;

namespace Cards.Application.Cards.Queries.GetCardList
{
    public class GetCardListQuery : IRequest<CardListVm>
    {
        public Guid? UserId { get; set; }
    }
}
