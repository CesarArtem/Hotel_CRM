using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class TypeMeal
    {
        public TypeMeal()
        {
            MenuDate = new HashSet<MenuDate>();
        }

        public int IdType { get; set; }
        public string MealName { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public TimeSpan TimeStart { get; set; }

        public virtual ICollection<MenuDate> MenuDate { get; set; }
    }
}
