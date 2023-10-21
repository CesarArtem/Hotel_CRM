using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Book
    {
        public int IdBook { get; set; }
        public DateTime DateStart { get; set; }
        public int NomerId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual Client Client { get; set; }
        public virtual Nomer Nomer { get; set; }
    }
}
