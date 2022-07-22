using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class CardDbContext:DbContext
    {
        public CardDbContext(DbContextOptions options):base(options)
        {

        }

        //DbSet
        public DbSet<Card> Cards { get; set; }
    }
}
