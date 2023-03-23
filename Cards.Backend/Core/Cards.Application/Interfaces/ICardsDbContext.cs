using System;
using Microsoft.EntityFrameworkCore;
using Cards.Domain;
using System.Collections.Generic;

namespace Cards.Application.Interfaces
{
    public interface ICardsDbContext
    {
        DbSet<Card> Cards { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
