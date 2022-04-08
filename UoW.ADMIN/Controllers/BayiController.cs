using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UoW.ADMIN.Helpers;
using UoW.ADMIN.Models.BayiVM;
using UoW.CORE.Interface;
using UoW.DATA.HashPassword;
using UoW.DATA.Toastr;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{
    public class BayiController : Controller
    {

        #region Feield

        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<MusteriGrubu> _unitOfWorkMusteriGrup;
        private readonly IUnitOfWork<Kullanici> _unitOfWorkKullanici;
        private readonly IUnitOfWork<Yetkililer> _unitOfWorkYetkililer;
        private readonly IUnitOfWork<OdemeSekli> _unitOfWorkOdemeSekli;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWork<Bulten> _unitOfWorkBulten;
        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitofWorkLogYonetici;
        private readonly IUnitOfWork<KargoFirmalar> _unitOfWorkKargoFirmalar;
        private readonly IUnitOfWork<MusteriGrupOzelUrun> _unitOfWorkMusteriGrupUrun;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Cuzdan> _unitOfWorkCuzdan;
        private readonly IUnitOfWork<CuzdanIslemler> _unitOfWorkCuzdanIslemler;
        private readonly IUnitOfWork<Siparis> _unitOfWorkSiparisler;
        private readonly IUnitOfWork<UrunStok> _unitOfWorkUrunStok;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork<UrunResimler> _unitOfWorkUrunResimler;
        private readonly IUnitOfWork<UrunFiyat> _unitOfWorkUrunFiyat;
        private readonly IUnitOfWork<EtiketGonderi> _unitOfWorkEtiketUrunler;

        private readonly int? page;
        public BayiController(IUnitOfWork<Bayi> unitOfWork, IUnitOfWork<MusteriGrubu> unitOfWorkMusteriGrub,
            IUnitOfWork<Kullanici> unitOfWorkKullanici, IWebHostEnvironment hostEnvironment,
            IUnitOfWork<Yetkililer> unitOfWorkYetkililer, IUnitOfWork<OdemeSekli> unitOfWorkOdemeSekli,
            IUnitOfWork<BayiTicari> unitOfWorkBayiTicari, IUnitOfWork<Bulten> unitOfWorkBulten,
            IUnitOfWorkLog<Durumlar> unitOfWorkDurumlar, IUnitOfWorkLog<Loglar> unitOfWorkLoglar,
            IUnitOfWorkLog<Islemler> unitOfWorkIslemler, IUnitOfWorkLog<Yonetici> unitOfWorkYonetici,
            IUnitOfWork<KargoFirmalar> unitOfWorkKargoFirmalar, IUnitOfWork<Urun> unitOfWorkUrun, IUnitOfWork<Cuzdan> unitOfWorkCuzdan, IUnitOfWork<CuzdanIslemler> unitOfWorkCuzdanIslemler, IUnitOfWork<Siparis> unitOfWorkSiparis, IUnitOfWork<UrunStok> unitOfWorkUrunStok, IUnitOfWork<MusteriGrupOzelUrun> unitOfWorkMusteriGrupOzelUrun,
            IUnitOfWork<UrunResimler> unitOfWorkUrunResimler, IUnitOfWork<UrunFiyat> unitOfWorkUrunFiyat, IUnitOfWork<EtiketGonderi> unitOfWorkEtiketUrunler)
        {
            this._unitOfWorkBayi = unitOfWork;
            this._unitOfWorkMusteriGrup = unitOfWorkMusteriGrub;
            this._unitOfWorkKullanici = unitOfWorkKullanici;
            this._hostEnvironment = hostEnvironment;
            this._unitOfWorkYetkililer = unitOfWorkYetkililer;
            this._unitOfWorkOdemeSekli = unitOfWorkOdemeSekli;
            this._unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            this._unitOfWorkBulten = unitOfWorkBulten;
            this._unitOfWorkLogDurumlar = unitOfWorkDurumlar;
            this._unitOfWorkLogIslemler = unitOfWorkIslemler;
            this._unitOfWorkLogLoglar = unitOfWorkLoglar;
            this._unitofWorkLogYonetici = unitOfWorkYonetici;
            this._unitOfWorkKargoFirmalar = unitOfWorkKargoFirmalar;
            this._unitOfWorkMusteriGrupUrun = unitOfWorkMusteriGrupOzelUrun;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkCuzdan = unitOfWorkCuzdan;
            this._unitOfWorkCuzdanIslemler = unitOfWorkCuzdanIslemler;
            this._unitOfWorkSiparisler = unitOfWorkSiparis;
            this._unitOfWorkUrunStok = unitOfWorkUrunStok;
            this._unitOfWorkUrunResimler = unitOfWorkUrunResimler;
            this._unitOfWorkUrunFiyat = unitOfWorkUrunFiyat;
            this._unitOfWorkEtiketUrunler = unitOfWorkEtiketUrunler;
        }

        #endregion

        #region Müşteri Grup İşlemleri

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> CustomerGroups()
        {
            List<MusteriGrubu> group = (List<MusteriGrubu>)await _unitOfWorkMusteriGrup.Repository.All();
            await CreateModeratorLog("Başarılı", "Listeleme", "CustomerGroups", "Bayi");
            return View(group);
        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> SellerByIdGroup(int id)
        {
            MusteriGrubu customerGroup = await _unitOfWorkMusteriGrup.Repository.GetById(id);
            List<Bayi> seller = (List<Bayi>)_unitOfWorkBayi.Repository.Where(x => x.BayiGrubuID == customerGroup.ID).Include("odeme").ToList();
            customerGroup.bayiMusteriGrublari = seller;
            await CreateModeratorLog("Başarılı", "Detay", "SellerByIdGroup", "Bayi");
            return View("SellerByIdGroup", customerGroup);
        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> InsertCustomerGroup()
        {
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "InsertCustomerGroup", "Bayi");
            return View();
        }

        [CheckLogin]
        [HttpPost]
        public async Task<IActionResult> InsertCustomerGroup(MusteriGrubu customerGroup, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                Kullanici getUser = await _unitOfWorkKullanici.Repository.Where(x => x.ID == id).SingleAsync();
                MusteriGrubu getCustomerGroup = await _unitOfWorkMusteriGrup.Repository.Where(x => x.GrubAdi == customerGroup.GrubAdi).FirstOrDefaultAsync();
                try
                {
                    if (getCustomerGroup != null)
                    {
                        TempData["mesaj"] = "toastr['warning']('Müşteri grubu sistemde bulunuyor','Müşteri Grubu Ekleme')";
                        await CreateModeratorLog("Başarısız", "Ekleme", "InsertCustomerGroup", "Bayi");
                        return RedirectToAction("InsertCustomerGroup", "Bayi");
                    }
                    else
                    {
                        if (customerGroup.GrubAdi != "" && customerGroup.Aciklama != "" && customerGroup.IskontoOrani != 0)
                        {
                            if (file != null)
                                customerGroup.GrupIcon = SaveImageProcess.ImageInsert(file, "Bayi");

                            customerGroup.Durum = true;
                            customerGroup.EklenmeTarih = DateTime.Now;
                            customerGroup.GuncellenmeTarih = DateTime.Now;
                            customerGroup.YoneticiID = getUser.ID;

                            await _unitOfWorkMusteriGrup.Repository.Add(customerGroup);
                            _unitOfWorkMusteriGrup.Repository.Save();

                            TempData["mesaj"] = "toastr['success']('Müşteri grubu başarıyla gerçekleşti','Müşteri Grubu Ekleme')";
                            await CreateModeratorLog("Başarılı", "Ekleme", "InsertCustomerGroup", "Bayi");
                            return RedirectToAction("CustomerGroups", "Bayi");
                        }
                        else
                        {
                            TempData["mesaj"] = "toastr['warning']('Müşteri grubu eklemesi başarısız','Hata')";
                            await CreateModeratorLog("Başarısız", "Ekleme", "InsertCustomerGroup", "Bayi");
                            return RedirectToAction("CustomerGroups", "Bayi");
                        }

                    }

                }
                catch (Exception)
                {
                    TempData["mesaj"] = $"toastr['warning']('Müşteri grubu eklemesi başarısız','Sistem Hatası')";
                    await CreateModeratorLog("Sistem Hatası", "Ekleme", "InsertCustomerGroup", "Bayi");
                    return RedirectToAction("CustomerGroups", "Bayi");
                }
            }
            else
            {
                await CreateModeratorLog("Başarısız", "Ekleme", "InsertCustomerGroup", "Bayi");
                return RedirectToAction("CustomerGroups", "Bayi");
            }
        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> RemoveCustomerGroup(int id)
        {
            try
            {
                MusteriGrubu getCustomerGroup = await _unitOfWorkMusteriGrup.Repository.GetById(id);
                List<Bayi> sellerList = await _unitOfWorkBayi.Repository.Where(x => x.BayiGrubuID == getCustomerGroup.ID).ToListAsync();

                if (sellerList.Count > 0)
                {
                    TempData["mesaj"] = "toastr['warning']('Bu gruba ait bayilikler mevcuttur. İlişkili bayileri kaldırıp silme işlemini tekrar deneyiniz!','Müşteri Grup İşlemi')";
                    await CreateModeratorLog("Başarılı", "Silme", "RemoveCustomerGroup", "Bayi");
                    return RedirectToAction("CustomerGroups", "Bayi");
                }
                else
                {
                    _unitOfWorkMusteriGrup.Repository.Delete(getCustomerGroup);
                    _unitOfWorkMusteriGrup.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Müşteri grubu başarıyla kaldırıldı','Müşteri Grup İşlemi')";
                    await CreateModeratorLog("Başarılı", "Silme", "RemoveCustomerGroup", "Bayi");
                    return RedirectToAction("CustomerGroups", "Bayi");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Müşteri Grup İşlemi')";
                await CreateModeratorLog("Başarısız", "Silme", "RemoveCustomerGroup", "Bayi");
                return RedirectToAction("CustomerGroups", "Bayi");
            }

        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> EditStatusCustomerGroup(int id)
        {
            try
            {
                MusteriGrubu getCustomerGroup = await _unitOfWorkMusteriGrup.Repository.GetById(id);
                if (getCustomerGroup.Durum == true)
                {
                    getCustomerGroup.Durum = false;
                    _unitOfWorkMusteriGrup.Repository.Update(getCustomerGroup);
                    _unitOfWorkMusteriGrup.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Bayi grubu başarıyla pasifleştirildi','Müşteri Grup Durum')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusCustomerGroup", "Bayi");
                    return RedirectToAction("CustomerGroups", "Bayi");
                }
                else
                {
                    getCustomerGroup.Durum = true;
                    _unitOfWorkMusteriGrup.Repository.Update(getCustomerGroup);
                    _unitOfWorkMusteriGrup.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Bayi grubu başarıyla aktifleştirildi','Müşteri Grup Durum')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "EditStatusCustomerGroup", "Bayi");
                    return RedirectToAction("CustomerGroups", "Bayi");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Müşteri Grup Düzenle')";
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditStatusCustomerGroup", "Bayi");
                return RedirectToAction("CustomerGroups", "Bayi");
            }
        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> EditCustomerGroup(int id)
        {
            MusteriGrubu getCustomerGroup = await _unitOfWorkMusteriGrup.Repository.GetById(id);
            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "EditCustomerGroup", "Bayi");
            return View(getCustomerGroup);
        }

        [CheckLogin]
        [HttpPost]
        public async Task<IActionResult> EditCustomerGroup(MusteriGrubu customerGroup, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MusteriGrubu getCustomerGroup = await _unitOfWorkMusteriGrup.Repository.GetById(customerGroup.ID);
                    getCustomerGroup.GuncellenmeTarih = DateTime.Now;
                    getCustomerGroup.EklenmeTarih = DateTime.Now;
                    getCustomerGroup.IskontoOrani = customerGroup.IskontoOrani;
                    getCustomerGroup.GrubAdi = customerGroup.GrubAdi;
                    getCustomerGroup.Aciklama = customerGroup.Aciklama;

                    List<MusteriGrupOzelUrun> productList = await _unitOfWorkMusteriGrupUrun.Repository.Where(x => x.MusteriGrupID == getCustomerGroup.ID).ToListAsync();

                    foreach (var item in productList)
                    {
                        Urun product = await _unitOfWorkUrun.Repository.GetById(item.UrunID);
                        product.IndirimOrani = getCustomerGroup.IskontoOrani;
                        product.IndirimliFiyat = Convert.ToDecimal(CalculateDiscount(product.KDVFiyat, product.IndirimOrani));
                        _unitOfWorkUrun.Repository.Update(product);
                        _unitOfWorkUrun.Repository.Save();
                    }
                    if (file != null)
                        getCustomerGroup.GrupIcon = SaveImageProcess.ImageInsert(file, "Bayi");

                    _unitOfWorkMusteriGrup.Repository.Update(getCustomerGroup);
                    _unitOfWorkMusteriGrup.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Grup başarıyla düzenlendi','Müşteri Grup Düzenle')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "EditMusteriGrub", "Bayi");
                    return RedirectToAction("CustomerGroups", "Bayi");

                }
                catch (Exception ex)
                {
                    TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Müşteri Grup Düzenle')";
                    await CreateModeratorLog("Sistem Hatası", "Güncelleme", "EditMusteriGrub", "Bayi");
                    return RedirectToAction("CustomerGroups", "Bayi");
                }
            }
            else
            {
                await CreateModeratorLog("Başarılı", "Güncelleme", "EditMusteriGrub", "Bayi");
                return RedirectToAction("EditMusteriGrub", new { id = customerGroup.ID });
            }
        }

        #endregion

        #region Bayi İşlemleri

        [CheckLogin]
        [HttpGet, ActionName("ListSeller")]
        public async Task<IActionResult> ListSeller(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Bayiler = await _unitOfWorkBayi.Repository.Where(x => x.ID > 0)
                  .Include("musteriGrubu")
                  .Include("yetkililer")
                  .Include("kullanici")
                  .Include("odeme").OrderByDescending(x => x.ID).ToPagedListAsync(pageNumber, 10);
            await CreateModeratorLog("Başarılı", "Listeleme", "ListSeller", "Bayi");
            return View();
        }

        [HttpGet, ActionName("ListSellerViewGrid")]
        public async Task<IActionResult> ListSellerViewGrid(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Bayiler = _unitOfWorkBayi.Repository.Where(x => x.ID > 0)
                  .Include("musteriGrubu")
                  .Include("yetkililer")
                  .Include("kullanici")
                  .Include("odeme").OrderByDescending(x => x.ID).ToPagedList(pageNumber, 12);
            await CreateModeratorLog("Başarılı", "Listeleme", "ListSellerViewGrid", "Bayi");
            return View();
        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> InsertSeller(SellerInsertVM bayi)
        {
            List<MusteriGrubu> customerGroup = (List<MusteriGrubu>)await _unitOfWorkMusteriGrup.Repository.Where(x => x.Durum != false).ToListAsync();
            ViewBag.BayiGrubuID = new SelectList(customerGroup, "ID", "GrubAdi");

            List<KargoFirmalar> cargoCompanies = (List<KargoFirmalar>)await _unitOfWorkKargoFirmalar.Repository.Where(x => x.Durum != false).ToListAsync();
            ViewBag.KargoFirmaID = new SelectList(cargoCompanies, "ID", "KargoAdi");

            List<OdemeSekli> odemeSekil = (List<OdemeSekli>)await _unitOfWorkOdemeSekli.Repository.All();
            ViewBag.OdemeID = new SelectList(odemeSekil, "ID", "Odeme");

            await CreateModeratorLog("Başarılı", "Sayfa Giriş", "InsertSeller", "Bayi");
            SellerInsertVM SellerInsert = new SellerInsertVM
            {
                BayiAdi = bayi.BayiAdi,
                Logo = bayi.Logo
            };
            return View(SellerInsert);
        }

        [HttpPost]
        [CheckLogin]
        public async Task<IActionResult> SellerInsert(SellerInsertVM sellervm, IFormFile file, int BayiGrubuID, int OdemeID, int KargoFirmaID)
        {

            if (ModelState.IsValid)
            {
                Bayi seller = new Bayi();
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                Kullanici getUser = await _unitOfWorkKullanici.Repository.GetById(id);
                MusteriGrubu customerGroup = (MusteriGrubu)await _unitOfWorkMusteriGrup.Repository.GetById(BayiGrubuID).ConfigureAwait(false);
                KargoFirmalar cargoCompanies = await _unitOfWorkKargoFirmalar.Repository.GetById(KargoFirmaID);
                sellervm.musteriGrubu = customerGroup;

                try
                {
                    if (file != null)
                        seller.Logo = SaveImageProcess.ImageInsert(file, "Bayi");

                    seller.BayiAdi = sellervm.BayiAdi;
                    seller.OdemeID = sellervm.OdemeID;
                    seller.UyeNo = sellervm.UyeNo;
                    seller.AktifMi = false;
                    seller.RiskLimiti = sellervm.RiskLimiti;
                    seller.GuncellenmeTarih = DateTime.Now;
                    seller.EklenmeTarih = DateTime.Now;
                    seller.AltBayiHakki = false;
                    seller.KullaniciAdi = sellervm.KullaniciAdi;
                    seller.Sifre = new Crypto().TryEncrypt(sellervm.Sifre);
                    seller.musteriGrubu = sellervm.musteriGrubu;
                    seller.BayiGrubuID = BayiGrubuID;
                    seller.OdemeID = OdemeID;
                    seller.YoneticiID = getUser.ID;
                    seller.kullanici = getUser;
                    seller.KargoFirmasiID = cargoCompanies.ID;
                    seller.kargoBayiFirma = cargoCompanies;
                    seller.HesapKisitla = false;

                    await _unitOfWorkBayi.Repository.Add(seller);
                    _unitOfWorkBayi.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Bayi ekleme başarılı!','Bayi ekleme')";
                    await CreateModeratorLog("Başarılı", "Ekleme", "SellerInsert", "Bayi");
                    return RedirectToAction("ListSeller", "Bayi");
                }
                catch (Exception)
                {
                    TempData["mesaj"] = "toastr['warning']('Sistem hatası meydana geldi','Bayi ekleme')";
                    await CreateModeratorLog("Sistem Hatası", "Ekleme", "SellerInsert", "Bayi");
                    return RedirectToAction("ListSeller", "Bayi");
                }
            }
            else
            {
                await CreateModeratorLog("Sistem Hatası", "Ekleme", "SellerInsert", "Bayi");
                return RedirectToAction("InsertSeller", new { bayi = sellervm });
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> SellerLoginAdmin(int id)
        {
            Bayi getSellerUser = await _unitOfWorkBayi.Repository.GetById(id);
            SellerLoginVM seller = new SellerLoginVM
            {
                bayi = getSellerUser
            };
            return View(seller);
        }

        [HttpPost]
        public IActionResult SellerLoginAdmin(SellerLoginVM sellerVM)
        {

            if (ModelState.IsValid)
            {
                sellerVM.password = new Crypto().TryEncrypt(sellerVM.password);
                Bayi isLogin = _unitOfWorkBayi.Repository.Where(x => x.KullaniciAdi == sellerVM.username && x.Sifre == sellerVM.password).FirstOrDefault();

                if (isLogin != null)
                {
                    HttpContext.Session.SetString("userB", isLogin.KullaniciAdi);
                    HttpContext.Session.SetInt32("IdB", isLogin.ID);
                    HttpContext.Session.SetString("ppB", isLogin.Logo);
                    return View("SellerLoginSucces", isLogin);
                }
                else
                {
                    TempData["Mesaj"] = "toastr['warning']('Kullanıcı adı veya şifre yanlış!','Bayi Girişi')";
                    return RedirectToAction("SellerLoginAdmin", new { id = sellerVM.ID });
                }
            }
            else
            {
                return RedirectToAction("SellerLoginAdmin", new { id = sellerVM.ID });
            }

        }

        [HttpGet]
        [CheckLogin]
        public IActionResult SellerLogOut()
        {
            HttpContext.Session.Remove("userB");
            HttpContext.Session.Remove("IdB");
            HttpContext.Session.Remove("ppB");
            HttpContext.Session.Clear();
            return RedirectToAction("SellerLoginAdmin", "Bayi");
        }

        public async Task<IActionResult> SellerDetailsAdmin(int ID, int? page)
        {
            List<Yetkililer> sellerCompetentlist = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.All();

            if (sellerCompetentlist.Count == 0)
            {
                return RedirectToAction("SellerCompetentInsert", "Islem");
            }

            else
            {

                List<Cuzdan> wallets = (List<Cuzdan>)await _unitOfWorkCuzdan.Repository.All();

                if (wallets.Count == 0)
                {
                    return RedirectToAction("CreateWalletAdmin", "Bayi");
                }
                else
                {

                    List<BayiTicari> commercialSellerList = (List<BayiTicari>)await _unitOfWorkBayiTicari.Repository.All();

                    if (commercialSellerList.Count == 0)
                    {
                        return RedirectToAction("CreateSellerCommercialData", "Islem");
                    }
                    else
                    {
                        if (ID != 0)
                        {
                            Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(ID);

                            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == getSeller.ID).SingleAsync();

                            List<UrunStok> stoklar = await _unitOfWorkUrunStok.Repository.Where(x => x.BayiID == getSeller.ID).ToListAsync();

                            List<BayiTicari> bt = (List<BayiTicari>)await _unitOfWorkBayiTicari.Repository.All();

                            List<Bulten> bulten = await _unitOfWorkBulten.Repository.Where(x => x.YetkiliID == getSellerCompetent.ID).ToListAsync();

                            List<Yetkililer> yetkililer = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == getSeller.ID).ToListAsync();
                            ViewBag.ToplamStok = stoklar.Select(x => x.Adet).Sum();
                            ViewBag.Yetkililer = yetkililer.Select(x => x.ID).Sum();

                            Cuzdan cuzdanim = await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == ID).SingleOrDefaultAsync();
                            if (cuzdanim != null && cuzdanim.ID > 0)
                            {
                                ViewBag.Bakiye = Convert.ToDecimal(cuzdanim.Bakiye);
                                ViewBag.ToplamTutarIslem = Convert.ToInt32(_unitOfWorkCuzdanIslemler.Repository.Where(x => x.CuzdanID == cuzdanim.ID).Sum(x => x.IslemTutari));
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

                            return View(getSeller);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
        }

        [CheckLogin]
        public async Task<IActionResult> SellerEditStatus(int id)
        {
            try
            {
                Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
                if (getSeller != null)
                {
                    if (getSeller.AktifMi == true)
                    {
                        getSeller.AktifMi = false;
                        _unitOfWorkBayi.Repository.Update(getSeller);
                        _unitOfWorkBayi.Repository.Save();

                        TempData["Mesaj"] = "toastr['success']('Bayi pasileştirme işlemi başarılı','Bayi Durum')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "SellerEditStatus", "Bayi");
                        return RedirectToAction("ListSeller", "Bayi");
                    }
                    else
                    {
                        getSeller.AktifMi = true;
                        _unitOfWorkBayi.Repository.Update(getSeller);
                        _unitOfWorkBayi.Repository.Save();

                        TempData["Mesaj"] = "toastr['success']('Bayi aktifleştirme işlemi başarılı','Bayi Durum')";
                        await CreateModeratorLog("Başarılı", "Güncelleme", "SellerEditStatus", "Bayi");
                        return RedirectToAction("ListSeller", "Bayi");
                    }
                }
                else
                {
                    TempData["Mesaj"] = "toastr['warning']('Bayi verisi alınamadı','Bayi Durum')";
                    await CreateModeratorLog("Başarılı", "Güncelleme", "SellerEditStatus", "Bayi");
                    return RedirectToAction("ListSeller", "Bayi");
                }


            }
            catch (Exception)
            {
                TempData["Mesaj"] = "toastr['success']('Sistemden kaynaklı bir hata meydana geldi','Bayi Durum')";
                await CreateModeratorLog("Başarılı", "Güncelleme", "SellerEditStatus", "Bayi");
                return RedirectToAction("ListSeller", "Bayi");
            }
        }

        [CheckLogin]
        public async Task<IActionResult> EditCargoManage(int id)
        {
            try
            {

                Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
                if (getSeller != null)
                {
                    if (getSeller.KargoYonet == true)
                    {
                        getSeller.KargoYonet = false;
                        _unitOfWorkBayi.Repository.Update(getSeller);
                        _unitOfWorkBayi.Repository.Save();

                        TempData["mesaj"] = new ToastrMessage
                        {
                            Title = "Başarılı",
                            Message = "Kargo yönetimi gerçekleştirilemez!",
                            type = ToastrType.success
                        };
                        await ListSeller(page);
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditCargoManage", "Bayi");

                        return View("ListSeller");
                    }
                    else
                    {
                        getSeller.KargoYonet = true;
                        _unitOfWorkBayi.Repository.Update(getSeller);
                        _unitOfWorkBayi.Repository.Save();

                        TempData["mesaj"] = new ToastrMessage
                        {
                            Title = "Başarılı",
                            Message = "Kargo yönetimi gerçekleştirilebilir!",
                            type = ToastrType.success
                        };
                        await ListSeller(page);
                        await CreateModeratorLog("Başarılı", "Güncelleme", "EditCargoManage", "Bayi");
                        return View("ListSeller");
                    }
                }
                else
                {
                    TempData["mesaj"] = new ToastrMessage
                    {
                        Title = "Hata",
                        Message = "Bayi verisi alınamadı!",
                        type = ToastrType.warning
                    };
                    await ListSeller(page);
                    await CreateModeratorLog("Başarısız", "Güncelleme", "SellerEditStatus", "Bayi");
                    return View("ListSeller");
                }


            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage
                {
                    Title = "Sistem Hatası",
                    Message = "Sistemden kaynaklı hata meydana geldi!",
                    type = ToastrType.info
                };
                await ListSeller(page);
                await CreateModeratorLog("Sistem Hatası", "Güncelleme", "SellerEditStatus", "Bayi");
                return View("ListSeller");
            }
        }

        [CheckLogin]
        public async Task<IActionResult> SellerRemove(int id)
        {
            try
            {

                Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
                if (getSeller != null)
                {
                    _unitOfWorkBayi.Repository.Delete(getSeller);
                    _unitOfWorkBayi.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Bayi silme başarılı!','Bayi ekleme')";

                    await ListSeller(page);
                    await CreateModeratorLog("Başarılı", "Silme", "SellerRemove", "Bayi");

                    return View("ListSeller");

                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Bayi verisi alınamadı!','Hata')";
                    await ListSeller(page);
                    await CreateModeratorLog("Başarısız", "Silme", "SellerRemove", "Bayi");

                    return View("ListSeller");
                }


            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi!','Sistem Hatası')";
                await ListSeller(page);
                await CreateModeratorLog("Sistem Hatası", "Silme", "SellerRemove", "Bayi");
                return View("ListSeller");
            }
        }

        [CheckOtherLogin]
        public async Task<IActionResult> EditSeller(int id)
        {
            ViewBag.ID = id;
            Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
            if (getSeller != null)
            {
                getSeller.Sifre = new Crypto().TryDecrypt(getSeller.Sifre);
                SellerEditVM sellerVM = new SellerEditVM
                {
                    ID = getSeller.ID,
                    BayiAdi = getSeller.BayiAdi,
                    Logo = getSeller.Logo,
                    RiskLimiti = getSeller.RiskLimiti,
                    KullaniciAdi = getSeller.KullaniciAdi,
                    Sifre = getSeller.Sifre,
                    SifreTekrar = getSeller.Sifre,
                    UyeNo = getSeller.UyeNo,
                    OdemeID = getSeller.OdemeID,
                    musteriGrubu = getSeller.musteriGrubu,
                    odeme = getSeller.odeme
                };

                List<MusteriGrubu> grup = (List<MusteriGrubu>)await _unitOfWorkMusteriGrup.Repository.All();
                ViewBag.BayiGrubuID = new SelectList(grup, "ID", "GrubAdi");

                List<OdemeSekli> payType = (List<OdemeSekli>)await _unitOfWorkOdemeSekli.Repository.All();
                ViewBag.OdemeID = new SelectList(payType, "ID", "Odeme");

                return View(sellerVM);
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Bayi verisi alınamadı!','Hata')";
                await ListSeller(page);
                return View("ListSeller");
            }
        }

        [CheckLogin]
        public async Task<IActionResult> EditSellerAdmin(int id)
        {

            Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
            if (getSeller != null)
            {
                getSeller.Sifre = new Crypto().TryDecrypt(getSeller.Sifre);
                SellerEditVM sellerVM = new SellerEditVM
                {
                    ID = getSeller.ID,
                    BayiAdi = getSeller.BayiAdi,
                    Logo = getSeller.Logo,
                    RiskLimiti = getSeller.RiskLimiti,
                    KullaniciAdi = getSeller.KullaniciAdi,
                    Sifre = getSeller.Sifre,
                    SifreTekrar = getSeller.Sifre,
                    UyeNo = getSeller.UyeNo,
                    OdemeID = getSeller.OdemeID,
                    musteriGrubu = getSeller.musteriGrubu,
                    odeme = getSeller.odeme
                };

                List<MusteriGrubu> grup = (List<MusteriGrubu>)await _unitOfWorkMusteriGrup.Repository.Where(x => x.Durum == true).ToListAsync();
                ViewBag.BayiGrubuID = new SelectList(grup, "ID", "GrubAdi", getSeller.BayiGrubuID);


                List<OdemeSekli> payTpye = (List<OdemeSekli>)await _unitOfWorkOdemeSekli.Repository.All();
                ViewBag.OdemeID = new SelectList(payTpye, "ID", "Odeme", getSeller.OdemeID);

                await CreateModeratorLog("Başarılı", "Güncelleme", "EditSellerAdmin", "Bayi");

                return View(sellerVM);
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Bayi verisi alınamadı!','Hata')";
                await ListSeller(page);
                await CreateModeratorLog("Başarısız", "Güncelleme", "EditSellerAdmin", "Bayi");

                return View("ListSeller");
            }
        }

        [HttpGet]
        public IActionResult LiveOrderList()
        {
            return PartialView("LiveOrderList");
        }

        [HttpPost]
        public async Task<IActionResult> SellerEdit(SellerEditVM sellerVM, IFormFile file, int BayiGrubuID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(sellerVM.ID);

                    if (sellerVM.Sifre == sellerVM.SifreTekrar)
                    {
                        if (file != null)
                            getSeller.Logo = SaveImageProcess.ImageInsert(file, "Bayi");

                        sellerVM.Sifre = new Crypto().TryEncrypt(sellerVM.Sifre);
                        getSeller.BayiAdi = sellerVM.BayiAdi;
                        getSeller.BayiGrubuID = BayiGrubuID;
                        getSeller.GuncellenmeTarih = DateTime.Now;
                        getSeller.RiskLimiti = sellerVM.RiskLimiti;
                        getSeller.OdemeID = sellerVM.OdemeID;
                        getSeller.Sifre = sellerVM.Sifre;
                        getSeller.KullaniciAdi = sellerVM.KullaniciAdi;

                        _unitOfWorkBayi.Repository.Update(getSeller);
                        _unitOfWorkBayi.Repository.Save();
                        TempData["mesaj"] = "toastr['success']('Bayi güncelleme işlemi başarılı!','Bayi Güncelleme')";
                        return RedirectToAction("ListSeller", "Bayi");
                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Şifreler uyuşmuyor!','Bayi Güncelleme')";
                        return RedirectToAction("EditSellerAdmin", new { id = sellerVM.ID });
                    }
                }
                else
                {
                    return RedirectToAction("EditSellerAdmin", new { id = sellerVM.ID });
                }

            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi!','Bayi Güncelleme')";
                return RedirectToAction("EditSellerAdmin", new { id = sellerVM.ID });
            }
        }

        [HttpGet]
        public IActionResult DetailPartialProductPriceAdmin()
        {
            return PartialView("DetailPartialProductPriceAdmin");
        }

        [HttpGet]
        public async Task<IActionResult> DetailProduct(int Id)
        {
            int IdSeller = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == IdSeller).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = getSeller;

            Urun u = await _unitOfWorkUrun.Repository.Where(x => x.ID == Id).Include("kategori").Include("marka").Include("stokbirimi").SingleAsync();

            List<UrunResimler> uResimlers = await _unitOfWorkUrunResimler.Repository.Where(x => x.UrunID == u.ID).ToListAsync();
            ViewBag.UrunResimler = uResimlers;

            List<UrunFiyat> uFiyatlars = await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == Id).ToListAsync();
            ViewBag.UrunFiyat = uFiyatlars;

            List<Urun> altUrunler = await _unitOfWorkUrun.Repository.Where(x => x.UstUrunID == u.ID).ToListAsync();
            ViewBag.AltUrunler = altUrunler;

            List<EtiketGonderi> etiketler = await _unitOfWorkEtiketUrunler.Repository.Where(x => x.GonderiID == u.ID).Include("etiketler").ToListAsync();
            ViewBag.Etiketler = etiketler;

            return View(u);
        }

        public decimal CalculateDiscount(decimal price, decimal? discount)
        {
            decimal _discount = (decimal)(price * (discount / 100));
            decimal total = price - _discount;
            return total;
        }

        [HttpGet]
        public IActionResult CreateWalletAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalletAdmin(Cuzdan wallet)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).FirstOrDefaultAsync();
            try
            {

                if (getSeller != null)
                {
                    Cuzdan newWallet = new Cuzdan
                    {
                        bayi = getSeller,
                        BayiID = id,
                        OlusturulmaTarih = DateTime.Now,
                        Bakiye = wallet.Bakiye,
                        CuzdanNo = wallet.CuzdanNo,
                        CuzdanTipi = wallet.CuzdanTipi
                    };

                    await _unitOfWorkCuzdan.Repository.Add(newWallet);
                    _unitOfWorkCuzdan.Repository.Save();
                    return RedirectToAction("SellerAdminDetails", "Bayi");
                }

                else
                {
                    return RedirectToAction("SellerAdminDetails", "Bayi");

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("SellerAdminDetails", "Bayi");
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
}
