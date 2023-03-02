using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvc.Controllers
{
    public class PersonelimController : Controller
    {

        Context c = new Context();

        [Authorize]
        public IActionResult Index()
        {
            var degerler = c.Personels.Include(x => x.Birim).ToList();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult YeniPersonel()
        {
            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        public IActionResult YeniPersonel(Personel p)
        {
            var per = c.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault();
            p.Birim = per;
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PersonelBilgi(int id)
        {
            var bir = c.Personels.Find(id);
            return View("PersonelBilgi", bir);
        }

        public ActionResult PersonelSil(int? id)
        {
            Personel kayit = c.Personels.Where(k => k.PersonelID == id).SingleOrDefault();
            c.Personels.Remove(kayit);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}