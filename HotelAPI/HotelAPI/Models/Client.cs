using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Client
    {
        public Client()
        {
            Book = new HashSet<Book>();
        }

        public int IdClient { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Otchestvo { get; set; }
        public DateTime DateRozhdeniya { get; set; }
        public string SeriaPas { get; set; }
        public string NumberPas { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
