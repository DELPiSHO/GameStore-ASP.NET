using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace GameStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Proszę Podać Imie")]
        [RegularExpression(@"[\p{L} ]+$", ErrorMessage = "Nie można używać cyfr")]
        [Display(Name = "Imie")]
        [MinLength(2)]
        [MaxLength(30)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Proszę Podać Nazwisko")]
        [RegularExpression(@"[\p{L} ]+$", ErrorMessage = "Nie można używać cyfr")]
        [Display(Name = "Nazwisko")]
        [MinLength(2)]
        [MaxLength(30)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Proszę podać Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę Podać Numer Karty")]
        [Range(1000000000000000, 9999999999999999, ErrorMessage = "Musi być 16 cyfr")]
        [Display(Name = "Numer Karty Płatniczej")]
        public long CartNumber { get; set; }

        [Required(ErrorMessage = "Proszę Podać Datę Ważności Karty")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Ważności Karty Płatniczej")]
        public DateTime ExpireDate { get; set; }


        [Required(ErrorMessage = "Proszę Podać Kod CVC")]
        [MinLength(3)]
        [MaxLength(3)]
        [Display(Name = "Proszę Podać Kod Trzycyfrowy CVC")]
        public int CVC;

        [Required(ErrorMessage = "Proszę Podać Adres")]
        [RegularExpression(@"[\p{L} ]+$", ErrorMessage = "Nie można używać cyfr")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Proszę Podać Kod pocztowy")]
        [RegularExpression(@"[0-9]{2}\-[0-9]{3}", ErrorMessage = "Niepoprawny kod pocztowy. Prawidłowy format to ##-###")]
        [Display(Name = "Kod Pocztowy")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Proszę Podać Miasto")]
        [Display(Name = "Miasto")]
        [RegularExpression(@"[\p{L} ]+$", ErrorMessage = "Nie można używać cyfr")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę Podać Państwo")]
        [RegularExpression(@"[\p{L} ]+$", ErrorMessage = "Nie można używać cyfr")]
        [Display(Name = "Państwo")]
        public string Country { get; set; }

        [Required(ErrorMessage ="Numer domu/domu i lokalu")]
        [Display(Name ="Numer domu/Domu i lokalu")]
        public string NumerDomu { get; set; }

        public bool GiftWrap { get; set; }

        public string getEmail()
        {
            return Email;
        }
    }
}