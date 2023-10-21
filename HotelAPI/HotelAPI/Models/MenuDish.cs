using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class MenuDish
    {
        public int IdMenuDish { get; set; }
        public int DishId { get; set; }
        public int MenuId { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
