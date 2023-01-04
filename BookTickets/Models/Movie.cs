using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Movie
    {

        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public int IdMovie { get; set; }
        public string NameMovie { get; set; }
        public string Mota { get; set; }
        public string Anhbia { get; set; }
        public int? IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
