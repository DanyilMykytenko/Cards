using Cards.Application.Cards.Commands.UpdateCard;
using Cards.Application.Common.Exceptions;
using Cards.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests.Cards.Commands
{
    public class UpdateCardCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateCardCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateCardCommandHandler(context);
            var updatedTitle = "newwwwww title";

            //Act
            await handler.Handle(
                new UpdateCardCommand
                {
                    Id = CardsContextFactory.CardIdForUpdate,
                    UserId = CardsContextFactory.UserBId,
                    Title = updatedTitle
                }, CancellationToken.None);

            //Assert
            Assert.NotNull(await context.Cards.SingleOrDefaultAsync(card =>
                card.Id == CardsContextFactory.CardIdForUpdate &&
                card.Title == updatedTitle));
        }
        [Fact]
        public async Task UpdateCardCommandHandlerTests_FailOnWrongId()
        {
            //Arrange
            var handler = new UpdateCardCommandHandler(context);
            var updatedTitle = "newwww title";

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCardCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = CardsContextFactory.UserAId
                    }, CancellationToken.None));
        }
        [Fact]
        public async Task UpdateCardCommandHandlerTests_FailOnWrongUserId()
        {
            //Arrange
            var handler = new UpdateCardCommandHandler(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCardCommand
                    {
                        Id = CardsContextFactory.CardIdForUpdate,
                        UserId = CardsContextFactory.UserAId
                    }, CancellationToken.None));
        }
    }
}
