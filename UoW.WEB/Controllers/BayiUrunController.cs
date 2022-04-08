using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.WEB.Controllers
{
    public class BayiUrunController : Controller
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
        private readonly IUnitOfWork<UrunStok> _unitOfWorkStoktakiler;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
        private readonly IUnitOfWork<UstListe> _unitOfWorkUstList;
        private readonly IUnitOfWork<Liste> _unitOfWorkListe;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;
        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepetler;
        private readonly IUnitOfWork<UrunRenk> _unitOfWorkUrunRenkler;
        private readonly IConfiguration _configuration;

        private readonly int? page;
        private readonly int? markaPage;

        public BayiUrunController(IUnitOfWork<Bayi> unitOfWorkBayi,
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
            IConfiguration configuration,
            IUnitOfWork<UrunStok> unitOfWorkStoktakiler,
            IUnitOfWork<Begeni> unitOfWOrkBegeni,
            IUnitOfWork<Liste> unitOfWorkListe,
            IUnitOfWork<UstListe> unitOfWorkUstList,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<Begeni> unitOfWorkBegeni,
            IUnitOfWork<Haber> unitOfWorkHaber,
            IUnitOfWork<SiteInfo> unitOfWorkSiteInfo,
            IUnitOfWork<Sepet> unitOfWorkSepetler,
            IUnitOfWork<UrunRenk> unitOfWorkUrunRenkler)
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
            this._unitOfWorkStoktakiler = unitOfWorkStoktakiler;
            this._unitOfWorkBegeni = unitOfWOrkBegeni;
            this._unitOfWorkUstList = unitOfWorkUstList;
            this._unitOfWorkListe = unitOfWorkListe;
            this._unitOfWorkBegeni = unitOfWorkBegeni;
            this._unitOfWorkStokBildirim = unitOfWorkStokBildirim;
            this._unitOfWorkHaber = unitOfWorkHaber;
            this._unitOfWorkSiteInfo = unitOfWorkSiteInfo;
            this._unitOfWorkSepetler = unitOfWorkSepetler;
            this._unitOfWorkUrunRenkler = unitOfWorkUrunRenkler;
        }

        #endregion

        [CheckOtherLogin]
        [HttpGet, ActionName("ListProductSpecialBayi")]
        public async Task<IActionResult> ListProductSpecialBayi(int? page)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            Bayi gelenBayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").FirstOrDefaultAsync();

            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkMusteriOzelUrun.Repository.Where(x => x.MusteriGrupID == gelenBayi.musteriGrubu.ID)
               .Include("urunOzelUrun")
               .Include("urunOzelUrun.kategori")
               .Include("urunOzelUrun.marka")
               .Include("urunOzelUrun.kullanici")
               .ToPagedList(pageNumber, 24);

            List<Urun> urunNo = (List<Urun>)await _unitOfWorkUrun.Repository.All();
            ViewBag.UrunNo = new SelectList(urunNo, "ID", "UrunNo");

            //await CreateModeratorLog("Başarılı", "Listeleme", "ListProduct", "Urun");
            return View();
        }

        [CheckOtherLogin]
        [HttpGet, ActionName("AllProductList")]
        public async Task<IActionResult> AllProductList(int? page)
        {

            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
               .Where(x => x.ID > 0 && x.UstUrunID == 0)
               .Include("kategori")
               .Include("kullanici")
               .Include("marka")
               .Include("stokbirimi")
               .ToPagedList(pageNumber, 24);

            List<Urun> urunNo = (List<Urun>)await _unitOfWorkUrun.Repository.All();
            ViewBag.UrunNo = new SelectList(urunNo, "ID", "UrunNo");

            //await CreateModeratorLog("Başarılı", "Listeleme", "ListProduct", "Urun");
            return View();
        }

        [HttpGet]
        public IActionResult DetailPartialProductPrice()
        {

            return PartialView("DetailPartialProductPrice");
        }

        [CheckOtherLogin]
        public async Task<IActionResult> DetailProduct(int Id)
        {

            int IdBayi = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi bayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == IdBayi).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = bayi;

            StokBildirim bildirimVarmi = await _unitOfWorkStokBildirim.Repository.Where(x => x.UrunId == Id && x.BayiId == IdBayi && x.Durum == CORE.Enums.StokBildirimDurum.BildirimYapildi).FirstOrDefaultAsync();

            if (bildirimVarmi != null)
            {
                bildirimVarmi.Durum = CORE.Enums.StokBildirimDurum.Okundu;
                _unitOfWorkStokBildirim.Repository.Update(bildirimVarmi);
                _unitOfWorkStokBildirim.Repository.Save();
            }

            Urun u = await _unitOfWorkUrun.Repository.Where(x => x.ID == Id).Include("kategori").Include("marka").Include("stokbirimi").SingleAsync();

            List<UrunRenk> urunRenks = await _unitOfWorkUrunRenkler.Repository.Where(x => x.UrunID == Id).Include("renk").ToListAsync();
            ViewBag.UrunRenkler = urunRenks;

            List<UrunResimler> uResimlers = await _unitOfWorkUrunResimler.Repository.Where(x => x.UrunID == u.ID).ToListAsync();
            ViewBag.UrunResimler = uResimlers;

            List<UrunFiyat> uFiyatlars = await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == Id).ToListAsync();
            ViewBag.UrunFiyat = uFiyatlars;

            List<Urun> altUrunler = await _unitOfWorkUrun.Repository.Where(x => x.UstUrunID == u.ID).ToListAsync();
            ViewBag.AltUrunler = altUrunler;

            List<EtiketGonderi> etiketler = await _unitOfWorkEtiketUrunler.Repository.Where(x => x.GonderiID == u.ID).Include("etiketler").ToListAsync();
            ViewBag.Etiketler = etiketler;

            List<Begeni> begeniler = await _unitOfWorkBegeni.Repository.Where(x => x.ID > 0).ToListAsync();

            List<Urun> urunler = ViewBag.Urunler;

            foreach (var item in begeniler)
            {
                if (item.UrunID == u.ID)
                {
                    TempData["Disable"] = true;
                }
                else
                {
                    TempData["Disable"] = false;
                }
            }


            //u.Fiyat = await CalculateDiscountProduct(u);
            //await CreateModeratorLog("Başarılı", "Detay", "DetailProduct", "Urun");
            return View(u);
        }

        [CheckOtherLogin]
        public async Task<IActionResult> DetailProductSearch(int productId)
        {

            Urun getProduct = await _unitOfWorkUrun.Repository.Where(x => x.ID == productId).Include("kategori").Include("marka").Include("stokbirimi").FirstOrDefaultAsync();

            List<UrunResimler> uResimlers = await _unitOfWorkUrunResimler.Repository.Where(x => x.UrunID == productId).ToListAsync();
            ViewBag.UrunResimler = uResimlers;

            List<UrunFiyat> uFiyatlars = await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == productId).ToListAsync();
            ViewBag.UrunFiyat = uFiyatlars;

            List<Urun> altUrunler = await _unitOfWorkUrun.Repository.Where(x => x.UstUrunID == productId).ToListAsync();
            ViewBag.AltUrunler = altUrunler;

            List<EtiketGonderi> etiketler = await _unitOfWorkEtiketUrunler.Repository.Where(x => x.GonderiID == productId).Include("etiketler").ToListAsync();
            ViewBag.Etiketler = etiketler;

            List<Begeni> begeniler = await _unitOfWorkBegeni.Repository.Where(x => x.ID > 0).ToListAsync();

            List<Urun> urunler = ViewBag.Urunler;

            foreach (var item in begeniler)
            {
                if (item.UrunID == productId)
                {
                    TempData["Disable"] = true;
                }
                else
                {
                    TempData["Disable"] = false;
                }
            }

            int IdBayi = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi bayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == IdBayi).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = bayi;

            //u.Fiyat = await CalculateDiscountProduct(u);
            //await CreateModeratorLog("Başarılı", "Detay", "DetailProduct", "Urun");
            return View(getProduct);
        }

        [CheckOtherLogin]
        public async Task<IActionResult> DetailProductByColor(int colorId)
        {
            UrunRenk getUrunRenk = await _unitOfWorkUrunRenkler.Repository.Where(x => x.ID == colorId).Include("urun").Include("renk").SingleOrDefaultAsync();

            Urun getProduct = await _unitOfWorkUrun.Repository.Where(x => x.ID == getUrunRenk.UrunID).Include("kategori").Include("marka").Include("stokbirimi").FirstOrDefaultAsync();

            List<UrunResimler> uResimlers = await _unitOfWorkUrunResimler.Repository.Where(x => x.UrunID == getUrunRenk.UrunID).ToListAsync();
            ViewBag.UrunResimler = uResimlers;

            List<UrunFiyat> uFiyatlars = await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == getUrunRenk.UrunID).ToListAsync();
            ViewBag.UrunFiyat = uFiyatlars;

            List<Urun> altUrunler = await _unitOfWorkUrun.Repository.Where(x => x.UstUrunID == getUrunRenk.UrunID).ToListAsync();
            ViewBag.AltUrunler = altUrunler;

            List<EtiketGonderi> etiketler = await _unitOfWorkEtiketUrunler.Repository.Where(x => x.GonderiID == getUrunRenk.UrunID).Include("etiketler").ToListAsync();
            ViewBag.Etiketler = etiketler;

            List<UrunRenk> urunRenks = await _unitOfWorkUrunRenkler.Repository.Where(x => x.UrunID == getUrunRenk.UrunID).Include("renk").ToListAsync();
            ViewBag.UrunRenkler = urunRenks;

            List<Begeni> begeniler = await _unitOfWorkBegeni.Repository.Where(x => x.ID > 0).ToListAsync();

            List<Urun> urunler = ViewBag.Urunler;

            foreach (var item in begeniler)
            {
                if (item.UrunID == getUrunRenk.UrunID)
                {
                    TempData["Disable"] = true;
                }
                else
                {
                    TempData["Disable"] = false;
                }
            }

            int IdBayi = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi bayi = await _unitOfWorkBayi.Repository.Where(x => x.ID == IdBayi).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = bayi;

            //u.Fiyat = await CalculateDiscountProduct(u);
            //await CreateModeratorLog("Başarılı", "Detay", "DetailProduct", "Urun");
            return View(getProduct);
        }

        private async Task<Decimal> CalculateDiscountProduct(Urun u)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").SingleOrDefaultAsync();
            MusteriGrubu mg = await _unitOfWorkMusteriGrubu.Repository.Where(x => x.ID == b.BayiGrubuID).SingleOrDefaultAsync();
            decimal indirimOran = mg.IskontoOrani;
            decimal hesap = (u.Fiyat * indirimOran) / 100;
            decimal toplam = u.Fiyat - hesap;
            return toplam;
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> MyUnitStockInProduct(int? page)
        {

            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            var pageNumber = page ?? 1;
            ViewBag.StokTakiler = await _unitOfWorkStoktakiler.Repository.Where(x => x.BayiID == Id)
                .Include("bayiStok")
                .Include("bayiUrun")
                .OrderByDescending(x => x.ID)
                .ToPagedListAsync(pageNumber, 30);

            return View();
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> InsertLike(int ID)
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.GetById(id);
            Urun u = (Urun)await _unitOfWorkUrun.Repository.GetById(ID);

            try
            {
                Begeni begeni = new Begeni
                {
                    BayiID = b.ID,
                    bayiBegeni = b,
                    UrunID = ID,
                    urunBegeni = u,
                    EklenmeTarih = DateTime.Now
                };

                await _unitOfWorkBegeni.Repository.Add(begeni);
                _unitOfWorkBegeni.Repository.Save();

                return RedirectToAction("HomePageList", "Home");

            }
            catch (Exception)
            {
                return RedirectToAction("HomePageList", "Home");
            }
        }

        [CheckOtherLogin]
        [HttpGet]
        public IActionResult ListProductLikes(int? page)
        {

            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            int pageNumber = page ?? 1;
            ViewBag.BegeniList = _unitOfWorkBegeni.Repository
                .Where(x => x.BayiID == Id)
                .Include("urunBegeni")
                .ToPagedList(pageNumber, 24);

            return View();
        }

        [CheckOtherLogin]
        [HttpGet]
        public IActionResult DissLike(int ID)
        {

            DissLikeDetail(ID);
            return View("ListProductLikes");
        }

        [CheckOtherLogin]
        [HttpGet]
        public IActionResult DissLikeHomePageList(int ID)
        {
            DissLikeDetail(ID);
            return RedirectToAction("HomePageList", "Home");
        }

        private void DissLikeDetail(int ID)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            Begeni gelenBegeni = _unitOfWorkBegeni.Repository.FirstOrDefault(x => x.UrunID == ID && x.BayiID == id);
            _unitOfWorkBegeni.Repository.Delete(gelenBegeni);
            _unitOfWorkBegeni.Repository.Save();

            ListProductLikes(page);
        }

        [HttpGet]
        public async Task<IActionResult> ListParentList(int ID)
        {
            Urun u = await _unitOfWorkUrun.Repository.GetById(ID);
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            List<UstListe> ustLists = await _unitOfWorkUstList.Repository.Where(x => x.BayiID == id).ToListAsync();
            if (ustLists.Count == 0)
            {
                return RedirectToAction("CreateList", "BayiUrun", new { ID });
            }
            else
            {
                return View(ustLists);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateList(int ID)
        {

            Urun u = await _unitOfWorkUrun.Repository.GetById(ID);
            ViewBag.Urun = u;
            return PartialView();
        }

        [CheckOtherLogin]
        [HttpPost]
        public async Task<JsonResult> CreateListJson(string ListeAdi)
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.GetById(id);

            try
            {
                UstListe ul = new UstListe
                {
                    ListeAdi = ListeAdi,
                    GuncellenmeTarih = DateTime.Now,
                    EklenmeTarih = DateTime.Now,
                    BayiID = gelenBayi.ID,
                    bayiListUst = gelenBayi
                };
                await _unitOfWorkUstList.Repository.Add(ul);
                _unitOfWorkUstList.Repository.Save();

                return Json(true);
                //return RedirectToAction("CreateSubListItem","BayiUrun",new { ID = ul.ID, UrunID = ID });
            }
            catch (Exception)
            {
                return Json(true);
                //await DetailProduct(ID);
                //return View("DetailProduct");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateSubListItem(int ID, int UrunID)
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("Idb"));
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.GetById(id);

            try
            {
                Urun gelenUrun = await _unitOfWorkUrun.Repository.GetById(UrunID);
                UstListe list = await _unitOfWorkUstList.Repository.GetById(ID);
                Liste liste = new Liste
                {
                    UrunID = gelenUrun.ID,
                    urunListe = gelenUrun,
                    bayiList = gelenBayi,
                    EklenmeTarih = DateTime.Now,
                    BayiID = gelenBayi.ID,
                    ListeUstID = list.ID,
                    listeUst = list
                };

                await _unitOfWorkListe.Repository.Add(liste);
                _unitOfWorkListe.Repository.Save();
                await DetailProduct(gelenUrun.ID);
                return View("DetailProduct");
            }
            catch (Exception)
            {
                await DetailProduct(UrunID);
                return View("DetailProduct");
            }
        }

        public async Task<IActionResult> ListParentLists()
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.GetById(id);
            List<UstListe> ustListe = await _unitOfWorkUstList.Repository.Where(x => x.BayiID == gelenBayi.ID).ToListAsync();
            return View(ustListe);
        }

        [HttpGet]
        public async Task<IActionResult> ListSubItems(int ID)
        {

            UstListe ustList = await _unitOfWorkUstList.Repository.GetById(ID);
            List<Liste> subList = await _unitOfWorkListe.Repository.Where(x => x.ListeUstID == ustList.ID).Include("urunListe").Include("bayiList").ToListAsync();
            return View(subList);
        }

        public async Task<IActionResult> NotificateProductWhenStock(int Id)
        {

            int bayiId = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            StokBildirim bildirimVarmi = _unitOfWorkStokBildirim.Repository.Where(x => x.BayiId == bayiId && x.UrunId == Id && x.Durum == CORE.Enums.StokBildirimDurum.Bekleniyor).FirstOrDefault();

            Urun urun = await _unitOfWorkUrun.Repository.GetById(Id);

            if (urun.Adet == 0)
            {
                if (bildirimVarmi == null)
                {
                    StokBildirim sb = new StokBildirim
                    {
                        BayiId = bayiId,
                        UrunId = Id,
                        Durum = CORE.Enums.StokBildirimDurum.Bekleniyor,
                        OlusturulmaTarihi = DateTime.Now
                    };

                    await _unitOfWorkStokBildirim.Repository.Add(sb);
                    _unitOfWorkStokBildirim.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Stoğa geldiğinde size bilgilendirme yapacağız. İşleminiz başarıyla alındı!', 'Stoğa Bildirim')";
                    return Redirect(Request.GetTypedHeaders().Referer.AbsolutePath);
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Bu ürün için isteğiniz alınmış durumda!', 'Stoğa Bildirim')";
                    return Redirect(Request.GetTypedHeaders().Referer.AbsolutePath);
                }
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Bu ürün stokta mevcuttur!', 'Stoğa Bildirim')";
                return Redirect(Request.GetTypedHeaders().Referer.AbsolutePath);

            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductComparison()
        {
            return View();
        }
    }
}
