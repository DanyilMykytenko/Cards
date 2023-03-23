using Cards.Application.Common.Exceptions;
using Cards.Application.Interfaces;
using Cards.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand>
    {
        private readonly ICardsDbContext _context;
        public DeleteCardCommandHandler(ICardsDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(DeleteCardCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.Cards.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Card), request.Id);
            }

            _context.Cards.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
