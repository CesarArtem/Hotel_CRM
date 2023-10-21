using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Fridge
    {
        public Fridge()
        {
            Food = new HashSet<Food>();
        }

        public int IdFridge { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Food> Food { get; set; }
    }
}
