using System;

namespace Cards.Application.Cards.Queries.GetCardList
{
    public class CardListVm
    {
        public IList<CardLookupDto> Cards { get; set; }
    }
}
