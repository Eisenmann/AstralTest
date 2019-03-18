using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HotelAdminPanel.Models
{
    public partial class Users : IdentityUser
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
    }
}
