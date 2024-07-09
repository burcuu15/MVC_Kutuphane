using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVC_KUTUPHANE.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TblUyeler.ToList();
            var degerler = db.TblUyeler.ToList().ToPagedList(sayfa, 3); //bir sayfada üç değer gözükmesi için
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TblUyeler p)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            db.TblUyeler.Add(p);
            db.SaveChanges();
            return View();
        }

        public ActionResult UyeSil(int id)
        {
            var uye = db.TblUyeler.Find(id);
            db.TblUyeler.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeGetir(int id)
        {
            var uye = db.TblUyeler.Find(id);
            return View("UyeGetir", uye);
        }
        public ActionResult UyeGuncelle(TblUyeler p)
        {
            var uye = db.TblUyeler.Find(p.ID);
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.MAIL = p.MAIL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.SIFRE = p.SIFRE;
            uye.OKUL = p.OKUL;
            uye.TELEFON = p.TELEFON;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.DOGUMTARIHI = p.DOGUMTARIHI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeKitapGecmis(int id)
        {
            var ktpgcms = db.TblHareket.Where(x => x.UYE==id).ToList();
            var uyekit = db.TblUyeler.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.u1 = uyekit;
            return View(ktpgcms);
        }
    }
}