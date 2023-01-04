using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Room
    {
        public Room()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public int IdRoom { get; set; }
        public string TenPhong { get; set; }
        public int? Soghetoida { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
