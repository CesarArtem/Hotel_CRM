using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuDate = new HashSet<MenuDate>();
            MenuDish = new HashSet<MenuDish>();
        }

        public int IdMenu { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MenuDate> MenuDate { get; set; }
        public virtual ICollection<MenuDish> MenuDish { get; set; }
    }
}
