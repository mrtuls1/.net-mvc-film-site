using MvcVize.Models;
using MvcVize.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcVize.Controllers
{

    
    class Cinsiyet
    {
        public bool ID { get; set; }
        public string cinsiyeti { get; set; }
    }

    public class RegisterController : Controller
    {
        Model1 db = new Model1();
        
        List<Cinsiyet> Cinsiyetler = new List<Cinsiyet>
        {
            new Cinsiyet{ID=true,cinsiyeti="Erkek"},
            new Cinsiyet{ID=false,cinsiyeti="Kadın"},
        };

        public ActionResult Tasarım()
        {
            return View();
        }





        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.iller = new SelectList(db.iller, "id", "sehir");
            ViewBag.ilce = new SelectList(db.ilceler.Where(x => x.sehir == null), "id", "ilce").ToList();
            List<SelectListItem> degerler = (from i in Cinsiyetler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.cinsiyeti,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.cinsiyet = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Kullanici k, HttpPostedFileBase resim2)
        {
        


            Dosya dosya = new Dosya();
            string resimAdi = dosya.resimYukle(resim2);
            if (resimAdi == "uzanti")
            {
                ViewBag.Sonuc = "Lütfen .png veya .jpg uzantılı resim giriniz.";
                ViewBag.kategori = new SelectList(db.Kategori, "kategorID", "ad");
                return View();
            }
            else if (resimAdi == "boyut")
            {
                ViewBag.Sonuc = "Lütfen 1MB dan fazla boyutta resim yükleyiniz.";
                ViewBag.kategori = new SelectList(db.Kategori, "kategorID", "ad");
                return View();
            }

            Kullanici kul = db.Kullanici.Where(x => x.eposta == k.eposta).FirstOrDefault();
            if (kul != null)
            {
                ViewBag.hata = "Bu eposta daha önceden kullanılmış";
                ViewBag.iller = new SelectList(db.iller, "id", "sehir");
                List<SelectListItem> degerler = (from i in Cinsiyetler.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.cinsiyeti,
                                                     Value = i.ID.ToString()
                                                 }).ToList();
                ViewBag.cinsiyet = degerler;
                return View("Index");

            }
            else
            {
                k.resim = resimAdi;
                db.Kullanici.Add(k);
                db.SaveChanges();
                return Redirect("/Login/Index");
            }
           
        }
        public JsonResult ilceGetir(int id)
        {
            List<SelectListItem> ilceler = new List<SelectListItem>();
            var ilceData = db.ilceler.Where(x => x.sehir == id).Select(x => new SelectListItem()
            {
                Text = x.ilce,
                Value = x.id.ToString(),
            });
            return Json(ilceData, JsonRequestBehavior.AllowGet);
        }
    }
}