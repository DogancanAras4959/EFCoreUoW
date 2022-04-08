using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UoW.ADMIN.Helpers;
using UoW.ADMIN.Models;
using UoW.ADMIN.Models.BayiVM;
using UoW.CORE.Enums;
using UoW.CORE.Interface;
using UoW.DATA.Toastr;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{

    public class HomeController : Controller
    {

        #region Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategori;
        private readonly IUnitOfWork<MusteriGrupOzelUrun> _unitOfWorkMusteriOzelUrun;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarkalar;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLog;
        private readonly IUnitOfWork<Bulten> _unitOfWOrkBulten;
        private readonly IUnitOfWork<Basvuru> _unitOfWorkBasvuru;
        private readonly IUnitOfWork<BasvuruDurum> _unitOfWorkBasvuruDurum;
        private readonly IUnitOfWork<BasvuruAyar> _unitOfWorkBasvuruAyar;
        private readonly IUnitOfWork<Kullanici> _unitOfWorkKullanici;
        private readonly IUnitOfWork<BasvuruDosyalar> _unitOfWorkBasvuruDosyalar;
        private readonly IUnitOfWork<CuzdanIslemler> _unitOfWorkCuzdanIslemler;
        private readonly IUnitOfWork<Cuzdan> _unitOfWorkCuzdan;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWork<OzelTeklif> _unitOfWorkOzelTeklif;
        private readonly IUnitOfWork<Siparis> _unitOfWorkSiparis;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<Dokuman> _unitOfWorkDokuman;
        private readonly int? page;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<MusteriGrupOzelUrun> unitOfWorkOzelUrun,
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWorkLog<Loglar> unitOfWorkLoglar,
            IUnitOfWork<Bulten> unitOfWorkBulten,
            IUnitOfWork<Basvuru> unitOfWorkBasvuru,
            IUnitOfWork<BasvuruDurum> unitOfWorkBasvuruDurum,
            IUnitOfWork<BasvuruAyar> unitOfWorkBasvuruAyar,
            IUnitOfWork<Kullanici> unitOfWorkKullanici,
            IUnitOfWork<BasvuruDosyalar> unitOfWorkBasvuruDosyalar,
            IUnitOfWork<CuzdanIslemler> unitOfWorkCuzdanIslemler,
            IUnitOfWork<Cuzdan> unitOfWorkCuzdan,
            IUnitOfWork<BayiTicari> unitOfWorkBayiTicari,
            IUnitOfWork<OzelTeklif> unitOfWorkOzelTeklif,
            IUnitOfWork<Siparis> unitOfWorkSiparis,
            IUnitOfWork<Sepet> unitOfWorkSepet,
            IUnitOfWork<Haber> unitOfWorkHaber,
            IUnitOfWork<Dokuman> unitOfWorkDokuman)
        {
            _logger = logger;
            this._unitOfWorkLog = unitOfWorkLoglar;
            this._unitOfWorkMarkalar = unitOfWorkMarka;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkBayi = unitOfWorkBayi;
            this._unitOfWorkKategori = unitOfWorkKategori;
            this._unitOfWorkMusteriOzelUrun = unitOfWorkOzelUrun;
            this._unitOfWOrkBulten = unitOfWorkBulten;
            this._unitOfWorkBasvuru = unitOfWorkBasvuru;
            this._unitOfWorkBasvuruDurum = unitOfWorkBasvuruDurum;
            this._unitOfWorkBasvuruAyar = unitOfWorkBasvuruAyar;
            this._unitOfWorkKullanici = unitOfWorkKullanici;
            this._unitOfWorkBasvuruDosyalar = unitOfWorkBasvuruDosyalar;
            this._unitOfWorkCuzdanIslemler = unitOfWorkCuzdanIslemler;
            this._unitOfWorkCuzdan = unitOfWorkCuzdan;
            this._unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            this._unitOfWorkOzelTeklif = unitOfWorkOzelTeklif;
            this._unitOfWorkSiparis = unitOfWorkSiparis;
            this._unitOfWorkSepet = unitOfWorkSepet;
            this._unitOfWorkHaber = unitOfWorkHaber;
            this._unitOfWorkDokuman = unitOfWorkDokuman;
        }

        #endregion

        [CheckLogin]
        public async Task<IActionResult> Index(int? page)
        {
            TempData["urunToplam"] = Convert.ToInt32(_unitOfWorkUrun.Repository.Where(x => x.ID > 0).Count());
            TempData["bayiToplam"] = Convert.ToInt32(_unitOfWorkBayi.Repository.Where(x => x.ID > 0).Count());
            TempData["kategoriToplam"] = Convert.ToInt32(_unitOfWorkKategori.Repository.Where(x => x.ID > 0).Count());
            TempData["ozelUrunToplam"] = Convert.ToInt32(_unitOfWorkMusteriOzelUrun.Repository.Where(x => x.ID > 0).Count());
            TempData["markaToplam"] = Convert.ToInt32(_unitOfWorkMarkalar.Repository.Where(x => x.ID > 0).Count());
            TempData["logToplam"] = Convert.ToInt32(_unitOfWorkLog.RepositoryLog.Where(x => x.ID > 0).Count());
            TempData["siparisToplam"] = Convert.ToInt32(_unitOfWorkSiparis.Repository.Where(x => x.ID > 0).Count());
            TempData["haberToplam"] = Convert.ToInt32(_unitOfWorkHaber.Repository.Where(x => x.ID > 0).Count());
            TempData["dokumanToplam"] = Convert.ToInt32(_unitOfWorkDokuman.Repository.Where(x => x.ID > 0).Count());

            int pageNumber = page ?? 1;

            ViewBag.Basvurular = await _unitOfWorkBasvuru.Repository.Where(x => x.basvuruDurum.ID != 2 && x.basvuruDurum.ID != 3).Include("basvuruDurum").ToPagedListAsync(page, 20);

            ViewBag.CuzdanIslemler = await _unitOfWorkCuzdanIslemler.Repository.Where(x => x.OdemeSekliID == 2 && x.IslemDurum != CORE.Enums.IslemDurum.BorcOdendi).Include("cuzdanHesabi").Include("odemeSekliCuzdan").ToPagedListAsync(page, 20);

            ViewBag.OzelTeklifler = await _unitOfWorkOzelTeklif.Repository.Where(x => x.Durum == false && x.TeklifDurum == OzelTeklifDurum.TeklifYapildi).Include("bayiOzelTeklif").Include("siparisOzelTeklif").ToPagedListAsync(page, 20);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> BulletinList()
        {
            List<Bulten> bulletinList = (List<Bulten>)await _unitOfWOrkBulten.Repository.Where(x => x.ID > 0).Include("yetkili")
               .ToListAsync();
            return View(bulletinList);
        }

        public async Task<IActionResult> BulletinSellerRemove(int Id)
        {
            try
            {
                Bulten bulletin = await _unitOfWOrkBulten.Repository.GetById(Id);
                if (bulletin.Durum == true)
                {
                    bulletin.Durum = false;
                    _unitOfWOrkBulten.Repository.Update(bulletin);
                    _unitOfWOrkBulten.Repository.Save();

                    await BulletinList();
                    return View("BulletinList");
                }
                else
                {
                    bulletin.Durum = true;
                    _unitOfWOrkBulten.Repository.Update(bulletin);
                    _unitOfWOrkBulten.Repository.Save();

                    await BulletinList();
                    return View("BulletinList");
                }
            }
            catch (Exception)
            {
                await BulletinList();
                return View("BulletinList");
            }
        }

        #region Bayi Başvuru İşlemleri

        public IActionResult ApplySellerList()
        {
            return PartialView("ApplySellerList");
        }

        [HttpGet]
        public async Task<IActionResult> ApproveToApplySellerList(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.Basvurular = await _unitOfWorkBasvuru.Repository.Where(x => x.basvuruDurum.ID == 2 || x.basvuruDurum.ID == 5).Include("basvuruDurum").OrderByDescending(x=> x.BasvuruTarih).ToPagedListAsync(page, 20);
            IPagedList<Basvuru> toApplies = ViewBag.Basvurular as IPagedList<Basvuru>;
            return View(toApplies);
        }

        [HttpGet]
        public async Task<IActionResult> ToApplyDetail(int ID)
        {
            Basvuru ToApplyDetail = await _unitOfWorkBasvuru.Repository.Where(x => x.ID == ID).Include("basvuruDurum").SingleOrDefaultAsync();

            List<BasvuruDosyalar> filesToApply = await _unitOfWorkBasvuruDosyalar.Repository.Where(x => x.BasvuruID == ToApplyDetail.ID).ToListAsync();

            ViewBag.Dosyalar = filesToApply;
            return View(ToApplyDetail);
        }

        public async Task<IActionResult> ApproveToApplySeller(int ID)
        {

            Basvuru toApply = await _unitOfWorkBasvuru.Repository.GetById(ID);
            BasvuruDurum toApplyStatus = await _unitOfWorkBasvuruDurum.Repository.GetById(2);
            if (toApply != null)
            {
                toApply.BasvuruDurumID = toApplyStatus.ID;
                toApply.basvuruDurum = toApplyStatus;
                _unitOfWorkBasvuru.Repository.Update(toApply);
                _unitOfWorkBasvuru.Repository.Save();

                await ApproveToApplySellerList(page);
                return View("ApproveToApplySellerList");
            }
            else
            {
                await Index(page);
                return View("Index");
            }
        }

        public async Task<IActionResult> MakeDraftToApplySeller(int ID)
        {

            Basvuru toApply = await _unitOfWorkBasvuru.Repository.GetById(ID);
            BasvuruDurum toApplyStatus = await _unitOfWorkBasvuruDurum.Repository.GetById(4);
            if (toApply != null)
            {
                toApply.BasvuruDurumID = toApplyStatus.ID;
                toApply.basvuruDurum = toApplyStatus;
                _unitOfWorkBasvuru.Repository.Update(toApply);
                _unitOfWorkBasvuru.Repository.Save();

                await Index(page);
                return View("Index");
            }
            else
            {
                await Index(page);
                return View("Index");
            }
        }

        public async Task<IActionResult> RejectedToApplySeller(int ID)
        {

            Basvuru toApply = await _unitOfWorkBasvuru.Repository.GetById(ID);
            BasvuruDurum toApplyStatus = await _unitOfWorkBasvuruDurum.Repository.GetById(3);
            if (toApply != null)
            {
                toApply.BasvuruDurumID = toApplyStatus.ID;
                toApply.basvuruDurum = toApplyStatus;
                _unitOfWorkBasvuru.Repository.Update(toApply);
                _unitOfWorkBasvuru.Repository.Save();

                await Index(page);
                return View("Index");
            }
            else
            {
                await Index(page);
                return View("Index");
            }
        }

        public async Task<IActionResult> AllApplySeller(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.TumBasvurular = await _unitOfWorkBasvuru.Repository.Where(x => x.ID > 0).Include("basvuruDurum").ToPagedListAsync(pageNumber, 20);
            IPagedList<Basvuru> ApplySellerListAll = ViewBag.TumBasvurular as IPagedList<Basvuru>;
            return View(ApplySellerListAll);
        }

        public async Task<IActionResult> SettingsToApplySeller()
        {
            BasvuruAyar setting = await _unitOfWorkBasvuruAyar.Repository.Where(x => x.SecilenAyar == true || x.ID == 1).Include("kullaniciBasvuruAyar").SingleOrDefaultAsync();
            if (setting == null)
            {
                await CreateApplySetting();
                return View("CreateApplySetting");
            }
            else
            {
                List<BasvuruDurum> statusListApply = await _unitOfWorkBasvuruDurum.Repository.Where(x => x.ID > 0).ToListAsync();
                ViewBag.Durumlar = statusListApply;
                return View(setting);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateApplySetting()
        {
            List<Kullanici> users = (List<Kullanici>)await _unitOfWorkKullanici.Repository.Where(x => x.Durum == true).ToListAsync();
            ViewBag.KullaniciID = new SelectList(users, "ID", "KullaniciAdi");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplySetting(BasvuruAyar setting, int KullaniciID)
        {
            Kullanici getUser = await _unitOfWorkKullanici.Repository.GetById(KullaniciID);
            BasvuruAyar newSetting = new BasvuruAyar
            {
                BasvuruLimitGunluk = setting.BasvuruLimitGunluk,
                GuncellenmeTarih = DateTime.Now,
                YoneticiID = getUser.ID,
                kullaniciBasvuruAyar = getUser,
                BasvuruAl = true,
                SecilenAyar = true
            };

            await _unitOfWorkBasvuruAyar.Repository.Add(newSetting);
            _unitOfWorkBasvuruAyar.Repository.Save();

            await SettingsToApplySeller();
            return View("SettingsToApplySeller");
        }

        [HttpPost]
        public async Task<IActionResult> EditSettingToApplySeller(int dataStatus)
        {
            BasvuruAyar ayar = await _unitOfWorkBasvuruAyar.Repository.Where(x => x.SecilenAyar == true || x.ID == 1).Include("kullaniciBasvuruAyar").SingleOrDefaultAsync();

            if (dataStatus == 1)
            {
                ayar.BasvuruAl = true;
                _unitOfWorkBasvuruAyar.Repository.Update(ayar);
                _unitOfWorkBasvuruAyar.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Başvuru alma işlemi gerçekleştirilebilir','Başvuru Ayarı')";
            }
            else if (dataStatus == 0)
            {
                ayar.BasvuruAl = false;
                _unitOfWorkBasvuruAyar.Repository.Update(ayar);
                _unitOfWorkBasvuruAyar.Repository.Save();

                TempData["Mesaj"] = new ToastrMessage
                {
                    Title = "Başvuru Ayarı",
                    Message = "Başvuru alma işlemi gerçekleştirilemez",
                    type = ToastrType.warning
                };
                TempData["mesaj"] = "toastr['warning']('Başvuru alma işlemi gerçekleştirilemez','Başvuru Ayarı')";
            }

            await SettingsToApplySeller();
            return View("SettingsToApplySeller");
        }

        public IActionResult CreateApplyFromSeller(int id)
        {
            try
            {
                Basvuru toApply = _unitOfWorkBasvuru.Repository.FirstOrDefault(x => x.ID == id);
                SellerInsertVM sellerInsertVM = new SellerInsertVM
                {
                    BayiAdi = toApply.BayiAdi
                };
                toApply.BasvuruDurumID = 5;
                _unitOfWorkBasvuru.Repository.Update(toApply);
                _unitOfWorkBasvuru.Repository.Save();

                return RedirectToAction("InsertSeller", "Bayi", sellerInsertVM);

            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        #region Borç Bildirim Listesi

        public IActionResult DeptorsList()
        {
            return PartialView("DeptorsList");
        }

        public async Task<IActionResult> RestrictAccount(int ID)
        {
            try
            {
                CuzdanIslemler walletSetting = await _unitOfWorkCuzdanIslemler.Repository.Where(x => x.ID == ID).Include("cuzdanHesabi").FirstOrDefaultAsync();
                Cuzdan wallet = await _unitOfWorkCuzdan.Repository.GetById(walletSetting.CuzdanID);
                Bayi seller = await _unitOfWorkBayi.Repository.GetById(wallet.BayiID);
                BayiTicari commercialSeller = await _unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == seller.ID).SingleOrDefaultAsync();
                if (seller.HesapKisitla == false)
                {
                    seller.AktifMi = true;
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Bu bayinin hesabında zaten sipariş işlemi kısıtlandı','Sipariş Kısıtı')";
                    await Index(page);
                    return View("Index");
                }
                _unitOfWorkBayi.Repository.Update(seller);
                _unitOfWorkBayi.Repository.Save();

                walletSetting.IslemDurum = CORE.Enums.IslemDurum.BorcBildirildi;
                string debt = "Ödemeniz bildirildi: Kredi borcunuz " + walletSetting.IslemTutari + "₺ kadardır...";

                _unitOfWorkCuzdanIslemler.Repository.Update(walletSetting);
                _unitOfWorkCuzdanIslemler.Repository.Save();

                SendMailHelper.SendMailToPayingNotification(debt, "Kıraç Bayim - Kredi Ödeme Bildirimi", commercialSeller.MuhasebeMutabakatEmail, "Ödeme Bildirimi", seller.BayiAdi);

                TempData["mesaj"] = "toastr['success']('Bu bayinin hesabında sipariş işlemi kısıtlandı','Sipariş Kısıtı')";
                await Index(page);
                return View("Index");

            }
            catch (Exception)
            {
                await Index(page);
                return View("Index");
            }
        }

        public async Task<IActionResult> RemoveRestrictFromAccount(int ID)
        {
            try
            {
                CuzdanIslemler walletSetting = await _unitOfWorkCuzdanIslemler.Repository.Where(x => x.ID == ID).Include("cuzdanHesabi").FirstOrDefaultAsync();
                Cuzdan wallet = await _unitOfWorkCuzdan.Repository.GetById(walletSetting.CuzdanID);
                Bayi seller = await _unitOfWorkBayi.Repository.GetById(wallet.BayiID);
                BayiTicari commercialSeller = await _unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == seller.ID).SingleOrDefaultAsync();
                if (seller.HesapKisitla == true)
                {
                    seller.AktifMi = false;
                }

                _unitOfWorkBayi.Repository.Update(seller);
                _unitOfWorkBayi.Repository.Save();

                walletSetting.IslemDurum = CORE.Enums.IslemDurum.Borclu;
                string debt = "Hesap bildirimi: Kredi borcunuz " + walletSetting.IslemTutari + "₺ kadardır... Hesabınızda sipariş işlem kısıtı kaldırılmıştır";

                _unitOfWorkCuzdanIslemler.Repository.Update(walletSetting);
                _unitOfWorkCuzdanIslemler.Repository.Save();

                SendMailHelper.SendMailToPayingNotification(debt, "Kıraç Bayim - Sipariş İşlemini Kısıtlama Bildirimi", commercialSeller.MuhasebeMutabakatEmail, "Ödeme Bildirimi", seller.BayiAdi);

                TempData["mesaj"] = "toastr['success']('Bu bayinin hesabında sipariş işlemini kısıtlama kaldırıldı','Sipariş Kısıtı')";
                await Index(page);
                return View("Index");

            }
            catch (Exception)
            {
                await Index(page);
                return View("Index");
            }
        }

        public FileResult DownloadApplyDocument(int ID)
        {
            BasvuruDosyalar getDocument = _unitOfWorkBasvuruDosyalar.Repository.GetById(ID).Result;
            MemoryStream stream = SaveImageProcess.GetDownloadableFile(getDocument.DosyaAdi, "Bayi");
            stream.Position = 0;
            var extensions = new FileInfo(getDocument.DosyaAdi).Extension;
            var fileTypes = MimeTypesMap.GetMimeType(getDocument.DosyaAdi);
            return File(stream, fileTypes, $"{getDocument.DosyaAdi}{extensions}");
        }

        #endregion

        #region Özel Teklif İşlemleri

        public async Task<IActionResult> AllSpecialOffers(int? page) 
        {
            int pageNumber = page ?? 1;
            ViewBag.OzelTeklifler = await _unitOfWorkOzelTeklif.Repository.Where(x => x.ID > 0).Include("bayiOzelTeklif").Include("siparisOzelTeklif").ToPagedListAsync(page, 20);
            return View();
        }

        public IActionResult SpecialOffer()
        {
            return PartialView("SpecialOffer");
        }

        public IActionResult EditOzelTeklif(int ID)
        {
            OzelTeklif specialOffer = _unitOfWorkOzelTeklif.Repository.Where(x => x.ID == ID).SingleOrDefault();

            specialOffer.Durum = false;
            specialOffer.TeklifTarih = DateTime.Now;
            _unitOfWorkOzelTeklif.Repository.Update(specialOffer);
            _unitOfWorkOzelTeklif.Repository.Save();

            return Json(true);
        }
            
        public async Task<IActionResult> MakeOfferToSeller(int id)
        {
            OzelTeklif specialOffer = await _unitOfWorkOzelTeklif.Repository.Where(x => x.ID == id).Include("bayiOzelTeklif").Include("siparisOzelTeklif").SingleOrDefaultAsync();

            Siparis order = await _unitOfWorkSiparis.Repository.Where(x => x.ID == specialOffer.siparisOzelTeklif.ID).Include("bayiSiparis").SingleOrDefaultAsync();
            List<Sepet> orderDetailList = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == order.ID)
            .Include("UrunSepet").ToListAsync();

            ViewBag.SiparisDetaylari = orderDetailList;
            ViewBag.TotalPrice = orderDetailList.Select(x => x.ToplamSatır).Sum();
            return View(specialOffer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOfferToSeller(string products)
        {

            try
            {
                decimal discount = 0;
                decimal total = 0;
                decimal counter = 0;
                int bayiID = 0;
                Bayi getSeller = null;
                BayiTicari getCommercialSeller = null;

                List<Sepet> basketData = JsonConvert.DeserializeObject<List<Sepet>>(products);

                foreach (var item in basketData)
                {
                    int CurrentId = Convert.ToInt32(item.UrunID);
                    Sepet getBasket = await _unitOfWorkSepet.Repository.Where(x => x.ID == item.ID).FirstOrDefaultAsync();
                    Siparis getOrder = await _unitOfWorkSiparis.Repository.Where(x => x.ID == getBasket.SiparisID).FirstOrDefaultAsync();

                    OzelTeklif specialOffer = await _unitOfWorkOzelTeklif.Repository.Where(x => x.SiparisID == getOrder.ID).FirstOrDefaultAsync();

                    if (bayiID == 0)
                    {
                        bayiID = specialOffer.BayiID;
                        getSeller = await _unitOfWorkBayi.Repository.GetById(bayiID);
                        getCommercialSeller = await _unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == bayiID).FirstAsync();
                    }

                    discount += (getBasket.BirimFiyat * getBasket.Adet) - (item.BirimFiyat * getBasket.Adet);

                    if (getBasket.BirimFiyat > item.BirimFiyat)
                    {
                        getBasket.BirimFiyat = item.BirimFiyat;
                    }
                    else
                    {
                        throw new AdminException("Birim fiyat girilen fiyattan düşük olamaz!");
                    }

                    getBasket.ToplamSatır = 0;
                    getBasket.ToplamSatır += (getBasket.BirimFiyat * getBasket.Adet);
                    _unitOfWorkSepet.Repository.Update(getBasket);
                    _unitOfWorkSepet.Repository.Save();
                    total += getBasket.ToplamSatır;
                    getOrder.ToplamFiyat = 0;
                    getOrder.ToplamFiyat += total;
                    getOrder.Indirim += discount;
                    _unitOfWorkSiparis.Repository.Update(getOrder);
                    _unitOfWorkSiparis.Repository.Save();

                    counter++;

                    if (counter == basketData.Count)
                    {
                        if (specialOffer.Durum == false)
                        {
                            specialOffer.Durum = true;
                            specialOffer.TeklifDurum = OzelTeklifDurum.TeklifHazirlandi;
                            _unitOfWorkOzelTeklif.Repository.Update(specialOffer);
                            _unitOfWorkOzelTeklif.Repository.Save();

                            SendMailHelper.SendMailToSpecialOfferToAdmin(specialOffer, "Siparişiniz için oluşturduğunuz özel teklif isteğine yanıt oldu. Sisteme giriş yaparak teklifi görebilirsiniz. İyi alışverişler - Kıraç Bayim ", getCommercialSeller.MuhasebeMutabakatEmail, "Kıraç Bayim - Özel Teklif Bildirimi", "Özel Teklif Oluşturma", getSeller.BayiAdi);
                        }
                    }
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                HttpContext.Response.WriteAsync(ex.Message).Wait();
                return Json(false);
            }
        }

        public async Task<IActionResult> OfferDetail(int id)
        {
            OzelTeklif specialOffer = await _unitOfWorkOzelTeklif.Repository.Where(x => x.ID == id).Include("bayiOzelTeklif").Include("siparisOzelTeklif").SingleOrDefaultAsync();

            Siparis order = await _unitOfWorkSiparis.Repository.Where(x => x.ID == specialOffer.siparisOzelTeklif.ID).Include("bayiSiparis").SingleOrDefaultAsync();
            List<Sepet> orderDetailList = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == order.ID)
            .Include("UrunSepet").ToListAsync();

            ViewBag.SiparisDetaylari = orderDetailList;
            ViewBag.TotalPrice = orderDetailList.Select(x => x.ToplamSatır).Sum();
            return View(specialOffer);
        }

        public async Task<IActionResult> OfferSendToSeller(int Id)
        {
            try
            {
                OzelTeklif getSpecialOffer = await _unitOfWorkOzelTeklif.Repository.Where(x => x.ID == Id).FirstOrDefaultAsync();
                getSpecialOffer.TeklifDurum = OzelTeklifDurum.TeklifGonderildi;
                _unitOfWorkOzelTeklif.Repository.Update(getSpecialOffer);
                _unitOfWorkOzelTeklif.Repository.Save();
                TempData["mesaj"] = "toastr['success']('Teklif başarıyla bayiye gönderildi','Teklif Gönder')";
                return RedirectToAction("AllSpecialOffers", "Home");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['success']('Sistemden kaynaklı bir hata meydana geldi','Teklif Gönder')";
                return RedirectToAction("AllSpecialOffers", "Home");
            }
        }

        #endregion
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