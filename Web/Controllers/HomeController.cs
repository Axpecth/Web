using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Web.DataModel;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private AktasMobilyaEntities db = new AktasMobilyaEntities();

        public ActionResult AnaSayfa()
        {
            MobilyaBilgi mobilyabilgi = new MobilyaBilgi();
            mobilyabilgi.kampanyaliUrunlers = db.KampanyaliUrunlers.ToList();
            mobilyabilgi.urunlers = db.Urunlers.ToList();
            return View(mobilyabilgi);
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult Kategori()
        {
            var aktifKataloglar = db.Kataloglars.Where(k => k.Aktif == true).ToList();
            return View(aktifKataloglar);
        }

        // Ürün Kategori Sayfaları (Tek tek action yazmak yerine tek bir action ile de yapılabilirdi ama yapını bozmuyorum)
        public ActionResult SalonTakimi() { return View(db.Urunlers.ToList()); }
        public ActionResult TVUnitesi() { return View(db.Urunlers.ToList()); }
        public ActionResult YemekMasasi() { return View(db.Urunlers.ToList()); }
        public ActionResult Sandalye() { return View(db.Urunlers.ToList()); }
        public ActionResult YatakTakimi() { return View(db.Urunlers.ToList()); }
        public ActionResult CocukTakimi() { return View(db.Urunlers.ToList()); }
        public ActionResult Dolap() { return View(db.Urunlers.ToList()); }
        public ActionResult CalismaMasasi() { return View(db.Urunlers.ToList()); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(IletisimModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Lütfen tüm zorunlu alanları doldurun.";
                return View(model);
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("aktasmobilya666@gmail.com");
                mail.From = new MailAddress("aktasmobilya666@gmail.com");
                mail.Subject = "Aktaş Mobilya: " + model.Konu;
                mail.Body = $"Gönderen: {model.Ad_Soyad}<br>Email: {model.Email}<br>Mesaj: {model.Mesaj}";
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("aktasmobilya666@gmail.com", "xisx jroe wbjk kbly");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                TempData["Message"] = "Mesaj başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Hata: " + ex.Message;
            }

            return View();
        }
    }
}