using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;

namespace UoW.WEB.Components
{
    public class FooterViewComponent : ViewComponent
    {
        #region Field

        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;

        public FooterViewComponent(IUnitOfWork<SiteInfo> unitOfWorkSiteInfo)
        {
            this._unitOfWorkSiteInfo = unitOfWorkSiteInfo;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            try
            {
                KurGetir();
                ViewBag.Sozlesmeler = _unitOfWorkSiteInfo.Repository.Where(x => x.AktifMi == true).ToListAsync().Result;
                return View();
            }
            catch (Exception)
            {
                KurGetir();
                ViewBag.Sozlesmeler = _unitOfWorkSiteInfo.Repository.Where(x => x.AktifMi == true).ToListAsync().Result;
                return View();
            }
          
        }

        public void KurGetir()
        {
            XmlDocument xml = new XmlDocument(); // yeni bir XML dökümü oluşturuyoruz.
            xml.Load("http://www.tcmb.gov.tr/kurlar/today.xml"); // bağlantı kuruyoruz.
            var Tarih_Date_Nodes = xml.SelectSingleNode("//Tarih_Date"); // Count değerini olmak için ana boğumu seçiyoruz.
            var CurrencyNodes = Tarih_Date_Nodes.SelectNodes("//Currency"); // ana boğum altındaki kur boğumunu seçiyoruz.
            int CurrencyLength = CurrencyNodes.Count; // toplam kur boğumu sayısını elde ediyor ve for döngüsünde kullanıyoruz.

            List<_Doviz> dovizler = new List<_Doviz>(); // Aşağıda oluşturduğum public class ile bir List oluşturuyoruz.
            for (int i = 0; i < CurrencyLength; i++) // for u çalıştırıyoruz.
            {
                var cn = CurrencyNodes[i]; // kur boğumunu alıyoruz.
                // Listeye kur bilgirini ekliyoruz.
                dovizler.Add(new _Doviz
                {
                    Kod = cn.Attributes["Kod"].Value,
                    CrossOrder = cn.Attributes["CrossOrder"].Value,
                    CurrencyCode = cn.Attributes["CurrencyCode"].Value,
                    Unit = cn.ChildNodes[0].InnerXml,
                    Isim = cn.ChildNodes[1].InnerXml,
                    CurrencyName = cn.ChildNodes[2].InnerXml,
                    ForexBuying = cn.ChildNodes[3].InnerXml,
                    ForexSelling = cn.ChildNodes[4].InnerXml,
                    BanknoteBuying = cn.ChildNodes[5].InnerXml,
                    BanknoteSelling = cn.ChildNodes[6].InnerXml,
                    CrossRateOther = cn.ChildNodes[8].InnerXml,
                    CrossRateUSD = cn.ChildNodes[7].InnerXml,
                });
            }

            HttpContext.Session.SetObject("dolar", dovizler.Where(x => x.CurrencyCode == "USD"));
            HttpContext.Session.SetObject("euro", dovizler.Where(x => x.CurrencyCode == "EUR"));
        }
    }

    public class _Doviz
    {
        public string CrossOrder { get; set; }
        public string Kod { get; set; }
        public string CurrencyCode { get; set; }
        public string Unit { get; set; }
        public string Isim { get; set; }
        public string CurrencyName { get; set; }
        public string ForexBuying { get; set; }
        public string ForexSelling { get; set; }
        public string BanknoteBuying { get; set; }
        public string BanknoteSelling { get; set; }
        public string CrossRateUSD { get; set; }
        public string CrossRateOther { get; set; }
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
}
