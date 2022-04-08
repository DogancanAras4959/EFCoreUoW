using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UoW.ADMIN.Helpers;
using UoW.CORE.Enums;
using UoW.CORE.Interface;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Contracts;
using UoW.SERVICE.Dtos;
using UoW.WEB.Models;
using X.PagedList;

namespace UoW.WEB.Controllers
{
    public class HomeController : Controller
    {

        #region Field

        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<Yetkililer> _unitOfWorkYetkililer;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWork<Bulten> _unitOfWorkBulten;
        private readonly IUnitOfWork<Kullanici> _unitOfWorkKullanici;
        private readonly IUnitOfWork<Cuzdan> _unitOfWorkCuzdan;
        private readonly IUnitOfWork<CuzdanIslemler> _unitOfWorkCuzdanIslemler;
        private readonly IUnitOfWork<MusteriGrupOzelUrun> _unitOfWorkOzelUrunler;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategoriler;
        private readonly IUnitOfWork<Slider> _unitOfWorkSlider;
        private readonly IUnitOfWork<SliderItem> _unitOfWorkSliderItem;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarka;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
        private readonly IUnitOfWork<SliderBannerItem> _unitOfWorKSliderBannerItem;
        private readonly IUnitOfWork<KargoSure> _unitOfWorkKargoSure;
        private readonly IUnitOfWork<OzelTeklif> _unitOfWorkOzelTeklif;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IUnitOfWork<Siparis> _unitOfWorkSiparis;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly IUnitOfWork<SliderUrun> _unitOfWorkSliderUrun;
        private readonly IUnitOfWork<Kargo> _unitOfWorkKargo;
        private readonly IUnitOfWork<KargoDetay> _unitOfWorkKargoDetay;
        private readonly IUnitOfWork<SiparisDurum> _unitOfWorkSiparisDurum;
        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _memoryCache;
        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<Dokuman> _unitOfWorkDokuman;
        private readonly IUnitOfWork<DokumanTipi> _unitOfWorkDokumanTipi;
        private readonly IUnitOfWork<UrunStok> _unitOfWorkUrunStok;
        private readonly int? page;
        private readonly ICargoService _cargoService;
        public HomeController(IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<Yetkililer> unitOfWorkYetkililer,
            IUnitOfWork<BayiTicari> unitOfWorkBayiTicari,
            IUnitOfWork<Bulten> unitOfWorkBulten,
            IUnitOfWork<Kullanici> unitOfWorkKullanici,
            IUnitOfWork<Cuzdan> unitOfWorkCuzdan,
            IUnitOfWork<CuzdanIslemler> unitOfWorkCuzdanIslemler,
            IUnitOfWork<MusteriGrupOzelUrun> unitOfWorkOzelUrunler,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Kategori> unitOfWorkKategoriler,
            IUnitOfWork<Slider> unitOfWorkSlider,
            IUnitOfWork<SliderItem> unitOfWorkSliderItem,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<Begeni> unitofWorkBegeni,
            IUnitOfWork<SliderBannerItem> unitOfWorkSliderBannerItem,
            IUnitOfWork<KargoSure> unitOfWorkKargoSure,
            IUnitOfWork<OzelTeklif> unitOfWorkOzelTeklif,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<Siparis> unitOfWorkSiparis,
            IUnitOfWork<Sepet> unitOfWorkSepet,
            IUnitOfWork<SliderUrun> unitOfWorkSliderUrun,
            IUnitOfWork<Kargo> unitOfWorkKargo,
            IUnitOfWork<SiparisDurum> unitOfWorkSiparisDurum,
            IUnitOfWork<Haber> unitOfWorkHaber,
            IUnitOfWork<SiteInfo> unitOfWorkSiteInfo,
            IUnitOfWork<Dokuman> unitOfWorkDokuman,
            IUnitOfWork<DokumanTipi> unitOfWorkDokumanTipi,
            IUnitOfWork<UrunStok> unitOfWorkUrunStok,
            Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
            IConfiguration configuration,
            ICargoService cargoService)

