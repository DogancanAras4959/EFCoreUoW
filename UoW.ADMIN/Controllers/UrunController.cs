using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using Newtonsoft.Json;
using OfficeOpenXml;
using UoW.ADMIN.Helpers;
using UoW.ADMIN.Models;
using UoW.CORE.Interface;
using UoW.DATA.Toastr;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{
    public class UrunController : Controller
    {

        #region Fields

        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<Kullanici> _unitOfWorkAdmin;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<UrunResimler> _unitOfWorkUrunResimler;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategori;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork<Marka> _unitofWorkMarka;
        private readonly IUnitOfWork<UrunFiyat> _unitOfWorkUrunFiyat;
        private readonly IUnitOfWork<MusteriGrupOzelUrun> _unitOfWorkMusteriOzelUrun;
        private readonly IUnitOfWork<MusteriGrubu> _unitOfWorkMusteriGrubu;
        private readonly IUnitOfWork<Etiketler> _unitOfWorkEtiketler;
        private readonly IUnitOfWork<EtiketGonderi> _unitOfWorkEtiketUrunler;
        private readonly IUnitOfWork<StokBirimi> _unitOfWorkStokBirimi;
        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitofWorkLogYonetici;
        private readonly IUnitOfWork<Slider> _unitOfWorkSlider;
        private readonly IUnitOfWork<SliderItem> _unitOfWorkSliderItem;
        private readonly IUnitOfWork<SliderBannerItem> _unitOfWorkSliderBannerItem;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IUnitOfWork<SliderUrun> _unitOfWorkSliderUrun;
        private readonly IUnitOfWork<KampanyaKoduSliderBanner> _unitOfWorkKampanyaKodu;
        private readonly IUnitOfWork<Renk> _unitOfWorkRenkler;
        private readonly IUnitOfWork<UrunRenk> _unitOfWorkUrunRenk;
        private readonly int? page;
        private readonly int? markaPage;

        public UrunController(IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<Kullanici> unitOfWorkAdmin,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<UrunResimler> unitOfWorkUrunResimler,
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<UrunFiyat> unitOfWorkUrunFiyat,
            IUnitOfWork<MusteriGrupOzelUrun> unitOfWorkMusteriOzelUrun,
            IUnitOfWork<MusteriGrubu> unitOfWorkMusteriGrubu,
            IUnitOfWork<Etiketler> unitOfWorkEtiketler,
            IUnitOfWork<EtiketGonderi> unitOfWorkEtiketUrunler,
            IUnitOfWork<StokBirimi> unitOfWorkStokBirimi,
            IWebHostEnvironment hostEnvironment,
            IUnitOfWorkLog<Durumlar> unitOfWorkLogDurumlar,
            IUnitOfWorkLog<Islemler> unitOfWorkLogIslemler,
            IUnitOfWorkLog<Loglar> unitOfWorkLogLoglar,
            IUnitOfWorkLog<Yonetici> unitofWorkLogYonetici,
            IUnitOfWork<Slider> unitOfWOrkSlider,
            IUnitOfWork<SliderItem> unitOfWOrkSliderItem,
            IUnitOfWork<SliderBannerItem> unitOfWorkSliderBannerItem,
            IConfiguration configuration,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<SliderUrun> unitOfWorkSliderUrun,
            IUnitOfWork<Renk> unitOfWorkRenkler,
            IUnitOfWork<KampanyaKoduSliderBanner> unitOfWorkKampanyaKodu,
            IUnitOfWork<UrunRenk> unitOfWorkUrunRenk)
        {
            this._unitOfWorkBayi = unitOfWorkBayi;
            this._unitOfWorkAdmin = unitOfWorkAdmin;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkKategori = unitOfWorkKategori;
            this._unitOfWorkUrunResimler = unitOfWorkUrunResimler;
            this._hostEnvironment = hostEnvironment;
            this._unitofWorkMarka = unitOfWorkMarka;
            this._unitOfWorkUrunFiyat = unitOfWorkUrunFiyat;
            this._unitOfWorkMusteriGrubu = unitOfWorkMusteriGrubu;
            this._unitOfWorkMusteriOzelUrun = unitOfWorkMusteriOzelUrun;
            this._unitOfWorkEtiketler = unitOfWorkEtiketler;
            this._unitOfWorkEtiketUrunler = unitOfWorkEtiketUrunler;
            this._unitOfWorkStokBirimi = unitOfWorkStokBirimi;
            this._configuration = configuration;
            this._unitOfWorkLogDurumlar = unitOfWorkLogDurumlar;
            this._unitOfWorkLogIslemler = unitOfWorkLogIslemler;
            this._unitOfWorkLogLoglar = unitOfWorkLogLoglar;
            this._unitofWorkLogYonetici = unitofWorkLogYonetici;
            this._unitOfWorkSlider = unitOfWOrkSlider;
            this._unitOfWorkSliderItem = unitOfWOrkSliderItem;
            this._unitOfWorkSliderBannerItem = unitOfWorkSliderBannerItem;
            this._unitOfWorkStokBildirim = unitOfWorkStokBildirim;
            this._unitOfWorkSliderUrun = unitOfWorkSliderUrun;
            this._unitOfWorkKampanyaKodu = unitOfWorkKampanyaKodu;
            this._unitOfWorkRenkler = unitOfWorkRenkler;
            this._unitOfWorkUrunRenk = unitOfWorkUrunRenk;
        }

        public IConfigurationRoot GetConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            return builder;
        }

        #endregion

        #region Kategori

        [CheckLogin]
        [CheckRole]
        [HttpGet, ActionName("Categories")]
        public async Task<IActionResult> Categories(int? page)
        {

            var pageNumber = page ?? 1;
            ViewBag.Kategoriler = _unitOfWorkKategori.Repository
                 .Where(x => x.UstKategori == 0).OrderByDescending(x => x.ID)
                 .Include("kullanici")
                 .ToPagedList(pageNumber, 10);

            List<Kategori> codeOfCategoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.All();
            ViewBag.KategoriKodu = new SelectList(codeOfCategoryList, "ID", "KategoriKodu");
            await CreateModeratorLog("Başarılı", "Listeleme", "Categories", "Urun");
            return View();
        } //OK

        [CheckLogin]
        [CheckRole]
        [HttpPost, ActionName("CategoriesFilterByCode")]
        public async Task<IActionResult> CategoriesFilterByCode(string KategoriKodu)
        {
            int id = Convert.ToInt32(KategoriKodu);
            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository
                 .Where(x => x.ID == id).OrderByDescending(x => x.ID)
                 .Include("kullanici").ToListAsync();

            List<Kategori> codeOfCategory = (List<Kategori>)await _unitOfWorkKategori.Repository.All();
            ViewBag.KategoriKodu = new SelectList(codeOfCategory, "ID", "KategoriKodu");
            await CreateModeratorLog("Başarılı", "Listeleme", "CategoriesFilterByCode", "Urun");
            return View(categoryList);
        } //OK

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        //[RoleAuthorize("Kategori Ekle")]
        public async Task<IActionResult> InsertCategory()
        {
            List<Kategori> mainCategoryList = await _unitOfWorkKategori.Repository.Where(x => x.UstKategori >= 0).ToListAsync();
            ViewBag.UstKategori = new SelectList(mainCategoryList, "ID", "KategoriAdi");
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "InsertCategory", "Urun");
            return View();
        } //OK

        [HttpPost]
        public async Task<IActionResult> InsertCategory(Kategori category, IFormFile file, int UstKategori)
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                category.GuncellenmeTarih = DateTime.Now;
                category.EklenmeTarih = DateTime.Now;
                category.Durum = true;
                category.YoneticiID = getUser.ID;
                category.UstKategori = UstKategori;
                category.kullanici = getUser;

                if (file != null)
                    category.KategoriFoto = SaveImageProcess.ImageInsert(file, "Urun");

                await _unitOfWorkKategori.Repository.Add(category);
                _unitOfWorkKategori.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Kategori başarıyla eklendi','Kategori ekleme')";
                await CreateModeratorLog("Başarılı", "Ekleme", "InsertCategory", "Urun");
                return RedirectToAction("Categories", "Urun");
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kategori Ekleme')";
                await CreateModeratorLog("Başarısız", "Ekleme", "InsertCategory", "Urun");
                return RedirectToAction("Categories", "Urun");
            }

        } //OK

        [CheckLogin]
        [HttpGet]
        [CheckRole]
        public async Task<IActionResult> EditCategory(int id)
        {
            try
            {
                Kategori getCategory = (Kategori)await _unitOfWorkKategori.Repository.GetById(id);
                if (getCategory != null)
                {
                    List<Kategori> mainCategoryList = await _unitOfWorkKategori.Repository.Where(x => x.UstKategori >= 0 && x.UstKategori < 8).ToListAsync();
                    ViewBag.UstKategori = new SelectList(mainCategoryList, "ID", "KategoriAdi");
                    return View(getCategory);
                }
                else
                {
                    TempData["mesaj"] = "toastr['success']('Kategori verisi alınamadı','Kategori Düzenleme')";
                    await CreateModeratorLog("Başarısız", "Güncelleme", "EditCategory", "Urun");
                    return RedirectToAction("Categories", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kategori Düzenleme')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditCategory", "Urun");
                return RedirectToAction("Categories", "Urun");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Kategori category, IFormFile file, int UstKategori)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                category.GuncellenmeTarih = DateTime.Now;
                category.UstKategori = UstKategori;
                category.YoneticiID = id;
                category.kullanici = getUser;
                if (file != null)
                    category.KategoriFoto = SaveImageProcess.ImageInsert(file, "Urun");

                _unitOfWorkKategori.Repository.Update(category);
                _unitOfWorkKategori.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Kategori başarıyla güncellendi','Kategori güncelleme')";
                await CreateModeratorLog("Başarılı", "Güncelleme", "EditCategory", "Urun");
                return RedirectToAction("Categories", "Urun");
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['success']('Kategori güncellenirken sorun oluştu','Kategori güncelleme')";
                return RedirectToAction("Categories", "Urun");
            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                Kategori getCategory = (Kategori)await _unitOfWorkKategori.Repository.GetById(id);
                if (getCategory != null)
                {
                    _unitOfWorkKategori.Repository.Delete(getCategory);
                    _unitOfWorkKategori.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Kategori başarıyla silindi','Kategori Silme')";
                    await CreateModeratorLog("Başarılı", "Silme", "RemoveCategory", "Urun");
                    return RedirectToAction("Categories", "Urun");
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Kategori verisi alınamadı','Kategori Silme')";
                    await CreateModeratorLog("Başarısız", "Silme", "RemoveCategory", "Urun");
                    return RedirectToAction("Categories", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Kategori silinirken sorun oluştu','Kategori Silme')";
                await CreateModeratorLog("Sistem Hatası", "Silme", "RemoveCategory", "Urun");
                return RedirectToAction("Categories", "Urun");
            }
        } //OK

        [CheckRole]
        [CheckLogin]
        public async Task<IActionResult> EditStatusCategory(int id)
        {
            try
            {
                Kategori getCategory = (Kategori)await _unitOfWorkKategori.Repository.GetById(id);
                if (getCategory != null)
                {
                    if (getCategory.Durum == true)
                    {
                        getCategory.Durum = false;
                        _unitOfWorkKategori.Repository.Update(getCategory);
                        _unitOfWorkKategori.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Kategori pasifleştirme işlemi başarılı!','Kategori Durum')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusCategory", "Urun");
                        return RedirectToAction("Categories", "Urun");
                    }
                    else
                    {
                        getCategory.Durum = true;
                        _unitOfWorkKategori.Repository.Update(getCategory);
                        _unitOfWorkKategori.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Kategori aktifleştirme işlemi başarılı!','Kategori Durum')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusCategory", "Urun");
                        return RedirectToAction("Categories", "Urun");
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Kategori verisi alınamadı','Kategori Durum')";
                    await CreateModeratorLog("Başarısız", "Güncelleme", "EditStatusCategory", "Urun");
                    return RedirectToAction("Categories", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kategori Durum')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditStatusCategory", "Urun");
                return RedirectToAction("Categories", "Urun");
            }
        } //OK

        [CheckRole]
        [CheckLogin]
        [HttpGet, ActionName("CategoriesGrid")]
        public async Task<IActionResult> CategoriesGrid(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Kategoriler = _unitOfWorkKategori.Repository
                 .Where(x => x.UstKategori == 0).OrderByDescending(x => x.ID)
                 .Include("kullanici")
                 .ToPagedList(pageNumber, 10);
            await CreateModeratorLog("Başarılı", "Listeleme", "CategoriesGrid", "Urun");
            return View();
        } //OK 

        [CheckRole]
        public async Task<IActionResult> DetailsCategory(int id)
        {
            List<Kategori> parentCategoryList = await _unitOfWorkKategori
                                                   .Repository
                                                   .Where(x => x.UstKategori == id)
                                                   .Include("kullanici")
                                                   .ToListAsync();
            ViewBag.AltKategoris = parentCategoryList;

            Kategori getCategory = await _unitOfWorkKategori.Repository.GetById(id);

            await CreateModeratorLog("Başarılı", "Detay", "DetailsCategory", "Urun");

            return View(getCategory);
        }

        #endregion

        #region Marka

        [HttpGet, ActionName("ListBrand")]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ListBrand(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Markalar = _unitofWorkMarka.Repository
                 .Where(x => x.UstMarkaID == 0)
                 .Include("kullanici").OrderByDescending(x => x.ID)
                 .ToPagedList(pageNumber, 10);
            await CreateModeratorLog("Başarılı", "Listeleme", "ListBrand", "Urun");

            return View();
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> InsertBrand()
        {
            List<Marka> mainBrandList = await _unitofWorkMarka.Repository.Where(x => x.UstMarkaID >= 0).ToListAsync();
            ViewBag.UstMarka = new SelectList(mainBrandList, "ID", "MarkaAdi");
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "InsertBrand", "Urun");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertBrand(Marka brand, int UstMarka, IFormFile file)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                Marka newBrand = new Marka
                {
                    Durum = true,
                    EklenmeTarih = DateTime.Now,
                    GuncellenmeTarih = DateTime.Now,
                    MarkaAciklama = brand.MarkaAciklama,
                    MarkaAdi = brand.MarkaAdi,
                    YoneticiID = getUser.ID,
                    kullanici = getUser,
                    UstMarkaID = UstMarka
                };

                if (file != null)
                    newBrand.MarkaResim = SaveImageProcess.ImageInsert(file, "Bayi");

                await _unitofWorkMarka.Repository.Add(newBrand);
                _unitOfWorkKategori.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Marka Ekleme Başarılı','Marka Ekleme')";
                await CreateModeratorLog("Başarılı", "Ekleme", "InsertBrand", "Urun");
                return RedirectToAction("ListBrand", "Urun");

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Marka Ekleme')";
                await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertBrand", "Urun");
                return RedirectToAction("ListBrand", "Urun");
            }
        }

        [HttpGet]
        [CheckRole]
        public async Task<IActionResult> EditBrand(int id)
        {
            Marka getBrand = await _unitofWorkMarka.Repository.GetById(id);

            List<Marka> mainBrandList = await _unitofWorkMarka.Repository.Where(x => x.UstMarkaID >= 0 && x.UstMarkaID < 8).ToListAsync();
            ViewBag.UstKategori = new SelectList(mainBrandList, "ID", "MarkaAdi");

            return View(getBrand);
        }

        [HttpPost]
        public async Task<IActionResult> EditBrand(Marka brand, int UstMarka, IFormFile file)
        {
            Marka getBrand = await _unitofWorkMarka.Repository.GetById(brand.ID);
            try
            {
                getBrand.MarkaAciklama = brand.MarkaAciklama;
                getBrand.MarkaAdi = brand.MarkaAdi;
                getBrand.GuncellenmeTarih = DateTime.Now;
                getBrand.UstMarkaID = UstMarka;

                if (file != null)
                    getBrand.MarkaResim = SaveImageProcess.ImageInsert(file, "Bayi");

                _unitofWorkMarka.Repository.Update(getBrand);
                _unitofWorkMarka.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Marka düzenleme başarılı','Marka Düzenleme')";
                await CreateModeratorLog("Başarılı", "Güncelleme", "EditBrand", "Urun");
                return RedirectToAction("ListBrand", "Urun");
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Marka Düzenleme')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditBrand", "Urun");
                return RedirectToAction("ListBrand", "Urun");
            }
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            try
            {
                Marka getBrand = await _unitofWorkMarka.Repository.GetById(id);
                _unitofWorkMarka.Repository.Delete(getBrand);
                _unitofWorkMarka.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Marka silme işlemi başarılı','Marka Silme')";
                await CreateModeratorLog("Başarılı", "Silme", "RemoveBrand", "Urun");
                return RedirectToAction("RemoveBrand", "Urun");
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Marka Silme')";
                await CreateModeratorLog("Sistem Hatası", "Silme", "RemoveBrand", "Urun");
                return RedirectToAction("RemoveBrand", "Urun");
            }
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> EditStatusBrand(int id)
        {
            try
            {
                Marka getBrand = await _unitofWorkMarka.Repository.GetById(id);

                if (getBrand.Durum == true)
                {
                    getBrand.Durum = false;
                    _unitofWorkMarka.Repository.Update(getBrand);
                    _unitofWorkMarka.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Marka pasifleştirildi','Marka Durum')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusBrand", "Urun");
                    return RedirectToAction("ListBrand", "Urun");
                }
                else
                {
                    getBrand.Durum = true;
                    _unitofWorkMarka.Repository.Update(getBrand);
                    _unitofWorkMarka.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Marka aktifleştirildi','Marka Durum')";
                    await CreateModeratorLog("Başarısız", "Güncelleme", "EditStatusBrand", "Urun");
                    return RedirectToAction("ListBrand", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Marka Durum')";
                await CreateModeratorLog("Sistem Haası", "Güncelleme", "EditStatusBrand", "Urun");
                return RedirectToAction("ListBrand", "Urun");
            }
        }

        #endregion

        #region StokBirimi

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ListUnitStock()
        {
            List<StokBirimi> unitStockList = await _unitOfWorkStokBirimi.Repository
                  .Where(x => x.ID > 0)
                  .Include("kullaniciBirim")
                  .ToListAsync();
            await CreateModeratorLog("Başarılı", "Listeleme", "ListUnitStock", "Urun");
            return View(unitStockList);
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public IActionResult InsertUnitStock()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertUnitStock(StokBirimi unitStock)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
                try
                {
                    StokBirimi newUnitStock = new StokBirimi
                    {
                        Durum = true,
                        EklenmeTarih = DateTime.Now,
                        GuncellenmeTarih = DateTime.Now,
                        BirimAdi = unitStock.BirimAdi,
                        YoneticiID = getUser.ID,
                        kullaniciBirim = getUser
                    };

                    await _unitOfWorkStokBirimi.Repository.Add(newUnitStock);
                    _unitofWorkMarka.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Stok birimi eklendi!','Stok Birimi Ekle')";
                    await CreateModeratorLog("Başarılı", "Ekleme", "InsertUnitStock", "Urun");
                    return RedirectToAction("ListUnitStocki", "Urun");
                }
                catch (Exception)
                {
                    TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Stok Birimi Ekle')";
                    await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertUnitStock", "Urun");
                    return RedirectToAction("ListUnitStocki", "Urun");

                }
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Lütfen sizden istenilen bilgileri eksiksiz doldurun','Stok Birimi Ekle')";
                await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertUnitStock", "Urun");
                return RedirectToAction("ListUnitStocki", "Urun");
            }

        }

        [HttpGet]
        [CheckRole]
        public async Task<IActionResult> EditUnitStock(int id)
        {
            StokBirimi getUnitStock = await _unitOfWorkStokBirimi.Repository.GetById(id);
            return View(getUnitStock);
        }

        [HttpPost]
        public async Task<IActionResult> EditUnitStock(StokBirimi unitStock)
        {
            if (ModelState.IsValid)
            {
                StokBirimi getUnitStock = await _unitOfWorkStokBirimi.Repository.GetById(unitStock.ID);
                try
                {
                    getUnitStock.BirimAdi = unitStock.BirimAdi;
                    getUnitStock.GuncellenmeTarih = DateTime.Now;

                    _unitOfWorkStokBirimi.Repository.Update(getUnitStock);
                    _unitOfWorkStokBirimi.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Stok birimi düzenleme başarılı!','Birim Güncelle')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "EditUnitStock", "Urun");
                    return RedirectToAction("ListUnitStocki", "Urun");
                }
                catch (Exception)
                {
                    TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Birim Güncelle')";
                    await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditUnitStock", "Urun");
                    return RedirectToAction("ListUnitStocki", "Urun");
                }
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Lütfen sizden istenilenleri eksiksiz giriniz!','Birim Güncelle')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditUnitStock", "Urun");
                return RedirectToAction("ListUnitStocki", "Urun");
            }

        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> RemoveUnitStock(int id)
        {
            try
            {
                StokBirimi getUnitStock = await _unitOfWorkStokBirimi.Repository.GetById(id);
                _unitOfWorkStokBirimi.Repository.Delete(getUnitStock);
                _unitOfWorkStokBirimi.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Birim silme işlemi başarılı!','Birim Silme')";
                await CreateModeratorLog("Başarılı", "Silme", "RemoveUnitStock", "Urun");
                return RedirectToAction("ListUnitStocki", "Urun");
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Birim Silme')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditUnitStock", "Urun");
                return RedirectToAction("ListUnitStocki", "Urun");
            }
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> EditStatusUnitStock(int id)
        {
            try
            {
                StokBirimi getUnitStock = await _unitOfWorkStokBirimi.Repository.GetById(id);

                if (getUnitStock.Durum == true)
                {
                    getUnitStock.Durum = false;
                    _unitOfWorkStokBirimi.Repository.Update(getUnitStock);

                    TempData["mesaj"] = "toastr['success']('Birim pasifleştirme başarılı!','Birim Durum')";
                    await CreateModeratorLog("Başarılı", "Silme", "EditStatusUnitStock", "Urun");
                    return RedirectToAction("ListUnitStock", "Urun");
                }
                else
                {
                    getUnitStock.Durum = true;
                    _unitOfWorkStokBirimi.Repository.Update(getUnitStock);
                    _unitOfWorkStokBirimi.Repository.Save();

                    TempData["mesaj"] = "toastr['succcess']('Birim aktifleştirme başarılı!','Birim Durum')";
                    await CreateModeratorLog("Başarılı", "Silme", "EditStatusUnitStock", "Urun");
                    return RedirectToAction("ListUnitStock", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Birim silme işlemi başarılı!','Birim Durum')";
                await CreateModeratorLog("Sistem Hatası", "Silme", "EditStatusUnitStock", "Urun");
                return RedirectToAction("ListUnitStock", "Urun");
            }
        }

        #endregion

        #region Ürün

        [CheckLogin]
        [HttpGet, ActionName("ListProduct")]
        public async Task<IActionResult> ListProduct(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
               .Where(x => x.ID > 0).OrderByDescending(x => x.ID)
               .Include("kategori")
               .Include("kullanici")
               .Include("marka")
               .Include("stokbirimi")
               .ToPagedList(pageNumber, 10);

            List<Urun> productNoList = (List<Urun>)await _unitOfWorkUrun.Repository.All();
            ViewBag.UrunNo = new SelectList(productNoList, "ID", "UrunNo");

            await CreateModeratorLog("Başarılı", "Listeleme", "ListProduct", "Urun");
            return View();
        }

        [CheckLogin]
        [HttpGet, ActionName("ListProductWithProductNo")]
        public async Task<IActionResult> ListProductWithProductNo(int? page, string UrunNo)
        {
            var pageNumber = page ?? 1;
            int id = Convert.ToInt32(UrunNo);
            ViewBag.Urunler = _unitOfWorkUrun.Repository
               .Where(x => x.ID == id).OrderByDescending(x => x.ID)
               .Include("kategori")
               .Include("kullanici")
               .Include("marka")
               .Include("stokbirimi")
               .ToPagedList(pageNumber, 10);

            List<Urun> productNoList = (List<Urun>)await _unitOfWorkUrun.Repository.All();
            ViewBag.UrunNo = new SelectList(productNoList, "ID", "UrunNo");

            await CreateModeratorLog("Başarılı", "Listeleme", "ListProductWithProductNo", "Urun");
            return View();
        }

        public async Task<IActionResult> ListProductGrid(int? page)
        {

            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.ID > 0).OrderByDescending(x => x.ID).ToListAsync();
            HttpContext.Session.SetObject("kategoriler", categoryList);

            List<Marka> brandList = (List<Marka>)await _unitofWorkMarka.Repository.Where(x => x.ID > 0).Include("urunler").OrderByDescending(x => x.ID).ToListAsync();
            ViewBag.Markalar = brandList;

            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
                .Where(x => x.ID > 0).OrderByDescending(x => x.ID)
                .Include("kategori")
                .Include("kullanici")
                .Include("marka")
                .Include("stokbirimi")
                .ToPagedList(pageNumber, 28);

            await CreateModeratorLog("Başarılı", "Listeleme", "ListProductGrid", "Urun");
            return View();
        }

        public async Task<IActionResult> ListProductWithGridFilter(int? page, int? KategoriID)
        {
            TempData["KategoriID"] = KategoriID;
            //TempData["MarkaID"] = MarkaID;

            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.ID > 0).OrderByDescending(x => x.ID).ToListAsync();
            HttpContext.Session.SetObject("kategoriler", categoryList);

            List<Marka> brandList = (List<Marka>)await _unitofWorkMarka.Repository.Where(x => x.ID > 0).OrderByDescending(x => x.ID).Include("urunler").ToListAsync();
            ViewBag.Markalar = brandList;

            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
                .Where(x => x.KategoriID == KategoriID).OrderByDescending(x => x.ID)
                .Include("kategori")
                .Include("kullanici")
                .Include("marka")
                .Include("stokbirimi")
                .ToPagedList(pageNumber, 20);

            await CreateModeratorLog("Başarılı", "Listeleme", "ListProductWithGridFilter", "Urun");

            return View("ListProductWithGridFilter");
        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> InsertProduct()
        {
            List<Marka> brandList = (List<Marka>)await _unitofWorkMarka.Repository.All();
            ViewBag.MarkaID = new SelectList(brandList, "ID", "MarkaAdi");

            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.UstKategori == 0).ToListAsync();

            ViewBag.KategoriID = new SelectList(categoryList, "ID", "KategoriAdi");

            List<StokBirimi> unitStockList = (List<StokBirimi>)await _unitOfWorkStokBirimi.Repository.All();
            ViewBag.StokBirimID = new SelectList(unitStockList, "ID", "BirimAdi");

            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "InsertProduct", "Urun");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertProduct(Urun product, int KategoriID, int AltKategoriID, int StokBirimID, int MarkaID, int AltMarkaID, int UstUrunID, string etiketler, IFormFile file, IFormFile fileDetail, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<Urun> getProductIsDoneListCheck = await _unitOfWorkUrun.Repository.Where(x => x.UrunAdi == product.UrunAdi || x.WebServisKodu == product.WebServisKodu).ToListAsync();

                    if (getProductIsDoneListCheck.Count != 0)
                    {
                        TempData["mesaj"] = "toastr['warning']('Bu ürün sistemde bulunuyor','Ürün ekleme')";
                        await CreateModeratorLog("Hata", "Ekleme", "InsertProduct", "Urun");
                        return RedirectToAction("ListProduct", "Urun");

                    }
                    else
                    {

                        int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));

                        Urun newProduct = new Urun
                        {
                            UrunAdi = product.UrunAdi,
                            WebServisKodu = product.WebServisKodu,
                            Adet = product.Adet,
                            UrunSpot = product.UrunSpot,
                            Fiyat = Convert.ToDecimal(product.Fiyat),
                            StokBirimID = StokBirimID,
                            OzelMi = false,
                            UrunBarkodNo = product.UrunBarkodNo,
                            Durum = true,
                            UrunNo = product.UrunNo,
                            YoneticiID = id,
                            Aciklama = product.Aciklama,
                            UstUrunID = UstUrunID,
                            IndirimOrani = product.IndirimOrani,
                            KDV = product.KDV
                        };
                        if (AltMarkaID == 0)
                        {
                            newProduct.MarkaID = MarkaID;
                        }
                        else
                        {
                            newProduct.MarkaID = AltMarkaID;
                        }
                        if (AltKategoriID == 0)
                        {
                            newProduct.KategoriID = KategoriID;
                        }
                        else
                        {
                            newProduct.KategoriID = AltKategoriID;
                        }

                        newProduct.KDVFiyat = CalculateKDV(newProduct.Fiyat, newProduct.KDV);
                        newProduct.MaxAdet = product.MaxAdet;
                        newProduct.MinAdet = product.MinAdet;
                        newProduct.En = product.En;
                        newProduct.Boy = product.Boy;
                        newProduct.Derinlik = product.Derinlik;
                        newProduct.Agirlik = product.Agirlik;
                       
                        if (product.Desi != 0 || product.Desi != null)
                        {
                            newProduct.Desi = product.Desi;
                        }
                        else
                        {
                            newProduct.Desi = CalculateDesi(newProduct.En, newProduct.Boy, newProduct.Derinlik);
                        }
                       

                        if (newProduct.Adet > 0)
                        {
                            newProduct.BildirimVarmi = false;
                        }

                        if (newProduct.IndirimOrani > 0)
                        {
                            newProduct.IndirimliFiyat = CalculateDiscount(newProduct.KDVFiyat, newProduct.IndirimOrani);
                        }
                        else
                        {
                            newProduct.IndirimOrani = 0;
                        }

                        if (fileDetail != null)
                            newProduct.Resim = SaveImageProcess.ImageInsert(fileDetail, "Urun");

                        if (file != null)
                            newProduct.DetayResim = SaveImageProcess.ImageInsert(file, "Urun");

                        await _unitOfWorkUrun.Repository.Add(newProduct);
                        _unitOfWorkUrun.Repository.Save();

                        int result = newProduct.ID;

                        if (!string.IsNullOrEmpty(etiketler))
                        {
                            if (etiketler[^1] == ',')
                            {
                                await InsertTagToProduct(etiketler[0..^1], result);
                            }
                            else
                            {
                                await InsertTagToProduct(etiketler, result);
                            }
                        }

                        var data = collection["AdetBaslangic"];
                        string[] datas = data.ToString().Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        var data2 = collection["AdetBitis"];
                        string[] datas2 = data2.ToString().Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        var data3 = collection["UrunFiyati"];
                        string[] datas3 = data3.ToString().Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        for (int i = 0; i < datas.Count(); i++)
                        {
                            if (datas3.Length > 0 && Convert.ToDecimal(datas3[i]) < product.KDVFiyat)
                            {
                                UrunFiyat productPrice = new UrunFiyat
                                {
                                    UrunID = newProduct.ID,
                                    AdetBaslangic = Convert.ToInt32(datas[i]),
                                    AdetBitis = Convert.ToInt32(datas2[i]),
                                    UrunFiyati = Convert.ToDecimal(datas3[i])
                                };
                                await _unitOfWorkUrunFiyat.Repository.Add(productPrice);
                                _unitOfWorkUrunFiyat.Repository.Save();
                            }
                            else
                            {
                                TempData["mesaj"] = "toastr['success']('Lütfen fiyat seçeneğini normal fiyattan yüksek girmeyin!','Ürün Ekleme')";
                                await CreateModeratorLog("Başarısız", "Ekleme", "InsertProduct", "Urun");
                                return RedirectToAction("InsertProduct", "Urun");
                            }
                        }

                        TempData["mesaj"] = "toastr['success']('Ürün başarıyla eklendi','Ürün Ekleme')";
                        await CreateModeratorLog("Başarılı", "Ekleme", "InsertProduct", "Urun");
                        return RedirectToAction("ListProduct", "Urun");
                    }
                }

                catch (Exception ex)
                {
                    TempData["mesaj"] = "toastr['warning']('Ürün eklenirken bir hata oluştu','Ürün ekleme')";
                    await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertProduct", "Urun");
                    return RedirectToAction("ListProduct", "Urun");
                }
            }
            else
            {
                TempData["mesaj"] = "toastr['success']('Ürün eklerken zorunlu alanları eksiksiz doldurun','Ürün ekleme')";
                await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertProduct", "Urun");
                return RedirectToAction("ListProduct", "Urun");
            }
        }

        //public IActionResult InsertColorToProduct(string colors, int urunId)
        //{
        //    if (colors != null)
        //    {
        //        List<UrunRenk> renk = JsonConvert.DeserializeObject<List<UrunRenk>>(colors);
        //        foreach (var item in renk)
        //        {
        //            if (renk != null)
        //            {
        //                UrunRenk newUrunRenk = new UrunRenk();
        //                newUrunRenk.Adet = item.Adet;
        //                newUrunRenk.RenkID = item.RenkID;
        //                newUrunRenk.UrunID = item.UrunID;

        //                _unitOfWorkUrunRenk.Repository.Add(newUrunRenk).GetAwaiter();
        //                _unitOfWorkUrunRenk.Repository.Save();
        //            }
        //        }
        //    }
        //    return View();
        //}

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id, int? page, string UrunNo = "")
        {
            if (UrunNo is null)
            {
                throw new ArgumentNullException(nameof(UrunNo));
            }

            Urun getProduct = await _unitOfWorkUrun.Repository.GetById(id);

            ViewBag.Kapak = true;
            List<Kategori> ListCategory = (List<Kategori>)await _unitOfWorkKategori.Repository.All();
            ViewBag.KategoriID = new SelectList(ListCategory, "ID", "KategoriAdi", getProduct.KategoriID);

            List<Marka> ListBrand = (List<Marka>)await _unitofWorkMarka.Repository.All();
            ViewBag.MarkaID = new SelectList(ListBrand, "ID", "MarkaAdi", getProduct.MarkaID);

            List<Urun> ListMainProduct = (List<Urun>)await _unitOfWorkUrun.Repository.All();
            ViewBag.UrunNo = new SelectList(ListMainProduct, "ID", "UrunNo");

            List<UrunFiyat> listProductPrice = await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == id).ToListAsync();
            ViewBag.UrunFiyatlar = listProductPrice;

            List<StokBirimi> listUnitStock = (List<StokBirimi>)await _unitOfWorkStokBirimi.Repository.All();
            ViewBag.StokBirimID = new SelectList(listUnitStock, "ID", "BirimAdi", getProduct.StokBirimID);

            ViewBag.UrunFiyatlar = (List<UrunFiyat>)await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == getProduct.ID).ToListAsync();

            List<UrunRenk> listRenk = (List<UrunRenk>)await _unitOfWorkUrunRenk.Repository.Include("renk").Where(x => x.UrunID == getProduct.ID).ToListAsync();
            ViewBag.Renkler = listRenk;

            List<Renk> renkler = (List<Renk>)await _unitOfWorkRenkler.Repository.All();
            ViewBag.RenklerListSecim = new SelectList(renkler, "ID","RenkAdi");

            TempData["data-id"] = id;

            var pageNumber = page ?? 1;
            ViewBag.Urun = await _unitOfWorkUrun.Repository
               .Where(x => x.ID > 0 && x.UstUrunID == 0).ToPagedListAsync(pageNumber, 10);

            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "EditProduct", "Urun");
            return View(getProduct);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Urun product, int KategoriID, int AltKategoriID, int StokBirimID, int MarkaID, int AltMarkaID, int RenkID, int UstUrunID, IFormFile file, IFormFile fileDetail, IFormCollection collection, int renkAdet)
        {
            Urun getProduct = await _unitOfWorkUrun.Repository.GetById(product.ID);
         
            //if (ModelState.IsValid)
            //{
                try
                {

                    if (RenkID != 0)
                    {
                        List<UrunRenk> getUrunRenk = await _unitOfWorkUrunRenk.Repository.Where(x => x.UrunID == getProduct.ID).ToListAsync();
                        int toplamRenkAdet = getUrunRenk.Select(x=> x.Adet).Sum();

                        toplamRenkAdet += renkAdet;

                        if (getProduct.Adet > toplamRenkAdet)
                        {
                            UrunRenk newUrunRenk = new UrunRenk();
                            newUrunRenk.Adet = renkAdet;
                            newUrunRenk.RenkID = RenkID;
                            newUrunRenk.UrunID = getProduct.ID;

                            _unitOfWorkUrunRenk.Repository.Add(newUrunRenk).GetAwaiter();
                            _unitOfWorkUrunRenk.Repository.Save();
                        }
                        else
                        {
                            TempData["mesaj"] = "toastr['warning']('Renk için girdiğiniz adet mevcut ürün adet sınırını aşmaktadır (Mevcut Ürün: "+getProduct.Adet+")','Ürün güncelleme')";
                            await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditProduct", "Urun");
                            VeriDoldur(getProduct);
                            return RedirectToAction("EditProduct", "Urun", new { ID = product.ID, page = page });
                        }
                    }

                    getProduct.UrunAdi = product.UrunAdi;
                    getProduct.Adet = product.Adet;
                    getProduct.UrunSpot = product.UrunSpot;
                    getProduct.Fiyat = Convert.ToDecimal(product.Fiyat);
                    getProduct.StokBirimID = StokBirimID;
                    getProduct.Etiketler = product.Etiketler;
                    getProduct.UrunBarkodNo = product.UrunBarkodNo;
                    getProduct.Durum = true;
                    getProduct.UrunNo = product.UrunNo;
                    getProduct.Aciklama = product.Aciklama;
                    getProduct.UstUrunID = UstUrunID;
                    getProduct.IndirimOrani = product.IndirimOrani;
                    getProduct.KDV = product.KDV;
                    getProduct.KDVFiyat = CalculateKDV(getProduct.Fiyat, getProduct.KDV);
                    getProduct.MaxAdet = product.MaxAdet;
                    getProduct.MinAdet = product.MinAdet;
                    getProduct.En = product.En;
                    getProduct.FiyatBirimi = product.FiyatBirimi;
                    getProduct.Boy = product.Boy;
                    getProduct.Derinlik = product.Derinlik;
                    getProduct.Agirlik = product.Agirlik;

                    if (product.Desi != 0 || product.Desi != null)
                    {
                        getProduct.Desi = product.Desi;
                    }
                    else
                    {
                        getProduct.Desi = CalculateDesi(getProduct.En, getProduct.Boy, getProduct.Derinlik);
                    }

                    if (AltKategoriID == 0)
                    {
                        getProduct.KategoriID = KategoriID;
                    }
                    else
                    {
                        getProduct.KategoriID = AltKategoriID;
                    }

                    if (AltMarkaID == 0)
                    {
                        getProduct.MarkaID = MarkaID;
                    }
                    else
                    {
                        getProduct.MarkaID = AltMarkaID;
                    }

                    if (getProduct.Adet > 0)
                    {
                        List<StokBildirim> listStokBildirim = await _unitOfWorkStokBildirim.Repository.Where(x => x.UrunId == getProduct.ID && x.Durum == CORE.Enums.StokBildirimDurum.Bekleniyor).ToListAsync();
                        foreach (StokBildirim item in listStokBildirim)
                        {
                            item.Durum = CORE.Enums.StokBildirimDurum.BildirimYapildi;
                            item.BildirimTarihi = DateTime.Now;
                            _unitOfWorkStokBildirim.Repository.Update(item);
                            _unitOfWorkStokBildirim.Repository.Save();
                        }
                    }

                    if (getProduct.IndirimOrani > 0)
                    {
                        getProduct.IndirimliFiyat = CalculateDiscount(getProduct.KDVFiyat, getProduct.IndirimOrani);
                    }
                    else
                    {
                        getProduct.IndirimOrani = 0;
                    }

                    if (file != null)
                        getProduct.DetayResim = SaveImageProcess.ImageInsert(file, "Urun");

                    if (fileDetail != null)
                        getProduct.Resim = SaveImageProcess.ImageInsert(fileDetail, "Urun");

                    _unitOfWorkUrun.Repository.Update(getProduct);
                    _unitOfWorkUrun.Repository.Save();

                    #region Ürün Fiyat

                    var data = collection["AdetBaslangic"];
                    string[] datas = data.ToString().Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                    var data2 = collection["AdetBitis"];
                    string[] datas2 = data2.ToString().Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                    var data3 = collection["UrunFiyati"];
                    string[] datas3 = data3.ToString().Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                    if (data != "" && data2 != "" && data3 != "")
                    {
                        for (int i = 0; i < datas.Count(); i++)
                        {
                            if (datas3.Length > 0 && Convert.ToDecimal(datas3[i]) < product.KDVFiyat)
                            {
                                UrunFiyat productPrice = new UrunFiyat
                                {
                                    UrunID = getProduct.ID,
                                    AdetBaslangic = Convert.ToInt32(datas[i]),
                                    AdetBitis = Convert.ToInt32(datas2[i]),
                                    UrunFiyati = Convert.ToDecimal(datas3[i])
                                };
                                await _unitOfWorkUrunFiyat.Repository.Add(productPrice);
                                _unitOfWorkUrunFiyat.Repository.Save();
                            }
                            else
                            {
                                TempData["mesaj"] = "toastr['success']('Lütfen fiyat seçeneğini normal fiyattan yüksek girmeyin!','Ürün güncelleme')";
                                await CreateModeratorLog("Başarısız", "Güncelleme", "EditProduct", "Urun");
                                VeriDoldur(getProduct);
                                return RedirectToAction("EditProduct", "Urun", new { ID = product.ID, page = page });
                            }
                        }

                        TempData["mesaj"] = "toastr['success']('Ürün başarıyla güncellendi','Ürün güncelleme')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditProduct", "Urun");
                        return View("ListProduct");
                    }

                    #endregion

                }
                catch (Exception ex)
                {
                    TempData["mesaj"] = "toastr['warning']('Ürün güncellenirken bir hata meydana geldi','Ürün güncelleme')";
                    await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditProduct", "Urun");
                    VeriDoldur(getProduct);
                    return RedirectToAction("EditProduct", "Urun", new { ID = product.ID, page = page });
                }
            //}
            //else
            //{
            //    TempData["mesaj"] = "toastr['warning']('Ürün güncellenirken bir hata meydana geldi','Ürün güncelleme')";
            //    await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditProduct", "Urun");
            //    VeriDoldur(getProduct);
            //    return RedirectToAction("EditProduct", "Urun", new { ID = product.ID, page = page });
            //}
            await VeriDoldur(getProduct);
            return RedirectToAction("EditProduct", "Urun", new { ID = product.ID, page = page });
        }

        [CheckLogin]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                Urun getProduct = await _unitOfWorkUrun.Repository.GetById(id);

                List<EtiketGonderi> tagProduct = (List<EtiketGonderi>)await _unitOfWorkEtiketUrunler.Repository.Where(x => x.GonderiID == getProduct.ID).ToListAsync();
                foreach (EtiketGonderi item in tagProduct)
                {
                    _unitOfWorkEtiketUrunler.Repository.Delete(item);
                    _unitOfWorkEtiketUrunler.Repository.Save();
                }

                _unitOfWorkUrun.Repository.Delete(getProduct);
                _unitOfWorkUrun.Repository.Save();

                TempData["mesaj"] = "toastr[success]('Ürün başarıyla silindi!','Ürün Silme')";
                await CreateModeratorLog("Başarılı", "Silme", "RemoveProduct", "Urun");
                return RedirectToAction("ListProduct", "Urun");

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr[warning]('Sistemden kaynaklı bir hata meydana geldi','Ürün Silme')";
                await CreateModeratorLog("Sistem Hatası", "Silme", "EditProduct", "Urun");
                return RedirectToAction("ListProduct", "Urun");

            }
        }

        [CheckLogin]
        public async Task<IActionResult> EditStatusProduct(int id)
        {
            try
            {
                Urun getProduct = await _unitOfWorkUrun.Repository.GetById(id);

                if (getProduct.Durum == true)
                {
                    getProduct.Durum = false;
                    _unitOfWorkUrun.Repository.Update(getProduct);
                    _unitOfWorkUrun.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Ürün başarıyla pasifleştirildi','Ürün Pasifleştirme')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusProduct", "Urun");
                    return RedirectToAction("ListProduct", "Urun");
                }
                else
                {
                    getProduct.Durum = true;
                    _unitOfWorkUrun.Repository.Update(getProduct);
                    _unitOfWorkUrun.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Ürün başarıyla aktifleştirildi','Ürün Aktifleşirme')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusProduct", "Urun");
                    return RedirectToAction("ListProduct", "Urun");
                }

            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu','Ürün Durum Düzenleme')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditStatusProduct", "Urun");
                return RedirectToAction("ListProduct", "Urun");
            }
        }

        [CheckLogin]
        public async Task<IActionResult> DetailProduct(int id)
        {
            Urun getProduct = await _unitOfWorkUrun.Repository.Where(x => x.ID == id)
                .Include("kategori").Include("marka").Include("stokbirimi").SingleOrDefaultAsync();

            List<UrunResimler> productPhotos = await _unitOfWorkUrunResimler.Repository.Where(x => x.UrunID == getProduct.ID).ToListAsync();
            ViewBag.UrunResimler = productPhotos;

            List<UrunFiyat> productPriceList = await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == id).ToListAsync();
            ViewBag.UrunFiyat = productPriceList;

            List<Urun> parentProductList = await _unitOfWorkUrun.Repository.Where(x => x.UstUrunID == getProduct.ID).ToListAsync();
            ViewBag.AltUrunler = parentProductList;

            List<EtiketGonderi> tagProductList = await _unitOfWorkEtiketUrunler.Repository.Where(x => x.GonderiID == getProduct.ID).Include("etiketler").ToListAsync();
            ViewBag.Etiketler = tagProductList;

            await CreateModeratorLog("Başarılı", "Detay", "DetailProduct", "Urun");
            return View(getProduct);
        }

        [HttpPost]
        public async Task<IActionResult> InsertProductPhoto(int ID, List<IFormFile> file)
        {
            Urun getProduct = await _unitOfWorkUrun.Repository.GetById(ID);

            if (file == null || file.Count == 0)
            {
                TempData["mesaj"] = "toastr['warning']('Ürün fotosu seçmediğiniz için ekleme yapılamadı!','Ürün Fotosu Ekleme')";
                await CreateModeratorLog("Başarısız", "Ekleme", "InsertProductPhoto", "Urun");
                return RedirectToAction("DetailProduct", "Urun", new { ID = getProduct.ID });
            }
            else
            {
                long size = file.Sum(f => f.Length);

                foreach (var formFile in file)
                {
                    if (formFile.Length > 0)
                    {
                        UrunResimler productPhoto = new UrunResimler
                        {
                            ResimYol = SaveImageProcess.ImageInsert(formFile, "Urun"),
                            UrunID = getProduct.ID,
                            urun = getProduct
                        };

                        await _unitOfWorkUrunResimler.Repository.Add(productPhoto);
                        _unitOfWorkUrunResimler.Repository.Save();
                    }
                }

                TempData["mesaj"] = "toastr['success']('Ürün fotosu başarıyla eklendi!','Ürün Fotosu Ekleme')";
                await CreateModeratorLog("Başarılı", "Ekleme", "InsertProductPhoto", "Urun");
                return RedirectToAction("DetailProduct", "Urun", new { ID = getProduct.ID });
            }
        }

        public async Task<IActionResult> ExportProduct()
        {
            List<Urun> productList = (List<Urun>)await _unitOfWorkUrun.Repository.Where(x => x.ID > 0).Take(20).ToListAsync();
            ViewBag.Urunler = productList;
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "ExportProduct", "Urun");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExportProductExcel()
        {
            try
            {
                using XLWorkbook workbook = new XLWorkbook();
                string time = DateTime.Now.ToString("yymmssfff");
                string sheetName = $"ÜRÜNLER_{time}";
                string fileName = $"urunler_{time}.xlsx";
                List<Urun> productList = (List<Urun>)await _unitOfWorkUrun.Repository.Where(x => x.ID > 0).Include("kategori").Include("marka").ToListAsync();
                var worksheet = workbook.Worksheets.Add(sheetName);
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "UrunAdi";
                worksheet.Cell(currentRow, 3).Value = "UrunNo";
                worksheet.Cell(currentRow, 4).Value = "UrunBarkodNo";
                worksheet.Cell(currentRow, 5).Value = "KDV";
                worksheet.Cell(currentRow, 6).Value = "Fiyat";
                worksheet.Cell(currentRow, 7).Value = "KDVFiyat";
                worksheet.Cell(currentRow, 8).Value = "IndirimliFiyat";
                worksheet.Cell(currentRow, 9).Value = "IndirimOrani";
                worksheet.Cell(currentRow, 10).Value = "Adet";
                worksheet.Cell(currentRow, 11).Value = "Agirlik";
                worksheet.Cell(currentRow, 12).Value = "Derinlik";
                worksheet.Cell(currentRow, 13).Value = "Boy";
                worksheet.Cell(currentRow, 14).Value = "Desi";
                worksheet.Cell(currentRow, 15).Value = "En";
                worksheet.Cell(currentRow, 16).Value = "Kategori";
                worksheet.Cell(currentRow, 17).Value = "Marka";
                worksheet.Cell(currentRow, 18).Value = "WebServisKodu";
                foreach (var user in productList)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.ID;
                    worksheet.Cell(currentRow, 2).Value = user.UrunAdi;
                    worksheet.Cell(currentRow, 3).Value = user.UrunNo;
                    worksheet.Cell(currentRow, 4).Value = user.UrunBarkodNo;
                    worksheet.Cell(currentRow, 5).Value = user.KDV;
                    worksheet.Cell(currentRow, 6).Value = user.Fiyat;
                    worksheet.Cell(currentRow, 7).Value = user.KDVFiyat;
                    worksheet.Cell(currentRow, 8).Value = user.IndirimliFiyat;
                    worksheet.Cell(currentRow, 9).Value = user.IndirimOrani;
                    worksheet.Cell(currentRow, 10).Value = user.Adet;
                    worksheet.Cell(currentRow, 11).Value = user.Agirlik;
                    worksheet.Cell(currentRow, 12).Value = user.Derinlik;
                    worksheet.Cell(currentRow, 13).Value = user.Boy;
                    worksheet.Cell(currentRow, 14).Value = user.Desi;
                    worksheet.Cell(currentRow, 15).Value = user.En;
                    worksheet.Cell(currentRow, 16).Value = user.kategori.KategoriAdi;
                    worksheet.Cell(currentRow, 17).Value = user.marka.MarkaAdi;
                    worksheet.Cell(currentRow, 18).Value = user.WebServisKodu;
                }

                using MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(
                    content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Ürünler dışa aktarılırken bir hata oluştu!','Ürünleri Dışa Akar')";
                return RedirectToAction("ExportProduct", "Urun");
            }
           
        }

        public async Task<IActionResult> InsertProductWithExcel()
        {
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "InsertProductWithExcel", "Urun");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertProductWithExcel(IFormFile file)
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
            try
            {
                if (file == null || file.Length == 0)
                {
                    await InsertProductWithExcel();
                    TempData["mesaj"] = "toastr['success']('Dosya verisi bulunamadı!', 'Ürün İçe Aktarma')";
                    await CreateModeratorLog("Başarısız", "Ekleme", "InsertProductWithExcel", "Urun");
                    return RedirectToAction("InsertProductWithExcel", "Urun");
                }
                else
                {
                    var newfile = new FileInfo(file.FileName);
                    var fileExtension = newfile.Extension;

                    if (fileExtension.Contains(".xls"))
                    {
                        using MemoryStream ms = new MemoryStream();
                        await file.CopyToAsync(ms);

                        using ExcelPackage package = new ExcelPackage(ms);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["urunler"];
                        int totalRows = workSheet.Dimension.Rows;

                        for (int i = 2; i <= totalRows; i++)
                        {
                            ProductViewModel productVM = new ProductViewModel();

                            productVM.ID = Convert.ToInt32(workSheet.Cells[i, 1].Value);
                            productVM.UrunAdi = workSheet.Cells[i, 2].Value.ToString();
                            productVM.UrunNo = workSheet.Cells[i, 3].Value.ToString();
                            productVM.Barkod = workSheet.Cells[i, 4].Value.ToString();
                            productVM.KDV = Convert.ToDecimal(workSheet.Cells[i, 5].Value);
                            productVM.Fiyat = Convert.ToDecimal(workSheet.Cells[i, 6].Value);
                            productVM.KDVFiyat = Convert.ToDecimal(workSheet.Cells[i, 7].Value);
                            productVM.IndirimliFiyat = Convert.ToDecimal(workSheet.Cells[i, 8].Value);
                            productVM.IndirimOrani = Convert.ToDecimal(workSheet.Cells[i, 9].Value);
                            productVM.Adet = Convert.ToInt32(workSheet.Cells[i, 10].Value);
                            productVM.Agirlik = Convert.ToDouble(workSheet.Cells[i, 11].Value);
                            productVM.Derinlik = Convert.ToDouble(workSheet.Cells[i, 12].Value);
                            productVM.Boy = Convert.ToDouble(workSheet.Cells[i, 13].Value);
                            productVM.Desi = Convert.ToDouble(workSheet.Cells[i, 14].Value);
                            productVM.En = Convert.ToDouble(workSheet.Cells[i, 15].Value);
                            productVM.Kategori = workSheet.Cells[i, 16].Value.ToString();
                            productVM.Marka = workSheet.Cells[i, 17].Value.ToString();
                            productVM.WebServisKodu = workSheet.Cells[i, 18].Value.ToString();

                            Kategori getCategory = await _unitOfWorkKategori.Repository.Where(x => x.KategoriAdi == productVM.Kategori).FirstOrDefaultAsync();
                            Marka getMarka = await _unitofWorkMarka.Repository.Where(x => x.MarkaAdi == productVM.Marka).FirstOrDefaultAsync();
                            Urun getProduct = await _unitOfWorkUrun.Repository.Where(x => x.ID == productVM.ID).FirstOrDefaultAsync();

                            if (getProduct == null)
                            {
                                Urun newProduct = new Urun
                                {
                                    WebServisKodu = productVM.WebServisKodu,
                                    UrunNo = productVM.UrunNo,
                                    UrunAdi = productVM.UrunAdi,
                                    Fiyat = productVM.Fiyat,
                                    UrunBarkodNo = productVM.Barkod,
                                    Adet = productVM.Adet,
                                    KDV = productVM.KDV,
                                    FiyatBirimi = productVM.FiyatBirimi,
                                    Aciklama = productVM.Aciklama,
                                    En = productVM.En,
                                    Boy = productVM.Boy,
                                    Derinlik = productVM.Derinlik,
                                    Agirlik = productVM.Agirlik,
                                    Resim = productVM.Resim,
                                    Durum = true,
                                    Etiketler = "",
                                    IndirimOrani = productVM.IndirimOrani,
                                    IndirimliFiyat = productVM.IndirimliFiyat,
                                    StokBirimID = 10,
                                    
                                };
                                newProduct.KDVFiyat = CalculateKDV(productVM.Fiyat, productVM.KDV);
                                newProduct.MaxAdet = 0;
                                newProduct.MinAdet = 0;
                                newProduct.OzelMi = false;
                                newProduct.UrunSpot = "";
                                newProduct.UstUrunID = 0;
                                newProduct.kullanici = getUser;
                                newProduct.YoneticiID = getUser.ID;

                                if (getCategory != null)
                                {
                                    newProduct.KategoriID = getCategory.ID;
                                    newProduct.kategori = getCategory;
                                }
                                else
                                {
                                    Kategori yeniKategoriGetir = await CreateCategoryWithInsertExcel();
                                    newProduct.kategori = yeniKategoriGetir;
                                    newProduct.KategoriID = yeniKategoriGetir.ID;
                                }

                                if (getMarka != null)
                                {
                                    newProduct.marka = getMarka;
                                    newProduct.MarkaID = getMarka.ID;
                                }
                                else
                                {
                                    Marka yeniMarkaGetir = await CreateBrandWithInsertExcel(productVM.Marka);
                                    newProduct.marka = yeniMarkaGetir;
                                    newProduct.MarkaID = yeniMarkaGetir.ID;
                                }

                                await _unitOfWorkUrun.Repository.Add(newProduct);
                                _unitOfWorkUrun.Repository.Save();
                            }
                            else
                            {
                                getProduct.WebServisKodu = productVM.WebServisKodu;
                                getProduct.UrunNo = productVM.UrunNo;
                                getProduct.UrunAdi = productVM.UrunAdi;
                                getProduct.Fiyat = productVM.Fiyat;
                                getProduct.UrunBarkodNo = productVM.Barkod;
                                getProduct.Adet = productVM.Adet;
                                getProduct.KDV = productVM.KDV;
                                getProduct.FiyatBirimi = productVM.FiyatBirimi;
                                getProduct.Aciklama = productVM.Aciklama;
                                getProduct.En = productVM.En;
                                getProduct.Boy = productVM.Boy;
                                getProduct.Derinlik = productVM.Derinlik;
                                getProduct.Agirlik = productVM.Agirlik;
                                getProduct.Resim = productVM.Resim;
                                getProduct.Durum = true;
                                getProduct.Etiketler = "";
                                getProduct.IndirimOrani = productVM.IndirimOrani;
                                getProduct.IndirimliFiyat = productVM.IndirimliFiyat;
                                getProduct.KDVFiyat = CalculateKDV(getProduct.Fiyat, getProduct.KDV);
                                getProduct.MaxAdet = 0;
                                getProduct.MinAdet = 0;
                                getProduct.OzelMi = false;
                                getProduct.UrunSpot = "";
                                getProduct.UstUrunID = 0;
                                getProduct.kullanici = getUser;
                                getProduct.YoneticiID = getUser.ID;

                                if (getCategory != null)
                                {
                                    getProduct.KategoriID = getCategory.ID;
                                    getProduct.kategori = getCategory;
                                }
                                else
                                {
                                    Kategori yeniKategoriGetir = await CreateCategoryWithInsertExcel();
                                    getProduct.kategori = yeniKategoriGetir;
                                    getProduct.KategoriID = yeniKategoriGetir.ID;
                                }

                                if (getMarka != null)
                                {
                                    getProduct.marka = getMarka;
                                    getProduct.MarkaID = getMarka.ID;
                                }
                                else
                                {
                                    Marka yeniMarkaGetir = await CreateBrandWithInsertExcel(productVM.Marka);
                                    getProduct.marka = yeniMarkaGetir;
                                    getProduct.MarkaID = yeniMarkaGetir.ID;
                                }


                                _unitOfWorkUrun.Repository.Update(getProduct);
                                _unitOfWorkUrun.Repository.Save();
                            }
                        }

                        TempData["mesaj"] = "toastr['success']('Ürün içe aktarma başarılı!', 'Ürün İçe Aktarma')";
                        await CreateModeratorLog("Başarılı", "Ekleme", "InsertProductWithExcel", "Urun");
                        return RedirectToAction("ListProduct", "Urun");
                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Geçerli bir uzantı giriniz!', 'Uzantı Hatası')";
                        await CreateModeratorLog("Başarısız", "Ekleme", "InsertProductWithExcel", "Urun");
                        return RedirectToAction("InsertProductWithExcel", "Urun");
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["mesaj"] = $"toastr['warning']('Sistem hatası {ex}', 'Ürün İçe Aktarma')";
                await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertProductWithExcel", "Urun");
                return RedirectToAction("InsertProductWithExcel", "Urun");
            }

        }

        [HttpGet]
        public IActionResult ProductPartialList()
        {
            return PartialView("ProductPartialList");
        }

        [HttpPost]
        public void ContextProduct(string urunList, int urunId)
        {
            string[] array = urunList.Split(',');
            foreach (var item in array)
            {
                int CurrentId = Convert.ToInt32(item);

                Urun getProduct = _unitOfWorkUrun.Repository.Where(x => x.ID == CurrentId).FirstOrDefault();
                getProduct.UstUrunID = urunId;
                _unitOfWorkUrun.Repository.Update(getProduct);
                _unitOfWorkUrun.Repository.Save();

            }
        }

        public async Task<IActionResult> DeleteOptionUnitOfPrice(int ID)
        {
            UrunFiyat getProductPrice = await _unitOfWorkUrunFiyat.Repository.GetById(ID);
            try
            {
                _unitOfWorkUrunFiyat.Repository.Delete(getProductPrice);
                _unitOfWorkUrunFiyat.Repository.Save();
                await EditProduct(getProductPrice.UrunID, page);
                return View("EditProduct");
            }
            catch (Exception)
            {
                await EditProduct(getProductPrice.UrunID, page);
                return View("EditProduct");
            }
        }

        [HttpGet]
        public JsonResult GetAltKategori(int id)
        {
            List<Kategori> altKategoriler = _unitOfWorkKategori.Repository.Where(x => x.UstKategori == id).ToList();
            return Json(altKategoriler);
        }

        [HttpGet]
        public JsonResult GetAltMarka(int id)
        {
            List<Marka> altMarkalar = _unitofWorkMarka.Repository.Where(x => x.UstMarkaID == id).ToList();
            return Json(altMarkalar);
        }

        public async Task<int> VeriDoldur(Urun getProduct)
        {
            List<Marka> ListBrand = (List<Marka>) await _unitofWorkMarka.Repository.Where(x=> x.ID > 0).ToListAsync();
            List<Kategori> ListCategory = (List<Kategori>) await _unitOfWorkKategori.Repository.Where(x=> x.ID > 0).ToListAsync();

            ViewBag.KategoriID = new SelectList(ListCategory, "ID", "KategoriAdi", getProduct.KategoriID);
            ViewBag.MarkaID = new SelectList(ListBrand, "ID", "MarkaAdi", getProduct.MarkaID);
            return 1;
        }

        #endregion

        #region Özel Ürün

        [HttpGet, ActionName("SpecialProductList")]
        public async Task<IActionResult> SpecialProductList(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkMusteriOzelUrun.Repository
                                                         .Where(x => x.urunOzelUrun.OzelMi == true).OrderByDescending(x => x.ID)
                                                         .Include("urunOzelUrun")
                                                         .Include("musteriGrubuOzelUrun")
                                                         .ToPagedList(pageNumber, 10);
            await CreateModeratorLog("Başarılı", "Listeleme", "SpecialProductList", "Urun");
            return View();
        }

        #region Eski Özel Ürün Ekleme Metotları

        //[HttpGet, ActionName("InsertSpecialProductByMusteriGrub")]
        //public async Task<IActionResult> InsertSpecialProductByMusteriGrub(int? page)
        //{

        //    List<MusteriGrubu> musteriGrubu = (List<MusteriGrubu>)await _unitOfWorkMusteriGrubgetProduct.Repository.All();
        //    List<Marka> markalar = (List<Marka>)await _unitofWorkMarka.Repository.Where(x => x.ID > 0).Include("urunler").ToListAsync();
        //    List<Kategori> kategoriler = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.UstKategori == 0).Include("urunler").ToListAsync();
        //    List<MusteriGrupOzelUrun> musteris = (List<MusteriGrupOzelUrun>)await _unitOfWorkMusteriOzelUrun.Repository.Where(x => x.ID > 0).Include("urunOzelUrun").ToListAsync();

        //    var pageNumber = page ?? 1;
        //    ViewBag.Urunler = _unitOfWorkUrun.Repository.Where(x => x.ID > 0).ToPagedList(pageNumber, 10);
        //    ViewBag.MusteriGruplar = musteriGrubu;
        //    ViewBag.Kategoriler = kategoriler;
        //    ViewBag.Markalar = markalar;
        //    ViewBag.OzelUrunler = musteris;

        //    return View();
        //}

        //[HttpGet, ActionName("InsertSpecialProductByMusteriGrubFilter")]
        //public async Task<IActionResult> InsertSpecialProductByMusteriGrubFilter(int KategoriID, int? page)
        //{
        //    List<MusteriGrubu> musteriGrubu = (List<MusteriGrubu>)await _unitOfWorkMusteriGrubu.Repository.All();
        //    List<Marka> markalar = (List<Marka>)await _unitofWorkMarka.Repository.Where(x => x.ID > 0).Include("urunler").ToListAsync();
        //    List<Kategori> kategoriler = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.UstKategori == KategoriID).Include("urunler").ToListAsync();

        //    var pageNumber = page ?? 1;
        //    ViewBag.Urunler = _unitOfWorkUrun.Repository.Where(x => x.KategoriID == KategoriID).ToPagedList(pageNumber, 10);

        //    ViewBag.MusteriGruplar = musteriGrubu;
        //    ViewBag.Kategoriler = kategoriler;
        //    ViewBag.Markalar = markalar;

        //    return View();
        //}

        //[HttpPost]
        //public async Task<JsonResult> InsertSpecialProductByMusteriGrub(OzelUrun ozelUrun, int urunid, int musteriid)
        //{
        //    try
        //    {
        //        Urun getProduct = await _unitOfWorkUrun.Repository.GetById(ozelUrun.UrunID);

        //        MusteriGrupOzelUrun gelenOzelUrun = await _unitOfWorkMusteriOzelUrun
        //            .Repository
        //            .Where(x => x.UrunID == getProduct.ID)
        //            .SingleAsync();

        //        if (gelenOzelUrun != null)
        //        {
        //            MusteriGrupOzelUrun newOzelUrun = new MusteriGrupOzelUrun();
        //            newOzelUrun.UrunID = urunid;
        //            newOzelUrun.MusteriGrupID = musteriid;
        //            newOzelUrun.Durum = true;
        //            newOzelUrun.Eklenme = DateTime.Now;

        //            await _unitOfWorkMusteriOzelUrun.Repository.Add(newOzelUrun);
        //            _unitOfWorkMusteriOzelUrun.Repository.Save();

        //            TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Özel ürün oluşturma işlemi başarılı", type = ToastrType.success };
        //            await InsertSpecialProductByMusteriGrub(page);
        //            return Json(ozelUrun);
        //        }
        //        else
        //        {
        //            TempData["mesaj"] = new ToastrMessage { Title = "Uyarı", Message = "Bu ürün zaten özel ürün kategorisinde!", type = ToastrType.warning };
        //            await InsertSpecialProductByMusteriGrub(page);
        //            return Json(ozelUrun);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi!", type = ToastrType.info };
        //        await InsertSpecialProductByMusteriGrub(page);
        //        return Json(ozelUrun);
        //    }
        //}

        #endregion

        [HttpGet, ActionName("SpecialProductCreate")]
        public async Task<IActionResult> SpecialProductCreate()
        {
            List<MusteriGrubu> customerGroupList = (List<MusteriGrubu>)await _unitOfWorkMusteriGrubu.Repository.All();
            List<Marka> brandList = (List<Marka>)await _unitofWorkMarka.Repository.Where(x => x.ID > 0).Include("urunler").ToListAsync();
            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.UstKategori == 0).Include("urunler").ToListAsync();

            ViewBag.MusteriGrubID = customerGroupList;
            ViewBag.Kategoriler = categoryList;
            ViewBag.Markalar = brandList;

            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
                                                         .Where(x => x.OzelMi == false)
                                                         .Include("kategori")
                                                         .Include("marka")
                                                         .ToPagedList(pageNumber, 10);
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "SpecialProductCreate", "Urun");

            return View();
        }

        public async Task<IActionResult> SpecialProductListFilter(int KategoriID)
        {
            List<MusteriGrubu> customerGroupList = (List<MusteriGrubu>)await _unitOfWorkMusteriGrubu.Repository.All();
            List<Marka> brandList = (List<Marka>)await _unitofWorkMarka.Repository.Where(x => x.ID > 0).Include("urunler").ToListAsync();
            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.UstKategori == 0).Include("urunler").ToListAsync();

            ViewBag.MusteriGrubID = customerGroupList;
            ViewBag.Kategoriler = categoryList;
            ViewBag.Markalar = brandList;

            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
                                                         .Where(x => x.KategoriID == KategoriID)
                                                         .Include("kategori")
                                                         .Include("marka")
                                                         .ToPagedList(pageNumber, 10);
            await CreateModeratorLog("Başarılı", "Listeleme", "SpecialProductListFilter", "Urun");
            return View();
        }

        public async Task<IActionResult> SpecialProductCreateItem(int urunId, int MusteriGrupID)
        {
            try
            {
                Urun getProduct = await _unitOfWorkUrun.Repository.GetById(urunId);
                MusteriGrubu getCustomerGroup = await _unitOfWorkMusteriGrubu.Repository.GetById(MusteriGrupID);

                MusteriGrupOzelUrun getCustomerGroupWithSpecialProduct = await _unitOfWorkMusteriOzelUrun
                    .Repository
                    .Where(x => x.UrunID == getProduct.ID)
                    .FirstOrDefaultAsync();

                if (getCustomerGroupWithSpecialProduct == null)
                {
                    MusteriGrupOzelUrun newCustomerGroupWithSpecialProduct = new MusteriGrupOzelUrun
                    {
                        UrunID = urunId,
                        MusteriGrupID = MusteriGrupID,
                        Durum = true,
                        Eklenme = DateTime.Now,
                        musteriGrubuOzelUrun = getCustomerGroup,
                        urunOzelUrun = getProduct
                    };

                    await _unitOfWorkMusteriOzelUrun.Repository.Add(newCustomerGroupWithSpecialProduct);
                    _unitOfWorkMusteriOzelUrun.Repository.Save();

                    getProduct.OzelMi = true;
                    getProduct.IndirimOrani = getCustomerGroup.IskontoOrani;
                    getProduct.IndirimliFiyat = Convert.ToDecimal(CalculateDiscount(getProduct.KDVFiyat, getProduct.IndirimOrani));
                    _unitOfWorkUrun.Repository.Update(getProduct);
                    _unitOfWorkUrun.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Özel ürün oluşturma işlemi başarılı','Özel Ürün oluşturma')";
                    await CreateModeratorLog("Başarılı", "Ekleme", "SpecialProductCreate", "Urun");
                    return RedirectToAction("SpecialProductCreate", "Urun");
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Ürün zaten özel olarak oluşturulmuş','Özel Ürün oluşturma')";
                    await CreateModeratorLog("Başarısız", "Ekleme", "SpecialProductCreate", "Urun");
                    return RedirectToAction("SpecialProductCreate", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Özel Ürün oluşturma')";
                await CreateModeratorLog("Sistem Hatası", "Ekleme", "SpecialProductCreate", "Urun");
                return RedirectToAction("SpecialProductCreate", "Urun");
            }
        }

        public async Task<IActionResult> DeleteSpecialProduct(int id)
        {
            try
            {
                MusteriGrupOzelUrun getCustomerGroupWithSpecialProduct = await _unitOfWorkMusteriOzelUrun.Repository.GetById(id);
                Urun getProduct = await _unitOfWorkUrun.Repository.GetById(getCustomerGroupWithSpecialProduct.UrunID);

                if (getCustomerGroupWithSpecialProduct != null)
                {
                    _unitOfWorkMusteriOzelUrun.Repository.Delete(getCustomerGroupWithSpecialProduct);
                    _unitOfWorkMusteriOzelUrun.Repository.Save();

                    getProduct.OzelMi = false;
                    getProduct.IndirimOrani = 0;
                    getProduct.IndirimliFiyat = 0;
                    getProduct.KDVFiyat = Convert.ToDecimal(CalculateKDV(getProduct.Fiyat, getProduct.KDV));
                    _unitOfWorkUrun.Repository.Update(getProduct);
                    _unitOfWorkUrun.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Özel ürün kaldırma işlemi başarılı','Özel Ürün kaldırma')";
                    await CreateModeratorLog("Başarısız", "Detay", "DeleteSpecialProduct", "Urun");
                    return RedirectToAction("SpecialProductList", "Urun");
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Özel ürün kaldırma işlemi başarısız','Özel Ürün kaldırma')";
                    await CreateModeratorLog("Başarısız", "Detay", "DeleteSpecialProduct", "Urun");
                    return RedirectToAction("SpecialProductList", "Urun");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Özel Ürün kaldırma')";
                await CreateModeratorLog("Sistem Hatası", "Detay", "DeleteSpecialProduct", "Urun");
                return RedirectToAction("SpecialProductList", "Urun");
            }
        }

        public async Task<IActionResult> EditStatusSpecialProduct(int id)
        {
            try
            {
                MusteriGrupOzelUrun getCustomerGroupWithSpecialProduct = await _unitOfWorkMusteriOzelUrun.Repository.GetById(id);
                if (getCustomerGroupWithSpecialProduct != null)
                {
                    if (getCustomerGroupWithSpecialProduct.Durum == true)
                    {
                        getCustomerGroupWithSpecialProduct.Durum = false;
                        _unitOfWorkMusteriOzelUrun.Repository.Update(getCustomerGroupWithSpecialProduct);
                        _unitOfWorkMusteriOzelUrun.Repository.Save();
                        TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Özel ürün pasifleştirme işlemi başarılı!", type = ToastrType.success };
                        await SpecialProductList(page);
                        await CreateModeratorLog("Başarılı", "Detay", "EditStatusSpecialProduct", "Urun");
                        return View("SpecialProductList");
                    }
                    else
                    {
                        getCustomerGroupWithSpecialProduct.Durum = true;
                        _unitOfWorkMusteriOzelUrun.Repository.Update(getCustomerGroupWithSpecialProduct);
                        _unitOfWorkMusteriOzelUrun.Repository.Save();
                        TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Özel ürün aktifleştirme işlemi başarılı!", type = ToastrType.success };
                        await SpecialProductList(page);
                        await CreateModeratorLog("Başarılı", "Detay", "EditStatusSpecialProduct", "Urun");
                        return View("SpecialProductList");
                    }
                }
                else
                {
                    TempData["mesaj"] = new ToastrMessage { Title = "Başarısız", Message = "Özel ürün verisi alınamadı!", type = ToastrType.warning };
                    await SpecialProductList(page);
                    await CreateModeratorLog("Başarısız", "Detay", "EditStatusSpecialProduct", "Urun");
                    return View("SpecialProductList");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.info };
                await SpecialProductList(page);
                await CreateModeratorLog("Sistem Hatası", "Detay", "EditStatusSpecialProduct", "Urun");
                return View("SpecialProductList");
            }
        }

        #endregion

        #region ExtendsMethots
        public decimal CalculateDiscount(decimal fiyat, decimal? indirimOrani)
        {
            decimal _indirim = (decimal)(fiyat * (indirimOrani / 100));
            decimal toplam = fiyat - _indirim;
            return toplam;
        }

        public decimal CalculateKDV(decimal fiyat, decimal kdv)
        {
            decimal _kdv = fiyat * (kdv / 100);
            decimal toplam = fiyat + _kdv;
            return toplam;
        }

        public double CalculateDesi(double en, double boy, double yukseklik)
        {
            double desi = (en * boy * yukseklik) / 3000;
            return desi;
        }

        public async Task<int> InsertTagToProduct(string etiketler, int id)
        {

            Urun gelenU = await _unitOfWorkUrun.Repository.GetById(id);
            string[] listTags = etiketler.Split(',');

            for (int i = 0; i < listTags.Count(); i++)
            {
                Etiketler e = new Etiketler
                {
                    EtiketAdi = listTags[i].Trim().ToString()
                };
                await _unitOfWorkEtiketler.Repository.Add(e);
                _unitOfWorkEtiketler.Repository.Save();
            }

            foreach (string item in listTags) //Çalışmıyor
            {
                string etiketAdi = item.Trim();
                Etiketler etiketiGetir = await _unitOfWorkEtiketler.Repository.Where(x => x.EtiketAdi == etiketAdi).FirstOrDefaultAsync();
                EtiketGonderi eg = new EtiketGonderi
                {
                    GonderiID = id,
                    EtiketID = etiketiGetir.ID,
                    etiketler = etiketiGetir,
                    urunler = gelenU
                };

                await _unitOfWorkEtiketUrunler.Repository.Add(eg);
                _unitOfWorkEtiketUrunler.Repository.Save();
            }

            return 1;
        }

        public async Task<Kategori> CreateCategoryWithInsertExcel()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici kullanici = await _unitOfWorkAdmin.Repository.GetById(id);

            Kategori k = new Kategori
            {
                KategoriAdi = "",
                KategoriAciklama = "'nın açıklaması",
                KategoriFoto = "pp.jpg",
                Durum = true,
                GuncellenmeTarih = DateTime.Now,
                KategoriKodu = "",
                EklenmeTarih = DateTime.Now,
                YoneticiID = kullanici.ID,
                UstKategori = 0,
                kullanici = kullanici
            };

            await _unitOfWorkKategori.Repository.Add(k);
            _unitOfWorkKategori.Repository.Save();

            return k;
        }

        public async Task<Marka> CreateBrandWithInsertExcel(string markaAdi)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            Kullanici kullanici = await _unitOfWorkAdmin.Repository.GetById(id);

            Marka m = new Marka
            {
                MarkaAdi = markaAdi,
                MarkaAciklama = markaAdi + "'nin açıklaması",
                Durum = true,
                EklenmeTarih = DateTime.Now,
                GuncellenmeTarih = DateTime.Now,
                kullanici = kullanici,
                YoneticiID = kullanici.ID
            };

            await _unitofWorkMarka.Repository.Add(m);
            _unitofWorkMarka.Repository.Save();

            return m;
        }

        #endregion

        #region Slider

        [HttpGet]
        [CheckLogin]
        public IActionResult SliderList(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.SliderList = _unitOfWorkSlider.Repository.Where(x => x.ID > 0).OrderByDescending(x => x.ID)
                                                                   .Include("sliderKullanici")
                                                                   .ToPagedList(pageNumber, 25);

            PagedList<Slider> sliders = ViewBag.SliderList as PagedList<Slider>;

            if (sliders.Count == 0)
            {
                return RedirectToAction("SliderInsert", "Urun");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        [CheckLogin]
        public IActionResult SliderInsert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SliderInsert(Slider slider, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                    Kullanici getUser = await _unitOfWorkAdmin.Repository.GetById(id);
                    Slider newSlider = new Slider
                    {
                        YoneticiID = getUser.ID,
                        sliderKullanici = getUser,
                        SliderDesc = slider.SliderDesc,
                        SliderName = slider.SliderName,
                        UpdatedTime = DateTime.Now,
                        CreatedTime = DateTime.Now,
                        IsActive = true,
                        IsUse = false
                    };

                    if (image != null)
                        newSlider.Thumbnail = SaveImageProcess.ImageInsert(image, "Kampanya");

                    await _unitOfWorkSlider.Repository.Add(newSlider);
                    _unitOfWorkSlider.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Slider ekleme başarılı!','Slider Ekleme')";
                    return RedirectToAction("SliderList", "Urun", new { page = page });
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Gerekli alanları lütfen doldurunuz!','Slider Ekleme')";
                    return RedirectToAction("SliderList", "Urun", new { page = page });
                }


            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Slider eklenirken sistemden kaynaklı bir sorun oluştu!','Slider Ekleme')";
                return RedirectToAction("SliderList", "Urun", new { page = page });
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SliderIsActivated(int id)
        {
            try
            {
                Slider getSlider = await _unitOfWorkSlider.Repository.GetById(id);
                if (getSlider != null)
                {
                    if (getSlider.IsActive == false)
                    {
                        getSlider.IsActive = true;
                        _unitOfWorkSlider.Repository.Update(getSlider);
                        _unitOfWorkSlider.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Slider kullanımı aktif!','Slider Durum')";
                        return RedirectToAction("SliderList", "Urun", new { page = page });
                    }
                    else
                    {
                        getSlider.IsActive = false;
                        _unitOfWorkSlider.Repository.Update(getSlider);
                        _unitOfWorkSlider.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Sistemden kullanımı pasif!','Slider Durum')";
                        return RedirectToAction("SliderList", "Urun", new { page = page });
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı!','Slider Durum')";
                    return RedirectToAction("SliderList", "Urun", new { page = page });
                }

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir sorun oluştu!','Slider Durum')";
                return RedirectToAction("SliderList", "Urun", new { page = page });
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SliderIsUsed(int id)
        {
            try
            {
                Slider getSlider = await _unitOfWorkSlider.Repository.GetById(id);
                if (getSlider != null)
                {
                    if (getSlider.IsUse == false)
                    {
                        getSlider.IsUse = true;
                        _unitOfWorkSlider.Repository.Update(getSlider);
                        _unitOfWorkSlider.Repository.Save();
                        TempData["mesaj"] = "toastr['success']('Slider kullanımı aktif!','Slider Kullanımı')";
                        return RedirectToAction("SliderList", "Urun", new { page = page });
                    }
                    else
                    {
                        getSlider.IsUse = false;
                        _unitOfWorkSlider.Repository.Update(getSlider);
                        _unitOfWorkSlider.Repository.Save();
                        TempData["mesaj"] = "toastr['success']('Slider kullanımı pasif!','Slider Kullanımı')";
                        return RedirectToAction("SliderList", "Urun", new { page = page });
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı','Slider Kullanımı')";
                    return RedirectToAction("SliderList", "Urun", new { page = page });
                }

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir sorun oluştu!','Slider Kullanımı')";
                return RedirectToAction("SliderList", "Urun", new { page = page });
            }
        }

        [CheckLogin]
        public async Task<IActionResult> SliderRemove(int id)
        {
            try
            {
                Slider getSlider = await _unitOfWorkSlider.Repository.GetById(id);
                if (getSlider != null)
                {
                    _unitOfWorkSlider.Repository.Delete(getSlider);
                    _unitOfWorkSlider.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Slider başarıyla silindi!','Slider Silme')";
                    return RedirectToAction("SliderList", "Urun", new { page = page });
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı!','Slider Silme')";
                    return RedirectToAction("SliderList", "Urun", new { page = page });
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu','Slider Silme')";
                return RedirectToAction("SliderList", "Urun", new { page = page });
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SliderUpdate(int id)
        {
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(id);
            if (getSlider != null)
            {
                return View(getSlider);
            }
            else
            {
                SliderList(page);
                return View("SliderList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SliderUpdate(Slider slider, IFormFile image)
        {
            try
            {
                Slider getSlider = await _unitOfWorkSlider.Repository.GetById(slider.ID);

                if (image != null)
                    getSlider.Thumbnail = SaveImageProcess.ImageInsert(image, "Kampanya");

                getSlider.SliderName = slider.SliderName;
                getSlider.SliderDesc = slider.SliderDesc;
                getSlider.UpdatedTime = DateTime.Now;

                _unitOfWorkSlider.Repository.Update(getSlider);
                _unitOfWorkSlider.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Slider başarıyla güncellendi','Slider Güncelleme')";
                return RedirectToAction("SliderList", "Urun", new { page = page });
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu','Slider Güncelleme')";
                return RedirectToAction("SliderList", "Urun", new { page = page });
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SliderItemList(int SliderID, int? page)
        {
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(SliderID);
            TempData["SliderID"] = SliderID;
            int pageNumber = page ?? 1;
            ViewBag.SliderItemLists = _unitOfWorkSliderItem.Repository.Where(x => x.SliderID == SliderID && x.ItemName != "NoName").OrderByDescending(x => x.ID).ToPagedList(pageNumber, 25);

            PagedList<SliderItem> sliderItemList = ViewBag.SliderItemLists as PagedList<SliderItem>;

            if (sliderItemList.Count == 0)
            {
                await SliderItemInsert(SliderID);
                return View("SliderItemInsert");
            }

            else
            {
                return View(getSlider);
            }

        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SliderItemInsert(int SliderID)
        {
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(SliderID);
            ViewBag.Slider = getSlider;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SliderItemInsert(SliderItem item, int SliderID, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Slider getSlider = await _unitOfWorkSlider.Repository.GetById(SliderID);
                    if (getSlider != null)
                    {

                        SliderItem newSliderItem = new SliderItem
                        {
                            ItemName = item.ItemName,
                            OrderBy = 0,
                            ItemDesc = item.ItemDesc,
                            SliderID = getSlider.ID,
                            sliderItemSlider = getSlider,
                            IsTıtle = true
                        };

                        Random r = new Random();
                        int number = r.Next(100, 999);
                        KampanyaKoduSliderBanner campaignCodeOfSliderOrBanner = new KampanyaKoduSliderBanner
                        {
                            Kod = "S" + number.ToString()
                        };
                        int result = Convert.ToInt32(KoduKontrolEt(campaignCodeOfSliderOrBanner.Kod));

                        if (result == 1)
                        {
                            await _unitOfWorkKampanyaKodu.Repository.Add(campaignCodeOfSliderOrBanner);
                            _unitOfWorkKampanyaKodu.Repository.Save();
                            newSliderItem.KampanyaKodu = campaignCodeOfSliderOrBanner.ID;
                        }
                        else
                        {
                            newSliderItem.KampanyaKodu = KodOlustur(campaignCodeOfSliderOrBanner.Kod);
                        }

                        if (image != null)
                        {

                            newSliderItem.ItemImage = SaveImageProcess.ImageInsert(image, "Urun");

                            await _unitOfWorkSliderItem.Repository.Add(newSliderItem);
                            _unitOfWorkSliderItem.Repository.Save();

                            SliderItem getSliderItem = await _unitOfWorkSliderItem.Repository.GetById(newSliderItem.ID);
                            getSliderItem.OrderBy = Convert.ToByte(newSliderItem.ID);
                            _unitOfWorkSliderItem.Repository.Update(getSliderItem);
                            _unitOfWorkSliderItem.Repository.Save();

                            TempData["mesaj"] = "toastr['success']('Slider elemanı başarıyla eklendi','Slider Elemanı Ekleme')";
                            return RedirectToAction("SliderList", "Urun");
                        }
                        else
                        {
                            TempData["mesaj"] = "toastr['warning']('Slider elemanına bir fotoğraf ekleyin','Slider Elemanı Ekleme')";
                            return RedirectToAction("SliderList", "Urun");
                        }
                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı!','Slider Elemanı Ekleme')";
                        return RedirectToAction("SliderList", "Urun");
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Gerekli alanları eksiksiz dolurunuz!','Slider Elemanı Ekleme')";
                    return RedirectToAction("SliderList", "Urun");
                }

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Slider Elemanı Ekleme')";
                return RedirectToAction("SliderList", "Urun");
            }
        }

        public int KoduKontrolEt(string kod)
        {
            KampanyaKoduSliderBanner getCampaignCodeOfSliderOrBanner = _unitOfWorkKampanyaKodu.Repository.Where(x => x.Kod == kod).FirstOrDefault();
            if (getCampaignCodeOfSliderOrBanner != null)
            {
                return KodOlustur(kod);
            }
            else
            {
                return 1;
            }
        }

        public int KodOlustur(string kod)
        {
            Random r = new Random();
            int number = r.Next(100, 999);
            KampanyaKoduSliderBanner campaignCodeOfSliderOrBanner = new KampanyaKoduSliderBanner();

            if (kod.Contains("S"))
            {
                campaignCodeOfSliderOrBanner.Kod = "S" + number.ToString();

            }
            else if (kod.Contains("B"))
            {
                campaignCodeOfSliderOrBanner.Kod = "B" + number.ToString();
            }

            int result = Convert.ToInt32(KoduKontrolEt(campaignCodeOfSliderOrBanner.Kod));
            if (result == 1)
            {
                result = campaignCodeOfSliderOrBanner.ID;
                return result;
            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        public async Task<IActionResult> SliderItemUpdate(int ID)
        {

            SliderItem getSliderItem = await _unitOfWorkSliderItem.Repository.GetById(ID);
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(getSliderItem.SliderID);
            if (getSliderItem != null)
            {
                ViewBag.Slider = getSlider;
                return View(getSliderItem);
            }
            else
            {
                await SliderItemBannerList(getSliderItem.SliderID, page);
                return View("SliderItemBannerList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SliderItemUpdate(SliderItem item, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                SliderItem getSliderItem = await _unitOfWorkSliderItem.Repository.GetById(item.ID);
                if (getSliderItem != null)
                {
                    if (image != null)
                        getSliderItem.ItemImage = SaveImageProcess.ImageInsert(image, "Kampanya");

                    getSliderItem.ItemDesc = item.ItemDesc;
                    getSliderItem.ItemName = item.ItemName;

                    _unitOfWorkSliderItem.Repository.Update(getSliderItem);
                    _unitOfWorkSliderItem.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Slider elemanı başarıyla düzenlendi','Slider Elemanı Düzenleme')";
                    return RedirectToAction("SliderItemUpdate", "Urun", new { ID = item.ID });
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı!','Slider Elemanı Düzenleme')";
                    return RedirectToAction("SliderItemUpdate", "Urun", new { ID = item.ID, page = page });
                }
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Gerekli alanları eksiksiz giriniz','Slider Elemanı Düzenleme')";
                return RedirectToAction("SliderItemUpdate", "Urun", new { ID = item.ID, page = page });
            }

        }

        [HttpGet]
        public async Task<IActionResult> SliderItemRemove(int id)
        {
            SliderItem getSliderItem = await _unitOfWorkSliderItem.Repository.GetById(id);
            KampanyaKoduSliderBanner campaignCodOfSliderOrBanner = await _unitOfWorkKampanyaKodu.Repository.GetById(getSliderItem.KampanyaKodu);

            try
            {
                if (getSliderItem != null)
                {
                    _unitOfWorkKampanyaKodu.Repository.Delete(campaignCodOfSliderOrBanner);
                    _unitOfWorkKampanyaKodu.Repository.Save();

                    _unitOfWorkSliderItem.Repository.Delete(getSliderItem);
                    _unitOfWorkSliderItem.Repository.Save();

                    TempData["mesaj"] = "toastr['warning']('Slider silme işlemi başarılı!','Slider Elemanı Silme')";
                    return RedirectToAction("SliderItemList", "Urun", new { SliderID = getSliderItem.SliderID, page = page });

                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı!','Slider Elemanı Silme')";
                    return RedirectToAction("SliderItemList", "Urun", new { SliderID = getSliderItem.SliderID, page = page });
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi!','Slider Elemanı Silme')";
                return RedirectToAction("SliderItemList", "Urun", new { SliderID = getSliderItem.SliderID, page = page });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SliderTitleIsShow(int id)
        {
            SliderItem getSliderItem = await _unitOfWorkSliderItem.Repository.GetById(id);
            try
            {
                if (getSliderItem != null)
                {
                    if (getSliderItem.IsTıtle == false)
                    {
                        getSliderItem.IsTıtle = true;
                        _unitOfWorkSliderItem.Repository.Update(getSliderItem);
                        _unitOfWorkSliderItem.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Slider başlığı etkinleştirildi!','Slider Başlık Düzenleme')";
                        return RedirectToAction("SliderItemList", "Urun", new { SliderID = getSliderItem.SliderID, page = page });
                    }
                    else
                    {
                        getSliderItem.IsTıtle = false;
                        _unitOfWorkSliderItem.Repository.Update(getSliderItem);
                        _unitOfWorkSliderItem.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Slider başlığı pasifleştirildi!','Slider Başlık Düzenleme')";
                        return RedirectToAction("SliderItemList", "Urun", new { SliderID = getSliderItem.SliderID, page = page });
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı!','Slider Başlık Düzenleme')";
                    return RedirectToAction("SliderItemList", "Urun", new { SliderID = getSliderItem.SliderID, page = page });
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi!','Slider Başlık Düzenleme')";
                return RedirectToAction("SliderItemList", "Urun", new { SliderID = getSliderItem.SliderID, page = page });
            }
        }


        public async Task<IActionResult> SliderDetail(int id, int? page)
        {
            SliderItem getSliderItem = await _unitOfWorkSliderItem.Repository.Where(x => x.ID == id).Include("sliderItemSlider").SingleOrDefaultAsync();

            int pageNumber = page ?? 1;
            ViewBag.Urunler = await _unitOfWorkSliderUrun.Repository.Where(x => x.SliderItemID == id).Include("urunSliderUrun").ToPagedListAsync(pageNumber, 20);

            KampanyaKoduSliderBanner getcampaignCodOfSliderOrBanner = await _unitOfWorkKampanyaKodu.Repository.Where(x => x.ID == getSliderItem.KampanyaKodu).SingleOrDefaultAsync();
            TempData["Kod"] = getcampaignCodOfSliderOrBanner.Kod;

            return View(getSliderItem);
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SliderItemBannerList(int SliderID, int? page)
        {
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(SliderID);
            TempData["SliderID"] = SliderID;
            int pageNumber = page ?? 1;
            ViewBag.SliderBannerItemLists = _unitOfWorkSliderBannerItem.Repository.Where(x => x.SliderID == SliderID && x.ItemName != "NoName").OrderByDescending(x => x.ID).ToPagedList(pageNumber, 25);

            PagedList<SliderBannerItem> bannerItemList = ViewBag.SliderBannerItemLists as PagedList<SliderBannerItem>;

            if (bannerItemList.Count == 0)
            {
                await SliderItemBannerInsert(SliderID);
                return View("SliderItemBannerInsert");
            }

            else
            {
                return View(getSlider);
            }

        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SliderItemBannerInsert(int SliderID)
        {
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(SliderID);
            ViewBag.Slider = getSlider;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SliderItemBannerInsert(SliderBannerItem sliderBannerItem, int SliderID, IFormFile image)
        {
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(SliderID);
            try
            {
                if (ModelState.IsValid)
                {
                    if (getSlider != null)
                    {
                        SliderBannerItem newBannerItem = new SliderBannerItem
                        {
                            ItemName = sliderBannerItem.ItemName,
                            OrderBy = 0,
                            ItemDesc = sliderBannerItem.ItemDesc,
                            SliderID = getSlider.ID,
                            sliderBannerItemSlider = getSlider
                        };

                        Random r = new Random();
                        int number = r.Next(100, 999);
                        KampanyaKoduSliderBanner campaignCodeOfSliderOrBanner = new KampanyaKoduSliderBanner
                        {
                            Kod = "B" + number.ToString()
                        };

                        int result = Convert.ToInt32(KoduKontrolEt(campaignCodeOfSliderOrBanner.Kod));

                        if (result == 1)
                        {
                            await _unitOfWorkKampanyaKodu.Repository.Add(campaignCodeOfSliderOrBanner);
                            _unitOfWorkKampanyaKodu.Repository.Save();
                            newBannerItem.KampanyaKodu = campaignCodeOfSliderOrBanner.ID;
                        }
                        else
                        {
                            newBannerItem.KampanyaKodu = KodOlustur(campaignCodeOfSliderOrBanner.Kod);
                        }

                        if (image != null)
                        {
                            newBannerItem.ItemImage = SaveImageProcess.ImageInsert(image, "Kampanya");

                            await _unitOfWorkSliderBannerItem.Repository.Add(newBannerItem);
                            _unitOfWorkSliderBannerItem.Repository.Save();

                            SliderBannerItem getSliderItem = await _unitOfWorkSliderBannerItem.Repository.GetById(newBannerItem.ID);
                            getSliderItem.OrderBy = Convert.ToByte(newBannerItem.ID);
                            _unitOfWorkSliderBannerItem.Repository.Update(getSliderItem);
                            _unitOfWorkSliderBannerItem.Repository.Save();

                            TempData["mesaj"] = "toastr['success']('Banner elemanı ekleme başarılı','Banner ekleme')";
                            return RedirectToAction("SliderList", "Urun");
                        }
                        else
                        {
                            TempData["mesaj"] = "toastr['warning']('Banner fotoğrafı zorunludur. Ekledikten sonra işleme devam edin!','Banner ekleme')";
                            return RedirectToAction("SliderItemBannerInsert", "Urun", new { SliderID = getSlider.ID });
                        }
                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Slider verisi alınamadı!','Banner ekleme')";
                        return RedirectToAction("SliderItemBannerInsert", "Urun", new { SliderID = getSlider.ID });
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Zorunlu alanları lütfen eksiksiz doldurunuz!','Banner ekleme')";
                    return RedirectToAction("SliderItemBannerInsert", "Urun", new { SliderID = getSlider.ID });
                }

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir sorun meydana geldi!','Banner ekleme')";
                return RedirectToAction("SliderItemBannerInsert", "Urun", new { SliderID = getSlider.ID });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SliderItemBannerUpdate(int id)
        {

            SliderBannerItem getBannerItem = await _unitOfWorkSliderBannerItem.Repository.GetById(id);
            Slider getSlider = await _unitOfWorkSlider.Repository.GetById(getBannerItem.SliderID);
            if (getBannerItem != null)
            {
                ViewBag.Slider = getSlider;
                return View(getBannerItem);
            }
            else
            {
                await SliderItemBannerList(getBannerItem.SliderID, page);
                return View("SliderItemBannerList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SliderItemBannerUpdate(SliderBannerItem sliderBannerItem, IFormFile image)
        {

            SliderBannerItem getBannerItem = await _unitOfWorkSliderBannerItem.Repository.GetById(sliderBannerItem.ID);

            try
            {
                if (ModelState.IsValid)
                {
                    if (getBannerItem != null)
                    {

                        if (image != null)
                            getBannerItem.ItemImage = SaveImageProcess.ImageInsert(image, "Kampanya");

                        getBannerItem.ItemDesc = sliderBannerItem.ItemDesc;
                        getBannerItem.ItemName = sliderBannerItem.ItemName;

                        _unitOfWorkSliderBannerItem.Repository.Update(getBannerItem);
                        _unitOfWorkSliderBannerItem.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Banner güncelleme başarılı!','Banner Düzenleme')";
                        return RedirectToAction("SliderItemBannerUpdate", "Urun", new { id = getBannerItem.ID });
                    }

                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Banner güncelleme başarısız veri alınamadı!','Banner Düzenleme')";
                        return RedirectToAction("SliderItemBannerUpdate", "Urun", new { id = getBannerItem.ID });
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Zorunlu alanları doldurduktan sonra işlemlerinize devam edebilirsiniz!','Banner Düzenleme')";
                    return RedirectToAction("SliderItemBannerUpdate", "Urun", new { id = getBannerItem.ID });
                }

            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir sorun meydana geldi!','Banner Düzenleme')";
                return RedirectToAction("SliderItemBannerUpdate", "Urun", new { id = getBannerItem.ID });
            }


        }

        [HttpGet]
        public async Task<IActionResult> SliderItemBannerRemove(int id)
        {
            SliderBannerItem getBannerItem = await _unitOfWorkSliderBannerItem.Repository.GetById(id);
            KampanyaKoduSliderBanner campaignCodeOfSliderOrBanner = await _unitOfWorkKampanyaKodu.Repository.GetById(getBannerItem.KampanyaKodu);
            try
            {
                if (getBannerItem != null)
                {
                    _unitOfWorkKampanyaKodu.Repository.Delete(campaignCodeOfSliderOrBanner);
                    _unitOfWorkKampanyaKodu.Repository.Save();

                    _unitOfWorkSliderBannerItem.Repository.Delete(getBannerItem);
                    _unitOfWorkSliderBannerItem.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Banner elemanı silme işlemi başarılı','Banner Silme')";
                    return RedirectToAction("SliderItemBannerList", "Urun", new { SliderID = getBannerItem.SliderID, page = page });
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Banner verisi alınamadı','Banner Silme')";
                    return RedirectToAction("SliderItemBannerList", "Urun", new { SliderID = getBannerItem.SliderID, page = page });
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Banner Silme')";
                return RedirectToAction("SliderItemBannerList", "Urun", new { SliderID = getBannerItem.SliderID, page = page });
            }
        }

        public async Task<IActionResult> SliderBannerDetail(int id, int? page)
        {
            SliderBannerItem sliderBannerItem = await _unitOfWorkSliderBannerItem.Repository.Where(x => x.ID == id).Include("sliderBannerItemSlider").SingleOrDefaultAsync();

            int pageNumber = page ?? 1;
            ViewBag.Urunler = await _unitOfWorkSliderUrun.Repository.Where(x => x.SliderBannerID == id).Include("urunSliderUrun").ToPagedListAsync(pageNumber, 20);

            KampanyaKoduSliderBanner getCampaignCodeOfSliderOrBanner = await _unitOfWorkKampanyaKodu.Repository.Where(x => x.ID == sliderBannerItem.KampanyaKodu).SingleOrDefaultAsync();
            TempData["Kod"] = getCampaignCodeOfSliderOrBanner.Kod;

            return View(sliderBannerItem);
        }

        public async Task<IActionResult> ProductRemoveInCampaign(int Id)
        {
            SliderUrun sliderProduct = await _unitOfWorkSliderUrun.Repository.Where(x => x.ID == Id).SingleOrDefaultAsync();
            try
            {
                _unitOfWorkSliderUrun.Repository.Delete(sliderProduct);
                _unitOfWorkSliderUrun.Repository.Save();
                await CreateModeratorLog("Başarılı", "Slider Ürün Kaldır", "ProductRemoveInCampaign", "Urun");
                TempData["mesaj"] = "toastr['success']('Kampanyalı ürün başarıyla kaldırıldı','Kampanyalı Ürün İşlemi')";
                return RedirectToAction("SliderList", "Urun");
            }
            catch (Exception)
            {
                await CreateModeratorLog("Sistem Hat", "Slider Ürün Kaldır", "ProductRemoveInCampaign", "Urun");
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kampanyalı Ürün İşlemi')";
                return RedirectToAction("SliderList", "Urun");
            }
        }

        [CheckLogin]
        [HttpGet, ActionName("ProductListToSliderBanner")]
        public async Task<IActionResult> ProductListToSliderBanner(int? page)
        {
            int pageNumber = page ?? 1;

            ViewBag.Urunler = await _unitOfWorkUrun.Repository.Where(x => x.Durum == true).OrderByDescending(x => x.ID)
               .Include("kategori")
               .Include("kullanici")
               .Include("marka")
               .Include("stokbirimi")
               .ToPagedListAsync(pageNumber, 30);

            List<Urun> productNoList = (List<Urun>)await _unitOfWorkUrun.Repository.All();
            ViewBag.UrunNo = new SelectList(productNoList, "ID", "UrunNo");

            List<SliderUrun> sliderProductIsHaveListCheck = await _unitOfWorkSliderUrun.Repository.Where(x => x.ID > 0).ToListAsync();
            ViewBag.SliderUrun = sliderProductIsHaveListCheck;

            List<KampanyaKoduSliderBanner> campaignCodeOfSliderOrBanner = (List<KampanyaKoduSliderBanner>)await _unitOfWorkKampanyaKodu.Repository.Where(x => x.Kod != "0001").ToListAsync();
            ViewBag.KampanyaKodu = campaignCodeOfSliderOrBanner;

            return View();
        }

        [CheckLogin]
        [HttpGet, ActionName("ProductListToSliderBannerToFilter")]
        public async Task<IActionResult> ProductListToSliderBannerToFilter(int? page, string UrunNo)
        {
            int pageNumber = page ?? 1;
            int id = Convert.ToInt32(UrunNo);
            ViewBag.Urunler = await _unitOfWorkUrun.Repository.Where(x => x.Durum == true && x.ID == id)
               .Include("kategori")
               .Include("kullanici")
               .Include("marka")
               .Include("stokbirimi")
               .ToPagedListAsync(pageNumber, 30);

            List<Urun> productNoList = (List<Urun>)await _unitOfWorkUrun.Repository.All();
            ViewBag.UrunNo = new SelectList(productNoList, "ID", "UrunNo");

            List<KampanyaKoduSliderBanner> campaignCodeOfSliderOrBanner = (List<KampanyaKoduSliderBanner>)await _unitOfWorkKampanyaKodu.Repository.Where(x => x.Kod != "0001").ToListAsync();
            ViewBag.KampanyaKodu = campaignCodeOfSliderOrBanner;

            return View();
        }

        public async Task<IActionResult> CreateCampaignProduct(int urunId, int Kod)
        {
            try
            {
                Urun getProduct = await _unitOfWorkUrun.Repository.Where(x => x.ID == urunId).SingleOrDefaultAsync();
                KampanyaKoduSliderBanner campaignCodeOfSliderOrBanner = await _unitOfWorkKampanyaKodu.Repository.Where(x => x.ID == Kod).SingleOrDefaultAsync();

                SliderBannerItem getBannerItem = await _unitOfWorkSliderBannerItem.Repository.Where(x => x.KampanyaKodu == campaignCodeOfSliderOrBanner.ID).SingleOrDefaultAsync();

                SliderItem getSliderItem = await _unitOfWorkSliderItem.Repository.Where(x => x.KampanyaKodu == campaignCodeOfSliderOrBanner.ID).SingleOrDefaultAsync();

                SliderUrun newSliderProduct = new SliderUrun
                {
                    UrunID = getProduct.ID,
                    urunSliderUrun = getProduct
                };

                if (getBannerItem != null)
                {
                    newSliderProduct.SliderBannerID = getBannerItem.ID;
                    newSliderProduct.sliderBannerItemSliderUrun = getBannerItem;
                    SliderItem sliderItemNull = await _unitOfWorkSliderItem.Repository.Where(x => x.ItemName == "NoName").SingleOrDefaultAsync();
                    newSliderProduct.SliderItemID = sliderItemNull.ID;
                    newSliderProduct.sliderItemSliderUrun = sliderItemNull;
                }
                else if (getSliderItem != null)
                {
                    newSliderProduct.SliderItemID = getSliderItem.ID;
                    newSliderProduct.sliderItemSliderUrun = getSliderItem;

                    SliderBannerItem sliderBannerNull = await _unitOfWorkSliderBannerItem.Repository.Where(x => x.ItemName == "NoName").SingleOrDefaultAsync();
                    newSliderProduct.SliderBannerID = sliderBannerNull.ID;
                    newSliderProduct.sliderBannerItemSliderUrun = getBannerItem;
                }

                await _unitOfWorkSliderUrun.Repository.Add(newSliderProduct);
                _unitOfWorkSliderUrun.Repository.Save();

                await ProductListToSliderBanner(page);
                return View("ProductListToSliderBanner");
            }
            catch (Exception)
            {
                await ProductListToSliderBanner(page);
                return View("ProductListToSliderBanner");
            }
        }

        #endregion

        #region Renk

        [HttpGet]
        public async Task<IActionResult> ColorsForProduct(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.Renkler = await _unitOfWorkRenkler.Repository.Where(x => x.ID > 0).ToPagedListAsync(pageNumber, 12);
            return View();
        }

        public IActionResult CreateNewColor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateColorForProduct(Renk renk)
        {
            try
            {
                _unitOfWorkRenkler.Repository.Add(renk);
                _unitOfWorkRenkler.Repository.Save();
                return RedirectToAction("ColorsForProduct","Urun");
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.ToString();
                return RedirectToAction("ColorsForProduct", "Urun");
            }
        }

        public async Task<IActionResult> EditColorAsync(int ColorID)
        {
            Renk getColor = await _unitOfWorkRenkler.Repository.GetById(ColorID);
            return View(getColor);
        }

        [HttpPost]
        public IActionResult EditColor(Renk renk)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> RemoveColor(int ColorID)
        {
            try
            {
                Renk getColor = await _unitOfWorkRenkler.Repository.GetById(ColorID);
                _unitOfWorkRenkler.Repository.Delete(getColor);
                _unitOfWorkRenkler.Repository.Save();
                TempData["mesaj"] = "toastr['success']('Ürün rengi başarıyla silindi.','Silme İşlemi')";
                return RedirectToAction("ColorsForProduct", "Urun");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['success']('Ürün rengi başarıyla silindi.','Silme İşlemi')";
                return RedirectToAction("ColorsForProduct", "Urun");
            }
        }

        public IActionResult DeleteColorInProduct(int ID)
        {
            UrunRenk getColor = _unitOfWorkUrunRenk.Repository.Where(x=> x.ID == ID).SingleOrDefault();
            Urun getProduct =  _unitOfWorkUrun.Repository.Where(x => x.ID == getColor.UrunID).SingleOrDefault();
            try
            {
                VeriDoldur(getProduct);
                _unitOfWorkUrunRenk.Repository.Delete(getColor);
                _unitOfWorkUrunRenk.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Bu üründen renk değeri kaldırıldı','Renk kaldırma')";
                return RedirectToAction("EditProduct", "Urun", new { ID = getProduct.ID, page = page });
            }
            catch (Exception ex)
            {
                VeriDoldur(getProduct);
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Renk kaldırma')";
                return RedirectToAction("EditProduct", "Urun", new { ID = getProduct.ID, page = page });
            }
        }

        #endregion

        public async Task<CreateLog> CreateModeratorLog(string durum, string islem, string action, string controller)
        {
            string kullaniciAdi = HttpContext.Session.GetString("username");
            CreateLog ct = new CreateLog(_unitOfWorkLogDurumlar, _unitOfWorkLogIslemler, _unitOfWorkLogLoglar, _unitofWorkLogYonetici);
            if (kullaniciAdi == "")
            {
                await ct.CreateLogs(durum, islem, action, controller, "Sistem");
                return ct;
            }
            await ct.CreateLogs(durum, islem, action, controller, kullaniciAdi);
            return ct;
        }

    }

    public class OzelUrun
    {
        public int MusteriGrubuID { get; set; }
        public int UrunID { get; set; }
    }
}
