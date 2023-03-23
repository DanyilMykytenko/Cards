using Cards.Application.Interfaces;
using MediatR;
using Cards.Domain;
using Cards.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand>
    {
        private readonly ICardsDbContext _context;
        public UpdateCardCommandHandler(ICardsDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(UpdateCardCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.Cards.FirstOrDefaultAsync(
                card => card.Id == request.Id);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Card), request.Id);
            }

            entity.Title = request.Title;
            entity.Details = request.Details;
            entity.EditDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
