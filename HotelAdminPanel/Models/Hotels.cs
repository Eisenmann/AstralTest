using System;
using System.Collections.Generic;

namespace HotelAdminPanel.Models
{
    public partial class Hotels
    {
        public Hotels()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int HotelId { get; set; }
        public string HotelName { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
