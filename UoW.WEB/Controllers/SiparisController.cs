using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UoW.ADMIN.Helpers;
using UoW.ADMIN.Models;
using UoW.CORE.Enums;
using UoW.CORE.Exceptions;
using UoW.CORE.Interface;
using UoW.CORE.Payment;
using UoW.DATA.Toastr;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Contracts;
using UoW.SERVICE.Dtos;
using UoW.WEB.Models;
using X.PagedList;

namespace UoW.WEB.Controllers
{
    public class SiparisController : Controller
    {

        #region Field

        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWork<BayiAdresler> _unitOfWorkBayiAdresler;
        private readonly IUnitOfWork<MusteriGrubu> _unitOfWorkMusterİGrubu;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly IUnitOfWork<Siparis> _unitOfWorkSiparis;
        private readonly IUnitOfWork<KargoDetay> _unitOfWorkKargoDetay;
        private readonly IUnitOfWork<Kargo> _unitOfWorkKargo;
        private readonly IUnitOfWork<KargoFirmalar> _unitOfWorkKargoFirma;
        private readonly IUnitOfWork<Cuzdan> _unitOfWorkCuzdan;
        private readonly IUnitOfWork<CuzdanIslemler> _unitOfWorkCuzdanIslemler;
        private readonly IUnitOfWork<UrunStok> _unitOfWorkUrunStok;
        private readonly IUnitOfWork<UrunFiyat> _unitOfWorkUrunFiyat;
        private readonly IUnitOfWork<MusteriGrupKargo> _unitOfWorkMusteriGrubuKargo;
        private readonly IUnitOfWork<OzelTeklif> _unitOfWorkOzelTeklif;
        private readonly IUnitOfWork<OdemeSekli> _unitOfWorkOdemeSekli;
        private readonly IUnitOfWork<Fatura> _unitOfWorkFatura;
        private readonly IUnitOfWork<FaturaTip> _unitOfWorkFaturaTip;
        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;
        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategoriler;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarka;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly int? page;
        private decimal toplamTutar;
        private readonly ICargoService _cargoService;
        private readonly IMapper _mapper;
        public SiparisController(IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<BayiTicari> unitOfWorkBayiTicari,
            IUnitOfWork<BayiAdresler> unitOfWorkBayiAdresler,
            IUnitOfWork<MusteriGrubu> unitOfWorkMusteriGrubu,
            IUnitOfWork<Sepet> unitOfWorkSepet,
            IUnitOfWork<Siparis> unitOfWorkSiparis,
            IUnitOfWork<KargoDetay> unitOfWorkKargoDetay,
            IUnitOfWork<Kargo> unitOfWorkKargo,
            IUnitOfWork<Cuzdan> unitOfWorkCuzdan,
            IUnitOfWork<CuzdanIslemler> unitOfOWorkCuzdanIslemler,
            IUnitOfWork<UrunStok> unitOfWorkUrunStok,
            IUnitOfWork<UrunFiyat> unitOfWorkUrunFiyat,
            IUnitOfWork<MusteriGrupKargo> unitOfWorkMusterİGrubuKargo,
            IUnitOfWork<OzelTeklif> unitOfWorkOzelTeklif,
            IUnitOfWork<OdemeSekli> unitOfWorkOdemeSekli,
            IUnitOfWork<Fatura> unitOfWorkFatura,
            IUnitOfWork<FaturaTip> unitOfWorkFaturaTip,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Begeni> unitOfWorkBegeni,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<Haber> unitOfWorkHaber,
            IUnitOfWork<SiteInfo> unitOfWorkSiteInfo,
            IUnitOfWork<KargoFirmalar> unitOfWorkKargoFirma,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            ICargoService cargoService,
            IMapper mapper
            )
        {
            this._unitOfWorkOzelTeklif = unitOfWorkOzelTeklif;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkBayi = unitOfWorkBayi;
            this._unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            this._unitOfWorkBayiAdresler = unitOfWorkBayiAdresler;
            this._unitOfWorkMusterİGrubu = unitOfWorkMusteriGrubu;
            this._unitOfWorkSepet = unitOfWorkSepet;
            this._unitOfWorkSiparis = unitOfWorkSiparis;
            this._unitOfWorkKargoDetay = unitOfWorkKargoDetay;
            this._unitOfWorkKargo = unitOfWorkKargo;
            this._unitOfWorkCuzdan = unitOfWorkCuzdan;
            this._unitOfWorkCuzdanIslemler = unitOfOWorkCuzdanIslemler;
            this._unitOfWorkUrunStok = unitOfWorkUrunStok;
            this._unitOfWorkUrunFiyat = unitOfWorkUrunFiyat;
            this._unitOfWorkMusteriGrubuKargo = unitOfWorkMusterİGrubuKargo;
            this._unitOfWorkOdemeSekli = unitOfWorkOdemeSekli;
            this._memoryCache = memoryCache;
            this._unitOfWorkFatura = unitOfWorkFatura;
            this._unitOfWorkFaturaTip = unitOfWorkFaturaTip;
            this._unitOfWorkMarka = unitOfWorkMarka;
            this._unitOfWorkKategoriler = unitOfWorkKategori;
            this._unitOfWorkBegeni = unitOfWorkBegeni;
            this._unitOfWorkStokBildirim = unitOfWorkStokBildirim;
            this._unitOfWorkHaber = unitOfWorkHaber;
            this._unitOfWorkSiteInfo = unitOfWorkSiteInfo;
            this._configuration = configuration;
            this._unitOfWorkKargoFirma = unitOfWorkKargoFirma;
            _cargoService = cargoService;
            _mapper = mapper;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        #region Sepete Ekle

        public IActionResult SepeteEkle(int id, int Adet, decimal fiyat, string products, bool remove = false)
        {
            try
            {
                int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB")); //Sisteme giriş yapan bayi Ids
                Bayi getSeller = _unitOfWorkBayi.Repository.Where(x => x.ID == Id).FirstOrDefault(); //Sisteme giriş yapan bayi
                BayiAdresler getSellerAdress = _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == getSeller.ID).FirstOrDefault();

                CuzdanIslemler getWalletProcess = _unitOfWorkCuzdanIslemler.Repository.Where(x => x.IslemDurum == CORE.Enums.IslemDurum.BorcBildirildi && x.cuzdanHesabi.BayiID == Id).Include("cuzdanHesabi").FirstOrDefault(); //Bu bayinin borcu var mı yok mu?

                if (getSellerAdress == null)
                {
                    throw new BorcVarException("Öncelikle kendinize bir adres tanımlaması gerçekleştirin. Hesap Takibi > Adreslerim bölümünden adres ekleyebilirsiniz");
                }
                else
                {
                    if (getWalletProcess == null) // Borç yok ise
                    {
                        /* var basket = new List<Sepet>();*/ //Yeni bir boş sepet list datası oluşturuyoruz

                        if (products != null) //gelen ürün bilgileri boş değilse
                        {
                            // sepet datasını deserilize ediyoruz
                            List<Sepet> basketData = JsonConvert.DeserializeObject<List<Sepet>>(products);
                            ListOrGridSepetDataInsert(basketData, getSeller, getSellerAdress);
                        }
                        else
                        {
                            if (Adet != 0 && Adet > 0)
                            {
                                DetailProductSepeteData(id, Adet, fiyat, remove, getSeller, getSellerAdress);
                            }
                            else
                            {
                                throw new BorcVarException("Lütfen doğru adet miktarını belirtin!");
                            }

                        }

                        return Json(true);
                    }
                    else
                    {
                        throw new BorcVarException("Borcunuzdan kaynaklı alışveriş kısıtınız bulunmaktadır.");
                    }
                }

            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                HttpContext.Response.WriteAsync(ex.Message).Wait();
                return View();

            }
        }
        public void ListOrGridSepetDataInsert(List<Sepet> basket, Bayi seller, BayiAdresler sellerAddress)
        {

            foreach (var item in basket) //her birini bir item döngüsüne atayıp döngüye alıyoruz
            {
                if (item.Adet != 0 && item.Adet > 0)
                {
                    int CurrentId = Convert.ToInt32(item.ID); // gelen ürün objesinden Id bilgisini kullanmak için alıyoruz
                    Urun getProduct = (Urun)_unitOfWorkUrun.Repository.Where(x => x.ID > 0).FirstOrDefault(x => x.ID == CurrentId);
                    Sepet getBasket = _unitOfWorkSepet.Repository.Where(x => x.UrunID == CurrentId && x.urunSepetteMi == true).FirstOrDefault(); // gelen ürün daha önce sepete atıl mış mı?               

                    if (getBasket != null) // atılmış ise
                    {
                        getBasket.Adet += item.Adet; // ona adet miktarını ekliyoruz
                        _unitOfWorkSepet.Repository.Update(getBasket); // güncelliyoruz
                        _unitOfWorkSepet.Repository.Save(); // kaydediyoruz
                    }
                    else
                    {
                        //gelen ürünü çağırıyoruz
                        if (getProduct != null && getProduct.Durum == true) // ürün var mı yok mu durumu aktif mi?
                        {
                            if (getProduct.MaxAdet > 0 && getProduct.MinAdet > 0) // ürün max ve min adeti 0'dan büyükse
                            {
                                if (item.Adet <= getProduct.MaxAdet && getProduct.MinAdet <= getProduct.Adet) // Gelen ürün adetini min ve max adet miktarıyla karşılaştırıyoruz
                                {
                                    Siparis getOrder = _unitOfWorkSiparis.Repository.Where(x => x.Durum == true).FirstOrDefault();
                                    if (getOrder == null)
                                    {
                                        Random rastgele = new Random();
                                        int no = rastgele.Next();
                                        Siparis newOrder = new Siparis
                                        {
                                            BayiID = seller.ID,
                                            BayiAdresID = sellerAddress.ID,
                                            bayiAdresleri = sellerAddress,
                                            bayiSiparis = seller,
                                            HazirlanmaTarih = DateTime.Now,
                                            SiparisTarih = DateTime.Now,
                                            SiparisNo = Convert.ToString(no),
                                            Durum = true,
                                            SiparisDurumID = 7
                                        };

                                        _unitOfWorkSiparis.Repository.Add(newOrder); // veritabanı kayıt için oluşturuyoruz
                                        _unitOfWorkSiparis.Repository.Save(); //kayıt ediyoruz

                                        YeniSepet(CurrentId, item.Adet, getProduct, item.BirimFiyat, seller, newOrder);
                                    }
                                    else
                                    {
                                        YeniSepet(CurrentId, item.Adet, getProduct, item.BirimFiyat, seller, getOrder);
                                    }
                                }

                                else
                                {
                                    throw new BorcVarException("Maksimum adeti aştınız veya Minimum adetin altına düştünüz.");
                                }
                            }
                            else // MAX VEYA MİNİMUN MİKTAR BELİRTİLMEMİŞSE
                            {
                                Siparis getOrder = _unitOfWorkSiparis.Repository.Where(x => x.Durum == true).FirstOrDefault();
                                if (getOrder == null)
                                {
                                    Random rastgele = new Random();
                                    int no = rastgele.Next();
                                    Siparis yeniSiparis = new Siparis
                                    {
                                        BayiID = seller.ID,
                                        BayiAdresID = sellerAddress.ID,
                                        bayiAdresleri = sellerAddress,
                                        bayiSiparis = seller,
                                        HazirlanmaTarih = DateTime.Now,
                                        SiparisTarih = DateTime.Now,
                                        SiparisNo = Convert.ToString(no),
                                        Durum = true,
                                        SiparisDurumID = 7
                                    };

                                    _unitOfWorkSiparis.Repository.Add(yeniSiparis); // veritabanı kayıt için oluşturuyoruz
                                    _unitOfWorkSiparis.Repository.Save(); //kayıt ediyoruz

                                    YeniSepet(CurrentId, item.Adet, getProduct, item.BirimFiyat, seller, yeniSiparis);
                                }
                                else
                                {
                                    YeniSepet(CurrentId, item.Adet, getProduct, item.BirimFiyat, seller, getOrder);
                                }
                            }
                        }

                        else
                        {
                            throw new BorcVarException("Bu ürünün satışı kısıtlanmıştır. En kısa sürede sistemde düzenleme yapılacaktır!");
                        }
                    }
                }
                else
                {
                    throw new BorcVarException("Lütfen doğru adet miktarını belirtin!");
                }

            }
        }
        private void DetailProductSepeteData(int id, int quantity, decimal price, bool remove, Bayi seller, BayiAdresler sellerAdress)
        {
            int CurrentId = Convert.ToInt32(id); // gelen ürün objesinden Id bilgisini kullanmak için alıyoruz
            Urun getProduct = (Urun)_unitOfWorkUrun.Repository.Where(x => x.ID > 0).FirstOrDefault(x => x.ID == CurrentId);
            Sepet getBasket = _unitOfWorkSepet.Repository.Where(x => x.UrunID == CurrentId && x.urunSepetteMi == true).FirstOrDefault(); // gelen ürün daha önce sepete atıl mış mı?               

            if (getBasket != null) // atılmış ise
            {
                getBasket.Adet += quantity; // ona adet miktarını ekliyoruz
                _unitOfWorkSepet.Repository.Update(getBasket); // güncelliyoruz
                _unitOfWorkSepet.Repository.Save(); // kaydediyoruz
            }
            else
            {
                //gelen ürünü çağırıyoruz
                if (getProduct != null && getProduct.Durum == true) // ürün var mı yok mu durumu aktif mi?
                {
                    if (getProduct.MaxAdet > 0 && getProduct.MinAdet > 0) // ürün max ve min adeti 0'dan büyükse
                    {
                        if (quantity <= getProduct.MaxAdet && getProduct.MinAdet <= getProduct.Adet) // Gelen ürün adetini min ve max adet miktarıyla karşılaştırıyoruz
                        {
                            Siparis getOrder = _unitOfWorkSiparis.Repository.Where(x => x.Durum == true).FirstOrDefault();
                            if (getOrder == null)
                            {
                                Random rastgele = new Random();
                                int no = rastgele.Next();
                                Siparis newOrder = new Siparis
                                {
                                    BayiID = seller.ID,
                                    BayiAdresID = seller.ID,
                                    bayiAdresleri = sellerAdress,
                                    bayiSiparis = seller,
                                    HazirlanmaTarih = DateTime.Now,
                                    SiparisTarih = DateTime.Now,
                                    SiparisNo = Convert.ToString(no),
                                    Durum = true,
                                    SiparisDurumID = 7

                                };

                                _unitOfWorkSiparis.Repository.Add(newOrder); // veritabanı kayıt için oluşturuyoruz
                                _unitOfWorkSiparis.Repository.Save(); //kayıt ediyoruz

                                YeniSepet(CurrentId, quantity, getProduct, price, seller, newOrder);

                            }
                            else
                            {
                                YeniSepet(CurrentId, quantity, getProduct, price, seller, getOrder);
                            }
                        }

                        else
                        {
                            throw new BorcVarException("Maksimum adeti aştınız veya Minimum adetin altına düştünüz.");
                        }
                    }
                    else // MAX VEYA MİNİMUN MİKTAR BELİRTİLMEMİŞSE
                    {
                        Siparis getOrder = _unitOfWorkSiparis.Repository.Where(x => x.Durum == true).FirstOrDefault();
                        if (getOrder == null)
                        {
                            Random rastgele = new Random();
                            int no = rastgele.Next();
                            Siparis newOrder = new Siparis
                            {
                                BayiID = seller.ID,
                                BayiAdresID = seller.ID,
                                bayiAdresleri = sellerAdress,
                                bayiSiparis = seller,
                                HazirlanmaTarih = DateTime.Now,
                                SiparisTarih = DateTime.Now,
                                SiparisNo = Convert.ToString(no),
                                Durum = true,
                                SiparisDurumID = 7
                            };

                            _unitOfWorkSiparis.Repository.Add(newOrder); // veritabanı kayıt için oluşturuyoruz
                            _unitOfWorkSiparis.Repository.Save(); //kayıt ediyoruz

                            YeniSepet(CurrentId, quantity, getProduct, price, seller, newOrder);
                        }
                        else
                        {
                            YeniSepet(CurrentId, quantity, getProduct, price, seller, getOrder);
                        }
                    }
                }

                else
                {
                    throw new BorcVarException("Bu ürünün satışı kısıtlanmıştır. En kısa sürede sistemde düzenleme yapılacaktır!");
                }
            }
        }
        public void YeniSepet(int CurrentId, int quantity, Urun getProduct, decimal price, Bayi seller, Siparis newOrder)
        {
            Sepet newBasket = new Sepet //ürünü sepete ekliyoruz
            {
                UrunID = CurrentId,
                Adet = quantity,
                UrunSepet = getProduct,
                BirimFiyat = price,
                urunSepetteMi = true,
                BayiID = seller.ID,
                BayiSepet = seller,
                SiparisID = newOrder.ID
            };
            _unitOfWorkSepet.Repository.Add(newBasket); // veritabanı kayıt için oluşturuyoruz
            _unitOfWorkSepet.Repository.Save(); //kayıt ediyoruz
        }

