using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;
using MVC_KUTUPHANE.Controllers;

namespace MVC_KUTUPHANE.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TblMesajlar.Where(x => x.ALICI==uyemail.ToString()).ToList();
            return View(mesajlar);
        }

        public ActionResult Giden()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TblMesajlar.Where(x => x.GONDEREN==uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TblMesajlar t)
        {
            var uyemail = (string)Session["Mail"].ToString();
            t.GONDEREN = uyemail.ToString();
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblMesajlar.Add(t);
            db.SaveChanges();
            return RedirectToAction("Giden","Mesajlar");
        }
    }
}