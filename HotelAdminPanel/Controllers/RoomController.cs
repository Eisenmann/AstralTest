using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelAdminPanel.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelAdminPanel
{
    [Route("home/api/[controller]")]
    public class RoomController : Controller
    {

        hotel_dbContext db;
        public RoomController(hotel_dbContext context)
        {
            this.db = context;
           
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<object> Get(string sortOrder, string searchString)
        {
            var rooms = db.Rooms
                .Include(c => c.Hotel)
                .Include(t => t.TypeNavigation)
               // .Include(t=>t.Guests)
               // .Include(x => x.Guests.Where(p=>p.DateOut.Value.Year > DateTime.Now.Year).Select(p=>p.RoomId).Count())
                .ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(s => s.Hotel.HotelName.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "hotel_name_desc":
                    rooms = rooms.OrderByDescending(s => s.Hotel.HotelName).ToList();
                    break;
                case "capacity":
                    rooms = rooms.OrderBy(s => s.Capacity).ToList();
                    break;
                case "capacity_desc":
                    rooms = rooms.OrderByDescending(s => s.Capacity).ToList();
                    break;
                default:
                    rooms = rooms.OrderBy(s => s.Hotel.HotelName).ToList();
                    break;
            }


            return rooms;
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

           var countGuests = db.Guests.Where(x => x.RoomId == guest.RoomId && x.DateOut > DateTime.Now).Select(x => x.RoomId).ToList();
            var openRoom = db.Rooms.Where(x => x.RoomId == guest.RoomId).Select(x => x.Capacity).ToList();

            int count = -1;
            if (countGuests.Count > 0)
            {
                count = countGuests.Count;
            }


            if (count+1 < openRoom.First())
            {
                db.Guests.Add(guest);
                db.SaveChanges();
            }
            if(count+1 == openRoom.First())
            {
               
                var room = db.Rooms.Where(x => x.RoomId == guest.RoomId).ToList();
                room[0].Status = false;
                db.Rooms.Update(room[0]);
                db.Guests.Add(guest);
                db.SaveChanges();
            }
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Rooms room)
        {
            if (room == null)
            {
                return ;
            }
            if (!db.Rooms.Any(x => x.RoomId == room.RoomId))
            {
               

                return ;
            }

            var roomType = db.Rooms.Where(x => x.RoomId == room.RoomId).Select(x => new { x.Type, x.HotelId, x.Capacity }).ToList();
            room.Type = roomType[0].Type;           
            room.HotelId = roomType[0].HotelId;
            room.Capacity = roomType[0].Capacity;

            var guest = db.Guests.Where(x => x.RoomId == room.RoomId && x.DateOut > DateTime.Now).ToList();
            foreach(Guests item in guest)
            {
                item.DateOut = DateTime.Now.AddDays(-1);
                db.Guests.Update(item);
            }

           
            db.Update(room);
            db.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
