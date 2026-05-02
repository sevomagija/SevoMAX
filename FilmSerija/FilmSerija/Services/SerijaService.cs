using FilmSerija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FilmSerija.Services
{
    public class SerijaService
    {
        private readonly string _filePath;

        public SerijaService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Serija> GetAll()
        {
            XDocument doc = XDocument.Load(_filePath);
            return doc.Root.Elements("Serija")
                .Select(x => new Serija
                {
                    Id = (int)x.Element("Id"),
                    Naslov = (string)x.Element("Naslov"),
                    Zanr = (string)x.Element("Zanr"),
                    Godina = (int)x.Element("Godina"),
                    BrojSezona = (int)x.Element("BrojSezona"),
                    PutanjaSlike = (string)x.Element("PutanjaSlike")
                }).ToList();
        }

        public void Add(Serija serija)
        {
            XDocument doc = XDocument.Load(_filePath);
            int newId = doc.Root.Elements("Serija").Any() ? doc.Root.Elements("Serija").Max(x => (int)x.Element("Id")) + 1 : 1;
            serija.Id = newId;

            XElement novaSerija = new XElement("Serija",
                new XElement("Id", serija.Id),
                new XElement("Naslov", serija.Naslov),
                new XElement("Zanr", serija.Zanr),
                new XElement("Godina", serija.Godina),
                new XElement("BrojSezona", serija.BrojSezona),
                new XElement("PutanjaSlike", serija.PutanjaSlike)
            );

            doc.Root.Add(novaSerija);
            doc.Save(_filePath);
        }

        public void Update(Serija serija)
        {
            XDocument doc = XDocument.Load(_filePath);
            var element = doc.Root.Elements("Serija").FirstOrDefault(x => (int)x.Element("Id") == serija.Id);
            if (element != null)
            {
                element.SetElementValue("Naslov", serija.Naslov);
                element.SetElementValue("Zanr", serija.Zanr);
                element.SetElementValue("Godina", serija.Godina);
                element.SetElementValue("BrojSezona", serija.BrojSezona);
                element.SetElementValue("PutanjaSlike", serija.PutanjaSlike);
                doc.Save(_filePath);
            }
        }

        public void Delete(int id)
        {
            XDocument doc = XDocument.Load(_filePath);
            var element = doc.Root.Elements("Serija").FirstOrDefault(x => (int)x.Element("Id") == id);
            if (element != null)
            {
                element.Remove();
                doc.Save(_filePath);
            }
        }

        public Serija GetById(int id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }
    }
}
