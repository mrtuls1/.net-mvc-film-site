using MvcVize.Models;
using MvcVize.Settings;
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcVize.Controllers
{
    [_SecurityFilter]
    public class HomeController : Controller
    {
        // GET: Home
        Model1 db = new Model1();
        public ActionResult Index()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            List<Film> deger = db.Film.Where(x => x.kullaniciID == k.kullaniciID).ToList();
            return View(deger);
        }
        public ActionResult Sil(int id)
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            // 1- Film silindikten sonra resim silinsin mi ?!!!!!
            Film f = db.Film.Where(x => x.filmID == id && x.kullaniciID == k.kullaniciID).SingleOrDefault();
            if (f == null)
            {
               return Redirect("/Home/Index/");
            }
            else
            {
                db.Film.Remove(f);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            ViewBag.kategoriID = new SelectList(db.Kategori, "kategoriID", "ad");
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Film k,HttpPostedFileBase resim2)
        {
            Dosya dosya = new Dosya();
            string resimAdi = dosya.resimYukle(resim2,"/Content/Resim/Film");
            if (resimAdi == "uzanti")
            {
                ViewBag.Sonuc = "Lütfen .png veya .jpg uzantılı resim giriniz.";
                ViewBag.kategoriID = new SelectList(db.Kategori, "kategoriID", "ad");
                return View();
            }
            else if (resimAdi == "boyut")
            {
                ViewBag.Sonuc = "Lütfen 1MB dan fazla boyutta resim yükleyiniz.";
                ViewBag.kategoriID = new SelectList(db.Kategori, "kategoriID", "ad");
                return View();
            }
            
            Kullanici kul = (Kullanici)Session["Kullanici"];
            k.kullaniciID = kul.kullaniciID;
            k.resim = resimAdi;
            db.Film.Add(k);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            Film f = db.Film.Where(x=>x.filmID==id).SingleOrDefault();
            ViewBag.kategori = new SelectList(db.Kategori, "kategoriID", "ad", f.kategoriID); 
            TempData["filmID"] = f.filmID;
            return View(f);
        }
        [HttpPost]
        public ActionResult Guncelle(Film film,HttpPostedFileBase resim2)
        {
            //resim yoksa
            int filmID = (int)TempData["filmID"];
            Film eskiFilm = db.Film.Where(x => x.filmID == filmID).SingleOrDefault();
            if (resim2==null)
            {
                eskiFilm.ad = film.ad;
                eskiFilm.ozet = film.ozet;
                eskiFilm.kategoriID = film.kategoriID;
                db.SaveChanges();
                return Redirect("/Home/Index/");
            }

            Dosya dosya = new Dosya();
            #region Resim İşlemleri
            string resimAdi = dosya.resimYukle(resim2,"/Content/Resim/Film/");
            if (resimAdi == "uzanti")
            {
                ViewBag.Sonuc = "Lütfen .png veya .jpg uzantılı resim giriniz.";
                ViewBag.kategori = new SelectList(db.Kategori, "kategoriID", "ad");
                return View();
            }
            else if (resimAdi == "boyut")
            {
                ViewBag.Sonuc = "Lütfen 1MB dan fazla boyutta resim yükleyiniz.";
                ViewBag.kategori = new SelectList(db.Kategori, "kategoriID", "ad");
                return View();
            }
            dosya.resimSil(eskiFilm.resim);
            eskiFilm.resim = resimAdi;
            #endregion
            eskiFilm.ad = film.ad;
            eskiFilm.ozet = film.ozet;
            eskiFilm.kategoriID = film.kategoriID;
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}