        #endregion

        [HttpGet]
        [CheckOtherLogin]
        public IActionResult RemoveFromBasket(int id, bool remove = false)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Sepet basketModel = _unitOfWorkSepet.Repository.Where(x => x.BayiID == Id && x.UrunID == id && x.urunSepetteMi == true).FirstOrDefault();

            if (basketModel != null)
            {
                if (remove == true && basketModel.Adet > 0)
                {
                    basketModel.Adet -= 1;
                    _unitOfWorkSepet.Repository.Update(basketModel);
                    _unitOfWorkSepet.Repository.Save();
                }
                else
                {
                    if (remove == false && basketModel.Adet > 0)
                    {
                        basketModel.Adet += 1;
                        _unitOfWorkSepet.Repository.Update(basketModel);
                        _unitOfWorkSepet.Repository.Save();
                    }
                }
            }

            return RedirectToAction("Basket", "Siparis");
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> Basket()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").Include("kargoBayiFirma").FirstOrDefaultAsync();
            int discountCustomer = Convert.ToInt32(getSeller.musteriGrubu.IskontoOrani);
            ViewBag.Iskonto = discountCustomer;
            SepetViewModel basketModel = new SepetViewModel(_unitOfWorkUrunFiyat);

            var getBasket = await _unitOfWorkSepet.Repository.Where(x => x.BayiID == id && x.urunSepetteMi == true).Include("UrunSepet").ToListAsync();
            ViewBag.Sepets = getBasket;

