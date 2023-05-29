using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHotel.Models
{
    public class User
    {
        int _id;
        string _username;
        string _password;
        string _name;
        double _balance;
        DateTime _birth;
        string _phone;
        int _status;

        public User() { }
        public User(string username, string password, string name)
        {
            _username = username;
            _password = password;
            _name = name;
            _balance = 0;
        }

        public User(int id, string username, string password, string name)
        {
            _id = id;
            _username = username;
            _password = password;
            _name = name;
        }

        public User(int id, string username, string password, string name, double balance, DateTime birth, string phone, int status)
        {
            _id = id;
            _username = username;
            _password = password;
            _name = name;
            _balance = balance;
            _birth = birth;
            _phone = phone;
            _status = status;
        }

        public int Id { get => _id; set => _id = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string Name { get => _name; set => _name = value; }
        public double Balance { get => _balance; set => _balance = value; }
        public DateTime Birth { get => _birth; set => _birth = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public int Status { get => _status; set => _status = value; }


    }
}
