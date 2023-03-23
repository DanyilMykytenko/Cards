using AutoMapper;
using Cards.Application.Cards.Queries.GetCardList;
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
    public class GetCardListQueryHandlerTests
    {
        private readonly CardsDbContext context;
        private readonly IMapper mapper;

        public GetCardListQueryHandlerTests(QueryTextFixture fixture) =>
            (this.context, this.mapper) = (fixture.context, fixture.mapper);

        [Fact]
        public async Task GetCardListQueryHandler_Success()
        {
            //Arrange
            var handler = new GetCardListQueryHandler(context, mapper);

            //Act
            var result = await handler.Handle(
                new GetCardListQuery
                {
                    UserId = CardsContextFactory.UserAId
                }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CardListVm>();
            result.Cards.Count.ShouldBe(2);
        }
    }
}
