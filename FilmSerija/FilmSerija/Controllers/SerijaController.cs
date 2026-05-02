using FilmSerija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmSerija.Services;
using System.IO;

namespace FilmSerija.Controllers
{
    public class SerijaController : Controller
    {
        private readonly SerijaService _service;

        public SerijaController()
        {
        
            _service = new SerijaService(
                System.Web.HttpContext.Current.Server.MapPath("~/App_Data/series.xml"));
        }

        public ActionResult Index()
        {
            var serije = _service.GetAll();
            return View(serije);
        }

       
        public ActionResult Details(int id)
        {
            var serija = _service.GetById(id);
            if (serija == null)
                return HttpNotFound();

            return View(serija);
        }

       
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        public ActionResult Create(Serija serija, HttpPostedFileBase slika)
        {
            if (ModelState.IsValid)
            {
                if (slika != null && slika.ContentLength > 0)
                {
                    string putanja = Path.Combine(Server.MapPath("~/Content/Slike"), Path.GetFileName(slika.FileName));
                    slika.SaveAs(putanja);
                    serija.PutanjaSlike = "/Content/Slike/" + Path.GetFileName(slika.FileName);
                }

                _service.Add(serija);
                return RedirectToAction("Index");
            }
            return View(serija);
        }

       
        public ActionResult Edit(int id)
        {
            var serija = _service.GetById(id);
            if (serija == null)
                return HttpNotFound();

            return View(serija);
        }

        [HttpPost]
        public ActionResult Edit(Serija serija, HttpPostedFileBase slika)
        {
            if (ModelState.IsValid)
            {
                if (slika != null && slika.ContentLength > 0)
                {
                    string putanja = Path.Combine(Server.MapPath("~/Content/Slike"), Path.GetFileName(slika.FileName));
                    slika.SaveAs(putanja);
                    serija.PutanjaSlike = "/Content/Slike/" + Path.GetFileName(slika.FileName);
                }

                _service.Update(serija);
                return RedirectToAction("Index");
            }
            return View(serija);
        }

 
        public ActionResult Delete(int id)
        {
            var serija = _service.GetById(id);
            if (serija == null)
                return HttpNotFound();

            return View(serija);
        }

    
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}