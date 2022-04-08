using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UoW.ADMIN.Helpers;
using UoW.ADMIN.Models;
using UoW.CORE.Interface;
using UoW.DATA;
using UoW.DATA.HashPassword;
using UoW.DATA.Toastr;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.LOG;
using UoW.LOG.Models;

namespace UoW.ADMIN.Controllers
{

    public class AccountController : Controller
    {

        #region Fields

        private readonly IUnitOfWork<Kullanici> _unitOfWorkKullanici;
        private readonly IUnitOfWork<Rol> _unitOfWorkYetki;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitofWorkLogYonetici;

        public AccountController(IUnitOfWork<Kullanici> unitOfWorkKullanici,
            IUnitOfWork<Rol> unitOfWorkYetki,
            IWebHostEnvironment hostEnvironment, IUnitOfWorkLog<Durumlar> unitOfWorkDurumlar,
            IUnitOfWorkLog<Loglar> unitOfWorkLoglar,
            IUnitOfWorkLog<Islemler> unitOfWorkIslemler,
            IUnitOfWorkLog<Yonetici> unitOfWorkYonetici)
        {
            this._unitOfWorkKullanici = unitOfWorkKullanici;
            this._unitOfWorkYetki = unitOfWorkYetki;
            this._hostEnvironment = hostEnvironment;
            this._unitOfWorkLogIslemler = unitOfWorkIslemler;
            this._unitOfWorkLogDurumlar = unitOfWorkDurumlar;
            this._unitOfWorkLogLoglar = unitOfWorkLoglar;
            this._unitofWorkLogYonetici = unitOfWorkYonetici;
        }

        #endregion

        #region Kullanıcı İşlemleri

