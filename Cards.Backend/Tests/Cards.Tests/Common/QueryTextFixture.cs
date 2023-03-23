using AutoMapper;
using Cards.Application.Common.Mappings;
using Cards.Persistence;
using Cards.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests.Common
{
    public class QueryTextFixture : IDisposable
    {
        public CardsDbContext context;
        public IMapper mapper;

        public QueryTextFixture()
        {
            context = CardsContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(ICardsDbContext).Assembly));
            });
            mapper = configurationProvider.CreateMapper();
        }
        public void Dispose() 
        {
            CardsContextFactory.Destroy(context);
        }
    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTextFixture> { }
}