        {
            _unitOfWorkBayi = unitOfWorkBayi;
            _unitOfWorkYetkililer = unitOfWorkYetkililer;
            _unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            _unitOfWorkBulten = unitOfWorkBulten;
            _unitOfWorkKullanici = unitOfWorkKullanici;
            _unitOfWorkCuzdan = unitOfWorkCuzdan;
            _unitOfWorkCuzdanIslemler = unitOfWorkCuzdanIslemler;
            _unitOfWorkOzelUrunler = unitOfWorkOzelUrunler;
            _unitOfWorkUrun = unitOfWorkUrun;
            _unitOfWorkKategoriler = unitOfWorkKategoriler;
            _unitOfWorkSlider = unitOfWorkSlider;
            _unitOfWorkSliderItem = unitOfWorkSliderItem;
            _unitOfWorkMarka = unitOfWorkMarka;
            _unitOfWorkBegeni = unitofWorkBegeni;
            _unitOfWorKSliderBannerItem = unitOfWorkSliderBannerItem;
            _unitOfWorkKargoSure = unitOfWorkKargoSure;
            _unitOfWorkOzelTeklif = unitOfWorkOzelTeklif;
            _unitOfWorkStokBildirim = unitOfWorkStokBildirim;
            _unitOfWorkSiparis = unitOfWorkSiparis;
            _unitOfWorkSepet = unitOfWorkSepet;
            _unitOfWorkSliderUrun = unitOfWorkSliderUrun;
            _unitOfWorkKargo = unitOfWorkKargo;
            _unitOfWorkHaber = unitOfWorkHaber;
            _unitOfWorkSiparisDurum = unitOfWorkSiparisDurum;
            _unitOfWorkSiteInfo = unitOfWorkSiteInfo;
            _memoryCache = memoryCache;
            _unitOfWorkDokuman = unitOfWorkDokuman;
            _unitOfWorkDokumanTipi = unitOfWorkDokumanTipi;
            _unitOfWorkUrunStok = unitOfWorkUrunStok;
            _configuration = configuration;
            _cargoService = cargoService;
        }

        #endregion

        #region Home

        public async Task<IActionResult> ProductListWithCampaignSliderItem(int id, int? page)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == Id).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = b;

            int pageNumber = page ?? 1;
            SliderItem item = await _unitOfWorkSliderItem.Repository.Where(x => x.ID == id).FirstOrDefaultAsync();

            ViewBag.Urunler = await _unitOfWorkSliderUrun.Repository.Where(x => x.SliderItemID == item.ID).Include("urunSliderUrun").ToPagedListAsync(pageNumber, 30);

