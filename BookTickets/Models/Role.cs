using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Role
    {
        public Role()
        {
            Khachhangs = new HashSet<Khachhang>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Mota { get; set; }

        public virtual ICollection<Khachhang> Khachhangs { get; set; }
    }
}
