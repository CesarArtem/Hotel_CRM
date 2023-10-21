using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Doljnost
    {
        public Doljnost()
        {
            SotrudnikDoljnost = new HashSet<SotrudnikDoljnost>();
        }

        public int IdDoljnost { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public virtual ICollection<SotrudnikDoljnost> SotrudnikDoljnost { get; set; }
    }
}
