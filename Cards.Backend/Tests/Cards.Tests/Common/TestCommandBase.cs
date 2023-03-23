using Cards.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly CardsDbContext context;
        public TestCommandBase()
        {
            context = CardsContextFactory.Create();
        }
        public void Dispose() 
        {
            CardsContextFactory.Destroy(context);
        }
    }
}
