using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UoW.ADMIN.Helpers;
using UoW.ADMIN.Models;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using UoW.SERVICE.Contracts;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{
    public class SiparisController : Controller
    {

        #region Fields
        private readonly IUnitOfWork<Kullanici> _unitOfWorkKullanici;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly IUnitOfWork<Siparis> _unitOfWorkSiparis;
        private readonly IUnitOfWork<Fatura> _unitOfWorkFatura;
        private readonly IUnitOfWork<FaturaTip> _unitOfWorkFaturaTip;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLoglar;
        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkIslemler;
        private readonly IUnitOfWorkLog<Yonetici> _unitOfWorkYoneticiler;
        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWork<BayiAdresler> _unitOfWorkBayiAdresler;
        private readonly IUnitOfWork<MusteriGrubu> _unitOfWorkMusterİGrubu;
        private readonly IUnitOfWork<KargoDetay> _unitOfWorkKargoDetay;
        private readonly IUnitOfWork<Kargo> _unitOfWorkKargo;
        private readonly IUnitOfWork<Cuzdan> _unitOfWorkCuzdan;
        private readonly IUnitOfWork<CuzdanIslemler> _unitOfWorkCuzdanIslemler;
        private readonly IUnitOfWork<UrunStok> _unitOfWorkUrunStok;
        private readonly IUnitOfWork<UrunFiyat> _unitOfWorkUrunFiyat;
        private readonly IUnitOfWork<MusteriGrupKargo> _unitOfWorkMusteriGrubuKargo;
        private readonly IUnitOfWork<OzelTeklif> _unitOfWorkOzelTeklif;
        private readonly ICargoService _cargoService;
        private decimal toplamTutar;
        private readonly int? page;
        public SiparisController(IUnitOfWork<Sepet> unitOfWorkSepet,
            IUnitOfWork<Siparis> unitOfWorkSiparis,
            IUnitOfWork<Fatura> unitOfWorkFatura,
            IUnitOfWork<FaturaTip> unitOfWorkFaturaTip,
            IUnitOfWork<Kullanici> unitOfWorkKullanici,
            IUnitOfWorkLog<Loglar> unitOfWorkLoglar,
            IUnitOfWorkLog<Durumlar> unitOfWorkDurumlar,
            IUnitOfWorkLog<Islemler> unitOfWorkIslemler,
            IUnitOfWorkLog<Yonetici> unitOfWorkYoneticiler,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<BayiTicari> unitOfWorkBayiTicari,
            IUnitOfWork<BayiAdresler> unitOfWorkBayiAdresler,
            IUnitOfWork<MusteriGrubu> unitOfWorkMusteriGrubu,
            IUnitOfWork<KargoDetay> unitOfWorkKargoDetay,
            IUnitOfWork<Kargo> unitOfWorkKargo,
            IUnitOfWork<Cuzdan> unitOfWorkCuzdan,
            IUnitOfWork<CuzdanIslemler> unitOfOWorkCuzdanIslemler,
            IUnitOfWork<UrunStok> unitOfWorkUrunStok,
            IUnitOfWork<UrunFiyat> unitOfWorkUrunFiyat,
            IUnitOfWork<MusteriGrupKargo> unitOfWorkMusterİGrubuKargo,
            IUnitOfWork<OzelTeklif> unitOfWorkOzelTeklif,
            ICargoService cargoService)
        {
            this._unitOfWorkFatura = unitOfWorkFatura;
            this._unitOfWorkFaturaTip = unitOfWorkFaturaTip;
            this._unitOfWorkSepet = unitOfWorkSepet;
            this._unitOfWorkSiparis = unitOfWorkSiparis;
            this._unitOfWorkKullanici = unitOfWorkKullanici;
            this._unitOfWorkLoglar = unitOfWorkLoglar;
            this._unitOfWorkIslemler = unitOfWorkIslemler;
            this._unitOfWorkDurumlar = unitOfWorkDurumlar;
            this._unitOfWorkYoneticiler = unitOfWorkYoneticiler;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkBayi = unitOfWorkBayi;
            this._unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            this._unitOfWorkBayiAdresler = unitOfWorkBayiAdresler;
            this._unitOfWorkMusterİGrubu = unitOfWorkMusteriGrubu;
            this._unitOfWorkKargoDetay = unitOfWorkKargoDetay;
            this._unitOfWorkKargo = unitOfWorkKargo;
            this._unitOfWorkCuzdan = unitOfWorkCuzdan;
            this._unitOfWorkCuzdanIslemler = unitOfOWorkCuzdanIslemler;
            this._unitOfWorkUrunStok = unitOfWorkUrunStok;
            this._unitOfWorkUrunFiyat = unitOfWorkUrunFiyat;
            this._unitOfWorkMusteriGrubuKargo = unitOfWorkMusterİGrubuKargo;
            this._unitOfWorkOzelTeklif = unitOfWorkOzelTeklif;
            _cargoService = cargoService;
        }
        #endregion

        [CheckLogin]
        [HttpGet, ActionName("OrderList")]
        public async Task<IActionResult> OrderList(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.Siparisler = _unitOfWorkSiparis.Repository.Where(x => x.ID > 0)
                          .Include("sepetSiparis")
                          .Include("bayiSiparis")
                          .Include("odemeSekliSiparis")
                          .Include(x => x.kargoSiparis)
                          .ToPagedList(pageNumber, 10);

            await CreateModeratorLog("Başarılı", "Listeleme", "OrderList", "Siparis");

            return View();
        }

        [CheckLogin]
        public async Task<IActionResult> OrderDetail(int ID)
        {
            try
            {
                Siparis getOrder = await _unitOfWorkSiparis.Repository.Where(x => x.ID == ID).Include("bayiSiparis").SingleOrDefaultAsync();
                List<Sepet> orderDetailList = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == getOrder.ID)
                .Include("UrunSepet").ToListAsync();
                var kargo = _unitOfWorkKargo.Repository.FirstOrDefault(x => x.SiparisID == getOrder.ID);
                ViewBag.CargoTracking = _cargoService.GetCargoTrackingInformation(kargo.KargoReferansNo);
                ViewBag.SiparisDetaylari = orderDetailList;

                ViewBag.TotalPrice = orderDetailList.Select(x => x.ToplamSatır).Sum();

                return View(getOrder);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> RemoveOrder(int Id)
        {
            try
            {
                Siparis order = await _unitOfWorkSiparis.Repository.GetById(Id);

                #region OzelTeklif

                List<OzelTeklif> specialOfferList = await _unitOfWorkOzelTeklif.Repository.Where(x => x.SiparisID == order.ID).ToListAsync();
                foreach (var item in specialOfferList)
                {
                    _unitOfWorkOzelTeklif.Repository.Delete(item);
                    _unitOfWorkOzelTeklif.Repository.Save();
                }

                #endregion

                #region Fatura

                List<Fatura> billingList = await _unitOfWorkFatura.Repository.Where(x => x.SiparisID == order.ID).ToListAsync();
                foreach (var item2 in billingList)
                {
                    _unitOfWorkFatura.Repository.Delete(item2);
                    _unitOfWorkFatura.Repository.Save();
                }

                #endregion

                #region Sepet

                List<Sepet> basketList = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == order.ID).ToListAsync();
                foreach (var item3 in basketList)
                {
                    _unitOfWorkSepet.Repository.Delete(item3);
                    _unitOfWorkSepet.Repository.Save();
                }

                #endregion

                #region Kargo

                List<Kargo> cargoList = await _unitOfWorkKargo.Repository.Where(x => x.SiparisID == order.ID).ToListAsync();
                foreach (var item4 in cargoList)
                {
                    _unitOfWorkKargo.Repository.Delete(item4);
                    _unitOfWorkKargo.Repository.Save();
                }

                #endregion

                _unitOfWorkSiparis.Repository.Delete(order);
                _unitOfWorkSiparis.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Sipariş kaldırma başarıyla gerçekleşti','Sipariş Yönetimi')";
                await OrderList(page);
                return View("OrderList");
            }
            catch (Exception ex)
            {

                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Sipariş Yönetimi')";
                await OrderList(page);
                return View("OrderList");
            }
        }

        [HttpGet]
        [CheckOtherLogin]
        public IActionResult InsertBasketAdmin(int id, bool remove = false)
        {
            List<BasketsAdmin> basketList = null;
            if (HttpContext.Session.GetString("sepet") == null)
            {
                basketList = new List<BasketsAdmin>();
            }
            else
            {
                basketList = HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");
            }
            if (basketList.Any(x => x.urun.ID == id))
            {
                var pro = basketList.FirstOrDefault(x => x.urun.ID == id);
                if (remove && pro.adet > 0)
                {
                    pro.adet -= 1;
                }
                else
                {
                    //if (pro.urun.Adet > pro.adet && pro.urun.MaxAdet > pro.adet && pro.urun.MinAdet < pro.adet)
                    //{
                    //    pro.adet += 1;
                    //}
                    if (pro.urun.Adet > pro.adet)
                    {
                        pro.adet += 1;
                    }
                    else
                    {
                        ViewBag.Error = "Stok sınırını aşamazsınız";
                        //Daha sonra toastr yazılacak
                    }
                }
            }
            else
            {
                Urun pro = (Urun)_unitOfWorkUrun.Repository.Where(x => x.ID > 0).FirstOrDefault(x => x.ID == id);

                if (pro != null && pro.Durum == true)
                {
                    basketList.Add(new BasketsAdmin()
                    {
                        adet = 1,
                        urun = pro
                    });
                }
                else if (pro != null && pro.Durum == false)
                {
                    ViewBag.Error = "Bu ürünün satışı durduruldu";
                    //Daha sonra toastr yazılacak
                }
            }
            basketList.RemoveAll(x => x.adet < 1);
            HttpContext.Session.SetObject("sepet", basketList);
            return RedirectToAction("BasketAdmin", "Siparis");
        }

        [HttpPost]
        public JsonResult SepetListEkleHomeProductDetailAdmin(int id, int Adet, bool remove = false)
        {

            List<BasketsAdmin> sepet = null;
            if (HttpContext.Session.GetString("sepet") == null)
            {
                sepet = new List<BasketsAdmin>();
            }
            else
            {
                sepet = HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");
            }
            if (sepet.Any(x => x.urun.ID == id))
            {
                var pro = sepet.FirstOrDefault(x => x.urun.ID == id);
                if (remove && pro.adet > 0)
                {
                    pro.adet -= 1;
                }
                else
                {
                    if (pro.urun.Adet > pro.adet)
                    {
                        pro.adet += Adet;

                    }
                    else
                    {
                        ViewBag.Error = "Stok sınırını aşamazsınız";
                    }
                }
            }
            else
            {
                Urun pro = (Urun)_unitOfWorkUrun.Repository.Where(x => x.ID > 0).FirstOrDefault(x => x.ID == id);

                if (pro != null && pro.Durum == true && pro.Adet > Adet)
                {
                    if (pro.MaxAdet > 0 && pro.MinAdet > 0)
                    {
                        if (Adet > pro.Adet && Adet < pro.MaxAdet)
                        {
                            sepet.Add(new BasketsAdmin()
                            {
                                adet = Adet,
                                urun = pro
                            });
                            TempData["Id"] = pro.ID;
                        }
                        else
                        {
                            ViewBag.Error = "Lütfen maximum ve minimun adet değerlerini aşmayınız";
                        }
                    }
                    else
                    {
                        sepet.Add(new BasketsAdmin()
                        {
                            adet = Adet,
                            urun = pro
                        });
                        TempData["Id"] = pro.ID;
                    }
                }
                else if (pro != null && pro.Durum == false)
                {
                    ViewBag.Error = "Bu ürünün satışı durduruldu";
                }

            }
            sepet.RemoveAll(x => x.adet < 1);
            HttpContext.Session.SetObject("sepet", sepet);

            return Json(true);

        }

        [HttpPost]
        public IActionResult SepetListEkleHomeProductAdmin(string products)
        {
            List<BasketsAdmin> sepetData = JsonConvert.DeserializeObject<List<BasketsAdmin>>(products);
            foreach (var item in sepetData)
            {
                int CurrentId = Convert.ToInt32(item.id);
                List<BasketsAdmin> sepet = null;
                if (HttpContext.Session.GetString("sepet") == null)
                {
                    sepet = new List<BasketsAdmin>();
                }
                else
                {
                    sepet = HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");
                }
                if (sepet.Any(x => x.urun.ID == CurrentId))
                {
                    var pro = sepet.FirstOrDefault(x => x.urun.ID == CurrentId);
                    if (pro.adet > 0)
                    {
                        pro.adet -= 1;
                    }
                    else
                    {
                        if (pro.urun.Adet > pro.adet)
                        {
                            pro.adet += item.urun.Adet;

                        }
                        else
                        {
                            ViewBag.Error = "Stok sınırını aşamazsınız";
                        }
                    }
                }
                else
                {
                    Urun pro = (Urun)_unitOfWorkUrun.Repository.Where(x => x.ID > 0).FirstOrDefault(x => x.ID == CurrentId);

                    if (pro != null && pro.Durum == true && pro.Adet > item.adet)
                    {
                        if (pro.MaxAdet > 0 && pro.MinAdet > 0)
                        {
                            if (item.adet > pro.Adet && item.adet < pro.MaxAdet)
                            {
                                sepet.Add(new BasketsAdmin()
                                {
                                    adet = item.adet,
                                    urun = pro
                                });
                                TempData["Id"] = pro.ID;
                            }
                            else
                            {
                                ViewBag.Error = "Lütfen maximum ve minimun adet değerlerini aşmayınız";
                            }
                        }
                        else
                        {
                            sepet.Add(new BasketsAdmin()
                            {
                                adet = item.adet,
                                urun = pro
                            });
                            TempData["Id"] = pro.ID;
                        }
                    }
                    else if (pro != null && pro.Durum == false)
                    {
                        ViewBag.Error = "Bu ürünün satışı durduruldu";
                    }

                }
                sepet.RemoveAll(x => x.adet < 1);
                HttpContext.Session.SetObject("sepet", sepet);
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult SepetListEkleHomeSpecialProductAdmin(int id, int Adet, bool remove = false)
        {
            List<BasketsAdmin> sepet = null;
            if (HttpContext.Session.GetString("sepet") == null)
            {
                sepet = new List<BasketsAdmin>();
            }
            else
            {
                sepet = HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");
            }
            if (sepet.Any(x => x.urun.ID == id))
            {
                var pro = sepet.FirstOrDefault(x => x.urun.ID == id);
                if (remove && pro.adet > 0)
                {
                    pro.adet -= 1;
                }
                else
                {
                    if (pro.urun.Adet > pro.adet)
                    {
                        pro.adet += Adet;

                    }
                    else
                    {
                        ViewBag.Error = "Stok sınırını aşamazsınız";
                    }
                }
            }
            else
            {
                Urun pro = (Urun)_unitOfWorkUrun.Repository.Where(x => x.ID > 0).FirstOrDefault(x => x.ID == id);

                if (pro != null && pro.Durum == true && pro.Adet > Adet)
                {
                    if (pro.MaxAdet > 0 && pro.MinAdet > 0)
                    {
                        if (Adet > pro.Adet && Adet < pro.MaxAdet)
                        {
                            sepet.Add(new BasketsAdmin()
                            {
                                adet = Adet,
                                urun = pro
                            });
                            TempData["Id"] = pro.ID;
                        }
                        else
                        {
                            ViewBag.Error = "Lütfen maximum ve minimun adet değerlerini aşmayınız";
                        }
                    }
                    else
                    {
                        sepet.Add(new BasketsAdmin()
                        {
                            adet = Adet,
                            urun = pro
                        });
                        TempData["Id"] = pro.ID;
                    }
                }
                else if (pro != null && pro.Durum == false)
                {
                    ViewBag.Error = "Bu ürünün satışı durduruldu";
                }

            }
            sepet.RemoveAll(x => x.adet < 1);
            HttpContext.Session.SetObject("sepet", sepet);

            return Json(true);
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> BasketAdmin()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").Include("kargoBayiFirma").SingleOrDefault();
            int iskonto = Convert.ToInt32(b.musteriGrubu.IskontoOrani);
            List<BasketsAdmin> model = (List<BasketsAdmin>)HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");

            decimal toplamAraToplam = 0;

            if (model == null)
            {
                return RedirectToAction("SellerDetailsAdmin", "Bayi", b.ID);
            }

            foreach (BasketsAdmin item in model)
            {

                if (item.urun.IndirimOrani > 0)
                {
                    toplamAraToplam += item.urun.IndirimliFiyat * item.adet;
                }
                else
                {
                    toplamAraToplam += item.urun.KDVFiyat * item.adet;
                }

                List<UrunFiyat> urunFiyatlar = await _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == item.urun.ID).Include("urun").ToListAsync();

                List<Bildirim> mesajlar = new List<Bildirim>();
                int toplam = 0;
                foreach (var fiyatlar in urunFiyatlar)
                {
                    Bildirim bild = new Bildirim();
                    toplam = +item.adet;
                    if ((fiyatlar.AdetBitis - toplam) == 2)
                    {
                        bild.MesajAdi = fiyatlar.urun.UrunAdi + " adlı üründen " + 2 + " tane daha ürün alarak " + fiyatlar.UrunFiyati + "₺ kadar fiyata ürünü satın alabilirsiniz";
                        mesajlar.Add(bild);
                    }
                    else if ((fiyatlar.AdetBitis - toplam) == 1)
                    {
                        bild.MesajAdi = fiyatlar.urun.UrunAdi + " adlı üründen " + 1 + " tane daha ürün alarak " + fiyatlar.UrunFiyati + "₺ kadar fiyata ürünü satın alabilirsiniz! Bu fırsatı kaçırmayın";
                        mesajlar.Add(bild);
                    }
                    else if (toplam == fiyatlar.AdetBitis || toplam > fiyatlar.AdetBitis)
                    {
                        toplamAraToplam = 0;
                        item.urun.KDVFiyat = fiyatlar.UrunFiyati;
                        toplamAraToplam += item.urun.KDVFiyat * item.adet;
                        bild.MesajAdi = fiyatlar.urun.UrunAdi + " adlı ürün'ün yeni satın alma fiyatı " + item.urun.KDVFiyat + "₺'dir ";
                        mesajlar.Add(bild);
                    }
                    ViewBag.Uyarilar = mesajlar;
                    TempData["Fark"] = toplam;
                }

            }

            ViewBag.TotalPrice = toplamAraToplam;
            decimal araToplam = model.Select(x => x.urun.KDVFiyat * x.adet).Sum();
            TempData["indirim"] = araToplam - ViewBag.TotalPrice;
            TempData["araToplam"] = araToplam;
            decimal indirim = Convert.ToDecimal(TempData["indirim"]);
            TempData["ToplamTutar"] = Convert.ToDecimal(toplamAraToplam + b.kargoBayiFirma.KargoUcret);
            TempData["KargoUcret"] = Convert.ToDecimal(b.kargoBayiFirma.KargoUcret);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveBasketAdmin(int id)
        {
            List<BasketsAdmin> sepet = (List<BasketsAdmin>)HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");
            if (sepet != null)
            {
                sepet.RemoveAll(x => x.urun.ID == id);
                HttpContext.Session.SetObject("sepet", sepet);
            }
            await BasketAdmin();
            return View("BasketAdmin");
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> ShoppingAdmin()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi gelenb = _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").Include("kargoBayiFirma").SingleOrDefault();
            int iskonto = Convert.ToInt32(gelenb.musteriGrubu.IskontoOrani);

            List<BasketsAdmin> model = (List<BasketsAdmin>)HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");

            if (model == null)
            {
                model = new List<BasketsAdmin>();
            }

            decimal toplamAraToplam = 0;

            foreach (BasketsAdmin item in model)
            {
                if (item.urun.IndirimOrani > 0)
                {
                    toplamAraToplam += item.urun.IndirimliFiyat * item.adet;
                }
                else
                {
                    toplamAraToplam += item.urun.KDVFiyat * item.adet;
                }
            }

            ViewBag.TotalPrice = toplamAraToplam;
            decimal araToplam = model.Select(x => x.urun.KDVFiyat * x.adet).Sum();
            TempData["indirim"] = araToplam - ViewBag.TotalPrice;
            TempData["araToplam"] = araToplam;
            decimal indirim = Convert.ToDecimal(TempData["indirim"]);
            TempData["ToplamTutar"] = Convert.ToDecimal(toplamAraToplam + gelenb.kargoBayiFirma.KargoUcret);
            toplamTutar = Convert.ToDecimal(TempData["ToplamTutar"]);
            TempData["KargoUcret"] = Convert.ToDecimal(gelenb.kargoBayiFirma.KargoUcret);


            Bayi b = (Bayi)await _unitOfWorkBayi.Repository.GetById(id);
            BayiTicari bt = (BayiTicari)_unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == b.ID).FirstOrDefault();
            List<BayiAdresler> adresler = _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == id).ToList();
            ViewBag.BayiAdresID = new SelectList(adresler, "ID", "Adres");

            CargoViewModel kgv = new CargoViewModel
            {
                BayiAdi = b.BayiAdi,
                EmailAdresi = bt.MuhasebeMutabakatEmail,
                TelefonNo = bt.Telefon,
                SehirID = bt.SehirID,
                IlceID = bt.IlceID
            };

            if (bt != null)
            {
                ViewBag.Sepet = model;
                return View(kgv);
            }
            else
            {
                return RedirectToAction("TicariSellerInsertAdmin", "Islem", b.ID);
            }
        }

        [HttpPost]
        [CheckOtherLogin]
        public async Task<IActionResult> FinishToShoppingAdmin(CargoViewModel kargoBilgi, int BayiAdresID)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi b = await _unitOfWorkBayi.Repository.Where(x => x.ID == id)
                .Include("odeme")
                .Include("musteriGrubu")
                .Include("kargoBayiFirma").SingleOrDefaultAsync();

            Cuzdan c = await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == b.ID).SingleOrDefaultAsync();
            DateTime simdikiZaman = DateTime.Now;
            DateTime teslim = simdikiZaman.AddDays(3);

            int iskonto = Convert.ToInt32(b.musteriGrubu.IskontoOrani);

            try
            {
                List<BayiAdresler> bayiAdresler = _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == b.ID).ToList();
                List<BasketsAdmin> model = (List<BasketsAdmin>)HttpContext.Session.GetObject<List<BasketsAdmin>>("sepet");

                Random rastgele = new Random();
                int no = rastgele.Next();
                Siparis yeniSiparis = new Siparis
                {
                    BayiID = b.ID,
                    bayiSiparis = b,
                    BayiAdresID = BayiAdresID,
                    bayiAdresleri = bayiAdresler.Where(x => x.ID == BayiAdresID).FirstOrDefault(),
                    HazirlanmaTarih = DateTime.Now,
                    SiparisTarih = DateTime.Now,
                    SiparisNo = Convert.ToString(no),
                    Durum = true,
                    OdemeSekliID = b.OdemeID,
                    odemeSekliSiparis = b.odeme
                };

                await _unitOfWorkSiparis.Repository.Add(yeniSiparis);
                _unitOfWorkSiparis.Repository.Save();

                Siparis gelenSiparis = await _unitOfWorkSiparis.Repository.GetById(yeniSiparis.ID);

                foreach (BasketsAdmin item in model)
                {
                    var siparisDetay = new Sepet
                    {
                        UrunID = item.urun.ID
                    };

                    if (item.urun.IndirimOrani > 0)
                    {
                        siparisDetay.BirimFiyat = item.urun.IndirimliFiyat;
                        gelenSiparis.Indirim += (item.urun.KDVFiyat - siparisDetay.BirimFiyat) * item.adet;
                    }
                    else
                    {
                        siparisDetay.BirimFiyat = item.urun.KDVFiyat;
                        gelenSiparis.Indirim += 0;
                    }

                    siparisDetay.Adet = item.adet;
                    siparisDetay.ToplamSatır = Convert.ToDecimal(siparisDetay.BirimFiyat * siparisDetay.Adet);
                    siparisDetay.SiparisID = yeniSiparis.ID;
                    siparisDetay.siparisSepet = yeniSiparis;

                    gelenSiparis.ToplamFiyat += siparisDetay.ToplamSatır;

                    await _unitOfWorkSepet.Repository.Add(siparisDetay);
                    _unitOfWorkSepet.Repository.Save();

                    await DropStockAdmin(siparisDetay.UrunID, siparisDetay.Adet);
                    await InsertOrEditStockAdmin(siparisDetay.UrunID, b.ID, siparisDetay.Adet);

                }

                gelenSiparis.ToplamFiyat += b.kargoBayiFirma.KargoUcret;
                gelenSiparis.KargoBedeli = b.kargoBayiFirma.KargoUcret;
                _unitOfWorkSiparis.Repository.Update(gelenSiparis);
                _unitOfWorkSiparis.Repository.Save();

                KargoDetay kargoDetay = new KargoDetay
                {
                    EklenmEtarih = DateTime.Now,
                    GuncellenmeTarih = DateTime.Now,
                    HazirlanmaTarih = DateTime.Now,
                    TeslimEdildiMi = false,
                    TeslimTarih = teslim
                };

                await _unitOfWorkKargoDetay.Repository.Add(kargoDetay);
                _unitOfWorkKargoDetay.Repository.Save();

                Kargo kargo = new Kargo
                {
                    BayiAdresID = BayiAdresID,
                    bayiAdresler = bayiAdresler.Where(x => x.ID == BayiAdresID).FirstOrDefault(),
                    BayiID = b.ID,
                    bayi = b,
                    SatinalmaNotu = kargoBilgi.SatinAlmaNotu,
                    SiparisID = yeniSiparis.ID,
                    siparis = yeniSiparis,
                    KargoDetayID = kargoDetay.ID,
                    kargoDetayi = kargoDetay,
                    KargoFirmaID = b.KargoFirmasiID,
                    kargoFirmaDetay = b.kargoBayiFirma
                };

                await _unitOfWorkKargo.Repository.Add(kargo);
                _unitOfWorkKargo.Repository.Save();

                CuzdanIslemler ci = new CuzdanIslemler
                {
                    CuzdanID = c.ID,
                    IslemNo = Convert.ToString(no),
                    IslemTarihi = DateTime.Now,
                    IslemTutari = gelenSiparis.ToplamFiyat,
                    cuzdanHesabi = c
                };

                await _unitOfWorkCuzdanIslemler.Repository.Add(ci);
                _unitOfWorkCuzdanIslemler.Repository.Save();

                if (b.RiskLimiti != 0)
                {
                    if (b.RiskLimiti > gelenSiparis.ToplamFiyat || b.RiskLimiti < gelenSiparis.ToplamFiyat)
                    {
                        b.RiskLimiti -= gelenSiparis.ToplamFiyat;
                        if (b.RiskLimiti < 0)
                        {
                            if (c.Bakiye > 0)
                            {
                                if (c.Bakiye > b.RiskLimiti)
                                {
                                    c.Bakiye += b.RiskLimiti;
                                    _unitOfWorkCuzdan.Repository.Update(c);
                                    _unitOfWorkCuzdan.Repository.Save();

                                    b.RiskLimiti = 0;
                                    _unitOfWorkBayi.Repository.Update(b);
                                    _unitOfWorkBayi.Repository.Save();
                                }

                            }
                        }
                        else
                        {
                            _unitOfWorkBayi.Repository.Update(b);
                            _unitOfWorkBayi.Repository.Save();
                        }
                    }
                }
                else
                {
                    if (c.Bakiye > 0)
                    {
                        if (c.Bakiye > gelenSiparis.ToplamFiyat)
                        {
                            c.Bakiye -= gelenSiparis.ToplamFiyat;
                            _unitOfWorkCuzdan.Repository.Update(c);
                            _unitOfWorkCuzdan.Repository.Save();
                        }
                    }
                }

                model.Clear();
                HttpContext.Session.SetObject("sepet", model);

                return RedirectToAction("ResultShoppingAdmin", "Siparis");
            }
            catch (Exception)
            {
                return RedirectToAction("SellerDetailsAdmin", "Bayi", b.ID);
            }

        }

        private async Task<int> InsertOrEditStockAdmin(int UrunID, int BayiID, int adet)
        {
            Bayi b = await _unitOfWorkBayi.Repository.GetById(BayiID);
            if (b != null)
            {
                UrunStok ur = _unitOfWorkUrunStok.Repository.Where(x => x.UrunID == UrunID).SingleOrDefault();
                if (ur != null)
                {
                    ur.Adet += adet;
                    _unitOfWorkUrunStok.Repository.Update(ur);
                    _unitOfWorkUrunStok.Repository.Save();
                }
                else
                {
                    UrunStok us = new UrunStok
                    {
                        UrunID = UrunID,
                        BayiID = BayiID,
                        Adet = adet,
                        bayiStok = b
                    };

                    Urun u = await _unitOfWorkUrun.Repository.GetById(UrunID);
                    us.bayiUrun = u;

                    await _unitOfWorkUrunStok.Repository.Add(us);
                    _unitOfWorkUrunStok.Repository.Save();
                }
            }
            return 1;
        }

        private async Task<int> DropStockAdmin(int UrunID, int adet)
        {
            Urun u = await _unitOfWorkUrun.Repository.GetById(UrunID);
            if (u != null)
            {
                u.Adet -= adet;
                _unitOfWorkUrun.Repository.Update(u);
                _unitOfWorkUrun.Repository.Save();
            }
            return 1;
        }

        public IActionResult ResultShoppingAdmin()
        {
            return View();
        }

        private decimal CalculateDiscountAdmin(decimal iskonto, decimal tutar)
        {
            decimal indirim = (tutar * (iskonto / 100));
            decimal toplam = tutar - indirim;
            return toplam;
        }

        public async Task<CreateLog> CreateModeratorLog(string durum, string islem, string action, string controller)
        {
            string kullaniciAdi = HttpContext.Session.GetString("username");
            CreateLog ct = new CreateLog(_unitOfWorkDurumlar, _unitOfWorkIslemler, _unitOfWorkLoglar, _unitOfWorkYoneticiler);
            if (kullaniciAdi == "")
            {
                await ct.CreateLogs(durum, islem, action, controller, "Sistem");
                return ct;
            }
            await ct.CreateLogs(durum, islem, action, controller, kullaniciAdi);
            return ct;
        }

        public string GetSHA1(string SHA1Data)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string HashedPassword = SHA1Data;
            byte[] hashbytes = Encoding.GetEncoding("ISO-8859-9").GetBytes(HashedPassword);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            return GetHexaDecimal(inputbytes);
        }

        public string GetHexaDecimal(byte[] bytes)
        {
            StringBuilder s = new StringBuilder();
            int length = bytes.Length;
            for (int n = 0; n <= length - 1; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }
    }

    public class Bildirim
    {
        public string MesajAdi { get; set; }
    }

    public class CreditCard
    {
        public string CardNo { get; set; }
        public int CVV { get; set; }
        public int expmonth { get; set; }
        public int expyear { get; set; }
        public string NameSurname { get; set; }
    }

    public class PaymentInfoAkbank
    {
        public string clientId { get; set; }
        public string amount { get; set; }
        public string oid { get; set; }
        public string okUrl { get; set; }
        public string failUrl { get; set; }
        public string rnd { get; set; }
        public string storekey { get; set; }
        public string storetype { get; set; }
        public string hashstr { get; set; }
        public string hash { get; set; }
        public byte[] hashbytes { get; set; }
        public byte[] inputbytes { get; set; }

    }

    public class PaymentInfoGaranti
    {
        public string strMode { get; set; }
        public string strApiVersion { get; set; }
        public string strTerminalProvUserID { get; set; }
        public string strType { get; set; }
        public string strAmount { get; set; }
        public string strCurrencyCode { get; set; }
        public string strInstallmentCount { get; set; }
        public string strTerminalUserID { get; set; }
        public string strOrderID { get; set; }
        public string strCustomeripaddress { get; set; }
        public string strcustomeremailaddress { get; set; }
        public string strTerminalID { get; set; }
        public string _strTerminalID { get; set; }
        public string strTerminalMerchantID { get; set; }
        public string strStoreKey { get; set; }
        public string strProvisionPassword { get; set; }
        public string strSuccessURL { get; set; }
        public string strErrorURL { get; set; }
        public string SecurityData { get; set; }
        public string HashData { get; set; }
    }

}
