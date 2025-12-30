using System;
using System.Linq;
using System.Web.Mvc;
using Web.DataModel;
using Web.Models; // Veritabanı modelin burada olmalı

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        AktasMobilyaEntities db = new AktasMobilyaEntities();

        // Giriş Sayfası
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string KullaniciAdi, string Sifre)
        {
            // Veritabanında kontrol et
            var admin = db.Adminlers.FirstOrDefault(x => x.KullaniciAdi == KullaniciAdi && x.Sifre == Sifre);

            if (admin != null)
            {
                // Başarılıysa oturum (Session) başlat
                Session["AdminGiris"] = admin.KullaniciAdi;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // Hatalıysa mesaj gönder
                ViewBag.Hata = "Hatalı kullanıcı adı veya şifre!";
                return View();
            }
        }

        // Çıkış Yap
        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}