using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{

    public class LogController : Controller
    {

        #region Fields

        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitOfWorkLogYoneticiler;
        private readonly int? page;
        public LogController(IUnitOfWorkLog<Durumlar> unitOfWorkDurumlar,
            IUnitOfWorkLog<Loglar> unitOfWorkLoglar,
            IUnitOfWorkLog<Islemler> unitOfWorkIslemler,
            IUnitOfWorkLog<Yonetici> unitOfWorkYoneticiler)
        {
            this._unitOfWorkLogDurumlar = unitOfWorkDurumlar;
            this._unitOfWorkLogIslemler = unitOfWorkIslemler;
            this._unitOfWorkLogLoglar = unitOfWorkLoglar;
            this._unitOfWorkLogYoneticiler = unitOfWorkYoneticiler;
        }

        #endregion

        [CheckLogin]
        [HttpGet, ActionName("ListLog")]
        public async Task<IActionResult> ListLog(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.Loglar = _unitOfWorkLogLoglar.RepositoryLog
               .Where(x => x.ID > 0)
               .Include("islemler")
               .Include("durumlar")
               .Include("yonetici").OrderByDescending(x=> x.ID)
               .ToPagedList(pageNumber, 20);

            List<Islemler> processList = (List<Islemler>)await _unitOfWorkLogIslemler.RepositoryLog.All();
            ViewBag.IslemAdi = new SelectList(processList, "ID", "IslemAdi");

            List<Durumlar> statusList = (List<Durumlar>)await _unitOfWorkLogDurumlar.RepositoryLog.All();
            ViewBag.DurumAdi = new SelectList(statusList, "ID", "DurumAdi");

            return View();
        }

        [CheckLogin]
        [HttpGet, ActionName("ListLogWithIslem")]
        public async Task<IActionResult> ListLogWithIslem(int? page, string IslemAdi)
        {
            TempData["islem"] = Convert.ToString(IslemAdi);
            int pageNumber = page ?? 1;
            int id = Convert.ToInt32(IslemAdi);
            ViewBag.Loglar = _unitOfWorkLogLoglar.RepositoryLog
               .Where(x => x.IslemID == id)
               .Include("islemler")
               .Include("durumlar")
               .Include("yonetici").OrderByDescending(x=> x.ID)
               .ToPagedList(pageNumber, 20);

            List<Islemler> processList = (List<Islemler>)await _unitOfWorkLogIslemler.RepositoryLog.All();
            ViewBag.IslemAdi = new SelectList(processList, "ID", "IslemAdi");

            List<Durumlar> statusList = (List<Durumlar>)await _unitOfWorkLogDurumlar.RepositoryLog.All();
            ViewBag.DurumAdi = new SelectList(statusList, "ID", "DurumAdi");

            return View();
        }

        [CheckLogin]
        [HttpGet, ActionName("ListLogWithDurum")]
        public async Task<IActionResult> ListLogWithDurum(int? page, string durumAdi)
        {
            TempData["durum"] = Convert.ToString(durumAdi);
            int pageNumber = page ?? 1;
            int id = Convert.ToInt32(durumAdi);
            ViewBag.Loglar = _unitOfWorkLogLoglar.RepositoryLog
               .Where(x => x.DurumID == id)
               .Include("islemler")
               .Include("durumlar")
               .Include("yonetici").OrderByDescending(x=> x.ID)
               .ToPagedList(pageNumber, 20);

            List<Islemler> processList = (List<Islemler>)await _unitOfWorkLogIslemler.RepositoryLog.All();
            ViewBag.IslemAdi = new SelectList(processList, "ID", "IslemAdi");

            List<Durumlar> statusList = (List<Durumlar>)await _unitOfWorkLogDurumlar.RepositoryLog.All();
            ViewBag.DurumAdi = new SelectList(statusList, "ID", "DurumAdi");

            return View();
        }

        public async Task<IActionResult> RemoveLog(int Id)
        {
            try
            {
                Loglar getLog = await _unitOfWorkLogLoglar.RepositoryLog.GetById(Id);
                _unitOfWorkLogLoglar.RepositoryLog.Delete(getLog);
                _unitOfWorkLogLoglar.RepositoryLog.Save();
              
                TempData["mesaj"] = "toastr['success']('Log kaldırma başarıyla gerçekleşti','Log Yönetimi')";
                await ListLog(page);
                return View("ListLog");
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['success']('Sistemden kaynaklı bir hata meydana geldi,'Log Yönetimi')";
                await ListLog(page);
                return View("ListLog");
            }
        }
    }
}
