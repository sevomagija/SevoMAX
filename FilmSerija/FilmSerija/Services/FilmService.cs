using FilmSerija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FilmSerija.Services
{
    public class FilmService
    {
        private readonly string _filePath;

        public FilmService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Film> GetAll()
        {
            XDocument doc = XDocument.Load(_filePath);
            return doc.Root.Elements("Film")
                .Select(x => new Film
                {
                    Id = (int)x.Element("Id"),
                    Naslov = (string)x.Element("Naslov"),
                    Zanr = (string)x.Element("Zanr"),
                    Godina = (int)x.Element("Godina"),
                    Reziser = (string)x.Element("Reziser"),
                    PutanjaSlike = (string)x.Element("PutanjaSlike")
                }).ToList();
        }

        public void Add(Film film)
        {
            XDocument doc = XDocument.Load(_filePath);
            int newId = doc.Root.Elements("Film").Any() ? doc.Root.Elements("Film").Max(x => (int)x.Element("Id")) + 1 : 1;
            film.Id = newId;

            XElement noviFilm = new XElement("Film",
                new XElement("Id", film.Id),
                new XElement("Naslov", film.Naslov),
                new XElement("Zanr", film.Zanr),
                new XElement("Godina", film.Godina),
                new XElement("Reziser", film.Reziser),
                new XElement("PutanjaSlike", film.PutanjaSlike)
            );

            doc.Root.Add(noviFilm);
            doc.Save(_filePath);
        }

        public void Update(Film film)
        {
            XDocument doc = XDocument.Load(_filePath);
            var element = doc.Root.Elements("Film").FirstOrDefault(x => (int)x.Element("Id") == film.Id);
            if (element != null)
            {
                element.SetElementValue("Naslov", film.Naslov);
                element.SetElementValue("Zanr", film.Zanr);
                element.SetElementValue("Godina", film.Godina);
                element.SetElementValue("Reziser", film.Reziser);
                element.SetElementValue("PutanjaSlike", film.PutanjaSlike);
                doc.Save(_filePath);
            }
        }

        public void Delete(int id)
        {
            XDocument doc = XDocument.Load(_filePath);
            var element = doc.Root.Elements("Film").FirstOrDefault(x => (int)x.Element("Id") == id);
            if (element != null)
            {
                element.Remove();
                doc.Save(_filePath);
            }
        }

        public Film GetById(int id)
        {
            return GetAll().FirstOrDefault(f => f.Id == id);
        }
    }
}