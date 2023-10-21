using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Sotrudnik
    {
        public Sotrudnik()
        {
            Cleaning = new HashSet<Cleaning>();
            SotrudnikDoljnost = new HashSet<SotrudnikDoljnost>();
            Users = new HashSet<Users>();
        }

        public int IdSotrudnik { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Otchestvo { get; set; }
        public DateTime DateRozhdenia { get; set; }

        public virtual ICollection<Cleaning> Cleaning { get; set; }
        public virtual ICollection<SotrudnikDoljnost> SotrudnikDoljnost { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
