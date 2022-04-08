using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DATA.Toastr;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{

    public class FaturaController : Controller
    {

        #region Fields 
   
        private readonly IUnitOfWork<Fatura> _unitOfWorkFatura;
        private readonly IUnitOfWork<FaturaTip> _unitOfWorkFaturaTip;
        private readonly IUnitOfWork<Sepet> _unitOfWorkSepet;
        private readonly IUnitOfWork<Siparis> _unitOfWorkSiparis;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<BayiAdresler> _unitOfWorkBayiAdresler;
        private readonly IUnitOfWork<BayiTicari> _unitOfWorkBayiTicari;
        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitofWorkLogYonetici;
        private readonly IUnitOfWork<Kargo> _unitOfWorkKargo;
        private readonly IUnitOfWork<KargoDetay> _unitOfWorkKargoDetay;

        private readonly int? page;
        public FaturaController(IUnitOfWork<Fatura> unitOfWorkFatura, IUnitOfWork<FaturaTip> unitOfWorkFaturaTip, IUnitOfWork<Sepet> unitOfWorkSepet, IUnitOfWork<Siparis> unitOfWorkSiparis, IUnitOfWork<Urun> unitOfWorkUrun, IUnitOfWork<Bayi> unitOfWorkBayi, IUnitOfWork<BayiAdresler> unitOfWorkBayiAdresler, IUnitOfWork<BayiTicari> unitOfWorkBayiTicari, IUnitOfWorkLog<Durumlar> unitOfWorkLogDurumlar, IUnitOfWorkLog<Islemler> unitOfWorkLogIslemler, IUnitOfWorkLog<Loglar> unitOfWorkLogLoglar, IUnitOfWorkLog<Yonetici> unitOfWorkLogYonetici, IUnitOfWork<Kargo> unitOfWorkKargo, IUnitOfWork<KargoDetay> unitOfWorkKargoDetay)
        {
            this._unitOfWorkFatura = unitOfWorkFatura;
            this._unitOfWorkFaturaTip = unitOfWorkFaturaTip;
            this._unitOfWorkSepet = unitOfWorkSepet;
            this._unitOfWorkSiparis = unitOfWorkSiparis;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkBayi = unitOfWorkBayi;
            this._unitOfWorkBayiAdresler = unitOfWorkBayiAdresler;
            this._unitOfWorkBayiTicari = unitOfWorkBayiTicari;
            this._unitOfWorkLogDurumlar = unitOfWorkLogDurumlar;
            this._unitOfWorkLogIslemler = unitOfWorkLogIslemler;
            this._unitOfWorkLogLoglar = unitOfWorkLogLoglar;
            this._unitofWorkLogYonetici = unitOfWorkLogYonetici;
            this._unitOfWorkKargo = unitOfWorkKargo;
            this._unitOfWorkKargoDetay = unitOfWorkKargoDetay;
                 
        }

        #endregion

        [HttpGet]
        [CheckRole]
        [CheckLogin]
        public async Task<IActionResult> Billings()
        {
            int pageNumber = page ?? 1;
            IPagedList<Fatura> Billings = (IPagedList<Fatura>)await _unitOfWorkFatura.Repository.Where(x => x.ID > 0).OrderByDescending(x => x.EklenmeTarih).Include("faturaTipi").Include("bayiFatura").Include("siparisFatura").ToPagedListAsync(pageNumber, 40);
            ViewBag.Faturalar = Billings;
            return View();
        }

        [CheckLogin]
        [CheckRole]
        [RoleAuthorize("RemoveBillmeYetkisi")] //RemoveBillmeYetkisi diyede veritabanında yazacak
        public async Task<IActionResult> RemoveBill(int id)
        {
            try
            {
                Fatura bill = await _unitOfWorkFatura.Repository.GetById(id);
                _unitOfWorkFatura.Repository.Delete(bill);
                _unitOfWorkFatura.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Fatura silme işlemi başarılı','Sistem Hatası')";
                await CreateModeratorLog("Başarılı", "Silme", "RemoveBill", "Fatura");
                await Billings();
                return View("Billings");
            }
            catch (Exception)
            {
                await CreateModeratorLog("Sistem Hatası", "Silme", "RemoveBill", "Fatura");
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Sistem Hatası')";
                await Billings();
                return View("Billings");

            }          
        }

        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> BillDetail(int id)
        {
            try
            {
                Fatura getbill = await _unitOfWorkFatura.Repository.Where(x => x.ID == id).Include("faturaTipi").Include("bayiFatura").Include("siparisFatura").SingleOrDefaultAsync();

                List<Sepet> basketList = await _unitOfWorkSepet.Repository.Where(x => x.SiparisID == getbill.SiparisID).Include("UrunSepet").ToListAsync();
                ViewBag.Sepetler = basketList;

                List<Kargo> cargos = await _unitOfWorkKargo.Repository.Where(x => x.SiparisID == getbill.SiparisID).Include("kargoFirmaDetay").Include("kargoDetayi").ToListAsync();
                ViewBag.Kargolar = cargos;

                ViewBag.TotalPrice = basketList.Select(x => x.ToplamSatır).Sum();

                await CreateModeratorLog("Başarılı", "Detay", "BillDetail", "Fatura");              
                return View(getbill);
            }
            catch (Exception)
            {
                await CreateModeratorLog("Sistem Hatası", "Detay", "BillDetail", "Fatura");
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Sistem Hatası')";
                await Billings();
                return View("Billings");
            }
        }

        public async Task<CreateLog> CreateModeratorLog(string durum, string islem, string action, string controller)
        {
            string kullaniciAdi = HttpContext.Session.GetString("username");
            CreateLog ct = new CreateLog(_unitOfWorkLogDurumlar, _unitOfWorkLogIslemler, _unitOfWorkLogLoglar, _unitofWorkLogYonetici);
            if (kullaniciAdi == "" || kullaniciAdi == null)
            {
                await ct.CreateLogs(durum, islem, action, controller, "Sistem");
                return ct;
            }
            await ct.CreateLogs(durum, islem, action, controller, kullaniciAdi);
            return ct;
        }

    }
}
