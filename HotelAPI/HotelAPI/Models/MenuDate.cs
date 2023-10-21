using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class MenuDate
    {
        public MenuDate()
        {
            PriemPitaniya = new HashSet<PriemPitaniya>();
        }

        public int IdMenuDate { get; set; }
        public DateTime Date { get; set; }
        public int TypeId { get; set; }
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual TypeMeal Type { get; set; }
        public virtual ICollection<PriemPitaniya> PriemPitaniya { get; set; }
    }
}
