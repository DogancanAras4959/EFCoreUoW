using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;

namespace UoW.ADMIN.Controllers
{
    public class SiteInfoController : Controller
    {
        private readonly IUnitOfWork<SiteInfo> _unitOfWorkSiteInfo;

        public SiteInfoController(IUnitOfWork<SiteInfo> unitOfWorkSiteInfo)
        {
            this._unitOfWorkSiteInfo = unitOfWorkSiteInfo;
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> ListSiteInfo()
        {
            List<SiteInfo> sitesInfo = await _unitOfWorkSiteInfo.Repository.Where(x => x.ID > 0).Include("kullanici").ToListAsync();
            return View(sitesInfo);
        }

        [HttpGet]
        [CheckLogin]
        public IActionResult InsertSiteInfo()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult InsertSiteInfo(SiteInfo siteInfo)
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));

                siteInfo.AktifMi = true;
                siteInfo.EklenmeTarih = DateTime.Now;
                siteInfo.GuncellenmeTarih = DateTime.Now;
                siteInfo.YoneticiID = id;

                _unitOfWorkSiteInfo.Repository.Add(siteInfo);
                _unitOfWorkSiteInfo.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Site bilgisi başarıyla eklendi','Site Bilgisi Ekle')";
                return RedirectToAction("ListSiteInfo", "SiteInfo");
            }

            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Site Bilgisi Ekle')";
                return RedirectToAction("ListSiteInfo", "SiteInfo");
            }        
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> UpdateSiteInfo(int Id)
        {
            SiteInfo getSiteInfo = await _unitOfWorkSiteInfo.Repository.GetById(Id);
            return View(getSiteInfo);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSiteInfo(SiteInfo siteInfo)
        {
            try
            {
                SiteInfo getSiteInfo = await _unitOfWorkSiteInfo.Repository.GetById(siteInfo.ID);

                getSiteInfo.Baslik = siteInfo.Baslik;
                getSiteInfo.Icerik = siteInfo.Icerik;
                getSiteInfo.LinkAdi = siteInfo.LinkAdi;
                getSiteInfo.GuncellenmeTarih = DateTime.Now;

                _unitOfWorkSiteInfo.Repository.Update(getSiteInfo);
                _unitOfWorkSiteInfo.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Site Bilgileri güncellendi','Site Bilgileri Güncelle')";
                return RedirectToAction("ListSiteInfo", "SiteInfo");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir sorun meydana geldi','Site Bilgileri Güncelle')";
                return RedirectToAction("ListSiteInfo", "SiteInfo");
            }
        }

        public async Task<IActionResult> RemoveSiteInfo(int Id)
        {
            try
            {
                SiteInfo getSiteInfo = await _unitOfWorkSiteInfo.Repository.GetById(Id);
                _unitOfWorkSiteInfo.Repository.Delete(getSiteInfo);
                _unitOfWorkSiteInfo.Repository.Save();
                TempData["mesaj"] = "toastr['success']('Site bilgileri güncellendi','Site Bilgileri Güncelle')";
                return RedirectToAction("ListSiteInfo", "SiteInfo");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir sorun meydana geldi','Site Bilgileri Sil')";
                return RedirectToAction("ListSiteInfo", "SiteInfo");
            }
        }

        public async Task<IActionResult> EditStatusSiteInfo(int Id)
        {
            try
            {
                SiteInfo getSiteInfo = await _unitOfWorkSiteInfo.Repository.GetById(Id);
                if (getSiteInfo.AktifMi == true)
                {
                    getSiteInfo.AktifMi = false;
                    _unitOfWorkSiteInfo.Repository.Update(getSiteInfo);
                    _unitOfWorkSiteInfo.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Site bilgileri pasifleştirildi','Site Bilgileri Güncelle')";
                    return RedirectToAction("ListSiteInfo", "SiteInfo");
                }
                else
                {
                    getSiteInfo.AktifMi = true;
                    _unitOfWorkSiteInfo.Repository.Update(getSiteInfo);
                    _unitOfWorkSiteInfo.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Site bilgileri aktifleştirildi','Site Bilgileri Güncelle')";
                    return RedirectToAction("ListSiteInfo", "SiteInfo");
                }
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir sorun meydana geldi','Site Bilgileri Durum')";
                return RedirectToAction("ListSiteInfo", "SiteInfo");
            }
        }

        public async Task<IActionResult> DetailSiteInfo(int Id)
        {
            SiteInfo getSiteInfo = await _unitOfWorkSiteInfo.Repository.GetById(Id);
            return View(getSiteInfo);
        }
    }
}