        public async Task<IActionResult> Login()
        {
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "Login", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            CreateLog createLog = new CreateLog(_unitOfWorkLogDurumlar,
                _unitOfWorkLogIslemler,
                _unitOfWorkLogLoglar,
                _unitofWorkLogYonetici);

            try
            {
                if (ModelState.IsValid)
                {
                    model.password = new Crypto().TryEncrypt(model.password);
                    Kullanici isDoneLog = (Kullanici)_unitOfWorkKullanici.Repository
                   .Where(x => x.KullaniciAdi == model.username && x.Sifre == model.password).Include("Rol")
                   .SingleOrDefault();

                    if (isDoneLog != null && isDoneLog.Durum == true)
                    {
                        HttpContext.Session.SetString("username", isDoneLog.KullaniciAdi);
                        HttpContext.Session.SetInt32("Id", isDoneLog.ID);
                        HttpContext.Session.SetString("pp", isDoneLog.ProfilResim);

                        if (isDoneLog.Rol.RolAdi == "Admin")
                        {
                            HttpContext.Session.SetString("admin", isDoneLog.Rol.RolAdi);
                        }

                        await createLog.CreateLogs("Başarılı", "Kullanıcı Girişi", "Login", "Account", isDoneLog.KullaniciAdi);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await createLog.CreateLogs("Başarısız", "Kullanıcı Girişi", "Login", "Account", model.username);
                        TempData["mesaj"] = @"toastr['warning']('Giriş işleminiz başarısız. Lütfen bilgilerinizi doğru girdiğinize emin olun','Yönetici Girişi')";
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    await createLog.CreateLogs("Başarısız", "Kullanıcı Girişi", "Login", "Account", model.username);
                    TempData["mesaj"] = "toastr['warning']('Giriş işleminiz başarısız. Lütfen gerekli tüm alanları doldurun','Yönetici Girişi')";
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                await createLog.CreateLogs("Sistem Hatası", "Kullanıcı Girişi", "Login", "Account", model.username);
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Yönetici Girişi')";
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> LogOut()
        {
            await CreateModeratorLog("Başarılı", "Kullanıcı Çıkışı", "LogOut", "Account");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("admin");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [CheckRole]
        public async Task<IActionResult> ProfileDetail(int id)
        {

            var isHaveUser = await _unitOfWorkKullanici.Repository.GetById(id);
            if (isHaveUser != null)
            {
                Kullanici kullanici = _unitOfWorkKullanici
                    .Repository
                    .Where(x => x.ID == isHaveUser.ID)
                    .Include("Rol").SingleOrDefault();

                await CreateModeratorLog("Başarılı", "Detay", "ProfileDetail", "Account");
                return View(kullanici);
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Kullanıcı verisi alınırken bir hata meydana geldi','Hata')";
                await CreateModeratorLog("Başarısız", "Detay", "ProfileDetail", "Account");
                return View("Index");
            }
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> EditProfile(int id)
        {

            UserEditViewModel uevm = new UserEditViewModel();
            var isHaveUser = await _unitOfWorkKullanici.Repository.GetById(id);
            if (isHaveUser != null)
            {
                Kullanici user = _unitOfWorkKullanici
                    .Repository
                    .Where(x => x.ID == isHaveUser.ID)
                    .Include("Rol").SingleOrDefault();

                user.Sifre = new Crypto().TryDecrypt(user.Sifre);
                uevm.ID = user.ID;
                uevm.KullaniciAdi = user.KullaniciAdi;
                uevm.Sifre = user.Sifre;
                uevm.SifreTekrar = user.Sifre;
                uevm.YetkiID = user.RolID;
                uevm.yetkiKullanici = user.Rol;
                uevm.ProfilResim = user.ProfilResim;

                List<Rol> yetkiler = (List<Rol>)await _unitOfWorkYetki.Repository.All();
                uevm.yetkiList = yetkiler;

                ViewBag.RolID = new SelectList(uevm.yetkiList, "ID", "RolAdi", uevm.YetkiID);
                await CreateModeratorLog("Başarılı", "Sayfa Giriş", "EditProfile", "Account");
                return View(uevm);
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Kullanıcı verisi alınırken bir hata oluştu','Kullanıcı Düzenleme')";
                await CreateModeratorLog("Başarısız", "Sayfa Giriş", "EditProfile", "Account");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [CheckLogin]
        public async Task<IActionResult> EditProfile(UserEditViewModel model, int RolID, IFormFile file)
        {
           
            if (ModelState.IsValid)
            {
                Kullanici user = new Kullanici();
                List<Rol> rols = (List<Rol>)await _unitOfWorkYetki.Repository.All();
                try
                {
                    if (model.Sifre == model.SifreTekrar)
                    {
                        model.Sifre = new Crypto().TryEncrypt(model.Sifre);
                        user.ID = model.ID;
                        user.KullaniciAdi = model.KullaniciAdi;
                        user.Sifre = model.Sifre;
                        user.GuncellenmeTarih = DateTime.Now;
                        user.RolID = RolID;
                        user.Durum = true;
                        
                        if (file != null)
                            user.ProfilResim = SaveImageProcess.ImageInsert(file, "Admin");

                        _unitOfWorkKullanici.Repository.Update(user);
                        _unitOfWorkKullanici.Repository.Save();

                        HttpContext.Session.SetString("username", user.KullaniciAdi);
                        HttpContext.Session.SetInt32("Id", user.ID);
                        HttpContext.Session.SetString("pp", user.ProfilResim);

                        TempData["mesaj"] = "toastr['success']('Kullanıcı güncelleme başarılı','Kullanıcı Düzenleme')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditProfile", "Account");
                        ViewBag.RolID = new SelectList(rols, "ID", "RolAdi", user.RolID);
                        return RedirectToAction("EditProfile", new { id = user.ID });

                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Kullanıcı güncellenirken bir hata oluştu','Kullanıcı Düzenleme')";
                        await CreateModeratorLog("Başarısız", "Güncelleme", "EditProfile", "Account");
                        ViewBag.RolID = new SelectList(rols, "ID", "RolAdi", user.RolID);
                        return RedirectToAction("EditProfile", new { id = user.ID });

                    }

                }
                catch (Exception ex)
                {
                    TempData["mesaj"] = "toastr['warning']('Kullanıcı güncellenirken sistemden kaynaklı bir hata oluştu','Kullanıcı Düzenleme')";
                    await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditProfile", "Account");
                    ViewBag.RolID = new SelectList(rols, "ID", "RolAdi", user.RolID);
                    return RedirectToAction("EditProfile", new { id = user.ID });

                }
            }
            else
            {
                return RedirectToAction("EditProfile", new { id = model.ID });
            }
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> InsertAccount()
        {
            List<Rol> rols = (List<Rol>)await _unitOfWorkYetki.Repository.All();
            ViewBag.RolID = new SelectList(rols, "ID", "RolAdi");
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "InsertAccount", "Account");
            return View();
        }

        [HttpPost]
        [CheckLogin]
        public async Task<IActionResult> InsertAccount(UserInsertViewModel modelUser, int RolID, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (modelUser.Sifre == modelUser.SifreTekrar)
                    {
                        modelUser.Sifre = new Crypto().TryEncrypt(modelUser.Sifre);

                        Kullanici newUser = new Kullanici
                        {
                            Durum = modelUser.Durum,
                            EklenmeTarih = DateTime.Now,
                            GuncellenmeTarih = DateTime.Now,
                            KullaniciAdi = modelUser.KullaniciAdi,
                            Sifre = modelUser.Sifre,
                            ProfilResim = modelUser.ProfilResim,
                            RolID = RolID,
                            Rol = modelUser.yetkiKullanici,
                        };

                        if (file != null)
                            newUser.ProfilResim = SaveImageProcess.ImageInsert(file, "Admin");

                        await _unitOfWorkKullanici.Repository.Add(newUser);
                        _unitOfWorkKullanici.Repository.Save();
                        await CreateModeratorLog("Başarılı", "Ekleme", "InsertProfile", "Account");
                    }
                    else
                    {

                        TempData["mesaj"] = "toastr['warning']('Şifreler uyuşmuyor lütfen kontrol edip tekrar işleminizi tekrar gerçekleştirin','Kullanıcı Ekleme')";
                        await CreateModeratorLog("Başarısız", "Ekleme", "InsertProfile", "Account");
                        return RedirectToAction("InsertAccount", "Account");

                    }
                    TempData["mesaj"] = "toastr['success']('Kullanıcı ekleme işlemi başarılı','Kullanıcı Ekleme')";
                    await CreateModeratorLog("Başarılı", "Ekleme", "InsertProfile", "Account");
                    return RedirectToAction("InsertAccount", "Account");

                }
                else
                {
                    return RedirectToAction("InsertAccount", "Account");
                }

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu','Kullanıcı Ekleme')";
                await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertProfile", "Account");
                return RedirectToAction("InsertAccount", "Account");
            }
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ListAccount()
        {

            List<Kullanici> userList = (List<Kullanici>)await _unitOfWorkKullanici.Repository.AllByObject("Rol");
            //Bağlantılı tablonun istediğimiz değerini getiren list sorgusu
            await CreateModeratorLog("Başarılı", "Listeleme", "ListAccount", "Account");
            return View(userList);
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ProfileRemove(int id)
        {
            try
            {
                int ID = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                Kullanici getUserLogin = (Kullanici)await _unitOfWorkKullanici.Repository.GetById(ID);
                if (getUserLogin.RolID == 1)
                {
                    if (id > 0)
                    {
                        getUserLogin.KullaniciAdi = HttpContext.Session.GetString("username");
                        Kullanici getUserRemove = await _unitOfWorkKullanici.Repository.GetById(id);
                        _unitOfWorkKullanici.Repository.Delete(getUserRemove);
                        _unitOfWorkKullanici.Repository.Save();
                        if (getUserRemove.KullaniciAdi == getUserLogin.KullaniciAdi)
                        {
                            HttpContext.Session.Remove("username");
                            await CreateModeratorLog("Başarılı", "Silme", "ProfileRemove", "Account");
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            TempData["mesaj"] = "toastr['success']('Kullanıcı başarıyla kaldırıldı','Kullanıcı Silme')";
                            await CreateModeratorLog("Başarılı", "Silme", "ProfileRemove", "Account");
                            return RedirectToAction("ListAccount", "Account");

                        }
                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Kullanıcı verisi alınamadı','Kullanıcı Silme')";
                        await CreateModeratorLog("Başarısız", "Silme", "ProfileRemove", "Account");
                        return RedirectToAction("ListAccount", "Account");
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Bu işlemi gerçekleştirme yetkiniz bulunmuyor','Kullanıcı Silme')";
                    await CreateModeratorLog("Başarısız", "Silme", "ProfileRemove", "Account");
                    return RedirectToAction("ListAccount", "Account");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kullanıcı Silme')";
                await CreateModeratorLog("Sistem Hatası", "Silme", "ProfileRemove", "Account");
                return RedirectToAction("ListAccount", "Account");

            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ProfileEditStatus(int id)
        {
            try
            {
                if (id > 0)
                {
                    Kullanici getUser = await _unitOfWorkKullanici.Repository.GetById(id);
                    if (getUser.Durum == true)
                    {
                        getUser.Durum = false;
                        _unitOfWorkKullanici.Repository.Update(getUser);
                        _unitOfWorkKullanici.Repository.Save();

                        await CreateModeratorLog("Başarısız", "Güncelleme", "ProfileEditStatus", "Account");
                        return RedirectToAction("ListAccount", "Account");

                    }
                    else
                    {
                        getUser.Durum = true;
                        _unitOfWorkKullanici.Repository.Update(getUser);
                        _unitOfWorkKullanici.Repository.Save();

                        await CreateModeratorLog("Başarılı", "Güncelleme", "ProfileEditStatus", "Account");
                        return RedirectToAction("ListAccount", "Account");

                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Kullanıcı verisi alınamadı','Kullanıcı Durum Düzenle')";
                    await CreateModeratorLog("Başarısız", "Güncelleme", "ProfileEditStatus", "Account");
                    return RedirectToAction("ListAccount", "Account");

                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kullanıcı Durum Düzenle')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "ProfileEditStatus", "Account");
                return RedirectToAction("ListAccount", "Account");

            }
        }

        #endregion

        #region Yetki İşlemleri

        [CheckLogin]
        [HttpGet]
        [CheckRole]
        public async Task<IActionResult> Roles()
        {
            List<Rol> rolList = (List<Rol>)await _unitOfWorkYetki.Repository.All();
            await CreateModeratorLog("Başarılı", "Listeleme", "Roles", "Account");
            return View(rolList);
        }

        [CheckLogin]
        [CheckRole]
        public IActionResult InsertRole()
        {
            return View();
        }

        [CheckLogin]
        [HttpPost]
        public async Task<IActionResult> InsertRole(Rol rol)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    rol.Durum = true;
                    rol.GuncellenmeTarih = DateTime.Now;
                    rol.EklenmeTarih = DateTime.Now;


                    await _unitOfWorkYetki.Repository.Add(rol);
                    _unitOfWorkYetki.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Yetki ekleme başarılı','Yetki Ekleme')";
                    await CreateModeratorLog("Başarılı", "Ekleme", "InsertRole", "Account");
                    return RedirectToAction("InsertRole", "Account");

                }
                catch (Exception)
                {
                    TempData["mesaj"] = "toastr['waning']('Yetki ekleme işlemi sırasında sistemden kaynaklı bir hata oluştu','Yetki Ekleme')";
                    await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertRole", "Account");
                    return RedirectToAction("InsertRole", "Account");
                }
            }
            else
            {
                return RedirectToAction("InsertRole", "Account");
            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> RoleRemove(int id)
        {
            try
            {
                Rol rol = await _unitOfWorkYetki.Repository.GetById(id);
                _unitOfWorkYetki.Repository.Delete(rol);
                _unitOfWorkYetki.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Yetki silme başarılı','Yetki Silme')";
                await CreateModeratorLog("Başarılı", "Silme", "RemoveRole", "Account");
                return RedirectToAction("Roles", "Account");
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Yetki Silme')";
                await CreateModeratorLog("Sistem Hatası", "Silme", "RemoveRole", "Account");
                return RedirectToAction("Roles", "Account");
            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> RoleEditStatus(int id)
        {
            try
            {

                Rol rol = await _unitOfWorkYetki.Repository.GetById(id);
             
                if (rol.Durum == true)
                {
                    rol.Durum = false;
                    _unitOfWorkYetki.Repository.Update(rol);
                    _unitOfWorkYetki.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Yetki pasif hale getirildi','Yetki Durum Düzenleme')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "RoleEditStatus", "Account");
                    return RedirectToAction("Roles", "Account");
                }
                else
                {
                    rol.Durum = true;
                    _unitOfWorkYetki.Repository.Update(rol);
                    _unitOfWorkYetki.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Yetki aktif hale getirildi','Yetki Durum Düzenleme')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "RoleEditStatus", "Account");
                    return RedirectToAction("Roles", "Account");
                }

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['success']('Sistemden kaynaklı bir hata meydana geldi','Yetki Durum Düzenleme')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "RoleEditStatus", "Account");
                return RedirectToAction("Roles", "Account");
            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> UserListByRole(int id)
        {
            List<Kullanici> users = await _unitOfWorkKullanici.Repository.Where(x => x.RolID == id).ToListAsync();
            await CreateModeratorLog("Başarılı", "Listeleme", "UserListByRole", "Account");
            return PartialView(users);
        }

        #endregion

        public async Task<CreateLog> CreateModeratorLog(string durum, string islem, string action, string controller)
        {
            string kullaniciAdi = HttpContext.Session.GetString("username");
            CreateLog ct = new CreateLog(_unitOfWorkLogDurumlar, _unitOfWorkLogIslemler, _unitOfWorkLogLoglar, _unitofWorkLogYonetici);
            if (kullaniciAdi == "" || kullaniciAdi == null)
            {
                await ct.CreateLogs(durum, islem, action, controller, "Sistem");
                return ct;
            }
            await ct.CreateLogs(durum, islem, action, controller, kullaniciAdi);
            return ct;
        }

    }
}