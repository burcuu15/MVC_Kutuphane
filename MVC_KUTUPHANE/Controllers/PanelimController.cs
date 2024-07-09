using MVC_KUTUPHANE.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_KUTUPHANE.Controllers
{
    public class PanelimController : Controller
    {
        // GET: Panelim
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        [Authorize]
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            var degerler = db.TblUyeler.FirstOrDefault(z => z.MAIL==uyemail);
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TblUyeler p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TblUyeler.FirstOrDefault(x => x.MAIL==kullanici);
            uye.SIFRE=p.SIFRE;
            uye.AD =p.AD;
            uye.FOTOGRAF=p.FOTOGRAF;
            uye.OKUL=p.OKUL;
            uye.KULLANICIADI=p.KULLANICIADI;
            db.SaveChanges();
            return View("Index", "Panelim");
        }

        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TblUyeler.Where(x => x.MAIL==kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TblHareket.Where(x => x.UYE==id).ToList();
            return View(degerler);
        }
    }
}