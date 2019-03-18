using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelAdminPanel.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelAdminPanel.Controllers
{
    [Route("home/api/[controller]")]
    public class GuestController : Controller
    {

        hotel_dbContext db;
        public GuestController(hotel_dbContext context)
        {
            this.db = context;

        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<object> Get()
        {

            var guests = db.Guests
                .Include(c => c.Room)           
                .ToList();


            return guests;
            
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Guests guest)
        {
            if (guest == null)
            {
                return;
            }

            db.Guests.Add(guest);
            db.SaveChanges();
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Guests guest)
        {
            if (guest == null)
            {
                return;
            }
            if (!db.Rooms.Any(x => x.RoomId == guest.RoomId))
            {
                return;
            }

            db.Update(guest);
            db.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
