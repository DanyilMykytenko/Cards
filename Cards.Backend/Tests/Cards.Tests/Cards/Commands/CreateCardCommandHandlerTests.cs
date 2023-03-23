using Cards.Application.Cards.Commands.CreateCard;
using Cards.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests.Cards.Commands
{
    public class CreateCardCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateCardCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateCardCommandHandler(context);
            var cardName = "card name";
            var cardDetails = "card details";

            //Act
            var cardId = await handler.Handle(
                new CreateCardCommand
                {
                    Title = cardName,
                    Details = cardDetails,
                    UserId = CardsContextFactory.UserAId
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(
                await context.Cards.SingleOrDefaultAsync(card =>
                    card.Id == cardId && card.Title == cardName &&
                    card.Details == cardDetails));
        }
    }
}
