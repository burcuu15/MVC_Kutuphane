using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_KUTUPHANE.Controllers;
using MVC_KUTUPHANE.Models.Entity;

namespace MVC_KUTUPHANE.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public readonly DBKutuphaneEntities1 db = new DBKutuphaneEntities1();
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TblUyeler p)
        {
            var bilgiler = db.TblUyeler.FirstOrDefault(x => x.MAIL==p.MAIL && x.SIFRE==p.SIFRE);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Mail"] = bilgiler.MAIL.ToString();
                //TempData["ID"]=bilgiler.ID.ToString();
                //TempData["Ad"]=bilgiler.AD.ToString();
                //TempData["Soyad"]=bilgiler.SOYAD.ToString();
                //TempData["Kullanıcı Adı"]=bilgiler.KULLANICIADI.ToString();
                //TempData["Şifre"]=bilgiler.SIFRE.ToString();
                //TempData["Okul"]=bilgiler.OKUL.ToString();
                return RedirectToAction("Index","Panelim");
            }
            else
            {
                return View();
            }
        }
    }
}