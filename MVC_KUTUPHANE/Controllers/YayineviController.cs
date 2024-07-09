using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;

namespace MVC_KUTUPHANE.Controllers
{
    public class YayineviController : Controller
    {
        // GET: Yayinevi
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index()
        {
            var degerler = db.TblYayinevi.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YayineviEkle()
        {
            return View();
        }

        public ActionResult YayineviEkle(TblYayinevi p)
        {
            db.TblYayinevi.Add(p);
            db.SaveChanges();
            return View();
        }

        public ActionResult YayineviSil(int id)
        {
            var yayinevi = db.TblYayinevi.Find(id);
            db.TblYayinevi.Remove(yayinevi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YayineviGetir(int id)
        {
            var yayin = db.TblYayinevi.Find(id);
            return View("YayineviGetir", yayin);

        }

        public ActionResult YayineviGuncelle(TblYayinevi p)
        {
            var yayinevi = db.TblYayinevi.Find(p.ID);
            yayinevi.YAYINEVI=p.YAYINEVI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}