using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Dish
    {
        public Dish()
        {
            DishFood = new HashSet<DishFood>();
            MenuDish = new HashSet<MenuDish>();
        }

        public int IdDish { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }

        public virtual ICollection<DishFood> DishFood { get; set; }
        public virtual ICollection<MenuDish> MenuDish { get; set; }
    }
}
