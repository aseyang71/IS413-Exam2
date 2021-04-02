using System;
using System.ComponentModel.DataAnnotations;

namespace CoolQuotes.Models
{
    public class QuoteList
    {
        [Key]
        public int QuoteID { get; set; }

        [Required]
        public string Quote { get; set; }

        [Required]
        public string Speaker { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Subject { get; set; }

        public string Citation { get; set; } 
    }
}
