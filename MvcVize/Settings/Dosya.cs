using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcVize.Settings
{
    public class Dosya
    {
        public string resimYukle(HttpPostedFileBase resim)
        {
            //1- uzantı kontrol
            string uzanti = Path.GetExtension(resim.FileName).ToLower();
            if (uzanti == ".png" || uzanti == ".jpg")
            {
                if (resim.ContentLength > 10000000)
                {
                    return "boyut";
                }
                else
                {
                    Image orjinalResim = Image.FromStream(resim.InputStream);
                    string resimAd = Guid.NewGuid().ToString() + uzanti;
                    Bitmap res = new Bitmap(orjinalResim);
                    res.Save(HttpContext.Current.Server.MapPath("/Content/Resim/Kullanici/" + resimAd));
                    return resimAd;

                }
            }
            else
            {
                //izin verilmeyen uzantılar ise;
                return "uzanti";
            }
        }

        public string resimYukle(HttpPostedFileBase resim, string yol)
        {
            //1- uzantı kontrol
            if(resim ==null)
            {
                return null;
            }
            string uzanti = Path.GetExtension(resim.FileName).ToLower();
            if (uzanti == ".png" || uzanti == ".jpg")
            {
                if (resim.ContentLength > 10000000)
                {
                    return "boyut";
                }
                else
                {
                    Image orjinalResim = Image.FromStream(resim.InputStream);
                    string resimAd = Guid.NewGuid().ToString() + uzanti;
                    Bitmap res = new Bitmap(orjinalResim);
                    res.Save(HttpContext.Current.Server.MapPath(yol + resimAd));
                    return resimAd;

                }
            }
            else
            {
                //izin verilmeyen uzantılar ise;
                return "uzanti";
            }
        }

        public string pdfYukle(HttpPostedFileBase dosya)
        {
            //1- uzantı kontrol
            string uzanti = Path.GetExtension(dosya.FileName).ToLower();
            if (uzanti == ".pdf")
            {
                if (dosya.ContentLength > 10000000)
                {
                    return "boyut";
                }
                else
                {
                    string dosyaAd = Guid.NewGuid().ToString() + uzanti;
                    dosya.SaveAs(HttpContext.Current.Server.MapPath("/Content/Dosya/Ozgecmis/" + dosyaAd));
                    return dosyaAd;
                }
            }
            else
            {
                //izin verilmeyen uzantılar ise;
                return "uzanti";
            }
        }

        public string pdfYukle(HttpPostedFileBase dosya, string yol)
        {
            //1- uzantı kontrol
            string uzanti = Path.GetExtension(dosya.FileName).ToLower();
            if (uzanti == ".pdf")
            {
                if (dosya.ContentLength > 10000000)
                {
                    return "boyut";
                }
                else
                {
                    string dosyaAd = Guid.NewGuid().ToString() + uzanti;
                    dosya.SaveAs(HttpContext.Current.Server.MapPath(yol + dosyaAd));
                    return dosyaAd;
                }
            }
            else
            {
                //izin verilmeyen uzantılar ise;
                return "uzanti";
            }
        }

        public string wordYukle(HttpPostedFileBase dosya)
        {
            //1- uzantı kontrol
            string uzanti = Path.GetExtension(dosya.FileName).ToLower();
            if (uzanti == ".doc" || uzanti == ".docx")
            {
                if (dosya.ContentLength > 10000000)
                {
                    return "boyut";
                }
                else
                {
                    string dosyaAd = Guid.NewGuid().ToString() + uzanti;
                    dosya.SaveAs(HttpContext.Current.Server.MapPath("/Content/Dosya/Ozgecmis/" + dosyaAd));
                    return dosyaAd;
                }
            }
            else
            {
                //izin verilmeyen uzantılar ise;
                return "uzanti";
            }
        }

        public bool resimSil(string resimAd)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath("/Content/Resim/Film/" + resimAd));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool resimSil(string resimAd, string yol)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath(yol + resimAd));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}