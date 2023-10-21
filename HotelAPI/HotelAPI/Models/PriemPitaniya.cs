using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class PriemPitaniya
    {
        public int IdPriem { get; set; }
        public DateTime Date { get; set; }
        public int MenuDateId { get; set; }
        public int NomerId { get; set; }

        public virtual MenuDate MenuDate { get; set; }
        public virtual Nomer Nomer { get; set; }
    }
}
