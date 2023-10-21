using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            CleaningEquipment = new HashSet<CleaningEquipment>();
        }

        public int IdEquipment { get; set; }
        public string Name { get; set; }
        public int SkladId { get; set; }

        public virtual Sklad Sklad { get; set; }
        public virtual ICollection<CleaningEquipment> CleaningEquipment { get; set; }
    }
}
