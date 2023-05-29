using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHotel.Models
{
    public class Booking
    {
        int _idBooking;
        string _username;
        Room _room;
        int _time;
        DateTime _dateBooked;
        int _status;

        public Booking()
        {
        }

        public Booking(string username, Room room, int time, DateTime dateBooked, int status)
        {
            _username = username;
            _room = room;
            _time = time;
            _dateBooked = dateBooked;
            _status = status;
        }

        public Booking(int idBooking, string username, Room room, int time, DateTime dateBooked, int status)
        {
            IdBooking = idBooking;
            Username = username;
            Room = room;
            Time = time;
            DateBooked = dateBooked;
            Status = status;
        }

        public int IdBooking { get => _idBooking; set => _idBooking = value; }
        public string Username { get => _username; set => _username = value; }
        public Room Room { get => _room; set => _room = value; }
        public int Time { get => _time; set => _time = value; }
        public DateTime DateBooked { get => _dateBooked; set => _dateBooked = value; }
        public int Status { get => _status; set => _status = value; }
    }
}
