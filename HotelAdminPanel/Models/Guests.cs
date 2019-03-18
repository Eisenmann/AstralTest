using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelAdminPanel.Models
{
    public partial class Guests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]               
        public int GuestId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public int? RoomId { get; set; }

        public virtual Rooms Room { get; set; }
    }
}
