using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Oders = new HashSet<Oder>();
        }

        public int MaKh { get; set; }
        public string HoTen { get; set; }
        public string Taikhoan { get; set; }
        public string Matkhau { get; set; }
        public string Email { get; set; }
        public string DiachiKh { get; set; }
        public string DienthoaiKh { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Oder> Oders { get; set; }
    }
}
