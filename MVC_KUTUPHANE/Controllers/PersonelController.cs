using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;

namespace MVC_KUTUPHANE.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index()
        {
            var degerler = db.TblPersonel.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(TblPersonel p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            db.TblPersonel.Add(p);
            db.SaveChanges();
            return View();
        }

        public ActionResult PersonelSil(int id)
        {
            var person = db.TblPersonel.Find(id);
            db.TblPersonel.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var prs = db.TblPersonel.Find(id);
            return View("PersonelGetir", prs);
        }
        public ActionResult PersonelGuncelle(TblPersonel p)
        {
            var prs = db.TblPersonel.Find(p.ID);
            prs.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}