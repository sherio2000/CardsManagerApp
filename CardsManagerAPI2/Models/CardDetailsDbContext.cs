using Microsoft.EntityFrameworkCore;

namespace CardsManagerAPI2.Models
{
    public class CardDetailsDbContext : DbContext
    {
        public CardDetailsDbContext(DbContextOptions<CardDetailsDbContext> options) : base(options)
        {

        }

        public DbSet<CardDetails> PaymentDetails { get; set; }
    }
}
