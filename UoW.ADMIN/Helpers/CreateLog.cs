using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.CORE.Interface;
using UoW.LOG.Models;

namespace UoW.ADMIN.Helpers
{
    public class CreateLog
    {

        private readonly IUnitOfWorkLog<Durumlar> _unitOfWorkLogDurumlar;
        private readonly IUnitOfWorkLog<Islemler> _unitOfWorkLogIslemler;
        private readonly IUnitOfWorkLog<Loglar> _unitOfWorkLogLoglar;
        private readonly IUnitOfWorkLog<Yonetici> _unitofWorkLogYonetici;

        public CreateLog(IUnitOfWorkLog<Durumlar> unitOfWorkLogDurumlar,
             IUnitOfWorkLog<Islemler> unitOfWorkLogIslemler,
             IUnitOfWorkLog<Loglar> unitOfWorkLogLoglar,
             IUnitOfWorkLog<Yonetici> unitOfWorkLogYonetici)
        {
            this._unitOfWorkLogDurumlar = unitOfWorkLogDurumlar;
            this._unitOfWorkLogIslemler = unitOfWorkLogIslemler;
            this._unitOfWorkLogLoglar = unitOfWorkLogLoglar;
            this._unitofWorkLogYonetici = unitOfWorkLogYonetici;
        }

        public async Task<Loglar> CreateLogs(string durumAdi, string IslemAdi, string action, string controller, string kulladi)
        {
            Loglar l = new Loglar();

            Durumlar gelenDurumlar = await _unitOfWorkLogDurumlar.RepositoryLog
                .Where(x => x.DurumAdi == durumAdi).FirstOrDefaultAsync();

            if (gelenDurumlar != null)
            {
                Islemler gelenIslemler = await _unitOfWorkLogIslemler.RepositoryLog
                    .Where(x => x.IslemAdi == IslemAdi).FirstOrDefaultAsync();

                if (gelenIslemler != null)
                {
                   
                    Yonetici gelenK = await _unitofWorkLogYonetici.RepositoryLog.Where(x => x.KullaniciAdi == kulladi).SingleOrDefaultAsync();

                    if (gelenK == null)
                    {

                        Yonetici yeniYoneticiGetir = await YoneticiOlustur(kulladi);
                        l.yonetici = yeniYoneticiGetir;
                        l.YoneticiID = yeniYoneticiGetir.ID;
                        l.Action = action;
                        l.Controller = controller;
                        l.IslemID = gelenIslemler.ID;
                        l.islemler = gelenIslemler;
                        l.durumlar = gelenDurumlar;
                        l.DurumID = gelenDurumlar.ID;
                        l.Saat = DateTime.Now;
                        l.Tarih = DateTime.Now;

                        await _unitOfWorkLogLoglar.RepositoryLog.Add(l);
                        _unitOfWorkLogLoglar.RepositoryLog.Save();
                        return l;
                    }
                    else
                    {

                        l.Action = action;
                        l.Controller = controller;
                        l.IslemID = gelenIslemler.ID;
                        l.islemler = gelenIslemler;
                        l.durumlar = gelenDurumlar;
                        l.DurumID = gelenDurumlar.ID;
                        l.Saat = DateTime.Now;
                        l.Tarih = DateTime.Now;
                        l.YoneticiID = gelenK.ID;
                        l.yonetici = gelenK;

                        await _unitOfWorkLogLoglar.RepositoryLog.Add(l);
                        _unitOfWorkLogLoglar.RepositoryLog.Save();
                        return l;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private async Task<Yonetici> YoneticiOlustur(string name)
        {
            if (name != null || name != "")
            {
                Yonetici y = new Yonetici
                {
                    KullaniciAdi = name
                };

                await _unitofWorkLogYonetici.RepositoryLog.Add(y);
                _unitofWorkLogYonetici.RepositoryLog.Save();

                return y;
            }
            else
            {
                return null;
            }

        }
    }
}
