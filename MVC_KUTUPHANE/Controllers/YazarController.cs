using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;

namespace MVC_KUTUPHANE.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index()
        {
            var degerler = db.TblYazar.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }

        public ActionResult YazarEkle(TblYazar p)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TblYazar.Add(p);
            db.SaveChanges();
            return View();
        }

        public ActionResult YazarSil(int id)
        {
            var yazar = db.TblYazar.Find(id);
            db.TblYazar.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TblYazar.Find(id);
            return View("YazarGetir", yzr);

        }

        public ActionResult YazarGuncelle(TblYazar p)
        {
            var yzr = db.TblYazar.Find(p.ID);
            yzr.AD=p.AD;
            yzr.SOYAD=p.SOYAD;
            yzr.DETAY=p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TblKitap.Where(x => x.YAZAR==id).ToList();
            var yzrad = db.TblYazar.Where(y => y.ID==id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.y1=yzrad;
            return View(yazar);
        }
    }
}