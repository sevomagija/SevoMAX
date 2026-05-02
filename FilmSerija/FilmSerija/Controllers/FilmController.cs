using FilmSerija.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmSerija.Services;

namespace FilmSerija.Controllers
{
        public class FilmController : Controller
        {
            private readonly FilmService _service;

            public FilmController()
            {
             
                _service = new FilmService(
                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/films.xml"));
            }

         
            public ActionResult Index(string pretraga, string zanr, string sortiranje)
            {
                var filmovi = _service.GetAll();

                if (!string.IsNullOrEmpty(pretraga))
                    filmovi = filmovi.Where(f => f.Naslov.Contains(pretraga)).ToList();

                if (!string.IsNullOrEmpty(zanr))
                    filmovi = filmovi.Where(f => f.Zanr == zanr).ToList();

                switch (sortiranje)
                {
                    case "godina":
                        filmovi = filmovi.OrderBy(f => f.Godina).ToList();
                        break;
                    case "naslov_desc":
                        filmovi = filmovi.OrderByDescending(f => f.Naslov).ToList();
                        break;
                    default:
                        filmovi = filmovi.OrderBy(f => f.Naslov).ToList();
                        break;
                }

                return View(filmovi);
            }


            public ActionResult Details(int id)
            {
                var film = _service.GetById(id);
                if (film == null)
                    return HttpNotFound();

                return View(film);
            }

    
            public ActionResult Create()
            {
                return View();
            }

       
            [HttpPost]
            public ActionResult Create(Film film, HttpPostedFileBase slika)
            {
                if (ModelState.IsValid)
                {
                    if (slika != null && slika.ContentLength > 0)
                    {
                        string putanja = Path.Combine(Server.MapPath("~/Content/Slike"), Path.GetFileName(slika.FileName));
                        slika.SaveAs(putanja);
                        film.PutanjaSlike = "/Content/Slike/" + Path.GetFileName(slika.FileName);
                    }

                    _service.Add(film);
                    return RedirectToAction("Index");
                }
                return View(film);
            }

    
            public ActionResult Edit(int id)
            {
                var film = _service.GetById(id);
                if (film == null)
                    return HttpNotFound();

                return View(film);
            }

  
            [HttpPost]
            public ActionResult Edit(Film film, HttpPostedFileBase slika)
            {
                if (ModelState.IsValid)
                {
                    if (slika != null && slika.ContentLength > 0)
                    {
                        string putanja = Path.Combine(Server.MapPath("~/Content/Slike"), Path.GetFileName(slika.FileName));
                        slika.SaveAs(putanja);
                        film.PutanjaSlike = "/Content/Slike/" + Path.GetFileName(slika.FileName);
                    }

                    _service.Update(film);
                    return RedirectToAction("Index");
                }
                return View(film);
            }

  
            public ActionResult Delete(int id)
            {
                var film = _service.GetById(id);
                if (film == null)
                    return HttpNotFound();

                return View(film);
            }

            [HttpPost]
            public ActionResult DeleteConfirmed(int id)
            {
                _service.Delete(id);
                return RedirectToAction("Index");
            }

        
            public ActionResult Gallery()
            {
                var filmovi = _service.GetAll();
                return View(filmovi);
            }
        }
}