using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml.Linq;
using FilmSerija.Models;

namespace FilmSerija.Controllers
{
    public class AccountController : Controller
    {
        private readonly string xmlPutanja = HostingEnvironment.MapPath("~/App_Data/users.xml");

     
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Korisnik korisnik)
        {
            XDocument doc = XDocument.Load(xmlPutanja);

       
            var pronadjen = doc.Descendants("Korisnik")
                .FirstOrDefault(k => k.Element("KorisnickoIme").Value == korisnik.KorisnickoIme
                                  && k.Element("Lozinka").Value == korisnik.Lozinka);

            if (pronadjen != null)
            {
                Session["Korisnik"] = pronadjen.Element("KorisnickoIme").Value;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Neispravno korisničko ime ili lozinka.");
            return View(korisnik);
        }

  
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Korisnik noviKorisnik)
        {
            if (ModelState.IsValid)
            {
                XDocument doc = XDocument.Load(xmlPutanja);

        
                bool postoji = doc.Descendants("Korisnik")
                    .Any(k => k.Element("KorisnickoIme").Value == noviKorisnik.KorisnickoIme);

                if (postoji)
                {
                    ModelState.AddModelError("", "Korisničko ime je već zauzeto.");
                    return View(noviKorisnik);
                }

              
                int noviId = 1;
                if (doc.Descendants("Id").Any())
                {
                    noviId = doc.Descendants("Id").Max(x => int.Parse(x.Value)) + 1;
                }

             
                XElement korisnikElement = new XElement("Korisnik",
                    new XElement("Id", noviId),
                    new XElement("KorisnickoIme", noviKorisnik.KorisnickoIme),
                    new XElement("Lozinka", noviKorisnik.Lozinka),
                    new XElement("Email", noviKorisnik.Email),
                    new XElement("PunoIme", noviKorisnik.PunoIme)
                );

             
                doc.Element("Users").Add(korisnikElement);
                doc.Save(xmlPutanja);

                return RedirectToAction("Login");
            }
            return View(noviKorisnik);
        }


        public ActionResult Profile()
        {
            string kIme = (string)Session["Korisnik"];
            if (string.IsNullOrEmpty(kIme)) return RedirectToAction("Login");

            XDocument doc = XDocument.Load(xmlPutanja);
            var podaci = doc.Descendants("Korisnik")
                .FirstOrDefault(k => k.Element("KorisnickoIme").Value == kIme);

            if (podaci == null) return HttpNotFound();

        
            var model = new Korisnik
            {
                Id = int.Parse(podaci.Element("Id").Value),
                KorisnickoIme = podaci.Element("KorisnickoIme").Value,
                Email = podaci.Element("Email").Value,
                PunoIme = podaci.Element("PunoIme").Value
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(Korisnik model)
        {
            if (ModelState.IsValid)
            {
                XDocument doc = XDocument.Load(xmlPutanja);
                string trenutnoIme = (string)Session["Korisnik"];

                var element = doc.Descendants("Korisnik")
                    .FirstOrDefault(k => k.Element("KorisnickoIme").Value == trenutnoIme);

                if (element != null)
                {
               
                    element.Element("Email").Value = model.Email;
                    element.Element("PunoIme").Value = model.PunoIme;
                    if (!string.IsNullOrEmpty(model.Lozinka))
                        element.Element("Lozinka").Value = model.Lozinka;

                    doc.Save(xmlPutanja);
                    return RedirectToAction("Profile");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}