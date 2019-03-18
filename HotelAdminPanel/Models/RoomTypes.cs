using System;
using System.Collections.Generic;

namespace HotelAdminPanel.Models
{
    public partial class RoomTypes
    {
        public RoomTypes()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
