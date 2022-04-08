using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UoW.ADMIN.Helpers;
using UoW.CORE.Interface;
using UoW.DATA.Utility;
using UoW.DOMAIN.Models;
using X.PagedList;

namespace UoW.ADMIN.Controllers
{
    public class DocumentController : Controller
    {
        #region Fields

        private readonly IUnitOfWork<Kullanici> _unitOfWorkKullanici;
        private readonly IUnitOfWork<Dokuman> _unitOfWorkDokuman;
        private readonly IUnitOfWork<DokumanTipi> _unitOfWorkDokumanTipi;
        private readonly IHostEnvironment _hostEnvironment;
        public DocumentController(IUnitOfWork<Kullanici> unitOfWorkKullanici, IUnitOfWork<Dokuman> unitOfWorkDokuman, IUnitOfWork<DokumanTipi> unitOfWorkDokumanTipi, IHostEnvironment hostEnvironment)
        {
            this._unitOfWorkKullanici = unitOfWorkKullanici;
            this._unitOfWorkDokuman = unitOfWorkDokuman;
            this._unitOfWorkDokumanTipi = unitOfWorkDokumanTipi;
            this._hostEnvironment = hostEnvironment;
        }

        #endregion

        #region Döküman Tipi Metotları

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> DocumentTypeList()
        {
            List<DokumanTipi> documentTypeList = await _unitOfWorkDokumanTipi.Repository.Where(x => x.ID > 0).ToListAsync();
            return View(documentTypeList);
        }

        [HttpGet]
        [CheckLogin]
        public IActionResult InsertDocumentType()
        {
            return View();
        }

