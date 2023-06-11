using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        GenericRepository<Heading> heading = new GenericRepository<Heading>();
        GenericRepository<Writer> writer = new GenericRepository<Writer>();
        public ActionResult Index()
        {

            int yazilimId = 1009;
            var baslikSayisi = heading.List().Where(x => x.CategoryID == yazilimId).Count();
            var yazar = writer.List().Where(x => x.WriterName.ToLower().Contains("a")).Count();
            var trueSayisi = cm.GetList().Where(x => x.CategoryStatus == true).Count();
            var falseSayisi = cm.GetList().Where(x => x.CategoryStatus == false).Count();
            var baslikSayisiCategoriId = heading.List().OrderByDescending(x=>x.CategoryID).GroupBy(x=>x.CategoryID).FirstOrDefault().Select(x=>new { ad=x.Category.CategoryName});

            



            ViewBag.toplamKategori = cm.GetList().Count();
            ViewBag.BaslikSayisi = baslikSayisi;
            ViewBag.YazarSayisi = yazar;
            ViewBag.Fark = trueSayisi > falseSayisi ? trueSayisi-falseSayisi : falseSayisi-trueSayisi;
            ViewBag.BaslikSayisiCategoriId = baslikSayisiCategoriId.Select(x=>x.ad).FirstOrDefault();
            return View();
        }
    }
}