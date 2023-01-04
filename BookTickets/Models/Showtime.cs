using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Showtime
    {
        public Showtime()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdShowtime { get; set; }
        public int? IdMovie { get; set; }
        public int? IdRoom { get; set; }
        public DateTime? Thoigianchieu { get; set; }
        public double? Price { get; set; }

        public virtual Movie IdMovieNavigation { get; set; }
        public virtual Room IdRoomNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