            getBasket = basketModel.SepetHesapla(getBasket, getSeller);
            ViewBag.Uyarilar = basketModel.mesajlarList;

            SepetFiyatlari pricesBasket = new SepetFiyatlari();
            if (getBasket != null)
            {
                if (getBasket.Count > 0 && getBasket != null)
                {
                    pricesBasket.AraToplamSepet = basketModel.AraToplam;
                    pricesBasket.IndirimSepet = basketModel.Indirim;
                    pricesBasket.KargoBedeliSepet = basketModel.KargoBedeli;
                    pricesBasket.ToplamTutarSepet = basketModel.ToplamTutar;
                }
                else
                {
                    pricesBasket.AraToplamSepet = 0;
                    pricesBasket.IndirimSepet = 0;
                    pricesBasket.KargoBedeliSepet = 0;
                    pricesBasket.ToplamTutarSepet = 0;
                    ViewBag.Alert = "Sepete eklemek istediğiniz ürün veya ürünler stokta tükenmiştir veya ürün sepeti boştur. Satın almak istediğiniz ürünleri ürün listesi sayfasından takip edebilirsiniz!";
                }

                ViewBag.SepetSayi = getBasket;
                ViewBag.OdemeTutarlari = pricesBasket;
                return View(getBasket);
            }
            else
            {
                return RedirectToAction("HomePageList", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> RemoveBasket(int ID)
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            var basketModel = await _unitOfWorkSepet.Repository.Where(x => x.BayiID == Id && x.UrunID == ID && x.urunSepetteMi == true).FirstOrDefaultAsync();

            Siparis getSiparis = await _unitOfWorkSiparis.Repository.Where(x => x.ID == basketModel.SiparisID).FirstOrDefaultAsync();
            getSiparis.ToplamFiyat -= basketModel.ToplamSatır;

            if (basketModel != null)
            {
                _unitOfWorkSiparis.Repository.Update(getSiparis);
                _unitOfWorkSiparis.Repository.Save();

                _unitOfWorkSepet.Repository.Delete(basketModel);
                _unitOfWorkSepet.Repository.Save();
            }

            await Basket();
            return View("Basket");
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> BuyProduct()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            var getBasket = await _unitOfWorkSepet.Repository.Where(x => x.BayiID == id && x.urunSepetteMi == true).Include("UrunSepet").ToListAsync();
            var getOrder = _unitOfWorkSiparis.Repository.Where(x => x.Durum == true).FirstOrDefault();
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").Include("kargoBayiFirma").FirstOrDefaultAsync();
            int discount = Convert.ToInt32(getSeller.musteriGrubu.IskontoOrani);

            List<BayiAdresler> sellerAdressList = await _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == getSeller.ID).ToListAsync();

            if (sellerAdressList.Count == 0)
            {
                return RedirectToAction("InsertSellerAdress", "Bayi");
            }

            SepetViewModel basketModel = new SepetViewModel(_unitOfWorkUrunFiyat);

            getBasket = basketModel.BuyProductinanSepet(getBasket, getSeller);
            SepetFiyatlari pricesBasket = new SepetFiyatlari
            {
                AraToplamSepet = basketModel.AraToplam,
                IndirimSepet = basketModel.Indirim,
                KargoBedeliSepet = basketModel.KargoBedeli,
                ToplamTutarSepet = basketModel.ToplamTutar
            };
            ViewBag.OdemeTutarlari = pricesBasket;

            BayiTicari getSellerCommercial = (BayiTicari)_unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == getSeller.ID).FirstOrDefault();

