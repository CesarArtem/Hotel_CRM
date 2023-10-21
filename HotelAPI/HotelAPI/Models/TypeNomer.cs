using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class TypeNomer
    {
        public TypeNomer()
        {
            Nomer = new HashSet<Nomer>();
        }

        public int IdType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Nomer> Nomer { get; set; }
    }
}
