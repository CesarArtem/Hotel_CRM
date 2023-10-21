using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для ConnectDB.xaml
    /// </summary>
    public partial class ConnectDB : Window
    {
        SqlConnectionStringBuilder stringBuilder;
        public ConnectDB()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.HotelConnectionString1;
            servTB.Text = stringBuilder.DataSource;
            dbTB.Text = stringBuilder.InitialCatalog;
            login.Text = stringBuilder.UserID;
            pass.Text = stringBuilder.Password;
        }

        private async void ContinueBTN_Click(object sender, RoutedEventArgs e)
        {
            if (servTB.Text != "" && dbTB.Text != "")
            {
                bool result = true;
                try
                {
                    stringBuilder.DataSource = servTB.Text;
                    stringBuilder.InitialCatalog = dbTB.Text;
                    stringBuilder.UserID = login.Text;
                    stringBuilder.Password = pass.Text;
                    stringBuilder.IntegratedSecurity = true;
                    var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                    var conStrSect = (ConnectionStringsSection)config.GetSection("connectionStrings");
                    conStrSect.ConnectionStrings["Hotel.Properties.Settings.HotelConnectionString1"].ConnectionString = stringBuilder.ConnectionString;
                    config.Save();

                    ConfigurationManager.RefreshSection("connectionStrings");
                    String ConnectionString = stringBuilder.ConnectionString;
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    connection.Open();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Ошибка подключения! Проверьте корректность наименования сервера, базы данных, пользователя и пароля");
                    result = false;
                }
                if (result)
                {
                    MessageBox.Show("Успешное подключение");
                    Application.Current.Shutdown();
                }
                }
                else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
        }
    }
}