            if (getSellerCommercial != null)
            {
                List<BayiAdresler> sellerAdressListGet = _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == id).ToList();
                List<OdemeSekli> payTypeList = await _unitOfWorkOdemeSekli.Repository.Where(x => x.ID > 0).ToListAsync();
                ViewBag.BayiAdresID = new SelectList(sellerAdressListGet, "ID", "Adres");
                ViewBag.OdemeSekliID = new SelectList(payTypeList, "ID", "Odeme");
                ViewBag.TeslimatAdresID = new SelectList(sellerAdressListGet, "ID", "Adres");
                var kargoFirmalari = _unitOfWorkKargoFirma.Repository.All().Result.ToList();
                ViewBag.KargoFirmalari = new SelectList(kargoFirmalari,"ID", "KargoAdi");
                CargoViewModels cargoViewModel = new CargoViewModels
                {
                    BayiAdi = getSeller.BayiAdi,
                    EmailAdresi = getSellerCommercial.MuhasebeMutabakatEmail,
                    TelefonNo = getSellerCommercial.Telefon,
                    SehirID = getSellerCommercial.SehirID,
                    IlceID = getSellerCommercial.IlceID,
                    SeciliKargoFirmaId = getSeller.KargoFirmasiID
                };

                #region Akbank

                PaymentInfoAkbank pAkbank = new PaymentInfoAkbank();
                pAkbank.amount = pricesBasket.ToplamTutarSepet.ToString();
                pAkbank.okUrl = "https://localhost:44352/Siparis/ResultOrder";
                pAkbank.failUrl = "https://localhost:44352/Siparis/ResultOrder";
                //pAkbank.okUrl = "bayi.kiracelektrik.com.tr/Siparis/ResultOrder";
                //pAkbank.failUrl = "bayi.kiracelektrik.com.tr/Siparis/ResultOrder";
                pAkbank.clientId = "101926619";
                pAkbank.oid = getOrder.SiparisNo;
                pAkbank.rnd = DateTime.Now.ToString();
                pAkbank.storetype = "3d";
                pAkbank.hashstr = pAkbank.clientId + pAkbank.oid + pAkbank.amount + pAkbank.okUrl + pAkbank.failUrl + pAkbank.rnd + "Kir@c!E2019";
                System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                pAkbank.hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(pAkbank.hashstr);
                pAkbank.inputbytes = sha.ComputeHash(pAkbank.hashbytes);

