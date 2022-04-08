using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DATA.Toastr;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.WEB.Controllers
{
    public class IslemController : Controller
    {
        #region Field

        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<MusteriGrubu> _unitOfWorkMusteriGrup;
        private readonly IUnitOfWork<Kullanici> _unitOfWorkKullanici;
        private readonly IUnitOfWork<Yetkililer> _unitOfWorkYetkililer;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWork<Sehir> _unitOfWorkSehir;
        private readonly IUnitOfWork<Ilce> _unitOfWorkIlce;
        private readonly IUnitOfWork<Bulten> _unitOfOWorkBulten;
        private readonly IUnitOfWork<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWork<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWork<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWork<KargoFirmalar> _unitOfWorkKargoFirmalar;
        private readonly IUnitOfWork<KargoSure> _unitOfWorkKargoSure;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork<MusteriGrupKargo> _unitOfWorkMusteriGrupKargo;
        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;
        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategoriler;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarka;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        public IslemController(IUnitOfWork<Bayi> unitOfWork,
            IUnitOfWork<MusteriGrubu> unitOfWorkMusteriGrub,
            IUnitOfWork<Kullanici> unitOfWorkKullanici,
            IWebHostEnvironment hostEnvironment,
            IUnitOfWork<Yetkililer> unitOfWorkYetkililer,
            IUnitOfWork<BayiTicari> unitOfWorkBayiTicari,
            IUnitOfWork<Sehir> unitOfWorkSehir,
            IUnitOfWork<Ilce> unitOfWorkIlce,
            IUnitOfWork<Bulten> unitOfWorkBulten,
            IUnitOfWork<Durumlar> unitOfWorkDurumlar,
            IUnitOfWork<Loglar> unitOfWorkLoglar,
            IUnitOfWork<Islemler> unitOfWorkIslemler,
            IUnitOfWork<KargoFirmalar> unitOfWorkKargoFirmalar,
            IUnitOfWork<KargoSure> unitOfWorkKargoSure,
            IUnitOfWork<MusteriGrupKargo> unitOfWorkMusteriGrupKargo,
            IUnitOfWork<Haber> unitOfWorkHaberler,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Begeni> unitOfWorkBegeni,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<SiteInfo> unitOfWorkSiteInfo,
            IUnitOfWork<Sepet> unitOfWorkSepet,
            IUnitOfWork<Urun> unitOfWorkUrun)
        {
            this._unitOfWorkBayi = unitOfWork;
            this._unitOfWorkMusteriGrup = unitOfWorkMusteriGrub;
            this._unitOfWorkKullanici = unitOfWorkKullanici;
            this._hostEnvironment = hostEnvironment;
            this._unitOfWorkYetkililer = unitOfWorkYetkililer;
            this._unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            this._unitOfWorkSehir = unitOfWorkSehir;
            this._unitOfWorkIlce = unitOfWorkIlce;
            this._unitOfOWorkBulten = unitOfWorkBulten;
            this._unitOfWorkLogDurumlar = unitOfWorkDurumlar;
            this._unitOfWorkLogIslemler = unitOfWorkIslemler;
            this._unitOfWorkLogLoglar = unitOfWorkLoglar;
            this._unitOfWorkKargoFirmalar = unitOfWorkKargoFirmalar;
            this._unitOfWorkKargoSure = unitOfWorkKargoSure;
            this._unitOfWorkMusteriGrupKargo = unitOfWorkMusteriGrupKargo;
            this._unitOfWorkHaber = unitOfWorkHaberler;
            this._unitOfWorkMarka = unitOfWorkMarka;
            this._unitOfWorkKategoriler = unitOfWorkKategori;
            this._unitOfWorkBegeni = unitOfWorkBegeni;
            this._unitOfWorkStokBildirim = unitOfWorkStokBildirim;
            this._unitOfWorkSiteInfo = unitOfWorkSiteInfo;
            this._unitOfWorkSepet = unitOfWorkSepet;
            this._unitOfWorkUrun = unitOfWorkUrun;
        }

        #endregion

        [CheckOtherLogin]
        public async Task<IActionResult> EditCommercialSellerDataWeb()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            BayiTicari bt = _unitOfWorkBayiTicari.Repository.FirstOrDefault(x => x.BayiID == id);
            if (bt != null)
            {
                List<Sehir> listSehir = (List<Sehir>)await _unitOfWorkSehir.Repository.All();
                List<Ilce> listIlce = (List<Ilce>)await _unitOfWorkIlce.Repository.All();
                List<Yetkililer> yetkiliList = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.All();
                ViewBag.SehirID = new SelectList(listSehir, "ID", "SehirAdi", bt.SehirID);
                ViewBag.IlceID = new SelectList(listIlce, "ID", "IlceAdi", bt.IlceID);
                ViewBag.FirmaYetkiliID = new SelectList(yetkiliList, "ID", "YetkiliAdi", bt.FirmaYetkiliID);
                return View(bt);
            }
            else
            {
                return RedirectToAction("SellerDetail", "Bayi", new { ID = id });
            }

        }

        [HttpPost]
        public bool EditCommercialSellerDataWeb([FromBody] BayiTicari bayiTicari)
        {
            int BayiID = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Yetkililer yetkili = _unitOfWorkYetkililer.Repository.FirstOrDefault(x => x.ID == bayiTicari.FirmaYetkiliID);

            bayiTicari.MuhasebeMutabakatTel = yetkili.TelNo;
            bayiTicari.MuhasebeMutabakatEmail = yetkili.Email;
            bayiTicari.BayiID = BayiID;
            _unitOfWorkBayiTicari.Repository.Update(bayiTicari);
            _unitOfWorkBayiTicari.Repository.Save();

            return true;
        }

        [CheckOtherLogin]
        public async Task<IActionResult> InsertCommercialSellerDataWeb()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            List<Sehir> listSehir = (List<Sehir>)await _unitOfWorkSehir.Repository.All();
            List<Ilce> listIlce = (List<Ilce>)await _unitOfWorkIlce.Repository.All();
            List<Yetkililer> yetkiliList = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == id).ToListAsync();
            if (yetkiliList.Count > 0)
            {
                ViewBag.SehirID = new SelectList(listSehir, "ID", "SehirAdi");
                ViewBag.IlceID = new SelectList(listIlce, "ID", "IlceAdi");
                ViewBag.FirmaYetkiliID = new SelectList(yetkiliList, "ID", "YetkiliAdi");
                return View();
            }
            else
            {
                return RedirectToAction("InsertSellerCompetentWeb", "Islem");
            }

        }

        [HttpPost]
        public bool InsertCommercialSellerDataWeb([FromBody] BayiTicari bayiticari)
        {
            int BayiID = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            Bayi getSeller = _unitOfWorkBayi.Repository.FirstOrDefault(x => x.ID == BayiID);
            Yetkililer yetkili = _unitOfWorkYetkililer.Repository.FirstOrDefault(x => x.ID == bayiticari.FirmaYetkiliID);

            bayiticari.MuhasebeMutabakatEmail = yetkili.Email;
            bayiticari.MuhasebeMutabakatTel = yetkili.TelNo;
            bayiticari.BayiID = getSeller.ID;
            bayiticari.bayiBayiTicari = getSeller;

            _unitOfWorkBayiTicari.Repository.Add(bayiticari);
            _unitOfWorkBayiTicari.Repository.Save();
            return true;

        }

        [CheckOtherLogin]
        public async Task<IActionResult> SellerCompetentListWeb()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.ID = id;
            List<Yetkililer> yetkiliList = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == id).ToListAsync();
            if (yetkiliList.Count == 0)
            {
                return RedirectToAction("YetkiliInsert", "Islem", new { ID = id });
            }
            else
            {
                return View(yetkiliList);
            }

        }

        [CheckOtherLogin]
        public async Task<IActionResult> RemoveSellerCompetentWeb(int id)
        {
            try
            {
                Yetkililer gelenYetkili = await _unitOfWorkYetkililer.Repository.GetById(id);
                if (gelenYetkili != null)
                {

                    List<BayiTicari> gelenBT = (List<BayiTicari>)await _unitOfWorkBayiTicari.Repository.Where(x => x.FirmaYetkiliID == gelenYetkili.ID).ToListAsync();

                    foreach (BayiTicari item in gelenBT.ToList())
                    {
                        _unitOfWorkBayiTicari.Repository.Delete(item);
                        _unitOfWorkBayiTicari.Repository.Save();
                    }

                    _unitOfWorkYetkililer.Repository.Delete(gelenYetkili);
                    _unitOfWorkYetkililer.Repository.Save();

                    TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Yetkili sistemden kaldırıldı", type = ToastrType.success }; ;
                    await SellerCompetentListWeb();
                    return View("SellerCompetentListWeb");
                }
                else
                {
                    TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Yetkili verisi alınamadı", type = ToastrType.warning }; ;
                    await SellerCompetentListWeb();
                    return View("SellerCompetentListWeb");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Önce bu yetkiliye bağlı bilgileri kaldırınız", type = ToastrType.warning }; ;
                await SellerCompetentListWeb();
                return View("SellerCompetentListWeb");
            }

        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> EditSellerCompetentWeb(int id)
        {
            ViewBag.ID = id;
            List<Bayi> bayiler = (List<Bayi>)await _unitOfWorkBayi.Repository.All();
            ViewBag.BayiID = new SelectList(bayiler, "ID", "BayiAdi");

            Yetkililer yetkili = await _unitOfWorkYetkililer.Repository.GetById(id);
            return View(yetkili);

        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> EditSellerCompetent(int id)
        {
            Yetkililer yetkili = await _unitOfWorkYetkililer.Repository.GetById(id);
            List<Bayi> bayiler = (List<Bayi>)await _unitOfWorkBayi.Repository.All();
            ViewBag.BayiID = new SelectList(bayiler, "ID", "BayiAdi", yetkili.BayiID);

            return View(yetkili);
        }

        [HttpPost]
        public async Task<IActionResult> EditSellerCompetent(Yetkililer y, IFormFile file, int BayiID)
        {
            Yetkililer gelenYetkililer = await _unitOfWorkYetkililer.Repository.GetById(y.ID);

            try
            {
                gelenYetkililer.BayiID = BayiID;
                gelenYetkililer.Durum = true;
                gelenYetkililer.GuncellenmeTarih = DateTime.Now;
                gelenYetkililer.TelNo = y.TelNo;
                gelenYetkililer.Email = y.Email;
                gelenYetkililer.YetkiliAdi = y.YetkiliAdi;

                if (file != null)
                {
                    gelenYetkililer.YetkiliFoto = SaveImageProcess.ImageInsert(file, "Bayi");
                }

                _unitOfWorkYetkililer.Repository.Update(gelenYetkililer);
                _unitOfWorkYetkililer.Repository.Save();

                TempData["mesaj"] = new ToastrMessage
                {
                    Title = "Başarılı",
                    Message = "Yetkili düzenleme işlemi başarılı",
                    type = ToastrType.success
                };

                return View("ListBayi");

            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi!", type = ToastrType.warning };
                return View("ListBayi");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditSellerCompetentWeb(Yetkililer y, IFormFile file, int BayiID)
        {
            Yetkililer gelenYetkililer = await _unitOfWorkYetkililer.Repository.GetById(y.ID);

            try
            {
                gelenYetkililer.BayiID = BayiID;
                gelenYetkililer.Durum = true;
                gelenYetkililer.GuncellenmeTarih = DateTime.Now;
                gelenYetkililer.TelNo = y.TelNo;
                gelenYetkililer.Email = y.Email;
                gelenYetkililer.YetkiliAdi = y.YetkiliAdi;

                if (file != null)
                {
                    gelenYetkililer.YetkiliFoto = SaveImageProcess.ImageInsert(file, "Bayi");
                }

                _unitOfWorkYetkililer.Repository.Update(gelenYetkililer);
                _unitOfWorkYetkililer.Repository.Save();

                TempData["mesaj"] = new ToastrMessage
                {
                    Title = "Başarılı",
                    Message = "Yetkili düzenleme işlemi başarılı",
                    type = ToastrType.success
                };
                await SellerCompetentListWeb();
                return View("SellerCompetentListWeb");
            }

            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage
                {
                    Title = "Sistem Hatası",
                    Message = "Sistemden kaynaklı bir hata meydana geldi!",
                    type = ToastrType.warning
                };
                await SellerCompetentListWeb();
                return View("SellerCompetentListWeb");
            }

        }

        [CheckOtherLogin]
        [HttpGet]
        public IActionResult InsertSellerCompetentWeb()
        {
        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertSellerCompetentWeb(Yetkililer y, IFormFile file)
        {
            int bayiId = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            try
            {
                Bayi gelenBayi = await _unitOfWorkBayi.Repository.GetById(bayiId);
                Yetkililer yeniYetkili = new Yetkililer
                {
                    YetkiliAdi = y.YetkiliAdi,
                    TelNo = y.TelNo,
                    Email = y.Email,
                    BayiID = bayiId,
                    bayiYetkilisi = gelenBayi,
                    Durum = true,
                    EklenmeTarih = DateTime.Now,
                    GuncellenmeTarih = DateTime.Now
                };

                if (file != null)
                {
                    yeniYetkili.YetkiliFoto = SaveImageProcess.ImageInsert(file, "Bayi");
                }

                await _unitOfWorkYetkililer.Repository.Add(yeniYetkili);
                _unitOfWorkYetkililer.Repository.Save();

                TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Yetkili oluşturma işlemi başarılı", type = ToastrType.success };
                await SellerCompetentListWeb();
                return View("SellerCompetentListWeb");

            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.warning };
                return RedirectToAction("SellerDetail", "Bayi", bayiId);
            }

        }

        public async Task<IActionResult> EditStatusCompetentSeller(int ID)
        {
            try
            {
                Yetkililer gelenYetkili = await _unitOfWorkYetkililer.Repository.GetById(ID);
                if (gelenYetkili != null)
                {
                    if (gelenYetkili.Durum == true)
                    {
                        gelenYetkili.Durum = false;
                        _unitOfWorkYetkililer.Repository.Update(gelenYetkili);
                        _unitOfWorkYetkililer.Repository.Save();
                    }
                    else
                    {
                        gelenYetkili.Durum = true;
                        _unitOfWorkYetkililer.Repository.Update(gelenYetkili);
                        _unitOfWorkYetkililer.Repository.Save();
                    }
                    TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Yetkili durumu düzenlendi", type = ToastrType.success };
                    await SellerCompetentListWeb();
                    return View("SellerCompetentListWeb");
                }
                else
                {
                    TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Yetkili verisi alınamadı", type = ToastrType.warning };
                    await SellerCompetentListWeb();
                    return View("SellerCompetentListWeb");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Sistemden kaynaklı hata meydana geldi", type = ToastrType.warning }; ;
                await SellerCompetentListWeb();
                return View("SellerCompetentListWeb");
            }
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> BulletinMemberCreateSellerWeb()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
 
            ViewBag.ID = id;
            Yetkililer y = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == id).SingleAsync();
            Bulten b = new Bulten
            {
                YetkiliID = y.ID
            };
            return View(b);
        }

        [HttpPost]
        public async Task<IActionResult> BulletinMemberCreateSellerWeb(Bulten b, int YetkiliID)
        {
            Yetkililer y = await _unitOfWorkYetkililer.Repository.GetById(YetkiliID);
            try
            {
                Bulten yenib = new Bulten
                {
                    AdinizSoyadiniz = b.AdinizSoyadiniz,
                    Durum = true,
                    TelefonNo = b.TelefonNo,
                    EmailAdresi = b.EmailAdresi,
                    YetkiliID = YetkiliID
                };

                await _unitOfOWorkBulten.Repository.Add(yenib);
                _unitOfOWorkBulten.Repository.Save();

                //TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Bayi için bülten oluşturma işlemi başarılı", type = ToastrType.success };
                return RedirectToAction("SellerDetail", "Bayi", new { ID = y.BayiID });
            }
            catch (Exception)
            {
                //TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.success };
                return RedirectToAction("SellerDetail", "Bayi", new { ID = y.BayiID });
            }

        }

        [CheckOtherLogin]
        public async Task<IActionResult> BulletinMemberQuitWeb()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            ViewBag.ID = id;
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.GetById(id);
            try
            {
                Bulten gelenBulten = await _unitOfOWorkBulten.Repository.GetById(id);
                gelenBulten.Durum = false;
                _unitOfOWorkBulten.Repository.Update(gelenBulten);
                _unitOfOWorkBulten.Repository.Save();

                //TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Bülten üyeliğiniz donduruldu", type = ToastrType.success };
                return RedirectToAction("SellerDetail", "Bayi", new { gelenBayi.ID });
            }
            catch (Exception)
            {
                //TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.info };
                return RedirectToAction("SellerDetail", "Bayi", new { gelenBayi.ID });
            }
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> BulletinMemberEditSellerWeb()
        {
       
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.ID = id;
            Yetkililer gelenY = _unitOfWorkYetkililer.Repository.FirstOrDefault(x => x.BayiID == id);
            Bulten gelenBulten = await _unitOfOWorkBulten.Repository.Where(x => x.YetkiliID == gelenY.ID).SingleAsync();
            return View(gelenBulten);
        }

        [HttpPost]
        public async Task<IActionResult> BulletinMemberEditSellerWeb(Bulten b, int YetkiliID)
        {
            Yetkililer y = _unitOfWorkYetkililer.Repository.FirstOrDefault(x => x.ID == YetkiliID);
            Bulten gelenBulten = await _unitOfOWorkBulten.Repository.Where(x => x.YetkiliID == y.ID).SingleAsync();

            try
            {
                gelenBulten.TelefonNo = b.TelefonNo;
                gelenBulten.EmailAdresi = b.EmailAdresi;
                gelenBulten.AdinizSoyadiniz = b.AdinizSoyadiniz;

                _unitOfOWorkBulten.Repository.Update(gelenBulten);
                _unitOfOWorkBulten.Repository.Save();

                //TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.info };
                return RedirectToAction("SellerDetail", "Bayi", new { ID = y.BayiID });
            }
            catch (Exception)
            {
                //TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.info };
                return RedirectToAction("SellerDetail", "Bayi", new { ID = y.BayiID });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TradeWithCreditSellerWeb()
        {
            
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.ID = id;
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").SingleAsync();
            BayiTicari gelenTicari = (BayiTicari)await _unitOfWorkBayiTicari.Repository
                                               .Where(x => x.BayiID == b.ID)
                                               .SingleAsync();
            Yetkililer yetkili = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == b.ID).SingleAsync();
            ViewBag.BayiTicari = gelenTicari;
            ViewBag.Yetkili = yetkili;
            return View(b);
        }

        [HttpPost]
        public ActionResult OnlineApplyCreditSeller()
        {
            return View();
        } //Bu bölüm kodlanacak

        [HttpGet]
        public JsonResult GetIlcelerBySehirID(int id)
        {
            List<Ilce> ilceler = _unitOfWorkIlce.Repository.Where(x => x.SehirID == id).ToList();
            return Json(ilceler);
        }

        #region Kargo Yönetim

        public async Task<IActionResult> KargolariYonetWeb()
        {
          
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi gelenBayi = await _unitOfWorkBayi.Repository.GetById(id);
            ViewBag.GelenBayi = gelenBayi;

            ViewBag.Kargolar = await _unitOfWorkMusteriGrupKargo.Repository.Where(x => x.MusteriGrupID == gelenBayi.BayiGrubuID).Include("Kargo").ToListAsync();
            List<MusteriGrupKargo> kargolar = ViewBag.Kargolar as List<MusteriGrupKargo>;

            return View(kargolar);
        }

        public async Task<IActionResult> KargoSecBayi(int KargoID)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.GetById(id);
            KargoFirmalar kf = await _unitOfWorkKargoFirmalar.Repository.GetById(KargoID);

            try
            {
                b.KargoFirmasiID = KargoID;
                b.kargoBayiFirma = kf;

                _unitOfWorkBayi.Repository.Update(b);
                _unitOfWorkBayi.Repository.Save();

                MusteriGrupKargo gelenKargo = await _unitOfWorkMusteriGrupKargo.Repository.Where(x => x.KargoID == b.KargoFirmasiID && x.MusteriGrupID == b.BayiGrubuID).SingleOrDefaultAsync();
                gelenKargo.Durum = true;

                _unitOfWorkMusteriGrupKargo.Repository.Update(gelenKargo);
                _unitOfWorkMusteriGrupKargo.Repository.Save();

                List<MusteriGrupKargo> kargoKontrol = await _unitOfWorkMusteriGrupKargo.Repository.Where(x => x.MusteriGrupID == b.BayiGrubuID).ToListAsync();

                foreach (var item in kargoKontrol)
                {
                    if (gelenKargo.KargoID == item.KargoID)
                    {

                    }
                    else
                    {
                        item.Durum = false;
                        _unitOfWorkMusteriGrupKargo.Repository.Update(item);
                        _unitOfWorkMusteriGrupKargo.Repository.Save();
                    }
                }

                //KargoSure kg = new KargoSure();
                //kg.BayiID = b.ID;
                //kg.kargoSüreBayi = b;
                //kg.KargoID = KargoID;
                //kg.kargoSureKargo = kf;
                //kg.AktifMi = true;
                //kg.Baslangic = DateTime.Now;
                //kg.Bitis = kg.Baslangic.AddYears(1);

                //await _unitOfWorkKargoSure.Repository.Add(kg);
                //_unitOfWorkKargoSure.Repository.Save();

                await KargolariYonetWeb();
                return View("KargolariYonetWeb");
            }
            catch (Exception)
            {
                await KargolariYonetWeb();
                return View("KargolariYonetWeb");
            }
        }

        #endregion

    
    }
}
