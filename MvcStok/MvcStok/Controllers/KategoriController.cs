﻿    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var kategoriler = db.TblKategoriler.ToList();
            var kategoriler = db.TblKategoriler.ToList().ToPagedList(sayfa,5);

            return View(kategoriler);
        }


        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TblKategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }   
            db.TblKategoriler.Add(p1);
            db.SaveChanges();
            return View();
        }



        public ActionResult Sil(int id)
        {
            var ktg = db.TblKategoriler.Find(id);
            db.TblKategoriler.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TblKategoriler.Find(id);
            return View("KategoriGetir",ktgr);
        }

        public ActionResult Guncelle(TblKategoriler p1)
        {
            var ktg = db.TblKategoriler.Find(p1.KategoriID);
            ktg.KategoriAd = p1.KategoriAd;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}