                pAkbank.hash = Convert.ToBase64String(pAkbank.inputbytes);   //Günvelik amaçlı oluşturulan hash

                #endregion

                #region Garanti

                PaymentInfoGaranti pGaranti = new PaymentInfoGaranti();
                pGaranti.strType = "sales";
                pGaranti.strAmount = pricesBasket.ToplamTutarSepet.ToString();
                pGaranti.strInstallmentCount = "";
                pGaranti.strOrderID = getOrder.SiparisNo;
                pGaranti.strCustomeripaddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                pGaranti.strTerminalID = "10189179";
                pGaranti._strTerminalID = "0" + pGaranti.strTerminalID;             
                pGaranti.strStoreKey = "12345678";
                pGaranti.strProvisionPassword = "123qweASD/";
                pGaranti.strErrorURL = "https://bayi.kiracelektrik.com.tr/Siparis/ResultOrder";
                pGaranti.strSuccessURL = "https://bayi.kiracelektrik.com.tr/Siparis/ResultOrder";
                pGaranti.SecurityData = GetSHA1(pGaranti.strProvisionPassword + pGaranti._strTerminalID).ToUpper();
                pGaranti.HashData = GetSHA1(pGaranti.strTerminalID + pGaranti.strOrderID + pGaranti.strAmount + pGaranti.strSuccessURL + pGaranti.strErrorURL + pGaranti.strType + pGaranti.strInstallmentCount + pGaranti.strStoreKey + pGaranti.SecurityData).ToUpper();

                #endregion

