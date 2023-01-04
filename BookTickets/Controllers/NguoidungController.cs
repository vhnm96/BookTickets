using BookTickets.Extension;
using BookTickets.Helpper;
using BookTickets.Models;
using BookTickets.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookTickets.Controllers
{

    public class NguoidungController : Controller
    {
        private readonly bookticketsContext _context;
        private object _notyfService;

        public NguoidungController(bookticketsContext context)
        {
            _context = context;
        }
        public IActionResult Dangnhap()
        {
            return View();
        }
       
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel taikhoan, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool Email = Utilities.IsValidEmail(taikhoan.Taikhoan);
                    if (!Email) return View(taikhoan);

                    var khachhang = _context.MaKH.SingleOrDefault(x => x.Trim() == taikhoan.Taikhoan);

                    if (khachhang == null) return RedirectToAction("DangkyTaiKhoan");
                    string pass = (taikhoan.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.Password != pass)
                    {
                        return View(taikhoan);
                    }
                    //kiem tra xem account co bi disable hay khong

                    if (khachhang.Active == false)
                    {
                        return RedirectToAction("ThongBao", "Accounts");
                    }

                    //Luu Session MaKh
                    HttpContext.Session.SetString("MaKH", khachhang.MaKH.ToString());
                    var taikhoanID = HttpContext.Session.GetString("CustomerId");

                    //Identity
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.HoTen),
                            new Claim("MaKH", khachhang.HoTen.ToString())
                        };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            catch
            {
                return RedirectToAction("DangkyTaiKhoan", "Accounts");
            }
            return View(taikhoan);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public async Task<IActionResult> DangkyTaiKhoan(RegisterViewModel taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    Khachhang khachhang = new Khachhang
                    {
                        HoTen = taikhoan.Hoten,
                        DienthoaiKh = taikhoan.DienthoaiKH.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        Matkhau = (taikhoan.Matkhau + salt.Trim()).ToMD5()
                    };
                    try
                    {
                        _context.Add(khachhang);
                        await _context.SaveChangesAsync();
                        //Lưu Session MaKh
                        HttpContext.Session.SetString("MaKH", khachhang.HoTen.ToString());
                        var taikhoanID = HttpContext.Session.GetString("MaKH");

                        //Identity
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.HoTen),
                            new Claim("MaKH", khachhang.HoTen.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        return RedirectToAction("Dashboard", "Khachhang");
                    }
                    catch
                    {
                        return RedirectToAction("DangkyTaiKhoan", "Khachhang");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }
    }
}
