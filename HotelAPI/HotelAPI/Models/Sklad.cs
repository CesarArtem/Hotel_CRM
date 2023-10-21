using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Sklad
    {
        public Sklad()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int IdSklad { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
