using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;

namespace UoW.WEB.Components
{
    public class NewsViewComponent : ViewComponent
    {
        #region Field

        private readonly IUnitOfWork<Haber> _unitOfWorkHaber;

        public NewsViewComponent(IUnitOfWork<Haber> unitOfWorkHaber)
        {
            this._unitOfWorkHaber = unitOfWorkHaber;
        }

        #endregion
        public IViewComponentResult Invoke()
        {
            ViewBag.Haberler = _unitOfWorkHaber.Repository.Where(x => x.AktifMi == true).Include("kategori").Take(3).ToListAsync().Result;
            return View();
        }
    }
}
