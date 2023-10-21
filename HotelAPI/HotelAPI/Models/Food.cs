using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Food
    {
        public Food()
        {
            DishFood = new HashSet<DishFood>();
        }

        public int IdFood { get; set; }
        public string Name { get; set; }
        public int FridgeId { get; set; }

        public virtual Fridge Fridge { get; set; }
        public virtual ICollection<DishFood> DishFood { get; set; }
    }
}
