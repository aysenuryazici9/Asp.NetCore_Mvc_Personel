using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers
{
    public class DefaultController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var degerler = c.Birims.ToList();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult YeniBirim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YeniBirim(Birim b)
        {
            c.Birims.Add(b);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimSil(int id)
        {
            var bir = c.Birims.Find(id);
            c.Birims.Remove(bir);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimGetir(int id)
        {
            var bir = c.Birims.Find(id);
            return View("BirimGetir", bir);
        }

        public IActionResult BirimGuncelle(Birim b)
        {
            var bir = c.Birims.Find(b.BirimID);
            bir.BirimAd = b.BirimAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.BirimID == id).ToList();
            var brmad = c.Birims.Where(x => x.BirimID == id).Select(y => y.BirimAd).FirstOrDefault();
            ViewBag.brm = brmad;
            return View(degerler);
        }

    }
}