using System;

namespace SportsItemsStore.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int SizeId { get; set; }

        public int ColorId { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}