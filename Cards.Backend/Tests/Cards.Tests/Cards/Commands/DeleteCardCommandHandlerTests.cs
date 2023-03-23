using Cards.Application.Cards.Commands.CreateCard;
using Cards.Application.Cards.Commands.DeleteCard;
using Cards.Application.Common.Exceptions;
using Cards.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests.Cards.Commands
{
    public class DeleteCardCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteCardCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteCardCommandHandler(context);

            //Act
            await handler.Handle(new DeleteCardCommand
            {
                Id = CardsContextFactory.CardIdForDelete,
                UserId = CardsContextFactory.UserAId
            }, CancellationToken.None);

            //Assert
            Assert.Null(context.Cards.SingleOrDefault(card =>
                card.Id == CardsContextFactory.CardIdForDelete));
        }
        [Fact]
        public async Task DeleteCardCommandHandler_FailOnWrongId()
        {
            //Arrange 
            var handler = new DeleteCardCommandHandler(context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCardCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = CardsContextFactory.UserAId
                    },
                    CancellationToken.None));
        }
        [Fact]
        public async Task DeleteCardCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var deleteHandler = new DeleteCardCommandHandler(context);
            var createHandler = new CreateCardCommandHandler(context);
            var cardId = await createHandler.Handle( 
                new CreateCardCommand
                {
                    Title = "CardTitle",
                    Details = "CardDetails",
                    UserId = CardsContextFactory.UserAId,
                }, CancellationToken.None);
            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteCardCommand
                    {
                        Id = cardId,
                        UserId = CardsContextFactory.UserBId
                    }, CancellationToken.None));

        }
    }
}
