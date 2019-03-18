using System;
using System.Collections.Generic;

namespace HotelAdminPanel.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            Guests = new HashSet<Guests>();
        }

        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public int Type { get; set; }
        public bool Status { get; set; }
        public int HotelId { get; set; }

        public virtual Hotels Hotel { get; set; }
        public virtual RoomTypes TypeNavigation { get; set; }
        public virtual ICollection<Guests> Guests { get; set; }
    }
}
