using MVC_KUTUPHANE.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_KUTUPHANE.Controllers
{
    public class KayitOlController : Controller
    {
        // GET: KayitOl
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit(TblUyeler p)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }
            db.TblUyeler.Add(p);
            db.SaveChanges();
            return View();

        }
    }
}