using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;
using X.PagedList;

namespace UoW.WEB.Controllers
{
    public class PageController : Controller
    {

        #region Fields

        private readonly IUnitOfWork<HaberKategori> _unitOfWorkHaberKategoriler;
        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;
        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategoriler;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarka;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Dokuman> _unitOfWorkDokuman;
        private readonly IUnitOfWork<DokumanTipi> _unitOfWorkDokumanTipi;
        private readonly IUnitOfWork<Fatura> _unitOfWorkFatura;
        private readonly IUnitOfWork<Kargo> _unitOfWorkKargo;
        private readonly IUnitOfWork<KargoDetay> _unitOfWorkKargoDetay;
        //private readonly IUnitOfWork<Bulten> _unitOfWorkBultenler;

        public PageController(IUnitOfWork<Haber> unitOfWorkHaberler,
            IUnitOfWork<HaberKategori> unitOfWorkHaberKategoriler,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Begeni> unitOfWorkBegeni,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<SiteInfo> unitOfWorkSiteInfo,
            IUnitOfWork<Sepet> unitOfWorkSepet,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Dokuman> unitOfWorkDokuman,
            IUnitOfWork<DokumanTipi> unitOfWorkDokumanTipi, IUnitOfWork<Fatura> unitOfWorkFatura, IUnitOfWork<Kargo> unitOfWorkKargo, IUnitOfWork<KargoDetay> unitOfWorkKargoDetay/*, IUnitOfWork<Bulten> uniOfWorkBultenler*/)
        {
            this._unitOfWorkHaber = unitOfWorkHaberler;
            this._unitOfWorkHaberKategoriler = unitOfWorkHaberKategoriler;
            this._unitOfWorkMarka = unitOfWorkMarka;
            this._unitOfWorkKategoriler = unitOfWorkKategori;
            this._unitOfWorkBegeni = unitOfWorkBegeni;
            this._unitOfWorkStokBildirim = unitOfWorkStokBildirim;
            this._unitOfWorkSiteInfo = unitOfWorkSiteInfo;
            this._unitOfWorkSepet = unitOfWorkSepet;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkDokuman = unitOfWorkDokuman;
            this._unitOfWorkDokumanTipi = unitOfWorkDokumanTipi;
            this._unitOfWorkFatura = unitOfWorkFatura;
            this._unitOfWorkKargo = unitOfWorkKargo;
            this._unitOfWorkKargoDetay = unitOfWorkKargoDetay;
            //this._unitOfWorkBultenler = uniOfWorkBultenler;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> News(int? page)
        {

            int pageNumber = page ?? 1;
            ViewBag.HaberList = await _unitOfWorkHaber.Repository.Where(x => x.AktifMi == true).Include("kategori").ToPagedListAsync(pageNumber, 30);
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> BulletinList(int? page) 
        //{
        //    //ViewBag.Bultenler = await _unitOfWorkBultenler.Repository.Where(x => x.Durum == true).Include(x=> x.).ToListAsync();
        //    return View();
        //}

        [HttpGet]
        public IActionResult FAQs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CatalogList(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.Katalog = await _unitOfWorkDokuman.Repository.Where(x => x.DokumanTipi == 1 && x.Durum == true).OrderByDescending(x => x.EklenmeTarih).ToPagedListAsync(pageNumber,24);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PriceList(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.FiyatListesi = await _unitOfWorkDokuman.Repository.Where(x => x.DokumanTipi == 2 && x.Durum == true).OrderByDescending(x => x.EklenmeTarih).ToPagedListAsync(pageNumber, 24);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyBillList(int? page)
        {
            int pageNumber = page ?? 1;
            int bayiID = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.Faturalar = await _unitOfWorkFatura.Repository.Where(x => x.BayiID == bayiID).Include("bayiAdresler").OrderByDescending(x=> x.EklenmeTarih).ToPagedListAsync(page, 24);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BillDetail(int ID)
        {
            Fatura getBill = await _unitOfWorkFatura.Repository.Where(x => x.ID == ID).Include("bayiAdresler").Include("siparisFatura").Include("bayiFatura").FirstOrDefaultAsync();

            List<Sepet> basketList = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == getBill.SiparisID).Include("UrunSepet").ToListAsync();
            ViewBag.Sepetler = basketList;

            List<Kargo> cargos = await _unitOfWorkKargo.Repository.Where(x => x.SiparisID == getBill.SiparisID).Include("kargoFirmaDetay").Include("kargoDetayi").ToListAsync();
            ViewBag.Kargolar = cargos;

            ViewBag.TotalPrice = basketList.Select(x => x.ToplamSatır).Sum();

            return View(getBill);
        }


        [HttpGet]
        public async Task<IActionResult> GeneralDocuments(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.Dokuman = await _unitOfWorkDokuman.Repository.Where(x => x.DokumanTipi == 4 && x.Durum == true).OrderByDescending(x => x.EklenmeTarih).ToPagedListAsync(pageNumber, 24);
            return View();
        }

        [HttpGet] 
        public async Task<IActionResult> AllCampaignProduct(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.CampaignProduct = await _unitOfWorkUrun.Repository.Where(x => x.Durum == true && x.IndirimOrani > 0 && x.UstUrunID == 0).Include("kategori").Include("marka").ToPagedListAsync(pageNumber, 30);
            return View();
        }
    }
}
