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

namespace UoW.WEB.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        #region Field
    
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategoriler;
        private readonly IUnitOfWork<Marka> _unitOfWorkMarka;
        private readonly IUnitOfWork<Begeni> _unitOfWorkBegeni;
        private readonly IUnitOfWork<StokBildirim> _unitOfWorkStokBildirim;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly int? page;

        public HeaderViewComponent(IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<Kategori> unitOfWorkKategoriler,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IUnitOfWork<Begeni> unitofWorkBegeni,
            IUnitOfWork<StokBildirim> unitOfWorkStokBildirim,
            IUnitOfWork<Sepet> unitOfWorkSepet,
            Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache)
        {   
            this._unitOfWorkKategoriler = unitOfWorkKategoriler;        
            this._unitOfWorkMarka = unitOfWorkMarka;
            this._unitOfWorkBegeni = unitofWorkBegeni;        
            this._unitOfWorkStokBildirim = unitOfWorkStokBildirim;     
            this._unitOfWorkSepet = unitOfWorkSepet;
        }

        #endregion

        public IViewComponentResult Invoke()
        {

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("IdB"));

            List<Begeni> begenis =  _unitOfWorkBegeni.Repository.Where(x => x.BayiID == id).ToListAsync().Result;
            ViewBag.Begeniler = begenis;

            List<Sepet> sepetler =  _unitOfWorkSepet.Repository.Where(x => x.BayiID == id && x.urunSepetteMi == true).Include("UrunSepet").ToListAsync().Result;
            ViewBag.Sepetler = sepetler;

            List<StokBildirim> stokBildirim =  _unitOfWorkStokBildirim.Repository.Where(x => x.BayiId == id && x.Durum == CORE.Enums.StokBildirimDurum.BildirimYapildi).Include("urun").Take(5).OrderByDescending(x => x.BildirimTarihi).ToListAsync().Result;
            ViewBag.StokBildirimler = stokBildirim;

            return View();
        }
    }
}
