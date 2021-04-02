using System;
using Microsoft.EntityFrameworkCore;

namespace CoolQuotes.Models
{
    public class QuotesContext : DbContext
    {
        public QuotesContext(DbContextOptions<QuotesContext> options) : base(options)
        { }

        public DbSet<QuoteList> QuoteLists { get; set; }
    }
}
