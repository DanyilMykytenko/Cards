using Microsoft.EntityFrameworkCore;
using Cards.Domain;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Persistence.EntityTypeConfiguration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(card => card.Id);
            builder.HasIndex(card => card.Id).IsUnique();
            builder.Property(card => card.Title).HasMaxLength(250);

        }
    }
}
