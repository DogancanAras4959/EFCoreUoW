using ePayment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.CORE.Payment
{
    public class MakePayment
    {
        private readonly IConfiguration _configuration;

        public MakePayment(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [Obsolete]
        public string MakePaymentThis(string _exyear, string _expmonth, string _oid, string _cardNumber, string _cv2, decimal _subtotal)
        {
            cc5payment payment = new cc5payment();

            if (_exyear != "0" || _cardNumber != null || _cv2 != "0" || _expmonth != "0")
            {               
                payment.host = _configuration["PaymentSettings:Host"]; //Sabit
                payment.name = _configuration["PaymentSettings:Name"]; //Sabit
                payment.password = _configuration["PaymentSettings:Password"]; //Sabit
                payment.clientid = _configuration["PaymentSettings:Clientid"]; //Sabit
                payment.orderresult = 0;
                payment.oid = _oid;
                payment.cardnumber = _cardNumber;
                payment.expmonth = _expmonth;
                payment.expyear = _exyear;
                payment.cv2 = _cv2;
                payment.subtotal = _subtotal.ToString();
                payment.currency = "949";
                payment.chargetype = "Auth";
               
                //payment.payerauthenticationcode = "";
                //payment.cardholderpresentcode = "";
                //payment.payersecuritylevel = "";
                //payment.payertxnid = "";

                var Result1 = payment.processorder();
                var Procreturncode = payment.procreturncode;
                var ErrMsg = payment.errmsg;
                var Oid1 = payment.oid;
                var appr1 = payment.appr;

                if (!(string.IsNullOrWhiteSpace(ErrMsg)))
                {
                    throw new Exception(ErrMsg);
                }
            }

            else
            {
                var ErrMsg = "Siparişinizi tamamlayabilmemiz için lütfen kart bilgilerinizi boş veya eksik göndermeyiniz!";
                if (!(string.IsNullOrWhiteSpace(ErrMsg)))
                {
                    throw new Exception(ErrMsg);
                }
            }

            return payment.refno;
            //Dönen referans numarasını sipariş ile birlikte veritabanına kaydet
        }
    }
}
