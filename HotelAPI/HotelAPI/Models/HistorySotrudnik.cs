using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class HistorySotrudnik
    {
        public int Id { get; set; }
        public int SotrudnikId { get; set; }
        public string Operation { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
