using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;

namespace MVC_KUTUPHANE.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index(string p)
        {
            var kitaplar = from k in db.TblKitap select k;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(p));
            }
            //var kitaplar = db.TblKitap.ToList();
            return View(kitaplar.ToList());
        }
        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TblKategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text=i.AD,
                                               Value=i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1=deger1;

            List<SelectListItem> deger2 = (from i in db.TblYazar.ToList()
                                           select new SelectListItem
                                           {
                                               Text=i.AD +' '+ i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from i in db.TblYayinevi.ToList()
                                           select new SelectListItem
                                           {
                                               Text=i.YAYINEVI,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();

        }


        [HttpPost]
        public ActionResult KitapEkle(TblKitap p)
        {
            var ktg = db.TblKategori.Where(k => k.ID==p.TblKategori.ID).FirstOrDefault();
            var yzr = db.TblYazar.Where(y => y.ID==p.TblYazar.ID).FirstOrDefault();
            var yayin = db.TblYayinevi.Where(ya => ya.ID==p.TblYayinevi.ID).FirstOrDefault();

            p.TblKategori=ktg;
            p.TblYazar=yzr;
            p.TblYayinevi=yayin;

            db.TblKitap.Add(p);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(int id)
        {
            var kitap = db.TblKitap.Find(id);
            db.TblKitap.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TblKitap.Find(id);
            List<SelectListItem> deger1 = (from i in db.TblKategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text=i.AD,
                                               Value=i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1=deger1;

            List<SelectListItem> deger2 = (from i in db.TblYazar.ToList()
                                           select new SelectListItem
                                           {
                                               Text=i.AD +' '+ i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from i in db.TblYayinevi.ToList()
                                           select new SelectListItem
                                           {
                                               Text=i.YAYINEVI,
                                               Value=i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3=deger3;
            return View("KitapGetir", ktp);

        }

        public ActionResult KitapGuncelle(TblKitap p)
        {
            var kitap = db.TblKitap.Find(p.ID);
            kitap.AD = p.AD;
            var ktg = db.TblKategori.Where(k => k.ID == p.TblKategori.ID).FirstOrDefault();
            var yzr = db.TblYazar.Where(y => y.ID == p.TblYazar.ID).FirstOrDefault();
            kitap.BASIMYIL = p.BASIMYIL;
            var yayin = db.TblYayinevi.Where(ya => ya.ID == p.TblYayinevi.ID).FirstOrDefault();
            kitap.SAYFA = p.SAYFA;
            kitap.ADET = p.ADET;
            kitap.DURUM= p.DURUM;
            kitap.DURUM =true;
            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;
            kitap.YAYINEVI = yayin.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}