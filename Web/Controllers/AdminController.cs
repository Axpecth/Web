using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DataModel; // Veritabanı modellerinin olduğu yer
using Web.Models;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        // Veritabanı bağlantısı
        AktasMobilyaEntities db = new AktasMobilyaEntities();

        // ==========================================
        // 1. DASHBOARD (ANA YÖNETİM MERKEZİ)
        // ==========================================
        public ActionResult Index()
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");
            return View();
        }

        // ==========================================
        // 2. KATALOG YÖNETİMİ
        // ==========================================

        public ActionResult Katalog()
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");
            var model = new MobilyaBilgi();
            model.kataloglars = db.Kataloglars.ToList();
            return View(model);
        }

        // SADECE EKLEME İŞLEMİ
        [HttpPost]
        public ActionResult KatalogIslem(string KAdi, string KYol, string KResim)
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");

            try
            {
                Kataloglar ka = new Kataloglar();
                ka.KatalogAdi = KAdi;
                ka.KatalogYol = KYol;
                ka.KatalogResim = KResim;
                ka.Aktif = true;
                db.Kataloglars.Add(ka);
                db.SaveChanges();
            }
            catch (Exception) { /* Hata loglanabilir */ }

            return RedirectToAction("Katalog");
        }

        // SADECE SİLME İŞLEMİ (YENİ)
        [HttpPost]
        public ActionResult KatalogSil(int id)
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");

            var ka = db.Kataloglars.Find(id);
            if (ka != null)
            {
                ka.Aktif = false;
                db.SaveChanges();
            }
            return RedirectToAction("Katalog");
        }

        // ==========================================
        // 3. ÜRÜN YÖNETİMİ
        // ==========================================

        public ActionResult Urun()
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");
            var model = new MobilyaBilgi();
            model.urunlers = db.Urunlers.ToList();
            model.kataloglars = db.Kataloglars.Where(x => x.Aktif == true).ToList();
            return View(model);
        }

        // SADECE EKLEME İŞLEMİ
        [HttpPost]
        public ActionResult UrunIslem(string UAdi, string UAciklama, decimal? UFiyat, string UResimYolu, int? Kato)
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");

            try
            {
                Urunler u = new Urunler();
                u.UrunAdi = UAdi;
                u.Aciklama = UAciklama;
                u.Fiyat = UFiyat ?? 0; // Eğer boş gelirse 0 yap
                u.ResimYolu = UResimYolu;
                u.KatalogID = Kato ?? 0; // Eğer boş gelirse 0 yap
                u.Aktif = true;
                db.Urunlers.Add(u);
                db.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("Urun");
        }

        // SADECE SİLME İŞLEMİ (YENİ)
        [HttpPost]
        public ActionResult UrunSil(int id)
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");

            var u = db.Urunlers.Find(id);
            if (u != null)
            {
                u.Aktif = false;
                db.SaveChanges();
            }
            return RedirectToAction("Urun");
        }

        // ==========================================
        // 4. KAMPANYA YÖNETİMİ
        // ==========================================

        public ActionResult Kampanya()
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");
            var model = new MobilyaBilgi();
            model.kampanyaliUrunlers = db.KampanyaliUrunlers.ToList();
            model.urunlers = db.Urunlers.Where(x => x.Aktif == true).ToList();
            return View(model);
        }

        // SADECE EKLEME İŞLEMİ
        [HttpPost]
        public ActionResult KampanyaIslem(int? kampurun, int? KIndirimOran, decimal? KEskiFiyat, decimal? KYeniFiyat)
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");

            try
            {
                KampanyaliUrunler ku = new KampanyaliUrunler();
                ku.UrunID = kampurun ?? 0;
                ku.IndirimOrani = KIndirimOran ?? 0;
                ku.EskiFiyat = KEskiFiyat ?? 0;
                ku.GuncelFiyat = KYeniFiyat ?? 0;
                ku.KampanyaBaslangic = DateTime.Today;
                ku.KampanyaBitis = DateTime.Today.AddDays(3);
                ku.Aktif = true;
                db.KampanyaliUrunlers.Add(ku);
                db.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("Kampanya");
        }

        // SADECE SİLME İŞLEMİ (YENİ)
        [HttpPost]
        public ActionResult KampanyaSil(int id)
        {
            if (Session["AdminGiris"] == null) return RedirectToAction("Index", "Login");

            var k = db.KampanyaliUrunlers.Find(id);
            if (k != null)
            {
                k.Aktif = false;
                db.SaveChanges();
            }
            return RedirectToAction("Kampanya");
        }

        // ==========================================
        // 5. GÜVENLİ ÇIKIŞ
        // ==========================================
        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}