using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using UoW.LOG.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{

    public class KargoController : Controller
    {
        #region Fields

        private readonly IUnitOfWork<Bayi> _unitOfWorkBayi;
        private readonly IUnitOfWork<Kullanici> _unitOfWorkAdmin;
        private readonly IUnitOfWork<Urun> _unitOfWorkUrun;
        private readonly IUnitOfWork<Kategori> _unitOfWorkKategori;
        private readonly IUnitOfWork<Kargo> _unitOfWorkKargo;
        private readonly IUnitOfWork<KargoDetay> _unitOfWorkKargoDetay;
        private readonly IUnitOfWork<KargoFirmalar> _unitOfWorkKargoFirmalar;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork<Marka> _unitofWorkMarka;
        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitofWorkLogYonetici;
        private readonly IUnitOfWork<KargoSure> _unitOfWorkKargoSure;
        private readonly IUnitOfWork<MusteriGrubu> _unitOfWorkMusteriGrublar;
        private readonly IUnitOfWork<MusteriGrupKargo> _unitOfWorkMusteriGrubuKargolar;
        private readonly IConfiguration _configuration;
        private readonly int? page;
        private readonly int? markaPage;

        public KargoController(IUnitOfWork<Bayi> unitOfWorkBayi,
            IUnitOfWork<Kullanici> unitOfWorkAdmin,
            IUnitOfWork<Urun> unitOfWorkUrun,
            IUnitOfWork<Kategori> unitOfWorkKategori,
            IUnitOfWork<Marka> unitOfWorkMarka,
            IWebHostEnvironment hostEnvironment,
            IUnitOfWorkLog<Durumlar> unitOfWorkLogDurumlar,
            IUnitOfWorkLog<Islemler> unitOfWorkLogIslemler,
            IUnitOfWorkLog<Loglar> unitOfWorkLogLoglar,
            IUnitOfWorkLog<Yonetici> unitofWorkLogYonetici,
            IUnitOfWork<Kargo> unitOfWorkKargo,
            IUnitOfWork<KargoDetay> unitOfWorkKargoDetay,
            IUnitOfWork<KargoFirmalar> unitOfWorkKargoFirmalar,
            IUnitOfWork<KargoSure> unitOfWorkKargoSure,
            IUnitOfWork<MusteriGrubu> unitOfWorkMusteriGrubu,
            IUnitOfWork<MusteriGrupKargo> unitOfWorkMusteriGrubuKargo,
            IConfiguration configuration)
        {
            this._unitOfWorkBayi = unitOfWorkBayi;
            this._unitOfWorkAdmin = unitOfWorkAdmin;
            this._unitOfWorkUrun = unitOfWorkUrun;
            this._unitOfWorkKategori = unitOfWorkKategori;
            this._hostEnvironment = hostEnvironment;
            this._unitofWorkMarka = unitOfWorkMarka;
            this._configuration = configuration;
            this._unitOfWorkLogDurumlar = unitOfWorkLogDurumlar;
            this._unitOfWorkLogIslemler = unitOfWorkLogIslemler;
            this._unitOfWorkLogLoglar = unitOfWorkLogLoglar;
            this._unitofWorkLogYonetici = unitofWorkLogYonetici;
            this._unitOfWorkKargo = unitOfWorkKargo;
            this._unitOfWorkKargoSure = unitOfWorkKargoSure;
            this._unitOfWorkKargoDetay = unitOfWorkKargoDetay;
            this._unitOfWorkKargoFirmalar = unitOfWorkKargoFirmalar;
            this._unitOfWorkMusteriGrublar = unitOfWorkMusteriGrubu;
            this._unitOfWorkMusteriGrubuKargolar = unitOfWorkMusteriGrubuKargo;
        }

        public IConfigurationRoot GetConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();
            return builder;
        }

        #endregion

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ListCargo()
        {
            ViewBag.Kargolar = await _unitOfWorkKargoFirmalar.Repository.Where(x => x.ID > 0).ToListAsync();
            List<KargoFirmalar> cargoCompanies = ViewBag.Kargolar as List<KargoFirmalar>;
            return View(cargoCompanies);
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> EditCargo(int ID)
        {
            List<MusteriGrupKargo> customerGroupWithCargo = await _unitOfWorkMusteriGrubuKargolar.Repository.Where(x => x.KargoID == ID).Include("musteriGrubu").Include("Kargo").ToListAsync();
            ViewBag.MusteriGrublariKargolar = customerGroupWithCargo;

            KargoFirmalar getCargoCompany = await _unitOfWorkKargoFirmalar.Repository.GetById(ID);
            return View(getCargoCompany);
        }

        [HttpPost]
        public async Task<IActionResult> EditCargo(KargoFirmalar cargoCompany, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    KargoFirmalar getCargoCompany = await _unitOfWorkKargoFirmalar.Repository.GetById(cargoCompany.ID);

                    if (getCargoCompany != null)
                    {
                        getCargoCompany.KargoAdi = cargoCompany.KargoAdi;
                        getCargoCompany.KargoAciklama = cargoCompany.KargoAciklama;
                        getCargoCompany.KargoUcret = cargoCompany.KargoUcret;

                        if (file != null)
                            getCargoCompany.KargoFoto = SaveImageProcess.ImageInsert(file, "Bayi");

                        _unitOfWorkKargoFirmalar.Repository.Update(getCargoCompany);
                        _unitOfWorkKategori.Repository.Save();

                        TempData["mesaj"] = "toastr['success']('Kargo başarıyla düzenlenedi','Kargo Düzenleme')";
                        return RedirectToAction("EditCargo", "Kargo", new { ID = getCargoCompany.ID });
                    }
                    else
                    {
                        TempData["mesaj"] = "toastr['warning']('Kargo verisi alınamadı','Kargo Düzenleme')";
                        return RedirectToAction("EditCargo", "Kargo", new { ID = getCargoCompany.ID });
                    }
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Gerekli bilgileri eksiksiz giriniz','Kargo Düzenleme')";
                    return RedirectToAction("EditCargo", "Kargo", new { ID = cargoCompany.ID });
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Sistem Hatası')";
                return RedirectToAction("EditCargo", "Kargo", new { ID = cargoCompany.ID });
            }
        }


        [HttpGet]
        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> InsertCargo()
        {
            List<MusteriGrubu> customerGroupList = await _unitOfWorkMusteriGrublar.Repository.Where(x => x.Durum == true).ToListAsync();
            ViewBag.MusteriGrublari = customerGroupList;
            return View();
        }

        //public async Task<IActionResult> InsertCargo(KargoFirmalar kargo, string grupList, IFormFile fileImg)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            KargoFirmalar yeniKargo = new KargoFirmalar();
        //            yeniKargo.Durum = true;
        //            yeniKargo.EklenmeTarih = DateTime.Now;
        //            yeniKargo.GuncellenmeTarih = DateTime.Now;
        //            yeniKargo.KargoAdi = kargo.KargoAdi;
        //            yeniKargo.KargoUcret = kargo.KargoUcret;
        //            yeniKargo.KargoAciklama = kargo.KargoAciklama;

        //            if (fileImg != null)
        //            {
        //                string wwwRootPath = _hostEnvironment.WebRootPath;
        //                string fileName = Path.GetFileNameWithoutExtension(fileImg.FileName);
        //                string extension = Path.GetExtension(fileImg.FileName);
        //                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //                string path = Path.Combine(wwwRootPath + "/Files/Admin/", fileName);
        //                yeniKargo.KargoFoto = fileName;

        //                await _unitOfWorkKargoFirmalar.Repository.Add(yeniKargo);
        //                _unitOfWorkKategori.Repository.Save();

        //                using (var fileStream = new FileStream(path, FileMode.Create))
        //                {
        //                    await fileImg.CopyToAsync(fileStream);
        //                }

        //                string[] arr = grupList.Split(",");
        //                foreach (var item in arr)
        //                {
        //                    int currentId = Convert.ToInt32(item);
        //                    MusteriGrubu mg = await _unitOfWorkMusteriGrublar.Repository.Where(x => x.ID == currentId).SingleOrDefaultAsync();

        //                    MusteriGrupKargo mgk = new MusteriGrupKargo();
        //                    mgk.KargoID = yeniKargo.ID;
        //                    mgk.Kargo = yeniKargo;
        //                    mgk.MusteriGrupID = mg.ID;
        //                    mgk.musteriGrubu = mg;
        //                    mgk.Eklenme = DateTime.Now;
        //                    mgk.Durum = false;

        //                    await _unitOfWorkMusteriGrubuKargolar.Repository.Add(mgk);
        //                    _unitOfWorkMusteriGrubuKargolar.Repository.Save();
        //                }

        //            }
        //            else
        //            {
        //                string photoName = "default.jpg";
        //                string wwwRootPath = _hostEnvironment.WebRootPath;
        //                string path = Path.Combine(wwwRootPath + "/Files/Admin/", photoName);
        //                yeniKargo.KargoFoto = photoName;

        //                _unitOfWorkKargoFirmalar.Repository.Update(yeniKargo);
        //                _unitOfWorkKategori.Repository.Save();

        //            }
        //            return Json(true);
        //        }
        //        catch (Exception)
        //        {
        //            return Json(true);

        //        }
        //    }
        //    else
        //    {
        //        return Json(true);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> InsertCargo(KargoFirmalar cargoCompany, IFormFile fileImg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    KargoFirmalar newCargoCompany = new KargoFirmalar
                    {
                        Durum = true,
                        EklenmeTarih = DateTime.Now,
                        GuncellenmeTarih = DateTime.Now,
                        KargoAdi = cargoCompany.KargoAdi,
                        KargoUcret = cargoCompany.KargoUcret,
                        KargoAciklama = cargoCompany.KargoAciklama
                    };

                    if (fileImg != null)
                        newCargoCompany.KargoFoto = SaveImageProcess.ImageInsert(fileImg, "Bayi");

                    await _unitOfWorkKargoFirmalar.Repository.Add(newCargoCompany);
                    _unitOfWorkKategori.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Kargo başarıyla oluşturuldu','Kargo Ekleme')";
                    return RedirectToAction("ListCargo", "Kargo");
                }
                catch (Exception)
                {
                    TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu','Kargo Ekleme')";
                    return RedirectToAction("ListCargo", "Kargo");
                }
            }
            else
            {
                TempData["mesaj"] = "toastr['warning']('Lütfen istenilen bilgileri eksiksiz giriniz','Kargo Ekleme')";
                return RedirectToAction("ListCargo", "Kargo");
            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> DeleteCargo(int ID)
        {
            KargoFirmalar getCargoCompany = await _unitOfWorkKargoFirmalar.Repository.GetById(ID);

            try
            {
                if (getCargoCompany != null)
                {
                    List<Bayi> sellerList = await _unitOfWorkBayi.Repository.Where(x => x.KargoFirmasiID == getCargoCompany.ID).ToListAsync();
                    if (sellerList.Count > 0)
                    {
                        TempData["mesaj"] = "toastr['warning']('Kargoyu silemezsiniz çünkü hala bir bayi bu kargo hizmetinden yararlanmakta','Kargo Sil')";
                        return RedirectToAction("ListCargo", "Kargo");
                    }
                    else
                    {
                        _unitOfWorkKargoFirmalar.Repository.Delete(getCargoCompany);
                        _unitOfWorkKargoFirmalar.Repository.Save();
                        TempData["mesaj"] = "toastr['success']('Kargo başarıyla silindi','Kargo Sil')";
                        return RedirectToAction("ListCargo", "Kargo");
                    }            
                }
                else
                {
                    TempData["mesaj"] = "toastr['warning']('Kargo verisi alınamadı','Kargo Sil')";
                    return RedirectToAction("ListCargo", "Kargo");
                }
            }
            catch (Exception)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata meydana geldi','Kargo Sil')";
                return RedirectToAction("ListCargo", "Kargo");
            }

        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> ListCargoCompaniesToSeller(int? page, int KargoID)
        {
            KargoFirmalar getCargoCompany = await _unitOfWorkKargoFirmalar.Repository.GetById(KargoID);
            ViewBag.KargoAdi = getCargoCompany.KargoAdi;

            int pageNumber = page ?? 1;
            ViewBag.Bayi = await _unitOfWorkBayi.Repository.Where(x => x.KargoFirmasiID == KargoID).Include("odeme").Include("musteriGrubu").Include("kullanici").ToPagedListAsync(pageNumber, 20);

            IPagedList<Bayi> sellerList = ViewBag.Bayi as IPagedList<Bayi>;
            return View("ListCargoBySeller", sellerList);
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> CargoInsertInList(int ID)
        {
            KargoFirmalar getCargoCompany = await _unitOfWorkKargoFirmalar.Repository.GetById(ID);
            try
            {
                if (getCargoCompany != null)
                {
                    if (getCargoCompany.Durum == true)
                    {
                        getCargoCompany.Durum = false;
                        _unitOfWorkKargoFirmalar.Repository.Update(getCargoCompany);
                        _unitOfWorkKargoFirmalar.Repository.Save();
                    }
                    else
                    {
                        getCargoCompany.Durum = true;
                        _unitOfWorkKargoFirmalar.Repository.Update(getCargoCompany);
                        _unitOfWorkKargoFirmalar.Repository.Save();
                    }
                    await ListCargo();
                    return View("ListCargo");
                }
                else
                {
                    await ListCargo();
                    return View("ListCargo");
                }
            }
            catch (Exception)
            {
                await ListCargo();
                return View("ListCargo");

            }
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> CargoDelayList(int? page)
        {
            int pageNumber = page ?? 1;
            ViewBag.KargoSure = await _unitOfWorkKargoSure.Repository.Where(x => x.AktifMi == true).Include("kargoSüreBayi").Include("kargoSureKargo").ToPagedListAsync(pageNumber, 20);
            IPagedList<KargoSure> delayList = ViewBag.KargoSure as IPagedList<KargoSure>;
            return View(delayList);
        }

        [CheckLogin]
        [CheckRole]
        public async Task<IActionResult> BreakTimeToCargoUsing(int ID)
        {
            KargoSure delayUsingCargo = await _unitOfWorkKargoSure.Repository.GetById(ID);
            if (delayUsingCargo != null)
            {
                delayUsingCargo.AktifMi = false;
                delayUsingCargo.Bitis = DateTime.Now;
                _unitOfWorkKargoSure.Repository.Update(delayUsingCargo);
                _unitOfWorkKargoSure.Repository.Save();
            }
            else
            {
                await CargoDelayList(page);
                return View("CargoDelayList");
            }

            await CargoDelayList(page);
            return View("CargoDelayList");
        }

        [HttpPost]
        public async Task<JsonResult> ContextCargoByGroup(string grupList, int cargoId)
        {

            List<MusteriGrupKargo> isHaveCargosWithCustomerGroup = await _unitOfWorkMusteriGrubuKargolar.Repository.Where(x => x.KargoID == cargoId).ToListAsync();

            string[] array = grupList.Split(',');

            foreach (var item in array)
            {
                int CurrentId = Convert.ToInt32(item);

                foreach (var item2 in isHaveCargosWithCustomerGroup)
                {
                    if (item2.MusteriGrupID == CurrentId || item2.Durum == true)
                    {

                    }

                    else
                    {

                        if (item2.Durum == false)
                        {
                            item2.Durum = true;
                            _unitOfWorkMusteriGrubuKargolar.Repository.Update(item2);
                            _unitOfWorkMusteriGrubuKargolar.Repository.Save();
                        }
                        else
                        {
                            MusteriGrubu getCustomerGroup = await _unitOfWorkMusteriGrublar.Repository.GetById(CurrentId);
                            KargoFirmalar getCargoCompany = _unitOfWorkKargoFirmalar.Repository.Where(x => x.ID == cargoId).FirstOrDefault();
                            MusteriGrupKargo getCustomerGroupWithCargo = new MusteriGrupKargo
                            {
                                KargoID = getCargoCompany.ID,
                                Kargo = getCargoCompany,
                                MusteriGrupID = getCustomerGroup.ID,
                                musteriGrubu = getCustomerGroup,
                                Durum = true,
                                Eklenme = DateTime.Now
                            };

                            await _unitOfWorkMusteriGrubuKargolar.Repository.Add(getCustomerGroupWithCargo);
                            _unitOfWorkMusteriGrubuKargolar.Repository.Save();
                        }

                    }
                }
            }
            return Json(true);
        }
    }
}
