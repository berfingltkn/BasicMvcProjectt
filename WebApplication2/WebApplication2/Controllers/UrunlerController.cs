using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Entity;

namespace WebApplication2.Controllers

{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();

            return View(degerler);
        }
        [HttpGet]
        public ActionResult newProduct()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                                 //listeden öge seç --> select listItem
                                                 //TBLKATEGORI leriden verileri alıp i değişkenine atadık (LINQ)

                                             select new SelectListItem//yeni listeyi seç
                                             {
                                                 Text = i.KATEGORIAD,//seçmiş olduğun listenin text değeri= i den gelen kategoriAd
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            //text ile görünen kısma yazdırıyoruz value ile de arkaplanda döndürülen değer.

            //viewbag ifadesi controller kısmındaki ifadeyi diğer sayfaya (newProduct.cshtml) taşır.
            ViewBag.dgr = degerler;//dgr ismindeki viewbag değerler isimli listeyi başka bir sayfaya taşımamıza olanak sağlar.
            return View();

        }
        [HttpPost]
        public ActionResult newProduct(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            Console.WriteLine(p1.TBLKATEGORILER.KATEGORIID);
            p1.TBLKATEGORILER = ktg;
            // "m" her bir TBLKATEGORILER kaydını temsil eden bir değişkendir
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                                 //listeden öge seç --> select listItem
                                                 //TBLKATEGORI leriden verileri alıp i değişkenine atadık (LINQ)

                                             select new SelectListItem//yeni listeyi seç
                                             {
                                                 Text = i.KATEGORIAD,//seçmiş olduğun listenin text değeri= i den gelen kategoriAd
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            //text ile görünen kısma yazdırıyoruz value ile de arkaplanda döndürülen değer.

            //viewbag ifadesi controller kısmındaki ifadeyi diğer sayfaya (newProduct.cshtml) taşır.
            ViewBag.dgr = degerler;//dgr ismindeki viewbag değerler isimli listeyi başka bir sayfaya taşımamıza olanak sağlar.
            return View("UrunGetir",urun);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.FIYAT = p.FIYAT;

            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}