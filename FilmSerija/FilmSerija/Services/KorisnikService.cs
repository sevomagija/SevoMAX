using FilmSerija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FilmSerija.Services
{
    public class KorisnikService
    {
        private readonly string _filePath;

        public KorisnikService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Korisnik> GetAll()
        {
            XDocument doc = XDocument.Load(_filePath);
            return doc.Root.Elements("Korisnik")
                .Select(x => new Korisnik
                {
                    Id = (int)x.Element("Id"),
                    KorisnickoIme = (string)x.Element("KorisnickoIme"),
                    Lozinka = (string)x.Element("Lozinka"),
                    Email = (string)x.Element("Email"),
                    PunoIme = (string)x.Element("PunoIme")
                }).ToList();
        }

        public void Add(Korisnik korisnik)
        {
            XDocument doc = XDocument.Load(_filePath);
            int newId = doc.Root.Elements("Korisnik").Any() ? doc.Root.Elements("Korisnik").Max(x => (int)x.Element("Id")) + 1 : 1;
            korisnik.Id = newId;

            XElement noviKorisnik = new XElement("Korisnik",
                new XElement("Id", korisnik.Id),
                new XElement("KorisnickoIme", korisnik.KorisnickoIme),
                new XElement("Lozinka", korisnik.Lozinka),
                new XElement("Email", korisnik.Email),
                new XElement("PunoIme", korisnik.PunoIme)
            );

            doc.Root.Add(noviKorisnik);
            doc.Save(_filePath);
        }

        public void Update(Korisnik korisnik)
        {
            XDocument doc = XDocument.Load(_filePath);
            var element = doc.Root.Elements("Korisnik").FirstOrDefault(x => (int)x.Element("Id") == korisnik.Id);
            if (element != null)
            {
                element.SetElementValue("KorisnickoIme", korisnik.KorisnickoIme);
                element.SetElementValue("Lozinka", korisnik.Lozinka);
                element.SetElementValue("Email", korisnik.Email);
                element.SetElementValue("PunoIme", korisnik.PunoIme);
                doc.Save(_filePath);
            }
        }

        public Korisnik GetByUsername(string username, string password)
        {
            return GetAll().FirstOrDefault(k => k.KorisnickoIme == username && k.Lozinka == password);
        }
    }
}
