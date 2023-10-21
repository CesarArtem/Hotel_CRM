using System;
using System.Collections.Generic;

namespace HotelAPI.Models
{
    public partial class Users
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public int SotrudnikId { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }

        public virtual Role Role { get; set; }
        public virtual Sotrudnik Sotrudnik { get; set; }
    }
}
