using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Utility
{
    public static class SendMailHelper
    {
        public static string _message { get; set; }
        public static string _title { get; set; }
        public static string _email { get; set; }
        public static string _subject { get; set; }
        public static string _sellerName { get; set; }
        public static OzelTeklif _specialOffer { get; set; }
        public static Siparis _order { get; set; }

        public static void SendMailToPayingNotification(string message, string subject, string mail, string title, string sellerName)
        {
            try
            {
                _message = message;
                _title = title;
                _email = mail;
                _subject = subject;
                _sellerName = sellerName;

                string body = "";

                body += "<b>" + _title + "</b>";
                body += "<br/><br/><b>Bayi: </b>" + _sellerName;
                body += "<br/><br/><b>Email: </b>" + _email;
                body += "<br/><br/><b>Açıklama: </b>" + _message;

                SmtpClient Kaynak = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential("bayi@kiracelektrik.com.tr", "Krc190634"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false
                };

                MailAddress Gonderen = new MailAddress("bayi@kiracelektrik.com.tr", "Kıraç Bayim");

                MailAddress Giden = new MailAddress(_email, _message);
                MailMessage Mesaj = new MailMessage(Gonderen, Giden)
                {
                    From = Gonderen,
                    Subject = _subject,
                    Body = body,

                    IsBodyHtml = true
                };
                Kaynak.Send(Mesaj);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public static void SendMailToSpecialOfferToAdmin(OzelTeklif specialOffer, string message, string subject, string mail, string title, string sellerName)
        {
            try
            {
                _specialOffer = specialOffer;
                _title = title;
                _subject = subject;
                _message = message;
                _sellerName = sellerName;
                _email = mail;

                string body = "";

                body += "<b>" + _title + "</b>";
                body += "<br/><br/><b>Bayi: </b>" + _sellerName;
                body += "<br/><br/><b>Email: </b>" + _email;
                body += "<br/><br/><b>Açıklama: </b>" + _message;
                body += "<br/><br/><b>Teklif Tarihi: </b>" + _specialOffer.TeklifTarih;
                body += "<br/><br/><b>Sipariş No: </b>" + _specialOffer.siparisOzelTeklif.SiparisNo;
                body += "<br/><br/><b>İndirim: </b>" + _specialOffer.siparisOzelTeklif.Indirim;
                body += "<br/><br/><b>Toplam Tutar: </b>" + _specialOffer.siparisOzelTeklif.ToplamFiyat;

                SmtpClient Kaynak = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential("bayi@kiracelektrik.com.tr", "Krc190634"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false
                };

                MailAddress Gonderen = new MailAddress("bayi@kiracelektrik.com.tr", "Kıraç Bayim");

                MailAddress Giden = new MailAddress(_email, _message);
                MailMessage Mesaj = new MailMessage(Gonderen, Giden)
                {
                    From = Gonderen,
                    Subject = _subject,
                    Body = body,

                    IsBodyHtml = true
                };
                Kaynak.Send(Mesaj);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public static void SendMailToOrderCompletedInfoToSeller(Siparis order, string message, string subject, string mail, string title, string sellerName)
        {
            try
            {
                _order = order;
                _title = title;
                _subject = subject;
                _message = message;
                _sellerName = sellerName;
                _email = mail;

                string body = "";

                body += "<b>" + _title + "</b>";
                body += "<br/><br/><b>Bayi: </b>" + _sellerName;
                body += "<br/><br/><b>Email: </b>" + _email;
                body += "<br/><br/><b>Açıklama: </b>" + _message;
                body += "<br/><br/><b>Teklif Tarihi: </b>" + _order.SiparisTarih;
                body += "<br/><br/><b>Sipariş No: </b>" + _order.SiparisNo;
                body += "<br/><br/><b>İndirim: </b>" + _order.Indirim;
                body += "<br/><br/><b>Toplam Tutar: </b>" + _order.ToplamFiyat;

                SmtpClient Kaynak = new SmtpClient
                {
                    Credentials = new System.Net.NetworkCredential("bayi@kiracelektrik.com.tr", "Krc190634"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false
                };

                MailAddress Gonderen = new MailAddress("bayi@kiracelektrik.com.tr", "Kıraç Bayim");

                MailAddress Giden = new MailAddress(_email, _message);
                MailMessage Mesaj = new MailMessage(Gonderen, Giden)
                {
                    From = Gonderen,
                    Subject = _subject,
                    Body = body,

                    IsBodyHtml = true
                };
                Kaynak.Send(Mesaj);
            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}
