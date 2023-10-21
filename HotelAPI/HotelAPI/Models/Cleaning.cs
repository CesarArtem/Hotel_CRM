using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Cleaning
    {
        public Cleaning()
        {
            CleaningEquipment = new HashSet<CleaningEquipment>();
        }

        public int IdCleaning { get; set; }
        public DateTime Date { get; set; }
        public int SotrudnikId { get; set; }
        public int NomerId { get; set; }

        public virtual Nomer Nomer { get; set; }
        public virtual Sotrudnik Sotrudnik { get; set; }
        public virtual ICollection<CleaningEquipment> CleaningEquipment { get; set; }
    }
}
