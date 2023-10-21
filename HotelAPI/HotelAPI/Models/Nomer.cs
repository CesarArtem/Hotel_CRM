using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Nomer
    {
        public Nomer()
        {
            Book = new HashSet<Book>();
            Cleaning = new HashSet<Cleaning>();
            PriemPitaniya = new HashSet<PriemPitaniya>();
        }

        public int IdNomer { get; set; }
        public int Number { get; set; }
        public int TypeId { get; set; }

        public virtual TypeNomer Type { get; set; }
        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<Cleaning> Cleaning { get; set; }
        public virtual ICollection<PriemPitaniya> PriemPitaniya { get; set; }
    }
}
