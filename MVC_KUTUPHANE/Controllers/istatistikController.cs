using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;

namespace MVC_KUTUPHANE.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index()
        {
            var deger1 = db.TblUyeler.Count();
            var deger2 = db.TblKitap.Count();
            var deger3 = db.TblKitap.Where(x => x.DURUM==false).Count();
            var deger4 = db.TblCeza.Sum(x => x.PARA);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            return View();
        }

        public ActionResult Hava()
        {
            return View();
        }

        public ActionResult HavaKart()
        {
            return View();
        }

        public ActionResult Galeri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength>0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }

            return RedirectToAction("Galeri");

        }

        public ActionResult LinqKart()
        {
            var deger1 = db.TblKitap.Count();
            var deger2 = db.TblUyeler.Count();
            var deger3 = db.TblCeza.Sum(x=>x.PARA);
            var deger4 = db.TblKitap.Where(x=>x.DURUM==false).Count();
            var deger5 = db.TblKategori.Count();

            var deger8 = db.EnFazlaKitapYazar().FirstOrDefault();
            var deger9 = db.TblYayinevi
    .GroupBy(x => x.YAYINEVI)
    .Select(g => new { Yayinevi = g.Key, KitapSayisi = g.Count() })
    .OrderByDescending(x => x.KitapSayisi)
    .FirstOrDefault();
            var deger11 = db.TblIletisim.Count();

            ViewBag.dgr1= deger1;
            ViewBag.dgr2= deger2;
            ViewBag.dgr3= deger3;
            ViewBag.dgr4= deger4;
            ViewBag.dgr5= deger5;
            ViewBag.dgr8= deger8;
            ViewBag.dgr9= deger9;

            ViewBag.dgr11= deger11;
            return View();
        }

    }
}