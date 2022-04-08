using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{
    public class HaberController : Controller
    {

        #region Fields

        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<HaberKategori> _unitOfWorkHaberKategori;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitofWorkLogYonetici;
        private readonly IUnitOfWork<Kullanici> _unitOfWorkAdmin;

        public HaberController(IUnitOfWork<Haber> unitOfWorkHaber,
      IUnitOfWork<HaberKategori> unitOfWorkHaberKategori,
      IWebHostEnvironment hostEnvironment, IUnitOfWorkLog<Durumlar> unitOfWorkDurumlar,
      IUnitOfWorkLog<Loglar> unitOfWorkLoglar,
      IUnitOfWorkLog<Islemler> unitOfWorkIslemler,
      IUnitOfWorkLog<Yonetici> unitOfWorkYonetici,
      IUnitOfWork<Kullanici> unitOfWorkAdmin)
        {
            this._unitOfWorkHaber = unitOfWorkHaber;
            this._unitOfWorkHaberKategori = unitOfWorkHaberKategori;
            this._hostEnvironment = hostEnvironment;
            this._unitOfWorkLogIslemler = unitOfWorkIslemler;
            this._unitOfWorkLogDurumlar = unitOfWorkDurumlar;
            this._unitOfWorkLogLoglar = unitOfWorkLoglar;
            this._unitofWorkLogYonetici = unitOfWorkYonetici;
            this._unitOfWorkAdmin = unitOfWorkAdmin;
        }


        #endregion

        #region HaberKategori

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ListNewsCategory()
        {
            List<HaberKategori> categoriesNews = (List<HaberKategori>)await _unitOfWorkHaberKategori.Repository.Where(x => x.ID > 0).Include("kullanici").ToListAsync();
            await CreateModeratorLog("Başarılı", "Listeleme", "ListNewsCategory", "Haber");
            return View(categoriesNews);
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public IActionResult InsertNewsCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertNewsCategory(HaberKategori newsCategory)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                newsCategory.GuncellenmeTarih = DateTime.Now;
                newsCategory.EklenmeTarih = DateTime.Now;
                newsCategory.AtkifMi = true;
                newsCategory.KullaniciID = getUser.ID;
                newsCategory.kullanici = getUser;

                await _unitOfWorkHaberKategori.Repository.Add(newsCategory);
                _unitOfWorkHaberKategori.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Haber Kategorisi başarıyla eklendi','Haber Kategorisi ekleme')";
                await CreateModeratorLog("Başarılı", "Ekleme", "InsertNewsCategory", "Haber");
                return RedirectToAction("ListNewsCategory", "Haber");

            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kategori Ekleme')";
                await CreateModeratorLog("Başarısız", "Ekleme", "InsertNewsCategory", "Haber");
                return RedirectToAction("ListNewsCategory", "Haber");
            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> UpdateNewsCategory(int Id)
        {
            HaberKategori newsCategory = await _unitOfWorkHaberKategori.Repository.GetById(Id);
            return View(newsCategory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNewsCategory(HaberKategori newsCategory)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                newsCategory.GuncellenmeTarih = DateTime.Now;
                newsCategory.AtkifMi = true;
                newsCategory.KullaniciID = getUser.ID;
                newsCategory.kullanici = getUser;

                _unitOfWorkHaberKategori.Repository.Update(newsCategory);
                _unitOfWorkHaberKategori.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Haber Kategorisi başarıyla güncellendi','Haber Kategorisi Güncelleme')";
                await CreateModeratorLog("Başarılı", "Güncelleme", "UpdateNewsCategory", "Haber");
                return RedirectToAction("ListNewsCategory", "Haber");

            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kategori Ekleme')";
                await CreateModeratorLog("Başarısız", "Güncelleme", "UpdateNewsCategory", "Haber");
                return RedirectToAction("ListNewsCategory", "Haber");
            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> RemoveNewsCategory(int id)
        {
            try
            {
                HaberKategori getNewsCategory = (HaberKategori)await _unitOfWorkHaberKategori.Repository.GetById(id);
                if (getNewsCategory != null)
                {
                    _unitOfWorkHaberKategori.Repository.Delete(getNewsCategory);
                    _unitOfWorkHaberKategori.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Haber Kategorisi başarıyla silindi','Kategori Silme')";
                    await CreateModeratorLog("Başarılı", "Silme", "RemoveNewsCategory", "Urun");
                    return RedirectToAction("ListNewsCategory", "Haber");
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Haber Kategori verisi alınamadı','Kategori Silme')";
                    await CreateModeratorLog("Başarısız", "Silme", "RemoveCategory", "Urun");
                    return RedirectToAction("ListNewsCategory", "Haber");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Haber Kategori silinirken sorun oluştu','Kategori Silme')";
                await CreateModeratorLog("Sistem Hatası", "Silme", "RemoveCategory", "Urun");
                return RedirectToAction("CatListNewsCategoryegories", "Haber");
            }
        } //OK

        [CheckRole]
        [CheckLogin]
        public async Task<IActionResult> EditStatusNewsCategory(int id)
        {
            try
            {
                HaberKategori getNewsCategory = (HaberKategori)await _unitOfWorkHaberKategori.Repository.GetById(id);
                if (getNewsCategory != null)
                {
                    if (getNewsCategory.AtkifMi == true)
                    {
                        getNewsCategory.AtkifMi = false;
                        _unitOfWorkHaberKategori.Repository.Update(getNewsCategory);
                        _unitOfWorkHaberKategori.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Haber Kategorisi pasifleştirme işlemi başarılı!','Kategori Durum')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusNewsCategory", "Urun");
                        return RedirectToAction("ListNewsCategory", "Haber");
                    }
                    else
                    {
                        getNewsCategory.AtkifMi = true;
                        _unitOfWorkHaberKategori.Repository.Update(getNewsCategory);
                        _unitOfWorkHaberKategori.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Haber Kategorisi aktifleştirme işlemi başarılı!','Kategori Durum')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusNewsCategory", "Urun");
                        return RedirectToAction("ListNewsCategory", "Haber");
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Haber Kategorisi verisi alınamadı','Kategori Durum')";
                    await CreateModeratorLog("Başarısız", "Güncelleme", "EditStatusNewsCategory", "Haber");
                    return RedirectToAction("ListNewsCategory", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kategori Durum')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditStatusNewsCategory", "Haber");
                return RedirectToAction("ListNewsCategory", "Urun");
            }
        } //OK

        [CheckRole]
        public async Task<IActionResult> DetailsNewsCategory(int Id, int? page)
        {
            HaberKategori getNewsCategory = await _unitOfWorkHaberKategori.Repository.GetById(Id);

            int pageNumber = page ?? 1;
            ViewBag.Haberler = await _unitOfWorkHaber.Repository.Where(x => x.ID > 0).Include("kategori").Include("kullanici").OrderByDescending(x => x.EklenmeTarih).ToPagedListAsync(pageNumber, 20);

            return View(getNewsCategory);
        }

        #endregion

        #region Haber

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> ListNews(int? page) 
        {
            var categoryList = await _unitOfWorkHaberKategori.Repository.All();
            ViewBag.KategoriID = new SelectList(categoryList, "ID", "HaberKategoriAdi");

            int pageNumber = page ?? 1;
            ViewBag.Haberler = await _unitOfWorkHaber.Repository.Where(x => x.ID > 0).Include("kategori").Include("kullanici").OrderByDescending(x=> x.EklenmeTarih).ToPagedListAsync(pageNumber, 20);
            return View();
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> ListNewsByCategoryID(int? page, int KategoriID)
        {
            var categoryList = await _unitOfWorkHaberKategori.Repository.All();
            ViewBag.KategoriID = new SelectList(categoryList, "ID", "HaberKategoriAdi");

            int pageNumber = page ?? 1;
            ViewBag.Haberler = await _unitOfWorkHaber.Repository.Where(x => x.KategoriID > KategoriID).Include("kategori").Include("kullanici").OrderByDescending(x => x.EklenmeTarih).ToPagedListAsync(pageNumber, 20);
            return View();
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> InsertNews()
        {
            var categoryList = await _unitOfWorkHaberKategori.Repository.All();
            ViewBag.KategoriID = new SelectList(categoryList, "ID", "HaberKategoriAdi");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertNews(Haber news, int KategoriID, IFormFile file, IFormFile detailFile)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                Haber newNews = new Haber();
                newNews.AktifMi = true;
                newNews.KategoriID = KategoriID;
                newNews.KullaniciID = getUser.ID;
                newNews.EklenmeTarih = DateTime.Now;
                newNews.GuncellenmeTarih = DateTime.Now;
                newNews.HaberAdi = news.HaberAdi;
                newNews.HaberSpot = news.HaberSpot;
                newNews.Icerik = news.Icerik;

                if (file != null)
                    newNews.HaberOnizlemeFoto = SaveImageProcess.ImageInsert(file, "Bayi");

                if (detailFile != null)
                    newNews.HaberFoto = SaveImageProcess.ImageInsert(detailFile, "Bayi");

                await _unitOfWorkHaber.Repository.Add(newNews);
                _unitOfWorkHaber.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Haber Başarıyla eklendi!','Haber Ekleme')";
                return RedirectToAction("ListNews", "Haber");
            }

            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Haber Ekleme')";
                return RedirectToAction("ListNews", "Haber");
            }

        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> RemoveNews(int Id)
        {
            try
            {
                Haber getNews = await _unitOfWorkHaber.Repository.GetById(Id);
                _unitOfWorkHaber.Repository.Delete(getNews);
                _unitOfWorkHaber.Repository.Save();
                TempData["mesaj"] = "toastr['success']('Haber başarıyla silindi','Silme İşlemi')";
                return RedirectToAction("ListNews", "Haber");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu!','Silme İşlemi')";
                return RedirectToAction("ListNews", "Haber");
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> EditStatusNews(int Id)
        {
            try
            {
                Haber getNews = await _unitOfWorkHaber.Repository.GetById(Id);
                if (getNews.AktifMi == true)
                {
                    getNews.AktifMi = false;
                    _unitOfWorkHaber.Repository.Update(getNews);
                    _unitOfWorkHaber.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Haber paşarıyla pasifleştirildi','Durum Düzenl')";
                    return RedirectToAction("ListNews", "Haber");
                }
                else
                {
                    getNews.AktifMi = true;
                    _unitOfWorkHaber.Repository.Update(getNews);
                    _unitOfWorkHaber.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Haber paşarıyla aktifleştirildi','Durum Düzenl')";
                    return RedirectToAction("ListNews", "Haber");
                }
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Durum Düzenl')";
                return RedirectToAction("ListNews", "Haber");
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> DetailsNews(int Id)
        {
            Haber getNews = await _unitOfWorkHaber.Repository.Where(x => x.ID == Id).Include("kategori").SingleOrDefaultAsync();
            return View(getNews);
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> UpdateNews(int Id)
        {
            Haber getNews = await _unitOfWorkHaber.Repository.GetById(Id);
            var categoryList = await _unitOfWorkHaberKategori.Repository.All();
            ViewBag.KategoriID = new SelectList(categoryList, "ID", "HaberKategoriAdi", getNews.KategoriID);
            return View(getNews);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNews(Haber news, int KategoriID, IFormFile file, IFormFile DetailFile) 
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                Haber getHaber = await _unitOfWorkHaber.Repository.GetById(news.ID);
                getHaber.HaberAdi = news.HaberAdi;
                getHaber.HaberSpot = news.HaberSpot;
                getHaber.Icerik = news.Icerik;
                getHaber.KategoriID = KategoriID;
                getHaber.GuncellenmeTarih = DateTime.Now;
                getHaber.KullaniciID = getUser.ID;

                if (file != null)
                    getHaber.HaberOnizlemeFoto = SaveImageProcess.ImageInsert(file, "Bayi");
                else

                if (DetailFile != null)
                    getHaber.HaberFoto = SaveImageProcess.ImageInsert(DetailFile, "Bayi");
                else

                _unitOfWorkHaber.Repository.Update(getHaber);
                _unitOfWorkHaber.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Haber başarıyla güncellendi','Haber Güncelleme')";
                return RedirectToAction("ListNews","Haber");
            }

            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Sistem Hatası')";
                return RedirectToAction("ListNews","Haber");
            }
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