        [HttpPost]
        [CheckLogin]
        public async Task<IActionResult> InsertDocumentType(DokumanTipi dokuman)
        {
            try
            {
                dokuman.Durum = true;
                await _unitOfWorkDokumanTipi.Repository.Add(dokuman);
                _unitOfWorkDokumanTipi.Repository.Save();
                TempData["mesaj"] = "toastr['success']('Döküman tipi başarıyla eklendi','Ekleme İşlemi')";
                return RedirectToAction("DocumentTypeList", "Document");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden Kaynaklı bir hata meydana geldi','Ekleme İşlemi')";
                return RedirectToAction("DocumentTypeList", "Document");
            }

        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> UpdateDocumentType(int ID)
        {
            DokumanTipi getDocumentType = await _unitOfWorkDokumanTipi.Repository.GetById(ID);
            return View(getDocumentType);
        }

        [HttpPost]
        [CheckLogin]
        public async Task<IActionResult> UpdateDocumentType(DokumanTipi dokuman)
        {
            try
            {
                DokumanTipi getDocumentType = await _unitOfWorkDokumanTipi.Repository.GetById(dokuman.ID);
                getDocumentType.Pozisyon = dokuman.Pozisyon;
                getDocumentType.TipAdi = dokuman.TipAdi;

                _unitOfWorkDokumanTipi.Repository.Update(getDocumentType);
                _unitOfWorkDokumanTipi.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Döküman tipi başarıyla güncellendi','Güncelleme İşlemi')";
                return RedirectToAction("DocumentTypeList", "Document", new { ID = dokuman.ID });
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['success']('Döküman tipi başarıyla güncellendi','Güncelleme İşlemi')";
                return RedirectToAction("DocumentTypeList", "Document", new { ID = dokuman.ID });
            }

        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> RemoveDocumentType(int ID)
        {
            try
            {
                DokumanTipi getDocumentType = await _unitOfWorkDokumanTipi.Repository.GetById(ID);
                _unitOfWorkDokumanTipi.Repository.Delete(getDocumentType);
                _unitOfWorkDokumanTipi.Repository.Save();
                TempData["mesaj"] = "toastr['success']('Döküman tipi başarıyla silindi','Silme İşlemi')";
                return RedirectToAction("DocumentTypeList", "Document");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden Kaynaklı bir hata meydana geldi','Silme İşlemi')";
                return RedirectToAction("DocumentTypeList", "Document");
            }

        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> EditStatusDocumentType(int ID)
        {
            try
            {
                DokumanTipi getDocumentType = await _unitOfWorkDokumanTipi.Repository.GetById(ID);

                if (getDocumentType.Durum == true)
                {
                    getDocumentType.Durum = false;
                    _unitOfWorkDokumanTipi.Repository.Update(getDocumentType);
                    _unitOfWorkDokumanTipi.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Döküman tipi başarıyla pasifleştirildi','Pasifleştirme İşlemi')";
                    return RedirectToAction("DocumentTypeList", "Document");
                }
                else
                {
                    getDocumentType.Durum = true;
                    _unitOfWorkDokumanTipi.Repository.Update(getDocumentType);
                    _unitOfWorkDokumanTipi.Repository.Save();

                    TempData["mesaj"] = "toastr['success']('Döküman tipi başarıyla aktifleştirildi','Aktifleştirme İşlemi')";
                    return RedirectToAction("DocumentTypeList", "Document");
                }

            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden Kaynaklı bir hata meydana geldi','Aktif-Pasif İşlemi')";
                return RedirectToAction("DocumentTypeList", "Document");
            }
        }

        #endregion

        #region Doküman Metotları

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> Documents(int? page, int TipAdi)
        {

            List<DokumanTipi> dokumanTipi = await _unitOfWorkDokumanTipi.Repository.Where(x => x.Durum == true).ToListAsync();
            ViewBag.TipAdi = new SelectList(dokumanTipi, "ID", "TipAdi");

            int pageNumber = page ?? 1;
            var dokumanlar = await _unitOfWorkDokuman.Repository.Where(x => x.ID > 0).OrderBy(x => x.EklenmeTarih).Include("dokuman").Include("kullanici").ToPagedListAsync(pageNumber, 24);

            if (TipAdi != 0)
            {
                dokumanlar = await dokumanlar.Where(x => x.DokumanTipi == TipAdi).ToPagedListAsync(pageNumber, 24);
            }

            ViewBag.Dokumanlar = dokumanlar;
            return View();
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> InsertDocument()
        {
            var documentTypeList = await _unitOfWorkDokumanTipi.Repository.Where(x => x.Durum == true).ToListAsync();
            ViewBag.TipAdi = new SelectList(documentTypeList, "ID", "TipAdi");
            return View();
        }

        [HttpPost]
        [CheckLogin]
        public async Task<IActionResult> InsertDocument(Dokuman document, int TipAdi, IFormFile file, IFormFile image)
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                Kullanici k = await _unitOfWorkKullanici.Repository.GetById(id);
                document.DokumanTipi = TipAdi;
                document.Durum = true;
                document.EklenmeTarih = DateTime.Now;
                document.GuncellenmeTarih = DateTime.Now;
                document.YoneticiID = id;
                document.kullanici = k;
                if (image != null)
                {
                    document.Onizleme = SaveImageProcess.ImageInsert(image, "Dokumanlar");
                }
                if (file != null)
                {
                    document.DosyaYolu = SaveImageProcess.ImageInsert(file, "Dokumanlar");
                }

                await _unitOfWorkDokuman.Repository.Add(document);
                _unitOfWorkDokuman.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Döküman başarıyla eklendi','Ekleme İşlemi')";
                return RedirectToAction("Documents", "Document");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden Kaynaklı bir hata meydana geldi','Ekleme İşlemi')";
                return RedirectToAction("Documents", "Document");
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> UpdateDocument(int ID)
        {
            Dokuman getDocument = await _unitOfWorkDokuman.Repository.GetById(ID);
            var documentType = await _unitOfWorkDokumanTipi.Repository.Where(x => x.Durum == true).ToListAsync();
            ViewBag.TipAdi = new SelectList(documentType, "ID", "TipAdi", getDocument.DokumanTipi);
            return View(getDocument);
        }

        [HttpPost]
        [CheckLogin]
        public async Task<IActionResult> UpdateDocument(Dokuman getDocument, int TipAdi, IFormFile file, IFormFile Image)
        {
            try
            {
                Dokuman newDocument = await _unitOfWorkDokuman.Repository.GetById(getDocument.ID);
                newDocument.Durum = getDocument.Durum;
                newDocument.DokumanTipi = TipAdi;
                newDocument.KatalogAdi = getDocument.KatalogAdi;

                if (file != null)
                {
                    newDocument.DosyaYolu = SaveImageProcess.ImageInsert(file, "Dokumanlar");
                }
                if (Image != null)
                {
                    newDocument.Onizleme = SaveImageProcess.ImageInsert(Image, "Dokumanlar");
                }

                _unitOfWorkDokuman.Repository.Update(newDocument);
                _unitOfWorkDokuman.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Döküman başarıyla güncellendi','Güncelleme İşlemi')";
                return RedirectToAction("Documents", "Document", new { ID = getDocument.ID });
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu','Güncelleme İşlemi')";
                return RedirectToAction("Documents", "Document", new { ID = getDocument.ID });
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> RemoveDocument(int ID)
        {
            try
            {
                Dokuman document = await _unitOfWorkDokuman.Repository.GetById(ID);
                _unitOfWorkDokuman.Repository.Delete(document);
                _unitOfWorkDokuman.Repository.Save();

                TempData["mesaj"] = "toastr['success']('Döküman başarıyla silindi','Silme İşlemi')";
                return RedirectToAction("Documents", "Document");
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Döküman başarıyla silindi','Silme İşlemi')";
                return RedirectToAction("Documents", "Document");
            }
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> EditStatusDocument(int ID)
        {
            try
            {
                Dokuman document = await _unitOfWorkDokuman.Repository.GetById(ID);

                if (document.Durum == true)
                {
                    document.Durum = false;
                    _unitOfWorkDokuman.Repository.Update(document);
                    _unitOfWorkDokuman.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Döküman başarıyla pasifleştirilme','Aktif - Pasif İşlemi')";
                    return RedirectToAction("Documents", "Document");
                }
                else
                {
                    document.Durum = true;
                    _unitOfWorkDokuman.Repository.Update(document);
                    _unitOfWorkDokuman.Repository.Save();
                    TempData["mesaj"] = "toastr['success']('Döküman başarıyla aktifleştirildi','Aktif - Pasif İşlemi')";
                    return RedirectToAction("Documents", "Document");
                }
            }
            catch (Exception ex)
            {
                TempData["mesaj"] = "toastr['warning']('Sistemden kaynaklı bir hata oluştu','Aktif-Pasif İşlemi İşlemi')";
                return RedirectToAction("Documents", "Document");
            }
        }

        public FileResult DownloadDocument(int ID)
        {
            Dokuman getDocument = _unitOfWorkDokuman.Repository.GetById(ID).Result;           
            MemoryStream stream = SaveImageProcess.GetDownloadableFile(getDocument.DosyaYolu, "Dokumanlar");
            stream.Position = 0;
            var extensions = new FileInfo(getDocument.DosyaYolu).Extension;
            var fileTypes = MimeTypesMap.GetMimeType(getDocument.DosyaYolu);
            return File(stream, fileTypes, $"{getDocument.KatalogAdi}{extensions}");
        }     

        #endregion

    }
}