                ViewBag.Sepet = getBasket;
                return View(Tuple.Create<CargoViewModels, CreditCard, PaymentInfoAkbank, PaymentInfoGaranti>(cargoViewModel, new CreditCard(), pAkbank, pGaranti));
            }
            else
            {
                return RedirectToAction("InsertCommercialSellerDataWeb", "Islem", getSeller.ID);
            }
        }

        #region Alışverişi Tamamla

        [HttpPost]
        [CheckOtherLogin]
        public async Task<IActionResult> FinishShoppingToSellerWeb([Bind(Prefix = "Item1")] CargoViewModels kargoBilgi, int BayiAdresID, int TeslimatAdresID, /*int OdemeSekliID,*/ [Bind(Prefix = "Item2")] CreditCard getCardInfo)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            var basketModel = await _unitOfWorkSepet.Repository.Where(x => x.BayiID == id && x.urunSepetteMi == true).Include("UrunSepet").ToListAsync();
            Bayi getSeller = await _unitOfWorkBayi.Repository.Where(x => x.ID == id)
                .Include("odeme")
                .Include("musteriGrubu")
                .Include("kargoBayiFirma").FirstOrDefaultAsync();

            BayiTicari getSellerCommercial = await _unitOfWorkBayiTicari.Repository.Where(x => x.BayiID == getSeller.ID).FirstOrDefaultAsync();

            Cuzdan getWallet = await _unitOfWorkCuzdan.Repository.Where(x => x.BayiID == getSeller.ID).FirstOrDefaultAsync();
            DateTime nowTime = DateTime.Now;
            DateTime deliverTime = nowTime.AddDays(10);
            //OdemeSekli getPayType = await _unitOfWorkOdemeSekli.Repository.Where(x => getSeller.ID == OdemeSekliID).SingleOrDefaultAsync();
            int discount = Convert.ToInt32(getSeller.musteriGrubu.IskontoOrani);

            List<BayiAdresler> sellerAdressList = _unitOfWorkBayiAdresler.Repository.Where(x => x.BayiID == getSeller.ID).ToList();
            BayiAdresler getDeliverAdress = _unitOfWorkBayiAdresler.Repository.Where(x => x.ID == TeslimatAdresID).FirstOrDefault();

            var getOrder = _unitOfWorkSiparis.Repository.Where(x => x.Durum == true).FirstOrDefault();

            if (getDeliverAdress == null)
            {
                return RedirectToAction("ResultOrder", "Siparis", new { error = "Lütfen teslimat adresinizi belirtiniz!" });
            }
            else
            {
                getOrder.TeslimatAdres = getDeliverAdress.Adres;
                getOrder.TeslimatAdresID = TeslimatAdresID;
            }

            if (BayiAdresID == 0)
            {
                return RedirectToAction("ResultOrder", "Siparis", new { error = "Lütfen fatura adresinizi belirtiniz!" });

            }
            else
            {
                getOrder.BayiAdresID = BayiAdresID;
                getOrder.bayiAdresleri = sellerAdressList.Where(x => x.ID == BayiAdresID).FirstOrDefault();
            }

            getOrder.HazirlanmaTarih = DateTime.Now;
            getOrder.SiparisTarih = DateTime.Now;
            getOrder.SiparisNotu = kargoBilgi.BuyProductmaNotu;
            getOrder.odemeSekliSiparis = getSeller.odeme;
            getOrder.OdemeSekliID = getSeller.OdemeID;
            getOrder.ToplamFiyat = 0;
            getOrder.Indirim = 0;
            _unitOfWorkSiparis.Repository.Update(getOrder);
            _unitOfWorkSiparis.Repository.Save();

            Siparis getOrderNew = await _unitOfWorkSiparis.Repository.Where(x => x.ID == getOrder.ID).Include("bayiSiparis").FirstOrDefaultAsync();

            foreach (Sepet item in basketModel)
            {
                var getBasketDetail = basketModel.Where(x => x.UrunID == item.UrunID && x.urunSepetteMi == true).FirstOrDefault();

                getBasketDetail.UrunID = item.UrunSepet.ID;
                getBasketDetail.Adet = item.Adet;

                getBasketDetail.ToplamSatır = Convert.ToDecimal(getBasketDetail.BirimFiyat * getBasketDetail.Adet);
                getBasketDetail.SiparisID = getOrder.ID;
                getBasketDetail.siparisSepet = getOrder;

                if (item.UrunSepet.IndirimOrani > 0)
                {
                    getBasketDetail.BirimFiyat = item.BirimFiyat;
                    getOrderNew.Indirim += (item.UrunSepet.KDVFiyat - getBasketDetail.BirimFiyat) * item.Adet;
                }
                else
                {
                    getBasketDetail.BirimFiyat = item.BirimFiyat;
                    getOrderNew.Indirim += 0;
                }

                //getOrderNew.OdemeSekliID = OdemeSekliID;
                //getOrderNew.odemeSekliSiparis = getPayType;
                getOrderNew.ToplamFiyat += (getBasketDetail.ToplamSatır + getSeller.kargoBayiFirma.KargoUcret);
            }

            try
            {
                string refNo = await UnitStockInsertOrUpdate(basketModel, getSeller.ID, getOrderNew.SiparisNo, getOrderNew.ToplamFiyat, getWallet, /*getOrderNew.OdemeSekliID,*/ getCardInfo);

                await UnitStockDown(basketModel);

                basketModel.ForEach(getBasketDetail =>
                {
                    getBasketDetail.urunSepetteMi = false;
                    getBasketDetail.BayiID = getSeller.ID;
                    getBasketDetail.BayiSepet = getSeller;

                    _unitOfWorkSepet.Repository.Update(getBasketDetail);
                });

                _unitOfWorkSepet.Repository.Save();

                getOrderNew.KargoBedeli = getSeller.kargoBayiFirma.KargoUcret;
                getOrderNew.ReferansNo = refNo;
                getOrderNew.bayiSiparis = getSeller;
                getOrderNew.SiparisDurumID = 1;
                getOrderNew.Durum = false;
                _unitOfWorkSiparis.Repository.Update(getOrderNew);
                _unitOfWorkSiparis.Repository.Save();

                KargoDetay cargoDetail = new KargoDetay
                {
                    EklenmEtarih = DateTime.Now,
                    GuncellenmeTarih = DateTime.Now
                };
                cargoDetail.TeslimTarih = deliverTime;
                cargoDetail.HazirlanmaTarih = DateTime.Now;
                cargoDetail.TeslimEdildiMi = false;

                await _unitOfWorkKargoDetay.Repository.Add(cargoDetail);
                _unitOfWorkKargoDetay.Repository.Save();

                Kargo cargo = new Kargo
                {
                    BayiAdresID = BayiAdresID,
                    TeslimatAdresi = getDeliverAdress.Adres,
                    TeslimatAdresID = TeslimatAdresID,
                    bayiAdresler = sellerAdressList.Where(x => x.ID == BayiAdresID).FirstOrDefault(),
                    BayiID = getSeller.ID,
                    bayi = getSeller,
                    SatinalmaNotu = kargoBilgi.BuyProductmaNotu,
                    SiparisID = getOrderNew.ID,
                    siparis = getOrderNew,
                    KargoDetayID = cargoDetail.ID,
                    kargoDetayi = cargoDetail,
                    KargoFirmaID = getSeller.KargoFirmasiID,
                    kargoFirmaDetay = getSeller.kargoBayiFirma
                };
                var cargoDto = _mapper.Map<Kargo, CargoDto>(cargo);
                cargoDto.FaturaNo = getOrderNew.SiparisNo;
                cargoDto.AliciAdi = getSeller.BayiAdi;
                cargoDto.AliciTelefonNumarasi = kargoBilgi.TelefonNo;
                cargoDto.SehirId = getSellerCommercial.SehirID;
                cargoDto.IlceId = getSellerCommercial.IlceID;
                cargo.KargoReferansNo = _cargoService.GenerateCargoKey(cargoDto);
                Random rastgele = new Random();
                int no = rastgele.Next();

                CuzdanIslemler walletProcess = new CuzdanIslemler
                {
                    CuzdanID = getWallet.ID,
                    IslemNo = Convert.ToString(no),
                    IslemTarihi = DateTime.Now,
                    IslemTutari = getOrderNew.ToplamFiyat,
                    cuzdanHesabi = getWallet,
                    IslemDurum = CORE.Enums.IslemDurum.Borclu
                };

                //if (OdemeSekliID == 2)
                //{
                //    walletProcess.OdemeSekliID = 2;
                //    walletProcess.odemeSekliCuzdan = getPayType;
                //}
                //else
                //{
                //    walletProcess.odemeSekliCuzdan = getPayType;
                //    walletProcess.OdemeSekliID = 1;
                //}

                FaturaTip billingType = (FaturaTip)await _unitOfWorkFaturaTip.Repository.GetById(3).ConfigureAwait(false);

                Fatura newBill = new Fatura
                {
                    BayiAdresID = BayiAdresID,
                    bayiFatura = getSeller,
                    BayiID = getSeller.ID,
                    siparisFatura = getOrderNew,
                    SiparisID = getOrderNew.ID,
                    ToplamFiyat = getOrderNew.ToplamFiyat,
                    FaturaNo = getOrderNew.SiparisNo,
                    EklenmeTarih = DateTime.Now,
                    FaturaTipiID = 2,
                    faturaTipi = billingType
                };

                await _unitOfWorkFatura.Repository.Add(newBill);
                _unitOfWorkFatura.Repository.Save();

                await _unitOfWorkCuzdanIslemler.Repository.Add(walletProcess);
                _unitOfWorkCuzdanIslemler.Repository.Save();

                await _unitOfWorkKargo.Repository.Add(cargo);
                _unitOfWorkKargo.Repository.Save();

                string odemeMesaj = "Satın alma bildirimi:" + getOrderNew.SiparisNo + " nolu siparişiniz " + getOrderNew.ToplamFiyat + "₺ tutarındadır. Ödeme işleminiz başarılı bir şekilde gerçekleştirilmiştir";

                SendMailHelper.SendMailToOrderCompletedInfoToSeller(getOrderNew, odemeMesaj, "Kıraç Bayim - Alışveriş Tamamlandı", getSellerCommercial.MuhasebeMutabakatEmail, "Kıraç Bayim - Sipariş Oluşturma Bldirimi", getSeller.BayiAdi);

                return RedirectToAction("ResultOrder", "Siparis", getOrderNew);

            }
            catch (Exception ex)
            {
                return RedirectToAction("ResultOrder", "Siparis", new { error = ex.Message });
            }
        }

        #endregion

        public IActionResult ResultOrder(string error = "", Siparis getOrderNew = null)
        {
            //if (error != "" || getOrderNew != null)
            //{
            //    ViewBag.Error = error;
            //    return View();
            //}
            //else{
                //String[] odemeparametreleri = new String[] { "AuthCode", "Response", "HostRefNum", "ProcReturnCode", "TransId", "ErrMsg" };
                //var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                //ViewBag.Ok = dict["mdStatus"];
                //ViewBag.Message = dict["mdErrorMsg"];
                return View();
            //}   
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> OrderListWeb(int? page)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            var pageNumber = page ?? 1;
            ViewBag.OrderListWeb = await _unitOfWorkSiparis.Repository.Where(x => x.BayiID == id)
                .OrderByDescending(x => x.ID)
                .Include("odemeSekliSiparis")
                .ToPagedListAsync(pageNumber, 30);
            return View();
        }

        [CheckOtherLogin]
        [HttpGet]
        public async Task<IActionResult> OrderListWebContinue(int? page)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            var pageNumber = page ?? 1;
            ViewBag.OrderListWeb = await _unitOfWorkSiparis.Repository.Where(x => x.BayiID == id && x.Durum == true)
                .OrderByDescending(x => x.ID)
                .Include("odemeSekliSiparis")
                .ToPagedListAsync(pageNumber, 30);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailList(int ID)
        {
            Siparis getOrder = await _unitOfWorkSiparis.Repository.Where(x => x.ID == ID).Include("bayiSiparis").FirstOrDefaultAsync();

            List<Sepet> basketList = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == ID)
                .Include("UrunSepet").ToListAsync();

            ViewBag.SiparisDetaylari = basketList;

            ViewBag.TotalPrice = basketList.Select(x => x.ToplamSatır).Sum();

            return PartialView("OrderDetailList", getOrder);
        }

        public async Task<IActionResult> CreateSpecialOfferToSeller()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = await _unitOfWorkBayi.Repository.GetById(id);

            BayiAdresler getSellerCommercial = _unitOfWorkBayiAdresler.Repository.FirstOrDefault(x => x.BayiID == getSeller.ID);
            if (getSellerCommercial == null)
            {
                return RedirectToAction("InsertSellerAdress", "Bayi");
            }

            OdemeSekli getPayType = await _unitOfWorkOdemeSekli.Repository.Where(x => x.ID == 1).FirstOrDefaultAsync();
            Siparis getOrder = await _unitOfWorkSiparis.Repository.Where(x => x.BayiID == id && x.Durum == true).FirstOrDefaultAsync();

            List<Sepet> basketList = await _unitOfWorkSepet.Repository.Where(x => x.BayiID == id && x.SiparisID == getOrder.ID).ToListAsync();
            foreach (var item in basketList)
            {
                item.urunSepetteMi = false;
                _unitOfWorkSepet.Repository.Update(item);
                _unitOfWorkSepet.Repository.Save();
            }

            _unitOfWorkSiparis.Repository.Update(getOrder);
            _unitOfWorkSiparis.Repository.Save();

            #region Eski
            //foreach (Sepet item in sepet)
            //{
            //    var sepetDetay = new Sepet
            //    {
            //        UrunID = item.UrunSepet.ID
            //    };

            //    if (item.UrunSepet.IndirimOrani > 0)
            //    {
            //        sepetDetay.BirimFiyat = item.UrunSepet.IndirimliFiyat;
            //        gelenSiparis.Indirim += (item.UrunSepet.KDVFiyat - sepetDetay.BirimFiyat) * item.Adet;
            //    }
            //    else
            //    {
            //        sepetDetay.BirimFiyat = item.BirimFiyat;
            //        gelenSiparis.Indirim += 0;
            //    }

            //    sepetDetay.Adet = item.Adet;
            //    sepetDetay.ToplamSatır = Convert.ToDecimal(sepetDetay.BirimFiyat * sepetDetay.Adet);
            //    sepetDetay.SiparisID = yeniSiparis.ID;
            //    sepetDetay.siparisSepet = yeniSiparis;
            //    gelenSiparis.ToplamFiyat += sepetDetay.ToplamSatır;

            //    await _unitOfWorkSepet.Repository.Add(sepetDetay);
            //    _unitOfWorkSepet.Repository.Save();
            //}
            #endregion

            OzelTeklif specialOffer = new OzelTeklif
            {
                Durum = false,
                BayiID = getSeller.ID,
                bayiOzelTeklif = getSeller,
                TeklifTarih = DateTime.Now,
                SiparisID = getOrder.ID,
                siparisOzelTeklif = getOrder,
                TeklifDurum = OzelTeklifDurum.TeklifYapildi
            };

            await _unitOfWorkOzelTeklif.Repository.Add(specialOffer);
            _unitOfWorkOzelTeklif.Repository.Save();
            int Id = specialOffer.ID;

            TempData["mesaj"] = "toastr['success']('Özel teklifiniz oluşturuldu. Sizlere dönüş sağlayacağız','Özel Teklif')";

            return RedirectToAction("HomePageList", "Home");
        }

        private decimal CalculateDiscount(decimal discount, decimal total)
        {
            decimal _discount = (total * (discount / 100));
            decimal _total = total - _discount;
            return _total;
        }

        [HttpGet]
        [CheckOtherLogin]
        public async Task<IActionResult> CargoDetail(int ID)
        {
            try
            {
                KargoDetay getCargoDetail = await _unitOfWorkKargoDetay.Repository.Where(x => x.ID == ID).FirstOrDefaultAsync();
                Kargo cargo = await _unitOfWorkKargo.Repository.Where(x => x.ID == getCargoDetail.ID)
                    .Include("bayi")
                    .Include("kargoFirmaDetay")
                    .Include("kargoDetayi")
                    .Include("bayiAdresler")
                    .Include("siparis").FirstOrDefaultAsync();

                List<Sepet> siparisDetay = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == cargo.siparis.ID)
                    .Include("UrunSepet").ToListAsync();

                ViewBag.SiparisDetaylari = siparisDetay;

                ViewBag.TotalPrice = siparisDetay.Select(x => x.ToplamSatır).Sum();

                return View(cargo);
            }
            catch (Exception ex)
            {
                throw;
                //buraya bakılacak
            }
        }
        private async Task<string> UnitStockInsertOrUpdate(List<Sepet> basket, int BayiID, string orderNo, decimal totalPrice, Cuzdan wallet, /*int OdemeSekliID,*/ CreditCard getcard)
        {
            string expyear = getcard.expyear.ToString();
            string expmonth = getcard.expmonth.ToString();
            string cv2 = getcard.CVV.ToString();
            Bayi seller = await _unitOfWorkBayi.Repository.GetById(BayiID);

            if (seller != null)
            {
                MakePayment mPayment = new MakePayment(_configuration);
                //string ok = mPayment.MakePaymentThis(expyear, expmonth, orderNo, getcard.CardNo, cv2, totalPrice);

                foreach (var item in basket)
                {
                    UrunStok getProductStock = _unitOfWorkUrunStok.Repository.Where(x => x.UrunID == item.UrunSepet.ID).FirstOrDefault();
                    if (getProductStock != null)
                    {
                        getProductStock.Adet += item.UrunSepet.Adet;
                        _unitOfWorkUrunStok.Repository.Update(getProductStock);
                        _unitOfWorkUrunStok.Repository.Save();
                    }
                    else
                    {
                        UrunStok newUnitStock = new UrunStok
                        {
                            UrunID = item.UrunSepet.ID,
                            BayiID = BayiID,
                            Adet = item.UrunSepet.Adet,
                            bayiStok = seller,
                            bayiUrun = item.UrunSepet,
                            Durum = true
                        };

                        Urun getProduct = await _unitOfWorkUrun.Repository.GetById(item.UrunSepet.ID);
                        newUnitStock.bayiUrun = getProduct;

                        await _unitOfWorkUrunStok.Repository.Add(newUnitStock);
                        _unitOfWorkUrunStok.Repository.Save();
                    }
                }
                return "";
            }
            else
            {
                throw new Exception("Bayi Yoktur");
            }
        }
        private async Task<int> UnitStockDown(List<Sepet> basket)
        {
            foreach (var item in basket)
            {
                Urun getProduct = await _unitOfWorkUrun.Repository.GetById(item.UrunSepet.ID);
                if (getProduct != null)
                {
                    getProduct.Adet -= item.Adet;
                    _unitOfWorkUrun.Repository.Update(getProduct);
                    _unitOfWorkUrun.Repository.Save();
                }
                else
                {
                    return 0;
                }
            }

            return 1;
        }
        public void SendMailBuyProductProcess(string mesaj, string konu, string mail, string baslik, string bayiAdi)
        {
            try
            {
                string body = "";

                body += "<b>" + baslik + "</b>";
                body += "<br/><br/><b>Bayi: </b>" + bayiAdi;
                body += "<br/><br/><b>Email: </b>" + mail;
                body += "<br/><br/><b>Açıklama: </b>" + mesaj;

                SmtpClient Kaynak = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential("bayi@kiracelektrik.com.tr", "Krc190634"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                MailAddress Gonderen = new MailAddress("bayi@kiracelektrik.com.tr", "Kıraç Bayim");

                MailAddress Giden = new MailAddress(mail, mesaj);
                MailMessage Mesaj = new MailMessage(Gonderen, Giden)
                {
                    From = Gonderen,
                    Subject = konu,
                    Body = body,
                    IsBodyHtml = true
                };
                Kaynak.Send(Mesaj);
            }
            catch (Exception)
            {

            }

        }
        public IActionResult ExportCards()
        {
            using XLWorkbook workbook = new XLWorkbook();
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));
            Bayi getSeller = _unitOfWorkBayi.Repository.Where(x => x.ID == id).Include("musteriGrubu").FirstOrDefault();

            var getOrder = _unitOfWorkSiparis.Repository.Where(x => x.Durum == true).FirstOrDefaultAsync().Result;
            List<Sepet> basketList = _unitOfWorkSepet.Repository.Where(x => x.SiparisID == getOrder.ID).Include("UrunSepet").Include("siparisSepet").ToList();

            string time = DateTime.Now.ToString("yymmssfff");
            string sheetName = $"SEPET_{time}";
            string fileName = $"Basket_{time}.xlsx";

            var worksheet = workbook.Worksheets.Add(sheetName);
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "Id";
            worksheet.Cell(currentRow, 2).Value = "UrunAdi";
            worksheet.Cell(currentRow, 3).Value = "Adet";
            worksheet.Cell(currentRow, 4).Value = "Fiyat";
            worksheet.Cell(currentRow, 5).Value = "Indirim";
            worksheet.Cell(currentRow, 6).Value = "Iskonto";

            foreach (var user in basketList)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = user.ID;
                worksheet.Cell(currentRow, 2).Value = user.UrunSepet.UrunAdi;
                worksheet.Cell(currentRow, 3).Value = user.Adet;
                worksheet.Cell(currentRow, 4).Value = user.BirimFiyat;
                worksheet.Cell(currentRow, 5).Value = user.siparisSepet.Indirim;
                worksheet.Cell(currentRow, 6).Value = getSeller.musteriGrubu.IskontoOrani;

            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);

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