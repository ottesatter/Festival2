using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolTemplate.Models
{
    public class PersonModel
    {
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Kowed gooi izjem normaal achternaam moet anders komt motje je halen")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Kowed gooi izjem normaal E-mail want dat moet")]
        [EmailAddress(ErrorMessage = "Kowed gooi izjem normaal E-mail moet anders komt motje je halen")]
        public string Email { get; set; }
        
        public DateTime Geboortedatum { get; set; }
    }
}
