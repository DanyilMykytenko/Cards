using Cards.Application.Interfaces;
using Cards.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Cards.Commands.CreateCard
{
    public class CreateCardCommandHandler :
        IRequestHandler<CreateCardCommand, Guid>
    {
        private readonly ICardsDbContext _context;
        public CreateCardCommandHandler(ICardsDbContext context) =>
            _context = context;

        public async Task<Guid> Handle(CreateCardCommand request,
            CancellationToken cancellationToken)
        {
            var card = new Card
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync(cancellationToken);

            return card.Id;
        }
    }
}
