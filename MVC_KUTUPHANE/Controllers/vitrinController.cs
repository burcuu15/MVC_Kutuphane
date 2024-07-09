using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_KUTUPHANE.Models.Entity;
using MVC_KUTUPHANE.Models.Siniflarim;

namespace MVC_KUTUPHANE.Controllers
{
    public class vitrinController : Controller
    {
        // GET: vitrin
        DBKutuphaneEntities1 db = new DBKutuphaneEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TblKitap.ToList();
            cs.Deger2 = db.TblHakkimizda.ToList();
            // var degerler = db.TblKitap.ToList();
            return View(cs);
        }

        [HttpPost]
        public ActionResult Index(TblIletisim t)
        {
            db.TblIletisim.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}