using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var urunler = db.TblUrunler.ToList();

            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem()
                                             {
                                                 Text=i.KategoriAd,
                                                 Value=i.KategoriID.ToString()

                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrunEkle(TblUrunler u1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KategoriID == u1.TblKategoriler.KategoriID).FirstOrDefault();
            u1.TblKategoriler = ktg;

            db.TblUrunler.Add(u1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem()
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()

                                             }).ToList();
            ViewBag.dgr = degerler;
            var urun = db.TblUrunler.Find(id);
            db.SaveChanges();
            return View("UrunGetir",urun);
        }

        public ActionResult Guncelle(TblUrunler p1)
        {
            var urun = db.TblUrunler.Find(p1.UrunID);
            urun.UrunAdi = p1.UrunAdi;
            urun.Marka = p1.Marka;
            urun.Stok = p1.Stok;
            urun.Fiyat = p1.Fiyat;
            urun.UrunKategori = p1.UrunKategori;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}