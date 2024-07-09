using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;
using MVC_KUTUPHANE.Controllers;

namespace MVC_KUTUPHANE.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index()
        {
            var degerler = db.TblHareket.Where(x => x.ISLEMDURUM==false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from x in db.TblUyeler.ToList()
                                           select new SelectListItem
                                           {
                                               Text=x.AD +" " + x.SOYAD,
                                               Value =x.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from y in db.TblKitap.ToList()
                                           select new SelectListItem
                                           {
                                               Text=y.AD,
                                               Value =y.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from z in db.TblPersonel.ToList()
                                           select new SelectListItem
                                           {
                                               Text=z.PERSONEL,
                                               Value =z.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(TblHareket p)
        {
            var d1 = db.TblUyeler.Where(x => x.ID == p.TblUyeler.ID).FirstOrDefault();
            var d2 = db.TblKitap.Where(x => x.ID == p.TblKitap.ID).FirstOrDefault();
            var d3 = db.TblPersonel.Where(x => x.ID == p.TblPersonel.ID).FirstOrDefault();
            p.TblUyeler =d1;
            p.TblKitap =d2;
            p.TblPersonel =d3;

            if (d2 != null)
            {
                if (d2.ADET > 0)
                {
                    // Adet kontrolü
                    d2.ADET--;
                    d2.DURUM = d2.ADET > 0;

                    db.TblHareket.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Uyarı mesajı
                    ViewBag.Message = "Kitap başkasına ödünç verilmiş.";
                    return View();
                }
            }
            else
            {
                // Kitap bulunamadı
                ViewBag.Message = "Kitap bulunamadı.";
                return View();
            }
            
        }
        public ActionResult Odunciade(int id)
        {
            var odn = db.TblHareket.Find(id);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToLongDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("Odunciade", odn);

        }

        public ActionResult OduncGuncelle(TblHareket p)
        {
            var hrk = db.TblHareket.Find(p.ID);
           //var kitap = db.TblKitap.Find(hrk.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            hrk.TblKitap.ADET++;
            hrk.TblKitap.DURUM=true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
