using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace UoW.DATA.Utility
{
    public static class SaveImageProcess
    {

        public static FTPInformation GetFTPInformation(string _isAdminOrBayi)
        {
            FTPInformation ftpInfo = new FTPInformation();
            switch (_isAdminOrBayi)
            {
                case "Admin":

                    ftpInfo.Url = "ftp://admin.kiracelektrik.com.tr//files/uploads/profil";
                    ftpInfo.Username = "admin1";
                    ftpInfo.Password = "_67TOBtflkt?5sym";
                    break;

                case "Bayi":

                    ftpInfo.Url = "ftp://admin.kiracelektrik.com.tr//files/uploads/profil";
                    ftpInfo.Username = "admin1";
                    ftpInfo.Password = "_67TOBtflkt?5sym";
                    break;

                case "Urun":

                    ftpInfo.Url = "ftp://admin.kiracelektrik.com.tr//files/uploads/urun";
                    ftpInfo.Username = "admin1";
                    ftpInfo.Password = "_67TOBtflkt?5sym";
                    break;

                case "Haber":

                    ftpInfo.Url = "ftp://admin.kiracelektrik.com.tr//files/uploads/dokuman";
                    ftpInfo.Username = "admin1";
                    ftpInfo.Password = "_67TOBtflkt?5sym";
                    break;

                case "Kampanya":

                    ftpInfo.Url = "ftp://admin.kiracelektrik.com.tr//files/uploads/urun";
                    ftpInfo.Username = "admin1";
                    ftpInfo.Password = "_67TOBtflkt?5sym";
                    break;

                case "Dokumanlar":

                    ftpInfo.Url = "ftp://admin.kiracelektrik.com.tr//files/uploads/dokuman";
                    ftpInfo.Username = "admin1";
                    ftpInfo.Password = "_67TOBtflkt?5sym";
                    break;

                default:
                    break;
            }
            return ftpInfo;
        }

        public static string ImageInsert(IFormFile _file, string _isAdminOrBayi)
        {
            FTPInformation ftpInformation = GetFTPInformation(_isAdminOrBayi);
            var uploadurl = ftpInformation.Url;
            var username = ftpInformation.Username;
            var password = ftpInformation.Password;

            string uploadfilename = Path.GetFileNameWithoutExtension(_file.FileName);
            string extension = Path.GetExtension(_file.FileName);
            uploadfilename = uploadfilename + DateTime.Now.ToString("yymmssfff") + extension;
            Stream streamObj = _file.OpenReadStream();
            byte[] buffer = new byte[_file.Length];
            streamObj.Read(buffer, 0, buffer.Length);
            streamObj.Close();
            string ftpurl = string.Format("{0}/{1}", uploadurl, uploadfilename);
            var requestObj = WebRequest.Create(ftpurl) as FtpWebRequest;
            requestObj.Method = WebRequestMethods.Ftp.UploadFile;
            requestObj.Credentials = new NetworkCredential(username, password);
            Stream requestStream = requestObj.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Flush();
            requestStream.Close();

            return uploadfilename;
        }


        public static MemoryStream GetDownloadableFile(string fileName, string _isAdminOrBayi)
        {

            FTPInformation ftpInformation = GetFTPInformation(_isAdminOrBayi);
            string ftpurl = string.Format("{0}/{1}", ftpInformation.Url, fileName);
            var requestObj = WebRequest.Create(ftpurl) as FtpWebRequest;
            requestObj.Method = WebRequestMethods.Ftp.DownloadFile;
            requestObj.Credentials = new NetworkCredential(ftpInformation.Username, ftpInformation.Password);

            using (var response = (FtpWebResponse)requestObj.GetResponse())
            using (var responseStream = response.GetResponseStream())
            {
                MemoryStream stream = new MemoryStream();
                responseStream.CopyTo(stream);
                return stream;
            }
        }

    }

    public class FTPInformation
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
    }

}


