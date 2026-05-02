using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmSerija.Models
{
    public class Korisnik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno")]
        
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese")]
        public string Email { get; set; }

        [Display(Name = "Puno ime")]
        public string PunoIme { get; set; }
    }
}