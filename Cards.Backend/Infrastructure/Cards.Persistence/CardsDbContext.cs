using Microsoft.EntityFrameworkCore;
using System;
using Cards.Persistence.EntityTypeConfiguration;
using Cards.Application.Interfaces;
using Cards.Domain;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Cards.Persistence
{
    public class CardsDbContext : DbContext, ICardsDbContext
    {
        public DbSet<Card> Cards { get; set; }
        public CardsDbContext(DbContextOptions<CardsDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CardConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
