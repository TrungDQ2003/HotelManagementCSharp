using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TMHotel.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        // UserName Default = admin
        // PassDefault = admin
        public Login()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        //Sử dụng hàm encode và Decode để mã hóa username và password
        string EncodeKey(string strContent, string keyDecode)
        {
            string pwdEncoder = "";
            int lengthPwd = strContent.Length;
            int lengthKey = keyDecode.Length;
            for (int i = 0, j = 0; i < lengthPwd; i++, j++)
            {
                char charContent = strContent.ElementAt(i);
                if (j == lengthKey)
                    j = 0;
                char charKey = keyDecode.ElementAt(j);
                charContent += charKey;
                if (charContent > 127)
                    charContent = (char)(charContent - 127 + 32);
                pwdEncoder += charContent;
            }
            return pwdEncoder;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Nhập tên User");
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Nhập Pass");
                return;
            }
            string encodeUser = EncodeKey(txtUsername.Text, "UserNeverKnow");
            if (encodeUser != "Wxs|]") {
                MessageBox.Show("UserName không đúng");
                return;
            }
            string encodePass= EncodeKey(txtPassword.Password, "UserNeverKnow");
            if (encodePass != "Wxs|]")
            {
                MessageBox.Show("Password không đúng");
                return;
            }

            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
           
        }
    }
}
