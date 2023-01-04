using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Ticket
    {
        public int IdTicket { get; set; }
        public int? IdOder { get; set; }
        public int? IdShowtime { get; set; }
        public decimal? Dongia { get; set; }

        public virtual Oder IdOderNavigation { get; set; }
        public virtual Showtime IdShowtimeNavigation { get; set; }
    }
}
