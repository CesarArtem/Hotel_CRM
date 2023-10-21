using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class SotrudnikDoljnost
    {
        public int IdSotrudnikDoljnost { get; set; }
        public int DoljnostId { get; set; }
        public int SotrudnikId { get; set; }

        public virtual Doljnost Doljnost { get; set; }
        public virtual Sotrudnik Sotrudnik { get; set; }
    }
}
