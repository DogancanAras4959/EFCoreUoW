using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DATA.HashPassword;
using UoW.DATA.Toastr;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.WEB.Models;
using X.PagedList;

namespace UoW.WEB.Controllers
{
    public class BayiController : Controller
    {

        #region Fields

        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<Yetkililer> _unitOfWorkYetkililer;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWork<Bulten> _unitOfWorkBulten;
        private readonly IUnitOfWork<Cuzdan> _unitOfWorkCuzdan;
        private readonly IUnitOfWork<MusteriGrubu> _unitOfWorkMusteriGrubu;
        private readonly IUnitOfWork<CuzdanIslemler> _unitOfWorkCuzdanIslemler;
        private readonly IUnitOfWork<Siparis> _unitOfWorkSiparisler;
        private readonly IUnitOfWork<UrunStok> _unitOfWorkUrunStok;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork<BasvuruDurum> _unitOfWorkBasvuruDurum;
        private readonly IUnitOfWork<Basvuru> _unitOfWorkBasvuru;
        private readonly IUnitOfWork<BasvuruDosyalar> _unitOfWorkBasvuruDosyalar;
        private readonly IUnitOfWork<BayiAdresler> _unitOfWorkBayiAdresler;
        private readonly IUnitOfWork<Sehir> _unitOfWorkSehirler;
        private readonly IUnitOfWork<Ilce> _unitOfWorkIlceler;
        private readonly IUnitOfWork<BasvuruAyar> _unitOfWorkBasvuruAyar;
        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;
        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategoriler;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarka;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly int? page;
        private readonly int BayiID;
        public BayiController(IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<Yetkililer> unitOfWorkYetkililer,
            IUnitOfWork<BayiTicari> unitOfWorkBayiTicari,
            IUnitOfWork<Bulten> unitOfWorkBulten,
            IUnitOfWork<Cuzdan> unitOfWorkCuzdan,
            IUnitOfWork<MusteriGrubu> unitOfWorkMusteriGrubu,
            IUnitOfWork<CuzdanIslemler> unitOfWorkCuzdanIslemler,
            IUnitOfWork<Siparis> unitOfWorkSiparis,
            IUnitOfWork<UrunStok> unitOfWorkUrunStok,
            IWebHostEnvironment hostEnvironment,
            IUnitOfWork<BasvuruDurum> unitOfWorkBasvuruDurum,
            IUnitOfWork<Basvuru> unitOfWorkBasvuru,
            IUnitOfWork<BasvuruDosyalar> unitOfWorkBasvuruDosyalar,
            IUnitOfWork<BayiAdresler> unitOfWorkBayiAdresler,
            IUnitOfWork<Sehir> unitOfWorkSehirler,
            IUnitOfWork<Ilce> unitOfWorkIlceler,
            IUnitOfWork<BasvuruAyar> unitOfWorkBasvuruAyar,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Begeni> unitOfWorkBegeni,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<Haber> unitOfWorkHaber,
            IUnitOfWork<SiteInfo> unitOfWorkSiteInfo,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Sepet> unitOfWorkSepet)
        {
            this._unitOfWorkBayi = unitOfWorkBayi;
            this._unitOfWorkYetkililer = unitOfWorkYetkililer;
            this._unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            this._unitOfWorkBulten = unitOfWorkBulten;
            this._unitOfWorkCuzdan = unitOfWorkCuzdan;
            this._unitOfWorkMusteriGrubu = unitOfWorkMusteriGrubu;
            this._unitOfWorkCuzdanIslemler = unitOfWorkCuzdanIslemler;
            this._unitOfWorkSiparisler = unitOfWorkSiparis;
            this._unitOfWorkUrunStok = unitOfWorkUrunStok;
            this._hostEnvironment = hostEnvironment;
            this._unitOfWorkBasvuruDurum = unitOfWorkBasvuruDurum;
            this._unitOfWorkBasvuru = unitOfWorkBasvuru;
            this._unitOfWorkBasvuruDosyalar = unitOfWorkBasvuruDosyalar;
            this._unitOfWorkBayiAdresler = unitOfWorkBayiAdresler;
            this._unitOfWorkSehirler = unitOfWorkSehirler;
            this._unitOfWorkIlceler = unitOfWorkIlceler;
            this._unitOfWorkBasvuruAyar = unitOfWorkBasvuruAyar;
            this._unitOfWorkMarka = unitOfWorkMarka;
            this._unitOfWorkKategoriler = unitOfWorkKategori;
            this._unitOfWorkBegeni = unitOfWorkBegeni;
            this._unitOfWorkStokBildirim = unitOfWorkStokBildirim;
            this._unitOfWorkHaber = unitOfWorkHaber;
            this._unitOfWorkSiteInfo = unitOfWorkSiteInfo;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkSepet = unitOfWorkSepet;
        }

