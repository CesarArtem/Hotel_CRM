using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class CleaningEquipment
    {
        public int IdCleaningEquipment { get; set; }
        public int EquipmentId { get; set; }
        public int CleaningId { get; set; }

        public virtual Cleaning Cleaning { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
