using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookTickets.ModelViews
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = ("Vui lòng nhập số điện thoại"))]
        [Display(Name = "Số điện thoại")]
        [EmailAddress(ErrorMessage = "Sai định dạng số điện thoại")]
        public string Taikhoan { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        public string Password { get; set; }
    }
}
