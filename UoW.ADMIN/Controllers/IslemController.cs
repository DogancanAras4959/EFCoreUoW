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
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DATA.Toastr;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
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
        private readonly IUnitOfWork<MusteriGrupKargo> _unitOfWorkMusteriGrupKargo;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategori;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarka;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
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
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Begeni> unitOfWorkBegeni)
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
            this._unitOfWorkKategori = unitOfWorkKategori;
            this._unitOfWorkMarka = unitOfWorkMarka;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkBegeni = unitOfWorkBegeni;
        }

        #endregion

        [CheckOtherLogin]
        public async Task<IActionResult> EditSellerCommercialData()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            BayiTicari sellerCommercial = await _unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == id).FirstOrDefaultAsync();
            if (sellerCommercial != null)
            {
                List<Sehir> listState = (List<Sehir>)await _unitOfWorkSehir.Repository.All();
                //List<Ilce> listProvince = (List<Ilce>)await _unitOfWorkIlce.Repository.All();
                List<Yetkililer> SellerCompetentList = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.All();
                ViewBag.SehirID = new SelectList(listState, "ID", "SehirAdi", sellerCommercial.SehirID);
                //ViewBag.IlceID = new SelectList(listProvince, "ID", "IlceAdi", sellerCommercial.IlceID);
                ViewBag.FirmaYetkiliID = new SelectList(SellerCompetentList, "ID", "YetkiliAdi", sellerCommercial.FirmaYetkiliID);
                return View(sellerCommercial);
            }
            else
            {
                return RedirectToAction("SellerDetailsAdmin", "Bayi", new { ID = id });
            }

        }

        [HttpPost]
        public bool EditSellerCommercialData([FromBody] BayiTicari sellerCommercial)
        {
            int BayiID = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Yetkililer yetkili = _unitOfWorkYetkililer.Repository.FirstOrDefault(x => x.ID == sellerCommercial.FirmaYetkiliID);

            sellerCommercial.MuhasebeMutabakatTel = yetkili.TelNo;
            sellerCommercial.MuhasebeMutabakatEmail = yetkili.Email;
            sellerCommercial.BayiID = BayiID;
            _unitOfWorkBayiTicari.Repository.Update(sellerCommercial);
            _unitOfWorkBayiTicari.Repository.Save();

            return true;
        }

        [CheckOtherLogin]
        public async Task<IActionResult> CreateSellerCommercialData()
        {
            List<Sehir> listState = (List<Sehir>)await _unitOfWorkSehir.Repository.All();
            List<Ilce> listProvince = (List<Ilce>)await _unitOfWorkIlce.Repository.All();
            List<Yetkililer> SellerCompetentList = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.All();
            ViewBag.SehirID = new SelectList(listState, "ID", "SehirAdi");
            ViewBag.IlceID = new SelectList(listProvince, "ID", "IlceAdi");
            ViewBag.FirmaYetkiliID = new SelectList(SellerCompetentList, "ID", "YetkiliAdi");
            return View();
        }

        [HttpPost]
        public bool CreateSellerCommercialData([FromBody] BayiTicari sellerCommercial)
        {
            int BayiID = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            Bayi getSeller = _unitOfWorkBayi.Repository.FirstOrDefault(x => x.ID == BayiID);
            Yetkililer yetkili = _unitOfWorkYetkililer.Repository.FirstOrDefault(x => x.ID == sellerCommercial.FirmaYetkiliID);

            sellerCommercial.MuhasebeMutabakatEmail = yetkili.Email;
            sellerCommercial.MuhasebeMutabakatTel = yetkili.TelNo;
            sellerCommercial.BayiID = getSeller.ID;
            sellerCommercial.bayiBayiTicari = getSeller;

            _unitOfWorkBayiTicari.Repository.Add(sellerCommercial);
            _unitOfWorkBayiTicari.Repository.Save();
            return true;
        }

        [CheckOtherLogin]
        public async Task<IActionResult> SellerCompetentList()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            ViewBag.ID = id;
            List<Yetkililer> SellerCompetentList = (List<Yetkililer>)await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == id).ToListAsync();
            if (SellerCompetentList.Count == 0)
            {
                return RedirectToAction("SellerCompetentInsert", "Islem", new { ID = id });
            }
            else
            {
                return View(SellerCompetentList);
            }

        }

        [CheckOtherLogin]
        public async Task<IActionResult> SellerCompetentRemove(int id)
        {
            try
            {
                Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.GetById(id);
                if (getSellerCompetent != null)
                {

                    List<BayiTicari> getSellerCommercial = (List<BayiTicari>)await _unitOfWorkBayiTicari.Repository.Where(x => x.FirmaYetkiliID == getSellerCompetent.ID).ToListAsync();

                    foreach (BayiTicari item in getSellerCommercial.ToList())
                    {
                        _unitOfWorkBayiTicari.Repository.Delete(item);
                        _unitOfWorkBayiTicari.Repository.Save();
                    }

                    _unitOfWorkYetkililer.Repository.Delete(getSellerCompetent);
                    _unitOfWorkYetkililer.Repository.Save();

                    TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Yetkili sistemden kaldırıldı", type = ToastrType.success }; ;
                    await SellerCompetentList();
                    return View("SellerCompetentList");
                }
                else
                {
                    TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Yetkili verisi alınamadı", type = ToastrType.warning }; ;
                    await SellerCompetentList();
                    return View("SellerCompetentList");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Önce bu yetkiliye bağlı bilgileri kaldırınız", type = ToastrType.warning }; ;
                await SellerCompetentList();
                return View("SellerCompetentList");
            }

        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> SellerCompetentEdit(int id)
        {
            ViewBag.ID = id;
            if (id > 0)
            {
                List<Bayi> sellerList = (List<Bayi>)await _unitOfWorkBayi.Repository.All();
                ViewBag.BayiID = new SelectList(sellerList, "ID", "BayiAdi");

                Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.GetById(id);
                return View(getSellerCompetent);
            }
            else
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Yetkili verisi alınamadı", type = ToastrType.warning };
                await SellerCompetentList();
                return View("SellerCompetentList");
            }
        }

        [CheckLogin]
        [HttpGet]
        public async Task<IActionResult> SellerCompetentEditAdmin(int id)
        {
            if (id > 0)
            {
                Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.GetById(id);

                List<Bayi> listSeller = (List<Bayi>)await _unitOfWorkBayi.Repository.All();
                ViewBag.BayiID = new SelectList(listSeller, "ID", "BayiAdi", getSellerCompetent.BayiID);

                return View(getSellerCompetent);
            }
            else
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Hata", Message = "Yetkili verisi alınamadı", type = ToastrType.warning };
                await SellerCompetentList();
                return View("SellerCompetentList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SellerCompetentEditAdmin(Yetkililer sellerCompetent, IFormFile file, int BayiID)
        {
            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.GetById(sellerCompetent.ID);

            try
            {
                if (file != null)
                {

                    var uploadurl = "ftp://bayi.kiracelektrik.com.tr//httpdocs/wwwroot/Files/Bayi/";
                    string uploadfilename = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    uploadfilename = uploadfilename + DateTime.Now.ToString("yymmssfff") + extension;
                    getSellerCompetent.YetkiliFoto = uploadfilename;
                    var username = "admin";
                    var password = "8e28l!dX";
                    Stream streamObj = file.OpenReadStream();
                    byte[] buffer = new byte[file.Length];
                    streamObj.Read(buffer, 0, buffer.Length);
                    streamObj.Close();
                    streamObj = null;
                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                    requestObj.Credentials = new NetworkCredential(username, password);
                    Stream requestStream = requestObj.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                    requestObj = null;

                    getSellerCompetent.BayiID = BayiID;
                    getSellerCompetent.Durum = true;
                    getSellerCompetent.GuncellenmeTarih = DateTime.Now;
                    getSellerCompetent.TelNo = sellerCompetent.TelNo;
                    getSellerCompetent.Email = sellerCompetent.Email;
                    getSellerCompetent.YetkiliAdi = sellerCompetent.YetkiliAdi;

                    _unitOfWorkYetkililer.Repository.Update(getSellerCompetent);
                    _unitOfWorkYetkililer.Repository.Save();

                    TempData["mesaj"] = new ToastrMessage
                    {
                        Title = "Başarılı",
                        Message = "Yetkili düzenleme işlemi başarılı",
                        type = ToastrType.success
                    };
                    return View("ListSeller");
                }            
            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi!", type = ToastrType.warning };
                return View("ListSeller");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellerCompetentEdit(Yetkililer sellerCompetent, IFormFile file, int BayiID)
        {
            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.GetById(sellerCompetent.ID);

            try
            {
                if (file != null)
                {

                    var uploadurl = "ftp://bayi.kiracelektrik.com.tr//httpdocs/wwwroot/Files/Bayi/";
                    string uploadfilename = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    uploadfilename = uploadfilename + DateTime.Now.ToString("yymmssfff") + extension;
                    getSellerCompetent.YetkiliFoto = uploadfilename;
                    var username = "admin";
                    var password = "8e28l!dX";
                    Stream streamObj = file.OpenReadStream();
                    byte[] buffer = new byte[file.Length];
                    streamObj.Read(buffer, 0, buffer.Length);
                    streamObj.Close();
                    streamObj = null;
                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                    requestObj.Credentials = new NetworkCredential(username, password);
                    Stream requestStream = requestObj.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                    requestObj = null;


                    getSellerCompetent.BayiID = BayiID;
                    getSellerCompetent.Durum = true;
                    getSellerCompetent.GuncellenmeTarih = DateTime.Now;
                    getSellerCompetent.TelNo = sellerCompetent.TelNo;
                    getSellerCompetent.Email = sellerCompetent.Email;
                    getSellerCompetent.YetkiliAdi = sellerCompetent.YetkiliAdi;

                    _unitOfWorkYetkililer.Repository.Update(getSellerCompetent);
                    _unitOfWorkYetkililer.Repository.Save();

                    TempData["mesaj"] = new ToastrMessage
                    {
                        Title = "Başarılı",
                        Message = "Yetkili düzenleme işlemi başarılı",
                        type = ToastrType.success
                    };
                    await SellerCompetentList();
                    return View("SellerCompetentList");
                }           
            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi!", type = ToastrType.warning };
                await SellerCompetentList();
                return View("SellerCompetentList");
            }
            return View();
        }

        [CheckOtherLogin]
        [HttpGet]
        public IActionResult SellerCompetentInsert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellerCompetentInsert(Yetkililer y, IFormFile file)
        {
            int sellerID = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            try
            {
                Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(sellerID);
                Yetkililer newSellerCompetent = new Yetkililer();

                if (file != null)
                {

                    var uploadurl = "ftp://bayi.kiracelektrik.com.tr//httpdocs/wwwroot/Files/Bayi/";
                    string uploadfilename = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    uploadfilename = uploadfilename + DateTime.Now.ToString("yymmssfff") + extension;
                    newSellerCompetent.YetkiliFoto = uploadfilename;
                    var username = "admin";
                    var password = "8e28l!dX";
                    Stream streamObj = file.OpenReadStream();
                    byte[] buffer = new byte[file.Length];
                    streamObj.Read(buffer, 0, buffer.Length);
                    streamObj.Close();
                    streamObj = null;
                    string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                    var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                    requestObj.Credentials = new NetworkCredential(username, password);
                    Stream requestStream = requestObj.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                    requestObj = null;


                    newSellerCompetent.YetkiliAdi = y.YetkiliAdi;
                    newSellerCompetent.TelNo = y.TelNo;
                    newSellerCompetent.Email = y.Email;
                    newSellerCompetent.BayiID = sellerID;
                    newSellerCompetent.bayiYetkilisi = getSeller;
                    newSellerCompetent.Durum = true;
                    newSellerCompetent.EklenmeTarih = DateTime.Now;
                    newSellerCompetent.GuncellenmeTarih = DateTime.Now;

                    await _unitOfWorkYetkililer.Repository.Add(newSellerCompetent);
                    _unitOfWorkYetkililer.Repository.Save();

                    TempData["mesaj"] = new ToastrMessage { Title = "Başarılı", Message = "Yetkili oluşturma işlemi başarılı", type = ToastrType.success };
                    await SellerCompetentList();
                    return View("SellerCompetentList");
                }
          
            }
            catch (Exception)
            {
                TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.warning };
                return RedirectToAction("SellerDetailsAdmin", "Bayi", sellerID);
            }
            return View();
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> BulletinMemberCreate()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            ViewBag.ID = id;
            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == id).SingleAsync();
            Bulten bulletin = new Bulten
            {
                YetkiliID = getSellerCompetent.ID
            };
            return View(bulletin);
        }

        [HttpPost]
        public async Task<IActionResult> BulletinMemberCreate(Bulten bulletin, int YetkiliID)
        {
            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.GetById(YetkiliID);
            try
            {
                Bulten newBulletin = new Bulten
                {
                    AdinizSoyadiniz = bulletin.AdinizSoyadiniz,
                    Durum = true,
                    TelefonNo = bulletin.TelefonNo,
                    EmailAdresi = bulletin.EmailAdresi,
                    YetkiliID = YetkiliID
                };

                await _unitOfOWorkBulten.Repository.Add(newBulletin);
                _unitOfOWorkBulten.Repository.Save();

                return RedirectToAction("SellerDetailsAdmin", "Bayi", new { ID = getSellerCompetent.BayiID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("SellerDetailsAdmin", "Bayi", new { ID = getSellerCompetent.BayiID });
            }

        }

        [CheckOtherLogin]
        public async Task<IActionResult> BulletinMemberQuit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            ViewBag.ID = id;
            Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
            try
            {
                Bulten getBulletin = await _unitOfOWorkBulten.Repository.GetById(id);
                getBulletin.Durum = false;
                _unitOfOWorkBulten.Repository.Update(getBulletin);
                _unitOfOWorkBulten.Repository.Save();

                return RedirectToAction("SellerDetailsAdmin", new { getSeller.ID });
            }
            catch (Exception)
            {

                return RedirectToAction("SellerDetailsAdmin", new { getSeller.ID });
            }
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> BulletinMemberEdit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.ID = id;
            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == id).SingleAsync();
            Bulten getBulletin = await _unitOfOWorkBulten.Repository.Where(x => x.YetkiliID == getSellerCompetent.ID).SingleAsync();
            return View(getBulletin);
        }

        [HttpPost]
        public async Task<IActionResult> BulletinMemberEdit(Bulten bulletin, int YetkiliID)
        {
            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.GetById(YetkiliID);
            Bulten getBulletin = await _unitOfOWorkBulten.Repository.Where(x => x.YetkiliID == getSellerCompetent.ID).SingleAsync();

            try
            {
                getBulletin.TelefonNo = bulletin.TelefonNo;
                getBulletin.EmailAdresi = bulletin.EmailAdresi;
                getBulletin.AdinizSoyadiniz = bulletin.AdinizSoyadiniz;

                _unitOfOWorkBulten.Repository.Update(getBulletin);
                _unitOfOWorkBulten.Repository.Save();

                //TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.info };
                return RedirectToAction("SellerDetailsAdmin", "Bayi", new { ID = getSellerCompetent.BayiID });
            }
            catch (Exception)
            {
                //TempData["mesaj"] = new ToastrMessage { Title = "Sistem Hatası", Message = "Sistemden kaynaklı bir hata meydana geldi", type = ToastrType.info };
                return RedirectToAction("SellerDetailsAdmin", "Bayi", new { ID = getSellerCompetent.BayiID });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TradeWithCredit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            ViewBag.ID = id;
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").SingleAsync();
            BayiTicari getCommercialSeller = (BayiTicari)await _unitOfWorkBayiTicari.Repository
                                               .Where(x => x.BayiID == getSeller.ID)
                                               .SingleAsync();
            Yetkililer getSellerCompetent = await _unitOfWorkYetkililer.Repository.Where(x => x.BayiID == getSeller.ID).SingleAsync();
            ViewBag.BayiTicari = getCommercialSeller;
            ViewBag.Yetkili = getSellerCompetent;
            return View(getSeller);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder(int? page)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == Id).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = getSeller;

            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.ID > 0).ToListAsync();
            HttpContext.Session.SetObject("kategoriler", categoryList);

            List<Marka> brandList = (List<Marka>)await _unitOfWorkMarka.Repository.Where(x => x.ID > 0).Include("urunler").ToListAsync();
            ViewBag.Markalar = brandList;

            int pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
             .Where(x => x.IndirimOrani == 0 && x.UstUrunID == 0 && x.Durum == true)
             .Include("kategori")
             .Include("kullanici")
             .Include("marka")
             .Include("stokbirimi")
             .Include("begeniler")
             .ToPagedList(pageNumber, 24);

            ViewBag.KampanyaliUrunler = _unitOfWorkUrun.Repository
            .Where(x => x.IndirimOrani > 0 && x.UstUrunID == 0 && x.Durum == true)
            .Include("kategori")
            .Include("kullanici")
            .Include("marka")
            .Include("stokbirimi")
            .Include("begeniler")
            .ToPagedList(pageNumber, 24);

            ViewBag.Begeniler = await _unitOfWorkBegeni.Repository.Where(x => x.BayiID == getSeller.ID).Include("urunBegeni").Include("bayiBegeni").ToListAsync();

            //await CreateModeratorLog("Başarılı", "Listeleme", "ListProductGrid", "Urun");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrderList(int? page)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == Id).Include("musteriGrubu").FirstOrDefaultAsync();
            ViewBag.Bayi = getSeller;

            List<Kategori> categoryList = (List<Kategori>)await _unitOfWorkKategori.Repository.Where(x => x.ID > 0).ToListAsync();
            HttpContext.Session.SetObject("kategoriler", categoryList);

            List<Marka> brandList = (List<Marka>)await _unitOfWorkMarka.Repository.Where(x => x.ID > 0).Include("urunler").ToListAsync();
            ViewBag.Markalar = brandList;

            var pageNumber = page ?? 1;
            ViewBag.Urunler = _unitOfWorkUrun.Repository
                .Where(x => x.ID > 0 && x.UstUrunID == 0)
                .Include("kategori")
                .Include("kullanici")
                .Include("marka")
                .Include("stokbirimi")
                .ToPagedList(pageNumber, 28);

            ViewBag.KampanyaliUrunler = _unitOfWorkUrun.Repository
           .Where(x => x.ID > 0 && x.IndirimOrani > 0 && x.Adet > 0)
           .Include("kategori")
           .Include("kullanici")
           .Include("marka")
           .Include("stokbirimi")
           .ToPagedList(pageNumber, 24);

            //await CreateModeratorLog("Başarılı", "Listeleme", "ListProductGrid", "Urun");
            return View();
        }

        [HttpPost]
        public IActionResult OnlineApplyCredit()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetIlcelerBySehirIDToSeller(int id)
        {
            List<Ilce> ilceler = _unitOfWorkIlce.Repository.Where(x => x.SehirID == id).ToList();
            return Json(ilceler);
        }

        public IActionResult BackToSellerDetail()
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            return RedirectToAction("SellerDetailsAdmin", "Bayi", new { ID = Id });
        }

        #region Kargo Yönetim

        public async Task<IActionResult> ManageToCargos()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
            ViewBag.GelenBayi = getSeller;

            //KargoSure bayiKargo = await _unitOfWorkKargoSure.Repository.Where(x => x.BayiID == gelenBayi.ID && x.AktifMi == true).Include("kargoSureKargo").SingleOrDefaultAsync();

            //if (bayiKargo != null)
            //{
            //    ViewBag.KargoSure = bayiKargo;
            //    TempData["KargoDurum"] = bayiKargo.AktifMi;
            //}

            ViewBag.Kargolar = await _unitOfWorkMusteriGrupKargo.Repository.Where(x => x.MusteriGrupID == getSeller.BayiGrubuID).Include("Kargo").ToListAsync();
            List<MusteriGrupKargo> customerGroupWithCargoList = ViewBag.Kargolar as List<MusteriGrupKargo>;

            return View(customerGroupWithCargoList);
        }

        public async Task<IActionResult> ChooseToCargo(int KargoID)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);
            KargoFirmalar getCargoCompany = await _unitOfWorkKargoFirmalar.Repository.GetById(KargoID);

            try
            {
                getSeller.KargoFirmasiID = KargoID;
                getSeller.kargoBayiFirma = getCargoCompany;

                _unitOfWorkBayi.Repository.Update(getSeller);
                _unitOfWorkBayi.Repository.Save();

                MusteriGrupKargo getCustomerGroupWithCargo = await _unitOfWorkMusteriGrupKargo.Repository.Where(x => x.KargoID == getSeller.KargoFirmasiID && x.MusteriGrupID == getSeller.BayiGrubuID).SingleOrDefaultAsync();
                getCargoCompany.Durum = true;

                _unitOfWorkMusteriGrupKargo.Repository.Update(getCustomerGroupWithCargo);
                _unitOfWorkMusteriGrupKargo.Repository.Save();

                List<MusteriGrupKargo> checkCustomerGroupWithCargoList = await _unitOfWorkMusteriGrupKargo.Repository.Where(x => x.MusteriGrupID == getSeller.BayiGrubuID).ToListAsync();

                foreach (var item in checkCustomerGroupWithCargoList)
                {
                    if (getCustomerGroupWithCargo.KargoID == item.KargoID)
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

                await ManageToCargos();
                return View("ManageToCargos");
            }
            catch (Exception)
            {
                await ManageToCargos();
                return View("ManageToCargos");
            }
        }

        #endregion
    }
}
