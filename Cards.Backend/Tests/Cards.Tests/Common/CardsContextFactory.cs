using Cards.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards.Domain;

namespace Cards.Tests.Common
{
    public class CardsContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid CardIdForDelete = Guid.NewGuid();
        public static Guid CardIdForUpdate = Guid.NewGuid();

        public static CardsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CardsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new CardsDbContext(options);
            context.Database.EnsureCreated();
            context.AddRange(
                new Card
                {
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("C6265434-F216-40A7-A37A-0D49D7C55F2F"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Card
                {
                    CreationDate = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Id = Guid.Parse("07668CD9-7B85-41D1-996F-F8B6CC78DDC9"),
                    Title = "Title2",
                    UserId = UserBId
                },
                new Card
                {
                    CreationDate = DateTime.Today,
                    Details = "Details3",
                    EditDate = null,
                    Id = CardIdForDelete,
                    Title = "Title3",
                    UserId = UserAId
                },
                new Card
                {
                    CreationDate = DateTime.Today,
                    Details = "Details4",
                    EditDate = null,
                    Id = CardIdForUpdate,
                    Title = "Title4",
                    UserId = UserBId
                }
            );
            context.SaveChanges();
            return context;
        }
        public static void Destroy(CardsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