            return View();
        }

        public async Task<IActionResult> ProductListWithCampaign(int id, int? page)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == Id).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = b;

            int pageNumber = page ?? 1;
            SliderBannerItem item = await _unitOfWorKSliderBannerItem.Repository.Where(x => x.ID == id).FirstOrDefaultAsync();

            ViewBag.Urunler = await _unitOfWorkSliderUrun.Repository.Where(x => x.SliderBannerID == item.ID).Include("urunSliderUrun").ToPagedListAsync(pageNumber, 30);

            return View();
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> HomePage(int? page)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == Id).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = b;
            await LoadData(page);
            return View();
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> HomePageList(int? page)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == Id).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = b;
            await LoadData(page);
            return View();
        }

        public async Task LoadData(int? page)
        {

            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            List<Urun> products = _unitOfWorkUrun.Repository.Where(x => x.Durum == true).ToList();
            string data = JsonConvert.SerializeObject(products);
            ViewData["data"] = data;

            Slider gelenSlider = await _unitOfWorkSlider.Repository.Where(x => x.ID > 0 && x.IsUse == true && x.IsActive == true).FirstOrDefaultAsync();

            List<Marka> markalars = await _unitOfWorkMarka.Repository.Where(x => x.UstMarkaID == 0 && x.Durum == true).ToListAsync();
            ViewBag.Markalar = markalars;

            List<Kategori> categories = _unitOfWorkKategoriler.Repository.Where(x => x.Durum == true).Include("urunler").ToListAsync().Result;
            ViewBag.Kategoriler = categories;

            if (gelenSlider != null)
            {
                List<SliderItem> sliderItems = await _unitOfWorkSliderItem.Repository.Where(x => x.SliderID == gelenSlider.ID && x.ItemName != "NoName").OrderByDescending(x => x.OrderBy).ToListAsync();
                ViewBag.SliderItems = sliderItems;

                List<SliderBannerItem> sliderBannerItems = await _unitOfWorKSliderBannerItem.Repository.Where(x => x.SliderID == gelenSlider.ID && x.ItemName != "NoName").OrderByDescending(x => x.OrderBy).ToListAsync();
                ViewBag.SliderBannerItems = sliderBannerItems;
            }

            int pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
             .Where(x => x.UstUrunID == 0 && x.Durum == true)
             .Include("kategori")
             .Include("kullanici")
             .Include("marka")
             .Include("stokbirimi")
             .Include("begeniler")
             .ToPagedList(pageNumber, 24);

            ViewBag.Begeniler = await _unitOfWorkBegeni.Repository.Where(x => x.BayiID == Id).Include("urunBegeni").Include("bayiBegeni").ToListAsync();

        }

        #endregion

        #region Brand

        [CheckOtherLogin]
        [HttpGet, ActionName("FilterByBrand")]
        public async Task<IActionResult> FilterByBrand(int? page, int MarkaID, string sortType)
        {
            await LoadBrandData(page, MarkaID, sortType);
            await FilterBrandItemsAsync(MarkaID);
            return View();
        }

        [CheckOtherLogin]
        [HttpGet, ActionName("FilterByBrandList")]
        public async Task<IActionResult> FilterByBrandList(int? page, int MarkaID, string sortType)
        {
            await LoadBrandData(page, MarkaID, sortType);
            await FilterBrandItemsAsync(MarkaID);
            return View();
        }

        private async Task FilterBrandItemsAsync(int markaID)
        {
            Marka getBrand = await _unitOfWorkMarka.Repository.GetById(markaID);
            if (getBrand.UstMarkaID > 0)
            {
                Marka mainBrand = await _unitOfWorkMarka.Repository.Where(x => x.ID == getBrand.UstMarkaID).SingleOrDefaultAsync();
                List<Marka> subBrand = await _unitOfWorkMarka.Repository.Where(x => x.UstMarkaID == mainBrand.ID).ToListAsync();
                ViewBag.AltMarkalar = subBrand;
                TempData["MarkaAdi"] = mainBrand.MarkaAdi;
            }
            else
            {
                List<Marka> allBrands = await _unitOfWorkMarka.Repository.Where(x => x.UstMarkaID == 0 && x.Durum == true).ToListAsync();
                ViewBag.UstMarkalar = allBrands;
            }

            List<Urun> newList = _unitOfWorkUrun.Repository.Where(x => x.MarkaID == markaID && x.UstUrunID == 0 && x.Adet > 0 && x.IndirimOrani == 0)
           .Include("kategori").Include("kullanici").Include("marka").Include("stokbirimi").Include("begeniler").ToList();

            ListMarkaForFilter(newList);

            TempData["MarkaID"] = markaID;
            TempData["MarkaAdi"] = getBrand.MarkaAdi;

        }

        public async Task<int> LoadBrandData(int? page, int MarkaID, string sortType)
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").FirstOrDefaultAsync();

            Marka gelenMarka = await _unitOfWorkMarka.Repository.GetById(MarkaID);
            Slider gelenSlider = await _unitOfWorkSlider.Repository.Where(x => x.ID > 0 && x.IsUse == true && x.IsActive == true).FirstOrDefaultAsync();

            if (gelenSlider != null)
            {
                List<SliderItem> sliderItems = await _unitOfWorkSliderItem.Repository.Where(x => x.SliderID == gelenSlider.ID && x.ItemName != "NoName").OrderByDescending(x => x.OrderBy).ToListAsync();
                ViewBag.SliderItems = sliderItems;

                List<SliderBannerItem> sliderBannerItems = await _unitOfWorKSliderBannerItem.Repository.Where(x => x.SliderID == gelenSlider.ID && x.ItemName != "NoName").OrderByDescending(x => x.OrderBy).ToListAsync();
                ViewBag.SliderBannerItems = sliderBannerItems;

            }

            List<Dokuman> kataloglar = await _unitOfWorkDokuman.Repository.Where(x => x.DokumanTipi == 1 && x.Durum == true).OrderByDescending(x => x.EklenmeTarih).Take(6).ToListAsync();

            int pageNumber = page ?? 1;

            IPagedList<Urun> urunler = _unitOfWorkUrun.Repository
             .Where(x => x.MarkaID == MarkaID && x.UstUrunID == 0 && x.Durum == true)
             .Include("kategori")
             .Include("kullanici")
             .Include("marka")
             .Include("stokbirimi")
             .Include("begeniler")
             .ToPagedList(pageNumber, 24);

            if (sortType == "priceAsc")
            {
                urunler = urunler.OrderBy(x => x.KDVFiyat).ToPagedList(pageNumber, 24);
            }
            else if (sortType == "priceDesc")
            {
                urunler = urunler.OrderByDescending(x => x.KDVFiyat).ToPagedList(pageNumber, 24);
            }
            else if (sortType == "nameDesc")
            {
                urunler = urunler.OrderByDescending(x => x.UrunAdi).ToPagedList(pageNumber, 24);
            }
            else if (sortType == "topSeller")
            {
                urunler = urunler.OrderByDescending(x => x.UrunAdi).ToPagedList(pageNumber, 24);
            }

            ViewBag.Urunler = urunler;
            ViewBag.Kataloglar = kataloglar;
            ViewBag.Bayi = gelenBayi;
            TempData["MarkaID"] = MarkaID;

            return 1;
        }

        #endregion

        #region CategoryFilter

        [CheckOtherLogin]
        [HttpGet, ActionName("FilterByProduct")]
        public async Task<IActionResult> FilterByProduct(int? page, int KategoriID, string sortType)
        {

            await FilterCategoryItemsAsync(KategoriID);

            int pageNumber = page ?? 1;

            IPagedList<Urun> urunler = _unitOfWorkUrun.Repository
             .Where(x => x.KategoriID == KategoriID && x.UstUrunID == 0 && x.Adet > 0 && x.IndirimOrani == 0)
             .Include("kategori")
             .Include("kullanici")
             .Include("marka")
             .Include("stokbirimi")
             .Include("begeniler")
             .ToPagedList(pageNumber, 24);

            List<Urun> altUrunler = _unitOfWorkUrun.Repository.Where(x => x.UstUrunID != 0 && x.Adet > 0).Include("kategori")
             .Include("kullanici")
             .Include("marka")
             .Include("stokbirimi")
             .Include("begeniler")
             .ToList();

            if (sortType == "priceAsc")
            {
                urunler = urunler.OrderBy(x => x.KDVFiyat).ToPagedList(pageNumber, 24);
                altUrunler = altUrunler.OrderBy(x => x.KDVFiyat).ToList();
            }
            else if (sortType == "priceDesc")
            {
                urunler = urunler.OrderByDescending(x => x.KDVFiyat).ToPagedList(pageNumber, 24);
                altUrunler = altUrunler.OrderByDescending(x => x.KDVFiyat).ToList();
            }
            else if (sortType == "nameDesc")
            {
                urunler = urunler.OrderByDescending(x => x.UrunAdi).ToPagedList(pageNumber, 24);
                altUrunler = altUrunler.OrderByDescending(x => x.KDVFiyat).ToList();
            }
            else if (sortType == "topSeller")
            {
                urunler = urunler.OrderByDescending(x => x.UrunAdi).ToPagedList(pageNumber, 24);
            }

            ViewBag.Urunler = urunler;
            ViewBag.AltUrunler = altUrunler;

            return View();
        }

        [CheckOtherLogin]
        [HttpGet, ActionName("FilterByProductList")]
        public async Task<IActionResult> FilterByProductList(int? page, int KategoriID, string sortType)
        {

            await FilterCategoryItemsAsync(KategoriID);

            int pageNumber = page ?? 1;

            IPagedList<Urun> urunler = _unitOfWorkUrun.Repository
             .Where(x => x.KategoriID == KategoriID && x.UstUrunID == 0 && x.Adet > 0 && x.IndirimOrani == 0)
             .Include("kategori")
             .Include("kullanici")
             .Include("marka")
             .Include("stokbirimi")
             .Include("begeniler")
             .ToPagedList(pageNumber, 24);

            List<Urun> altUrunler = _unitOfWorkUrun.Repository.Where(x => x.UstUrunID != 0 && x.Adet > 0).Include("kategori")
             .Include("kullanici")
             .Include("marka")
             .Include("stokbirimi")
             .Include("begeniler")
             .ToList();

            if (sortType == "priceAsc")
            {
                urunler = urunler.OrderBy(x => x.KDVFiyat).ToPagedList(pageNumber, 24);
                altUrunler = altUrunler.OrderBy(x => x.KDVFiyat).ToList();
            }
            else if (sortType == "priceDesc")
            {
                urunler = urunler.OrderByDescending(x => x.KDVFiyat).ToPagedList(pageNumber, 24);
                altUrunler = altUrunler.OrderByDescending(x => x.KDVFiyat).ToList();
            }
            else if (sortType == "nameDesc")
            {
                urunler = urunler.OrderByDescending(x => x.UrunAdi).ToPagedList(pageNumber, 24);
                altUrunler = altUrunler.OrderByDescending(x => x.KDVFiyat).ToList();
            }

            ViewBag.Urunler = urunler;
            ViewBag.AltUrunler = altUrunler;

            return View();
        }

        public async Task FilterCategoryItemsAsync(int KategoriID)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = gelenBayi;

            Slider gelenSlider = await _unitOfWorkSlider.Repository.Where(x => x.ID > 0 && x.IsUse == true && x.IsActive == true).SingleOrDefaultAsync();

            if (gelenSlider != null)
            {
                List<SliderItem> sliderItems = await _unitOfWorkSliderItem.Repository.Where(x => x.SliderID == gelenSlider.ID).OrderByDescending(x => x.OrderBy).ToListAsync();
                ViewBag.SliderItems = sliderItems;

                List<SliderBannerItem> sliderBannerItems = await _unitOfWorKSliderBannerItem.Repository.Where(x => x.SliderID == gelenSlider.ID).OrderByDescending(x => x.OrderBy).ToListAsync();
                ViewBag.SliderBannerItems = sliderBannerItems;

            }

            TempData["KategoriID"] = KategoriID;
            Kategori getCategory = await _unitOfWorkKategoriler.Repository.GetById(KategoriID);
            Kategori mainCategory = await _unitOfWorkKategoriler.Repository.Where(x => x.ID == getCategory.UstKategori).SingleOrDefaultAsync();
            if (mainCategory != null)
            {
                List<Kategori> subCategories = await _unitOfWorkKategoriler.Repository.Where(x => x.UstKategori == mainCategory.ID).ToListAsync();
                ViewBag.AltKategoriler = subCategories;
                ViewBag.UstKategori = mainCategory.KategoriAdi;
            }

            List<Urun> newList = _unitOfWorkUrun.Repository.Where(x => x.KategoriID == KategoriID && x.UstUrunID == 0 && x.Adet > 0 && x.IndirimOrani == 0)
           .Include("kategori").Include("kullanici").Include("marka").Include("stokbirimi").Include("begeniler").ToList();

            ListMarkaForFilter(newList);

            ViewBag.Bayi = gelenBayi;
            TempData["KategoriID"] = KategoriID;
            TempData["KategoriAdi"] = getCategory.KategoriAdi;

        }

        public void ListMarkaForFilter(List<Urun> newList)
        {
            List<Marka> newListMarka = new List<Marka>();

            foreach (Urun item in newList)
            {
                Marka newMarka = new Marka();
                newMarka = item.marka;
                newListMarka.Add(newMarka);
            }

            ViewBag.MarkalarListToFilter = newListMarka.ToList();
        }

        #endregion

        #region PayDebt

        public async Task<IActionResult> PayToDebtSeller(int ID, decimal miktar)
        {
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.GetById(ID);
            List<CuzdanIslemler> borc = await _unitOfWorkCuzdanIslemler.Repository.Where(x => x.cuzdanHesabi.BayiID == gelenBayi.ID && x.IslemDurum != CORE.Enums.IslemDurum.BorcOdendi).Include("cuzdanHesabi").ToListAsync();
            decimal tutar = borc.Sum(x => x.IslemTutari);
            decimal hesap = tutar - miktar;
            if (hesap == 0)
            {
                foreach (CuzdanIslemler item in borc)
                {
                    item.IslemDurum = CORE.Enums.IslemDurum.BorcOdendi;
                    _unitOfWorkCuzdanIslemler.Repository.Update(item);
                    _unitOfWorkCuzdanIslemler.Repository.Save();
                }
                gelenBayi.AktifMi = true;
                _unitOfWorkBayi.Repository.Update(gelenBayi);
                _unitOfWorkBayi.Repository.Save();

                return RedirectToAction("SellerLogin", "Bayi");
            }
            else if (hesap > 0)
            {
                TempData["mesaj"] = "Hala " + tutar + "₺ kadar borcunuz bulunmaktadır";
                await PayDebutNotification();
                return View("PayDebutNotification", gelenBayi);
            }
            else
            {
                TempData["mesaj"] = "Hata Oluştu";
                await PayDebutNotification();
                return View("PayDebutNotification", gelenBayi);
            }

        }

        public async Task<IActionResult> PayDebutNotification()
        {

            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == Id).Include("musteriGrubu").FirstOrDefaultAsync();

            List<CuzdanIslemler> cuzdanIslemler = await _unitOfWorkCuzdanIslemler.Repository.Where(x => x.cuzdanHesabi.BayiID == gelenBayi.ID && x.OdemeSekliID == 2 && x.IslemDurum == CORE.Enums.IslemDurum.BorcBildirildi).Include("cuzdanHesabi").ToListAsync();

            ViewBag.Count = cuzdanIslemler.Sum(x => x.IslemTutari);

            return View(gelenBayi);
        }

        #endregion

        #region PartialViews

        [HttpGet]
        public IActionResult CampaingProductProductList()
        {
            return PartialView("CampaingProductProductList");
        }

        [HttpGet]
        public IActionResult CampaingProductProductGrid()
        {
            return PartialView("CampaingProductProductGrid");
        }

        [HttpGet]
        public IActionResult SliderViewPartial()
        {
            return PartialView("SliderViewPartial");

        }

        [HttpGet]
        public IActionResult BrandViewPartial()
        {
            return PartialView("BrandViewPartial");
        }
        [HttpGet]
        public IActionResult FooterViewPartial()
        {
            return PartialView("FooterViewPartial");
        }

        [HttpGet]
        public IActionResult CategoriesMenuListViewPartial()
        {
            return PartialView("CategoriesMenuListViewPartial");
        }

        [HttpGet]
        public IActionResult HeaderActionLinksAndUserViewPartial()
        {
            return PartialView("HeaderActionLinksAndUserViewPartial");
        }

        [HttpGet]
        public IActionResult NewsViewPartial()
        {
            return PartialView("NewsViewPartial");
        }

        #endregion

        #region Cargo

        [HttpGet]
        public async Task<IActionResult> SellerCargoCheck(int? page)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            int pageNumber = page ?? 1;
            ViewBag.Kargo = await _unitOfWorkKargo.Repository.Where(x => x.BayiID == id).Include("siparis").Include("bayiAdresler").Include("bayi").Include("kargoDetayi").Include("kargoFirmaDetay").ToPagedListAsync(page, 20);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CargoDetailBySeller(int Id)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            Kargo getCargo = await _unitOfWorkKargo.Repository.Where(x => x.ID == Id && x.BayiID == id).Include("siparis").Include("bayiAdresler").Include("bayi").Include("kargoDetayi").Include("kargoFirmaDetay").SingleOrDefaultAsync();

            List<Sepet> siparisDetay = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == getCargo.siparis.ID)
                  .Include("UrunSepet").ToListAsync();

            SiparisDurum getOrderStatus = await _unitOfWorkSiparisDurum.Repository.Where(x => x.ID == getCargo.siparis.SiparisDurumID && x.AktifMi == true).SingleOrDefaultAsync();

            CheckCargoOrderStatus(getOrderStatus.Durum);
            CargoTrackingInformation cargoTrackingInformation = _cargoService.GetCargoTrackingInformation(getCargo.KargoReferansNo);
            ViewBag.CargoTracking = cargoTrackingInformation;
            ViewBag.SiparisDetaylari = siparisDetay;
            ViewBag.TotalPrice = siparisDetay.Select(x => x.ToplamSatır).Sum();

            return View(getCargo);
        }

        private void CheckCargoOrderStatus(string durum)
        {
            if (siparisDurum.SiparisAlindi.ToString() == durum)
            {
                ViewBag.SiparisAlindi = true;
            }
            else if (siparisDurum.KargoHazirlandi.ToString() == durum)
            {
                ViewBag.KargoHazirlandi = true;
            }
            else if (siparisDurum.GonderiYolda.ToString() == durum)
            {
                ViewBag.GonderiYolda = true;
            }
            else if (siparisDurum.TeslimEdildi.ToString() == durum)
            {
                ViewBag.TeslimEdildi = true;
            }
            else
            {
                //Burası boş kalacak şimdilik
            }
        }

        #endregion

        #region SpecialOffer

        [HttpGet]
        public async Task<IActionResult> SpecialOfferBySeller(int? page)
        {
            int pageNumber = page ?? 1;
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.OzelTeklifler = await _unitOfWorkOzelTeklif.Repository.Where(x => x.BayiID == id && x.Durum == true && x.TeklifDurum == OzelTeklifDurum.TeklifGonderildi).Include("siparisOzelTeklif").Include("bayiOzelTeklif").ToPagedListAsync(pageNumber, 20);
            IPagedList<OzelTeklif> teklifList = ViewBag.OzelTeklifler as IPagedList<OzelTeklif>;
            return View(teklifList);
        }

        public async Task<IActionResult> SpecialOfferBySellerReadyToBasket(int Id)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi bayiSepette = await _unitOfWorkBayi.Repository.GetById(id);
            Cuzdan cuzdan = await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == bayiSepette.ID).SingleOrDefaultAsync();
            CuzdanIslemler cuzdanIslemi = await _unitOfWorkCuzdanIslemler.Repository.Where(x => x.CuzdanID == cuzdan.ID && x.IslemDurum == CORE.Enums.IslemDurum.BorcBildirildi).SingleOrDefaultAsync();

            if (cuzdanIslemi == null)
            {
                try
                {
                    OzelTeklif teklif = await _unitOfWorkOzelTeklif.Repository.Where(x => x.ID == Id).Include("bayiOzelTeklif").Include("siparisOzelTeklif").SingleOrDefaultAsync();

                    Siparis ozelTeklifsiparis = await _unitOfWorkSiparis.Repository.Where(x => x.BayiID == bayiSepette.ID && x.ID == teklif.SiparisID).SingleOrDefaultAsync();
                    ozelTeklifsiparis.Durum = true;

                    _unitOfWorkSiparis.Repository.Update(ozelTeklifsiparis);
                    _unitOfWorkSiparis.Repository.Save();

                    List<Sepet> BasketiGetir = await _unitOfWorkSepet.Repository.Where(x => x.BayiID == bayiSepette.ID && x.SiparisID == ozelTeklifsiparis.ID).ToListAsync();
                    foreach (Sepet item in BasketiGetir)
                    {
                        item.urunSepetteMi = true;
                        _unitOfWorkSepet.Repository.Update(item);
                        _unitOfWorkSepet.Repository.Save();
                    }

                    List<Siparis> AktifSiparisler = await _unitOfWorkSiparis.Repository.Where(x => x.BayiID == bayiSepette.ID && x.ID != ozelTeklifsiparis.ID).ToListAsync();
                    foreach (Siparis item in AktifSiparisler)
                    {
                        if (item.Durum == true)
                        {
                            item.Durum = false;
                            _unitOfWorkSiparis.Repository.Update(item);
                            _unitOfWorkSiparis.Repository.Save();

                            List<Sepet> AktifSepetUrunleri = await _unitOfWorkSepet.Repository.Where(x => x.BayiID == bayiSepette.ID && x.SiparisID == item.ID).ToListAsync();
                            foreach (Sepet itemUrun in AktifSepetUrunleri)
                            {
                                if (itemUrun.urunSepetteMi == true)
                                {
                                    itemUrun.urunSepetteMi = false;
                                    _unitOfWorkSepet.Repository.Update(itemUrun);
                                    _unitOfWorkSepet.Repository.Save();
                                }
                            }
                        }
                    }
                    teklif.Durum = true;
                    teklif.TeklifDurum = OzelTeklifDurum.TeklifBitti;
                    _unitOfWorkOzelTeklif.Repository.Update(teklif);
                    _unitOfWorkOzelTeklif.Repository.Save();

                    return RedirectToAction("Basket", "Siparis");
                }
                catch (Exception)
                {
                    return RedirectToAction("HomePageList", "Home");
                }
            }

            else
            {
                return RedirectToAction("PayDebutNotification", "Home");
            }

        }

        #endregion

        #region Other

        [HttpGet]
        public async Task<IActionResult> SiteInfosDetail(int Id)
        {
            SiteInfo getSiteInfo = await _unitOfWorkSiteInfo.Repository.Where(x => x.ID == Id).SingleOrDefaultAsync();
            return View(getSiteInfo);
        }

        [HttpGet]
        public async Task<IActionResult> NewsDetail(int Id)
        {
            Slider gelenSlider = await _unitOfWorkSlider.Repository.Where(x => x.ID > 0 && x.IsUse == true && x.IsActive == true).FirstOrDefaultAsync();
            if (gelenSlider != null)
            {
                List<SliderBannerItem> sliderBannerItems = await _unitOfWorKSliderBannerItem.Repository.Where(x => x.SliderID == gelenSlider.ID && x.ItemName != "NoName").OrderByDescending(x => x.OrderBy).ToListAsync();
                ViewBag.SliderBannerItems = sliderBannerItems;
            }

            Haber getNews = await _unitOfWorkHaber.Repository.Where(x => x.ID == Id).Include("kategori").SingleOrDefaultAsync();
            return View(getNews);
        }

        #endregion

        #region NotUse

        //public async Task<int> KargoSureKontrol()
        //{
        //    int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
        //    KargoSure sureGelen = await _unitOfWorkKargoSure.Repository.Where(x => x.BayiID == id && x.AktifMi == true).SingleOrDefaultAsync();

        //    if (sureGelen != null)
        //    {               
        //        DateTime simdikiZaman = DateTime.Now;
        //        if (sureGelen.Bitis.Date == simdikiZaman.Date)
        //        {
        //            sureGelen.AktifMi = false;
        //            _unitOfWorkKargoSure.Repository.Update(sureGelen);
        //            _unitOfWorkKargoSure.Repository.Save();
        //        }
        //    }
        //    return 1;
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[CheckOtherLogin]
        //[HttpGet, ActionName("FilterByBrandListTwo")]
        //public async Task<IActionResult> FilterByBrandListTwo(int? page, int MarkaID)
        //{
        //    TempData["MarkaID"] = MarkaID;
        //    int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
        //    Bayi gelenBayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").FirstOrDefaultAsync();
        //    ViewBag.Bayi = gelenBayi;
        //    await LoadBrandData(page, MarkaID);
        //    return View();

        //}

        //[HttpPost, ActionName("FilterByBrandList")]
        //public async Task<IActionResult> FilterByBrandList(int? page, string selectedItems, string selectedItemsProduct)
        //{

        //    int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
        //    Bayi gelenBayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").FirstOrDefaultAsync();

        //    if (selectedItems != null)
        //    {
        //        TreeViewNode item = JsonConvert.DeserializeObject<TreeViewNode>(selectedItems);
        //        int MarkaID = Convert.ToInt32(item.id);
        //        TempData["MarkaID"] = MarkaID;
        //        ViewBag.Bayi = gelenBayi;
        //        await LoadBrandData(page, MarkaID);
        //        return View();
        //    }
        //    else if (selectedItemsProduct != null)
        //    {
        //        TreeViewNode item = JsonConvert.DeserializeObject<TreeViewNode>(selectedItemsProduct);
        //        int MarkaID = Convert.ToInt32(item.id);
        //        TempData["MarkaID"] = MarkaID;
        //        ViewBag.Bayi = gelenBayi;
        //        await LoadBrandData(page, MarkaID);
        //        return View();
        //    }

        //    else
        //    {
        //        await HomePageList(page);
        //        return View("HomePageList");
        //    }

        //}

        #endregion

        public FileResult DownloadDocumentCatalog(int ID)
        {
            Dokuman getDocument = _unitOfWorkDokuman.Repository.GetById(ID).Result;
            MemoryStream stream = SaveImageProcess.GetDownloadableFile(getDocument.DosyaYolu, "Dokumanlar");
            stream.Position = 0;
            string extensions = new FileInfo(getDocument.DosyaYolu).Extension;
            string fileTypes = MimeTypesMap.GetMimeType(getDocument.DosyaYolu);
            return File(stream, fileTypes, $"{getDocument.KatalogAdi}{extensions}");
        }

    }

    public static class SessionExtensionMethod
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }
        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            T valueToDeserialize = JsonConvert.DeserializeObject<T>(objectString);
            return valueToDeserialize;
        }
    }

}
