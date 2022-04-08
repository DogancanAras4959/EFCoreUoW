using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.CORE.Interface;
using UoW.DATA;
using UoW.DOMAIN.Models;
using UoW.WEB.Controllers;

namespace UoW.WEB.Models
{
    public class SepetViewModel
    {
        private readonly IUnitOfWork<UrunFiyat> _unitOfWorkUrunFiyat;
        public SepetViewModel(IUnitOfWork<UrunFiyat> unitOfWorkUrunFiyat)
        {
            this.Urunler = new Dictionary<int, int>();
            this._unitOfWorkUrunFiyat = unitOfWorkUrunFiyat;
        }

        public decimal Indirim { get; set; }
        public decimal AraToplam { get; set; }
        public decimal ToplamTutar { get; set; }
        public decimal KargoBedeli { get; set; }
        public decimal fiyatIndirimli { get; set; }
        public List<Bildirim> mesajlarList { get; set; }
        public Dictionary<int, int> Urunler { get; set; }

        public List<Sepet> SepetHesapla(List<Sepet> model, Bayi b)
        {
            if (model != null)
            {
                decimal YeniIndirimFarki = 0;
                decimal ToplamAraToplam = 0;
                foreach (Sepet item in model)
                {
                    if (item.urunSepetteMi == true)
                    {
                        if (item.UrunSepet.IndirimOrani > 0)
                        {
                            ToplamAraToplam += item.UrunSepet.IndirimliFiyat * item.Adet;
                        }

                        else
                        {
                            ToplamAraToplam += item.UrunSepet.KDVFiyat * item.Adet;
                        }

                        List<Bildirim> mesajlar = new List<Bildirim>();
                        List<UrunFiyat> urunFiyatlar = _unitOfWorkUrunFiyat.Repository.Where(x => x.UrunID == item.UrunSepet.ID).Include("urun").ToList();
                        int? toplam = 0;
                        foreach (var fiyatlar in urunFiyatlar)
                        {
                            Bildirim bild = new Bildirim();
                            toplam += item.Adet;
                            if ((fiyatlar.AdetBaslangic - toplam) == 2)
                            {
                                bild.MesajAdi = fiyatlar.urun.UrunAdi + " adlı üründen " + 2 + " tane daha ürün alarak " + fiyatlar.UrunFiyati + "₺ kadar fiyata ürünü satın alabilirsiniz";
                                mesajlar.Add(bild);
                                mesajlarList = mesajlar;
                            }
                            else if ((fiyatlar.AdetBaslangic - toplam) == 1)
                            {
                                bild.MesajAdi = fiyatlar.urun.UrunAdi + " adlı üründen " + 1 + " tane daha ürün alarak " + fiyatlar.UrunFiyati + "₺ kadar fiyata ürünü satın alabilirsiniz! Bu fırsatı kaçırmayın";
                                mesajlar.Add(bild);
                                mesajlarList = mesajlar;
                            }
                            else if (toplam >= fiyatlar.AdetBaslangic && toplam < fiyatlar.AdetBitis)
                            {
                                YeniIndirimFarki = item.UrunSepet.KDVFiyat - fiyatlar.UrunFiyati;
                                bild.MesajAdi = fiyatlar.urun.UrunAdi + " adlı ürün'ün yeni satın alma fiyatı " + fiyatlar.UrunFiyati + "₺'dir ";
                                mesajlar.Add(bild);
                                mesajlarList = mesajlar;
                                item.BirimFiyat = fiyatlar.UrunFiyati;
                            }

                        }
                    }
                }

                decimal IndirimliAraToplam = model.Select(x => x.UrunSepet.KDVFiyat * x.Adet).Sum();

                Indirim = ToplamAraToplam - IndirimliAraToplam + YeniIndirimFarki;
                AraToplam = ToplamAraToplam;
                ToplamTutar = Convert.ToDecimal(AraToplam + b.kargoBayiFirma.KargoUcret);
                KargoBedeli = Convert.ToDecimal(b.kargoBayiFirma.KargoUcret);

                return model;
            }
            else
            {
                return null;
            }
        }

        public List<Sepet> BuyProductinanSepet(List<Sepet> model, Bayi b)
        {
            if (model == null)
            {
                model = new List<Sepet>();
            }

            decimal ToplamAraToplam = 0;
            foreach (Sepet item in model)
            {
                if (item.UrunSepet.IndirimOrani > 0)
                {
                    ToplamAraToplam += item.UrunSepet.IndirimliFiyat * item.Adet;

                }
                else
                {
                    ToplamAraToplam += item.UrunSepet.KDVFiyat * item.Adet;
                }
            }

            decimal IndirimliAraToplam = model.Select(x => x.BirimFiyat * x.Adet).Sum();

            Indirim = ToplamAraToplam - IndirimliAraToplam;
            AraToplam = ToplamAraToplam;
            ToplamTutar = Convert.ToDecimal(IndirimliAraToplam + b.kargoBayiFirma.KargoUcret);
            KargoBedeli = Convert.ToDecimal(b.kargoBayiFirma.KargoUcret);

            return model;
        }
    }
    public class SepetFiyatlari
    {
        public decimal IndirimSepet { get; set; }
        public decimal AraToplamSepet { get; set; }
        public decimal ToplamTutarSepet { get; set; }
        public decimal KargoBedeliSepet { get; set; }
    }

}
