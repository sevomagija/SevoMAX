using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmSerija.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naslov je obavezan")]
        public string Naslov { get; set; }

        [Required(ErrorMessage = "Žanr je obavezan")]
        public string Zanr { get; set; }

        [Range(1900, 2100, ErrorMessage = "Godina mora biti između 1900 i 2100")]
        public int Godina { get; set; }

        [Display(Name = "Režiser")]
        public string Reziser { get; set; }

        [Display(Name = "Putanja slike")]
        public string PutanjaSlike { get; set; }
    }
}