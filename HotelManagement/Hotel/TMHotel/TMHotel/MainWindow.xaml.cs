using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMHotel.Models;
namespace TMHotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository repository = new Repository();
        ObservableCollection<User> userList = Repository.instance.UserList;
        ObservableCollection<Room> roomList = Repository.instance.RoomList;
        ObservableCollection<Booking> bookingList = Repository.instance.BookingList;

        ObservableCollection<HotelModel> listPhong = new ObservableCollection<HotelModel>();

        enum STATUS_ROOM
        {
            DA_THUE = 0,
            CHUA_THUE = 1
        };

        string[] roomStatus = new string[] { "Da Thue", "Chua thue" };

        public ObservableCollection<HotelModel> ListPhong { get => listPhong; set => listPhong = value; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void loadToListPhong()
        {
            listPhong = new ObservableCollection<HotelModel>();
            foreach (Booking a in bookingList)
            {
                ListPhong.Add(new HotelModel(a.Room.Name, roomStatus[a.Room.Status], a.Username, a.Time.ToString(), a.Room.Price.ToString()));
            }
            hotleManager.ItemsSource = null;
            hotleManager.ItemsSource = ListPhong;
        }

        public void loadRoom()
        {
            roomList = new ObservableCollection<Room>();
            roomList = Repository.instance.RoomList;
            //dgRoom.ItemsSource = null;
            //dgRoom.ItemsSource = roomList;
        }
        private void hotleManager_Loaded(object sender, RoutedEventArgs e)
        {
            loadToListPhong();
        }

        public void updateRoomStatus(int id, int status)
        {
            int index = roomList.IndexOf(roomList.FirstOrDefault(item => item.IdRoom == id));
            roomList[index].Status = status;
            string sql = " UPDATE room SET status = '" + status + "' WHERE (id_room = '" + id + "');";
            database.executeNonQuery(sql);
            dgRoom.ItemsSource = null;
            dgRoom.ItemsSource = roomList;
        }

        private void hotleManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hotleManager.SelectedItem != null)
            {
                dynamic selected = hotleManager.SelectedItem;
                string tenPhong = selected.TenPhong;
                string tenNguoiThue = selected.TenNguoiThue;
                int status = 1;
                if (selected.TinhTrang.Equals("Da Thue")) status = 0;
                string thoiGian = selected.ThoiGianThue;
                string gia = selected.GiaTien;

                lb_ten_phong.Text = tenPhong;
                lb_tinh_trang_phong.Text = roomStatus[status];
                lb_ten_nguoi_thue.Text = tenNguoiThue;
                lb_thoi_gian_thue.Text = thoiGian;
                lb_gia_tien.Text = gia;
                lb_ten_nguoi_thue.IsReadOnly = true;
                lb_thoi_gian_thue.IsReadOnly = true;
                lb_gia_tien.IsReadOnly = true;

            }
        }

        private void btn_dat_phong_Click(object sender, RoutedEventArgs e)
        {

            var haveRoom = bookingList.FirstOrDefault(item => item.Room.Name.Equals(lb_ten_phong.Text));
            Room room = roomList.FirstOrDefault(item => item.Name.Equals(lb_ten_phong.Text));
            if (haveRoom != null)
            {
                MessageBox.Show("Phong da co nguoi thue, vui lòng chọn phòng khác");
                return;

            }
            string username = lb_ten_nguoi_thue.Text;
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Tên người thuê phòng không phù hợp");
                return;
            };

            
            int status = 1;
            if (lb_tinh_trang_phong.Text.Equals("Chua thue"))
                    { status = 0; 
            }
            else
            {
                MessageBox.Show("Tình trạng đặt phòng Da Thue / Chua thue .\n Bạn vui lòng nhập trạng thái phòng hợp lệ");
                return;

            }
            bool isValidTime; int time;
            isValidTime = int.TryParse(lb_thoi_gian_thue.Text, out time);
            if (!isValidTime)
            {
                MessageBox.Show("Thời gian thuê phòng không phù hơp");
                return;
            }

            bool isValidMoney; int money;
            isValidMoney = int.TryParse(lb_gia_tien.Text, out money);
            if (!isValidTime)
            {
                MessageBox.Show("Điền Giá phòng hợp lệ");
                return;
            }

            string dateBooked = DateTime.Now.ToString("yyyy-MM-dd");

            string sql = "INSERT INTO booking (id_booking, id_room, username, time, date_booked, status) VALUES ('" + "" + "', '" + room.IdRoom + "', '" + username + "','" + time + "', '" + dateBooked.ToString() + "', '" + status + "')";
            database.executeNonQuery(sql);

            bookingList.Add(new Booking(username, room, time, DateTime.Parse(dateBooked), status));
            updateRoomStatus(room.IdRoom, (int)STATUS_ROOM.DA_THUE);
            loadToListPhong();
        }

        private void btn_tra_phong_Click(object sender, RoutedEventArgs e)
        {
            if (hotleManager.SelectedItem != null)
            {
                HotelModel items = hotleManager.SelectedItem as HotelModel;

                ListPhong.Remove(items);
                Booking book = bookingList.FirstOrDefault(item => item.Username.Equals(items.TenNguoiThue) && item.Room.Name.Equals(items.TenPhong));
                hotleManager.Items.Refresh();
                string sqlDelete = "DELETE FROM booking WHERE (id_booking = '" + book.IdBooking + "');";
                database.executeNonQuery(sqlDelete);
                updateRoomStatus(book.Room.IdRoom, (int)STATUS_ROOM.CHUA_THUE);
                bookingList.Remove(book);
                loadToListPhong();
            }
            else
            {
                MessageBox.Show("Vui lòng đặ phòng để sử dụng tính năng này");
                return;
            }
        }

        private void btn_phong_da_dat_Click(object sender, RoutedEventArgs e)
        {
            var result = bookingList.Where(obj => obj.Status == 0);


            ObservableCollection<HotelModel> temp = new ObservableCollection<HotelModel>();
            foreach (var a in result)
            {
                temp.Add(new HotelModel(a.Room.Name, roomStatus[a.Room.Status], a.Username,a.Time.ToString(),a.Room.Price.ToString()));
            }
            if (temp.Count() == 0)
            {
                MessageBox.Show("Chưa có phòng nào đặt");
                return;
            }
            hotleManager.ItemsSource = null;
            hotleManager.ItemsSource = temp;
        }

        private void btn_phong_trong_Click(object sender, RoutedEventArgs e)
        {
            var result = roomList.Where(obj => obj.Status == 1);

            ObservableCollection<HotelModel> temp = new ObservableCollection<HotelModel>();
            foreach (var a in result)
            {
                temp.Add(new HotelModel(a.Name, roomStatus[a.Status], a.Name, "", a.Price.ToString()));
            }
            if (temp.Count() == 0)
            {
                MessageBox.Show("Không còn phòng trống");
                return;
            }
            hotleManager.ItemsSource = null;
            hotleManager.ItemsSource = temp;
        }

        private void btn_them_phong(object sender, RoutedEventArgs e)
        {
            string tenPhong = lbl_add_tenphong.Text;
            bool ok; int giaPhong;
            ok = int.TryParse(lbl_giaphong.Text, out giaPhong);
            if (!ok)
            {
                MessageBox.Show("Vui lòng nhập giá phòng");
                return;
            };

            Room room = new Room(roomList.Count()+1, tenPhong, giaPhong,1,"");
            roomList.Add(room);
            string sql = "INSERT INTO room (id_room,name, price,status,image) VALUES ('" + room.IdRoom + "', '" + room.Name + "', '" + room.Price + "','" + room.Status+ "','" +room.Image+ "');";
            database.executeNonQuery(sql);
            loadRoom();
        }

        private void btn_sua_phong(object sender, RoutedEventArgs e)
        {
            if (dgRoom.SelectedItem != null)
            {
                bool ok; double giaPhong;
                ok = double.TryParse(lbl_giaphong.Text, out giaPhong);
                if (!ok)
                {
                    MessageBox.Show("Giá phòng không hợp lệ");
                    return;
                };
                if (string.IsNullOrEmpty(lbl_add_tenphong.Text))
                {
                    MessageBox.Show("Tên mới của phòng không được để trống");
                    return;
                }
                Room items = dgRoom.SelectedItem as Room;
                int index = roomList.IndexOf(roomList.FirstOrDefault(item => item.IdRoom == items.IdRoom));
                items.Name = lbl_add_tenphong.Text;
                items.Price = giaPhong;
                roomList[index].Name = items.Name;
                roomList[index].Price = items.Price;

                string sql = " UPDATE room SET name = '" + items.Name + "', price = '" + items.Price + "' WHERE (id_room = '" + items.IdRoom + "');";
                database.executeNonQuery(sql);
                loadRoom();
            }
        }

        private void btn_xoa_phong(object sender, RoutedEventArgs e)
        {
            if (dgRoom.SelectedItem != null)
            {
                Room items = dgRoom.SelectedItem as Room;
                roomList.Remove(items);
                string sql = "DELETE FROM room WHERE (id_room = '" + items.IdRoom + "');";
                database.executeNonQuery(sql);
                loadRoom();
            }
        }

        private void dgRoom_Loaded(object sender, RoutedEventArgs e)
        {
            dgRoom.ItemsSource = roomList;
        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRoom.SelectedItem != null)
            {
                dynamic selected = dgRoom.SelectedItem;
                string tenPhong = selected.Name;
                int tinhTrangPhong = selected.Status;
                double gia_tien_phong = selected.Price;

                lb_ten_phong.Text = tenPhong;
                lb_tinh_trang_phong.Text = roomStatus[tinhTrangPhong];

                lbl_add_tenphong.Text = tenPhong.Trim();
                lb_gia_tien.Text = gia_tien_phong.ToString();
                lb_ten_nguoi_thue.Text = "";
                lb_thoi_gian_thue.Text = "";
                lbl_giaphong.Text = "";
                lb_ten_nguoi_thue.IsReadOnly = true;
                lb_thoi_gian_thue.IsReadOnly = true;
                lb_gia_tien.IsReadOnly = true;


            }
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            hotleManager.ItemsSource = null;
            hotleManager.ItemsSource = ListPhong;

            lb_ten_nguoi_thue.IsReadOnly = false;
            lb_thoi_gian_thue.IsReadOnly = false;
            lb_gia_tien.IsReadOnly = false;
        }
    }
}
