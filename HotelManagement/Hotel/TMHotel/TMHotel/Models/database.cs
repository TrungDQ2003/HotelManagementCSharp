using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace TMHotel.Models
{
    public class database
    {
        private static string connectionString = "server=localhost;user=root;database=phongdb;password=root";

        private static string connStr = @"Data Source=LAPTOP-5P8GREI4\SQLEXPRESS01;Initial Catalog=phongdb;Integrated Security=True";
        public static void executeNonQuery(string sql)
        {

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public static MySqlDataReader executeReader(string sql)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();
                return command.ExecuteReader();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu");
                throw new Exception();
            }
        }
        public static SqlDataReader executeSqlReader(string sql)
        {
            try
            {
                SqlConnection sqlConection = new SqlConnection(connStr);
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConection);
                sqlConection.Open();
                return sqlCommand.ExecuteReader();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu");
                throw new Exception();
            }
        }
    }
}
