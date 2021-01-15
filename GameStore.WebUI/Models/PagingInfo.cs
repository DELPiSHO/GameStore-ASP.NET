using System;

namespace GameStore.WebUI.Models
{
    public class PagingInfo
    {
        // Ilość gier
        public int TotalItems { get; set; }

        // Ilość gier na jednej stronie
        public int ItemsPerPage { get; set; }

        // Numer aktualnej strony
        public int CurrentPage { get; set; }

        // Ilość stron ogólnie
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}