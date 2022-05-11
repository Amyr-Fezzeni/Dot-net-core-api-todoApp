using Microsoft.EntityFrameworkCore;
using todoapp.models;

namespace todoapp.Data
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions options) : base(options)
        { 
        
    }
        public DbSet<Card> cards {get; set;}
        public DbSet<User> users { get; set; }
     
        
      
    }
}
