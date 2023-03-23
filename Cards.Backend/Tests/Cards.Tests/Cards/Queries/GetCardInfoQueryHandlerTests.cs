using AutoMapper;
using Cards.Application.Cards.Queries.GetCardInfo;
using Cards.Persistence;
using Cards.Tests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests.Cards.Queries
{
    [Collection("QueryCollection")]
    public class GetCardInfoQueryHandlerTests
    {
        private readonly CardsDbContext context;
        private readonly IMapper mapper;

        public GetCardInfoQueryHandlerTests(QueryTextFixture fixture) =>
            (this.context, this.mapper) = (fixture.context, fixture.mapper);

        [Fact]
        public async Task GetCardInfoQueryHandler_Success()
        {
            //Arrange
            var handler = new GetCardInfoQueryHandler(context, mapper);

            //Act
            var result = await handler.Handle(
                new GetCardInfoQuery
                {
                    UserId = CardsContextFactory.UserBId,
                    Id = Guid.Parse("07668CD9-7B85-41D1-996F-F8B6CC78DDC9")
                }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CardInfoVm>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }
    }
}
