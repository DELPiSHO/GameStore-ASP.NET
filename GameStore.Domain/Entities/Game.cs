using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Domain.Entities
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }
        [MinLength(2)]
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Proszę podać nazwę gry")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Proszę podać opis gry")]
        public string Description { get; set; }
        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Proszę podać kategorię gry")]
        public string Category { get; set; }

        [Display(Name = "Cena (zł)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena nie może być ujemna")]
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
