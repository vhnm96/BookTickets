using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Oder
    {
        public Oder()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdOder { get; set; }
        public bool? Dathanhtoan { get; set; }
        public DateTime? Ngaydat { get; set; }
        public int? MaKh { get; set; }

        public virtual Khachhang MaKhNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
