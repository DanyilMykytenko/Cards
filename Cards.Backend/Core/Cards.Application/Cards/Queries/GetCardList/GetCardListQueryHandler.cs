using System;
using System.Collections.Generic;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Cards.Application.Cards.Queries.GetCardList
{
    public class GetCardListQueryHandler : IRequestHandler<GetCardListQuery, CardListVm>
    {
        private readonly ICardsDbContext _context;
        private readonly IMapper _mapper;

        public GetCardListQueryHandler(ICardsDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<CardListVm> Handle(GetCardListQuery request, CancellationToken cancellationToken)
        {
            var cardsQuery = await _context.Cards.
                Where(card => !request.UserId.HasValue || card.UserId == request.UserId)
                .ProjectTo<CardLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new CardListVm { Cards = cardsQuery };
        }
    }
}
