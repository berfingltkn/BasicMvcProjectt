using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace WebApplication2.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        MvcDbStokEntities db = new MvcDbStokEntities();// tablolara ulaşmak için db nesnesini oluşturduk
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLKATEGORILER.ToList();
            //tblkategorilerdeki verileri listeler.
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa,4);//Bu şekilde yaparak sadece 4 kategorinin gelmesini sağladık.
           //birinci bloktan geldi çünkü parametreyi öyle ayarladık (topagedlist(1,4)
            return View(degerler);
        }

         [HttpGet] // sayfa yüklendiğinde burası çalışır. Sunucu tarafından belirli bir kaynağın (veritabanı, dosya vb.) verilerini getirmek için kullanılır.
         public ActionResult YeniKategori()
         {
             return View();
         }
        [HttpPost]//sayfada bir şeye tıklanınca burası çalışır. Genellikle sunucuya veri göndermek için kullanılır.
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();// sayfayı döndürür

        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir",ktgr);
        }

        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}