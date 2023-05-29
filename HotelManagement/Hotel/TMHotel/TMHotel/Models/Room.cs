using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHotel.Models
{
    public class Room
    {
        int _idRoom;
        string _name;
        double _price;
        int _status;
        string _image;

        public Room() { }
        public Room(string name, double price,int status)
        {
            _name = name;
            _price = price;
            _status = status;
            _image = "";
        }

        public Room(int idRoom, string name, double price, int status, string image)
        {
            _idRoom = idRoom;
            _name = name;
            _price = price;
            _status = status;
            _image = image;
        }

        public int IdRoom { get => _idRoom; set => _idRoom = value; }
        public string Name { get => _name; set => _name = value; }
        public double Price { get => _price; set => _price = value; }
        public int Status { get => _status; set => _status = value; }
        public string Image { get => _image; set => _image = value; }
    }
}
