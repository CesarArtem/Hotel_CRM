using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class DishFood
    {
        public int IdDishFood { get; set; }
        public int Weight { get; set; }
        public int DishId { get; set; }
        public int FoodId { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Food Food { get; set; }
    }
}
