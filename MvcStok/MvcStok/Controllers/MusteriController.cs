using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(string p)
        {
            var degerler = from i in db.TblMusteriler select i;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p)|| m.MusteriSoyad.Contains(p));
            }
            return View(degerler.ToList());
            
            
            //var musteriler = db.TblMusteriler.ToList();
            //return View(musteriler);

        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler m1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(m1);
            db.SaveChanges();
            return View();
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            return View("MusteriGetir",musteri);
        }

        public ActionResult Guncelle(TblMusteriler p1)
        {
            var musteri = db.TblMusteriler.Find(p1.MusteriID);

            musteri.MusteriAd = p1.MusteriAd;
            musteri.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}