using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TMHotel.Models
{
    public class Repository
    {
        ObservableCollection<User> userList = new ObservableCollection<User>();
        ObservableCollection<Room> roomList = new ObservableCollection<Room>();
        ObservableCollection<Booking> bookingList = new ObservableCollection<Booking>();

        public static Repository instance = null;
        public ObservableCollection<User> UserList { get => userList; set => userList = value; }
        public ObservableCollection<Room> RoomList { get => roomList; set => roomList = value; }
        public ObservableCollection<Booking> BookingList { get => bookingList; set => bookingList = value; }

        public Repository()
        {
            instance = this;
            initUser();
            initRoom();
            initBooking();
        }
        private void initBooking()
        {
            SqlDataReader _dataReader = database.executeSqlReader("Select * from booking");
            while (_dataReader.Read())
            {
                int id =                _dataReader.GetInt32(_dataReader.GetOrdinal("id_booking"));
                int idRoom =            _dataReader.GetInt32(_dataReader.GetOrdinal("id_room"));
                int status =            _dataReader.GetInt32(_dataReader.GetOrdinal("status"));
                int time =              _dataReader.GetInt32(_dataReader.GetOrdinal("time"));
                string dateBooked =     _dataReader.GetString(_dataReader.GetOrdinal("date_booked"));
                string username =       _dataReader.GetString(_dataReader.GetOrdinal("username"));
                Room room = RoomList.FirstOrDefault(item => item.IdRoom == idRoom);
                BookingList.Add(new Booking(id, username, room, time, DateTime.Parse(dateBooked), status));
            }
        }
        public void initRoom()
        {
            SqlDataReader _dataReader = database.executeSqlReader("Select * from room");
            while (_dataReader.Read())
            {
                int id =                _dataReader.GetInt32(_dataReader.GetOrdinal("id_room"));
                int status =            _dataReader.GetInt32(_dataReader.GetOrdinal("status"));
                string name =           _dataReader.GetString(_dataReader.GetOrdinal("name"));
                string image =          _dataReader.GetString(_dataReader.GetOrdinal("image"));
                double price = (double) _dataReader.GetDecimal(_dataReader.GetOrdinal("price"));
                Room item = new Room(id,name, price, status, image);
                RoomList.Add(item);
            }
        }
        private void initUser()
        {
            SqlDataReader _dataReader = database.executeSqlReader("Select * from userHT");
            while (_dataReader.Read())
            {
                int id =                _dataReader.GetInt32(_dataReader.GetOrdinal("id"));
                string username =       _dataReader.GetString(_dataReader.GetOrdinal("username"));
                string password =        _dataReader.GetString(_dataReader.GetOrdinal("password"));
                string name =           _dataReader.GetString(_dataReader.GetOrdinal("name"));
                User user = new User(id,username, password, name);
                UserList.Add(user);
            }
        }
    }
}