        #endregion

        [HttpGet]
        public IActionResult SellerLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellerLoginWeb(LoginViewModel model)
        {
            try
            {
                model.password = new Crypto().TryEncrypt(model.password);

                Bayi girisVarmi = (Bayi)await _unitOfWorkBayi.Repository
                    .Where(x => x.KullaniciAdi == model.username && x.Sifre == model.password).SingleOrDefaultAsync();

                MusteriGrubu mg = _unitOfWorkMusteriGrubu.Repository.Where(x => x.ID == girisVarmi.BayiGrubuID).FirstOrDefault();

                Cuzdan cuzdan = await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == girisVarmi.ID).SingleOrDefaultAsync();
                girisVarmi.musteriGrubu = mg;

                if (mg.Durum == false)
                {
                    TempData["mesaj"] = "toastr['warning']('Müşteri grubunuz aktif değildir. Bu yüzden giriş işlemini gerçekleştiremiyoruz','Giriş İşlemi')";
                    SellerLogin();
                    return View("SellerLogin");
                }

                if (girisVarmi.AktifMi == false)
                {
                    TempData["mesaj"] = "toastr['warning']('Bayiniz aktif değildir. Bu yüzden giriş işlemini gerçekleştiremiyoruz.','Giriş İşlemi')";
                    SellerLogin();
                    return RedirectToAction("SellerLogin","Bayi");
                }

                if (girisVarmi != null)
                {
                    string durum = Convert.ToString(girisVarmi.AktifMi);
                    HttpContext.Session.SetString("userB", girisVarmi.KullaniciAdi);
                    HttpContext.Session.SetInt32("IdB", girisVarmi.ID);
                    HttpContext.Session.SetString("ppB", girisVarmi.Logo);
                    HttpContext.Session.SetString("mG", mg.GrubAdi);
                    HttpContext.Session.SetString("durum", durum);

                    if (cuzdan != null)
                    {
                        List<CuzdanIslemler> c = await _unitOfWorkCuzdanIslemler.Repository.Where(x => x.cuzdanHesabi.ID == cuzdan.ID).Include("cuzdanHesabi").ToListAsync();
                        if (c.Count > 0)
                        {
                            cuzdan.cuzdanIslemlers = c;

                            foreach (var item in c)
                            {
                                if (item.IslemDurum == CORE.Enums.IslemDurum.BorcBildirildi)
                                {
                                    return RedirectToAction("PayDebutNotification", "Home", girisVarmi);
                                }
                            }
                        }
                        else
                        {
                            return RedirectToAction("HomePageList", "Home");
                        }
                    }
                    return RedirectToAction("HomePageList", "Home");
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Giriş işlemi hatalı. Kullanıcı adı veya şifrenizi doğru giriniz...','Giriş İşlemi')";
                    SellerLogin();
                    return View("SellerLogin");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = $"toastr['info']('Sistemden kaynaklı bir hata meydana geldi. Lütfen tekrar deneyin!','Giriş İşlemi')";
                SellerLogin();
                return View("SellerLogin");
            }

        }

        [HttpGet]
        [CheckOtherLogin]
        public IActionResult SellerLogOutWeb()
        {

            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("admin");
            HttpContext.Session.Clear();
            return RedirectToAction("SellerLogin", "Bayi");
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> SellerDetail(int ID, int? page)
        {
            List<Cuzdan> cuzdanlar = (List<Cuzdan>)await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == ID).ToListAsync();

            if (cuzdanlar.Count == 0)
            {
                return RedirectToAction("CreateWallet", "Bayi");
            }
            else
            {
                List<BayiTicari> bayiTicari = (List<BayiTicari>)await _unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == ID).ToListAsync();

                if (bayiTicari.Count == 0)
                {
                    return RedirectToAction("InsertCommercialSellerDataWeb", "Islem");
                }
                else
                {

                    Yetkililer gelenYetkili = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == ID).Include("bayiYetkilisi").SingleOrDefaultAsync();

                    if (gelenYetkili == null)
                    {
                        return RedirectToAction("InsertSellerCompetentWeb", "Islem");
                    }

                    List<Bulten> bulten = await _unitOfWorkBulten.Repository.Where(x => x.YetkiliID == gelenYetkili.ID).ToListAsync();
                    List<UrunStok> stoklar = await _unitOfWorkUrunStok.Repository.Where(x => x.BayiID == ID).ToListAsync();
                    List<BayiTicari> bt = (List<BayiTicari>)await _unitOfWorkBayiTicari.Repository.All();
                    List<Yetkililer> yetkililers = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == ID).ToListAsync();

