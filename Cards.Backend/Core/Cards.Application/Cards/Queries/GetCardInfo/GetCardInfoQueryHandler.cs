using AutoMapper;
using Cards.Application.Common.Exceptions;
using Cards.Application.Interfaces;
using Cards.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Cards.Queries.GetCardInfo
{
    public class GetCardInfoQueryHandler : IRequestHandler<GetCardInfoQuery, CardInfoVm>
    {
        private readonly ICardsDbContext _context;
        private readonly IMapper _mapper;

        public GetCardInfoQueryHandler(ICardsDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<CardInfoVm> Handle(GetCardInfoQuery request,
            CancellationToken cancellationToken)
        {
            var entity = _context.Cards.FirstOrDefault(
                card => card.Id == request.Id);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Card), request.Id);
            }

            return _mapper.Map<CardInfoVm>(entity);
        }
    }
}