                    List<BayiAdresler> badres = await _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == BayiID).ToListAsync();

                    ViewBag.ToplamStok = stoklar.Select(x => x.Adet).Sum();
                    ViewBag.Yetkililer = yetkililers.Select(x => x.ID).Sum();

                    Cuzdan MyWallet = await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == ID).SingleOrDefaultAsync();
                    if (MyWallet != null && MyWallet.ID > 0)
                    {
                        ViewBag.Bakiye = Convert.ToDecimal(MyWallet.Bakiye);
                        ViewBag.ToplamTutarIslem = Convert.ToInt32(_unitOfWorkCuzdanIslemler.Repository.Where(x => x.CuzdanID == MyWallet.ID).Sum(x => x.IslemTutari));
                    }

                    var pageNumber = page ?? 1;
                    ViewBag.Siparisler = await _unitOfWorkSiparisler.Repository
                                                                  .Where(x => x.BayiID == ID)
                                                                  .Include("odemeSekliSiparis")
                                                                  .OrderByDescending(x => x.ID)
                                                                  .Take(10)
                                                                  .ToPagedListAsync(pageNumber, 10);

                    if (bt.Count > 0)
                    {
                        ViewBag.Durum = true;
                    }

                    else
                    {
                        ViewBag.Durum = false;
                    }

                    if (bulten.Count > 0)
                    {
                        ViewBag.BultenDurum = true;
                    }
                    else
                    {
                        ViewBag.BultenDurum = false;
                    }

                    if (badres.Count > 0)
                    {
                        ViewBag.Adresler = true;
                    }
                    else
                    {
                        ViewBag.Adresler = false;
                    }

                    return View(gelenYetkili.bayiYetkilisi);
                }
            }
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> MyWallet(int ID, int? page)
        {
            Cuzdan MyWallet = await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == ID).SingleOrDefaultAsync();
            if (MyWallet != null && MyWallet.ID > 0)
            {

                ViewBag.ToplamTutarIslem = Convert.ToInt32(_unitOfWorkCuzdanIslemler.Repository.Where(x => x.CuzdanID == MyWallet.ID).Sum(x => x.IslemTutari));

                var pageNumber = page ?? 1;
                ViewBag.CuzdanIslemler = await _unitOfWorkCuzdanIslemler.Repository
                                                              .Where(x => x.CuzdanID == MyWallet.ID)
                                                              .OrderByDescending(x => x.ID)
                                                              .ToPagedListAsync(pageNumber, 30);

                return View(MyWallet);
            }
            else
            {
                return RedirectToAction("CreateWallet", "Bayi", new { Id = ID });
            }
        }

        [HttpGet]
        public IActionResult ProcessList()
        {
            return PartialView("ProcessList");
        }

        [HttpGet]
        public IActionResult LiveOrderListWeb()
        {
            return PartialView("LiveOrderListWeb");
        }

        [HttpGet]
        [CheckOtherLogin]
        public IActionResult CreateWallet()
        {
            return View();
        }

        [HttpPost]
        [CheckOtherLogin]
        public async Task<IActionResult> CreateWallet(Cuzdan c)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).FirstOrDefaultAsync();
            try
            {
                Random rastgele = new Random();
                int no = rastgele.Next();

                if (b != null)
                {
                    Cuzdan yenic = new Cuzdan
                    {
                        
                        bayi = b,
                        BayiID = id,
                        OlusturulmaTarih = DateTime.Now,
                        Bakiye = 0,
                        CuzdanNo = Convert.ToString(no),
                        CuzdanTipi = c.CuzdanTipi
                    };

                    await _unitOfWorkCuzdan.Repository.Add(yenic);
                    _unitOfWorkCuzdan.Repository.Save();
                    return RedirectToAction("HomePageList", "Home");
                }

                else
                {
                    return RedirectToAction("HomePageList", "Home");

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("HomePageList", "Home");

            }
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> EditWallet(int Id)
        {
            Cuzdan c = await _unitOfWorkCuzdan.Repository.Where(x => x.ID == Id).FirstOrDefaultAsync();
            if (c != null)
            {
                return View(c);
            }
            else
            {
                return RedirectToAction("SellerDetail", "Bayi", c.BayiID);
            }
        }

        [HttpPost]
        [CheckOtherLogin]
        public async Task<IActionResult> EditWallet(int BayiID, Cuzdan c)
        {
            try
            {
                Cuzdan gelenCuzdan = await _unitOfWorkCuzdan.Repository.Where(x => x.ID == BayiID).FirstOrDefaultAsync();

                if (c != null)
                {
                    gelenCuzdan.bayi = gelenCuzdan.bayi;
                    gelenCuzdan.BayiID = BayiID;
                    gelenCuzdan.OlusturulmaTarih = DateTime.Now;
                    gelenCuzdan.Bakiye = c.Bakiye;
                    gelenCuzdan.CuzdanNo = c.CuzdanNo;
                    gelenCuzdan.CuzdanTipi = c.CuzdanTipi;

                    _unitOfWorkCuzdan.Repository.Update(gelenCuzdan);
                    _unitOfWorkCuzdan.Repository.Save();
                    return RedirectToAction("SellerDetail", "Bayi", BayiID);
                }

                else
                {
                    await EditWallet(c.ID);
                    return View("EditWallet");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("SellerDetail", "Bayi", BayiID);
            }
        }

        [CheckOtherLogin]
        public async Task<IActionResult> DropWallet(int Id)
        {
            Cuzdan c = await _unitOfWorkCuzdan.Repository.GetById(Id);
            if (c != null)
            {
                _unitOfWorkCuzdan.Repository.Delete(c);
                _unitOfWorkCuzdan.Repository.Save();
                return RedirectToAction("SellerDetail", "Bayi", c.BayiID);
            }
            else
            {
                return RedirectToAction("HomePageList", "Home");
            }
        }

        [HttpGet]
        public IActionResult MakeToApplySystem()
        {
            BasvuruAyar basvuruAyar = _unitOfWorkBasvuruAyar.Repository.FirstOrDefault();
            if (basvuruAyar.BasvuruAl == false)
            {
                ViewBag.Ayar = basvuruAyar.BasvuruAl;
                TempData["mesaj"] = "toastr['warning']('Başvuru alımlarımız aktif değildir!','Başvuru İşlemi')";
                return View();
            }
            ViewBag.Ayar = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeToApplySystem(Basvuru b, IFormFile image, List<IFormFile> files, bool SMSAlma, bool BilgilendirmeAlma, bool EpostaBilgiAlma, bool UyelikSozlesmesiOkuduMu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (UyelikSozlesmesiOkuduMu != false)
                    {
                        BasvuruDurum bd = await _unitOfWorkBasvuruDurum.Repository.Where(x => x.ID == 1).SingleOrDefaultAsync();
                        Random rastgele = new Random();
                        int no = rastgele.Next();

                        Basvuru yeniBasvuru = new Basvuru
                        {
                            BasvuruNo = no.ToString(),
                            BasvuruTarih = DateTime.Now,
                            TelNo = b.TelNo,
                            EmailAdresi = b.EmailAdresi,
                            BayiAdi = b.BayiAdi,
                            BayiUnvani = b.BayiUnvani,
                            YetkiliAdi = b.YetkiliAdi,
                            basvuruDurum = bd,
                            BasvuruDurumID = bd.ID,
                            VergiNo = b.VergiNo,
                            SMSAlma = SMSAlma,
                            EpostaBilgiAlma = EpostaBilgiAlma,
                            BilgilendirmeAlma = BilgilendirmeAlma,
                            UyelikSozlesmesiOkuduMu = UyelikSozlesmesiOkuduMu
                        };

                        if (image != null)
                        {
                            yeniBasvuru.basvuruFoto = SaveImageProcess.ImageInsert(image, "Bayi");
                            await _unitOfWorkBasvuru.Repository.Add(yeniBasvuru);
                            _unitOfWorkBasvuru.Repository.Save();
                        }

                        if (files.Count != 0)
                        {
                            long size = files.Sum(f => f.Length);

                            var filePaths = new List<string>();

                            foreach (var formFile in files)
                            {
                                if (formFile.Length > 0 || formFile.ContentType == ".pdf" || formFile.ContentType == ".jpg" || formFile.ContentType == ".png")
                                {
                                    BasvuruDosyalar basvuruDosyalar = new BasvuruDosyalar
                                    {
                                        DosyaAdi = SaveImageProcess.ImageInsert(formFile, "Bayi"),
                                        BasvuruID = yeniBasvuru.ID,
                                        basvuru = yeniBasvuru,
                                        DosyaTuru = formFile.ContentType.ToString(),
                                        DosyaBoyutu = Convert.ToString(0)
                                    };

                                    await _unitOfWorkBasvuruDosyalar.Repository.Add(basvuruDosyalar);
                                    _unitOfWorkBasvuruDosyalar.Repository.Save();
                                }
                            }
                        }
                        else
                        {
                            return RedirectToAction("ResultToApplyNotMatchFiles", "Bayi");
                        }
                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Lütfen sözleşmeyi okuyup onaylamadan işlemi tamamlamayın!','Bayi Başvuru')";
                        return RedirectToAction("MakeToApplySystem", "Bayi");
                    }
                    return RedirectToAction("ResultToApply", "Bayi");
                }
                catch (Exception)
                {
                    return RedirectToAction("ResultToApplyUnSuccessful", "Bayi");
                }
            }
            else
            {

                MakeToApplySystem();
                return View("MakeToApplySystem");
            }

        }

        [HttpGet]
        public IActionResult ResultToApply()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResultToApplyUnSuccessful()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResultToApplyNotMatchFiles()
        {
         
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SellerAdressList(int? page)
        {
        
            int pageNumber = page ?? 1;
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.Adresler = await _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == id).Include("sehirAdres").Include("ilceAdres").ToPagedListAsync(pageNumber, 20);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> InsertSellerAdress()
        {

            List<Sehir> sehirler = (List<Sehir>)await _unitOfWorkSehirler.Repository.Where(x => x.ID > 0).ToListAsync();
            ViewBag.SehirID = new SelectList(sehirler, "ID", "SehirAdi");

            return View();
        }

        [HttpPost]
        public bool InsertSellerAdress([FromBody] BayiAdresler sellerAdress)
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
                Bayi bayi = _unitOfWorkBayi.Repository.GetById(id).Result;

                sellerAdress.BayiID = id;
                sellerAdress.bayiAdres = bayi;

                _unitOfWorkBayiAdresler.Repository.Add(sellerAdress);
                _unitOfWorkBayiAdresler.Repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IActionResult> RemoveAdress(int ID)
        {
            try
            {
                BayiAdresler bayiAdres = await _unitOfWorkBayiAdresler.Repository.GetById(ID);
                _unitOfWorkBayiAdresler.Repository.Delete(bayiAdres);
                _unitOfWorkBayiAdresler.Repository.Save();
                TempData["mesaj"] = "toastr['success']('Bayi adresi başarıyla silindi','Adres Kaldır')";
                return RedirectToAction("SellerAdressList", "Bayi");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Bayi adresi silinirken sistemden kaynaklı bir hata oluştu','Adres Kaldır')";
                return RedirectToAction("SellerAdressList", "Bayi");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditAddress(int ID)
        {
      
            BayiAdresler adres = await _unitOfWorkBayiAdresler.Repository.GetById(ID);
            if (adres != null)
            {
                List<Sehir> sehirler = (List<Sehir>)await _unitOfWorkSehirler.Repository.Where(x => x.ID > 0).ToListAsync();
                ViewBag.SehirID = new SelectList(sehirler, "ID", "SehirAdi", adres.SehirID);

                return View(adres);
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Adres Düzenle')";
                return RedirectToAction("HomePageList", "Home");
            }
        }

        [HttpPost]
        public bool EditAdress([FromBody] BayiAdresler sellerAdress)
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
                Bayi seller = _unitOfWorkBayi.Repository.GetById(id).Result;
                sellerAdress.BayiID = id;
                sellerAdress.bayiAdres = seller;
                _unitOfWorkBayiAdresler.Repository.Update(sellerAdress);
                _unitOfWorkBayiAdresler.Repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
