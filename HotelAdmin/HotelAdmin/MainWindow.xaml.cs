using HotelAdmin.DataSet1TableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace HotelAdmin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> idforlists = new List<int>();
        DataSet1 dataset = new DataSet1();
        View_UsersTableAdapter view_userTA = new View_UsersTableAdapter();
        SotrudnikTableAdapter sotrudnikTA = new SotrudnikTableAdapter();
        DoljnostTableAdapter doljnostTA = new DoljnostTableAdapter();
        UsersTableAdapter usersTA = new UsersTableAdapter();
        RoleTableAdapter roleTA = new RoleTableAdapter();
        BookTableAdapter bookTA = new BookTableAdapter();
        CleaningTableAdapter cleaningTA = new CleaningTableAdapter();
        Cleaning_EquipmentTableAdapter cleaning_EquipmentTA = new Cleaning_EquipmentTableAdapter();
        ClientTableAdapter clientTA = new ClientTableAdapter();
        DishTableAdapter dishTA = new DishTableAdapter();
        Dish_FoodTableAdapter dish_FoodTA = new Dish_FoodTableAdapter();
        EquipmentTableAdapter equipmentTA = new EquipmentTableAdapter();
        FoodTableAdapter foodTA = new FoodTableAdapter();
        FridgeTableAdapter fridgeTA = new FridgeTableAdapter();
        MenuTableAdapter menuTA = new MenuTableAdapter();
        Menu_DateTableAdapter menu_DateTA = new Menu_DateTableAdapter();
        Menu_DishTableAdapter menu_DishTA = new Menu_DishTableAdapter();
        NomerTableAdapter nomerTA = new NomerTableAdapter();
        Priem_PitaniyaTableAdapter priemTA = new Priem_PitaniyaTableAdapter();
        SkladTableAdapter skladTA = new SkladTableAdapter();
        Sotrudnik_DoljnostTableAdapter sotrudnik_DoljnostTA = new Sotrudnik_DoljnostTableAdapter();
        Type_MealTableAdapter type_MealTA = new Type_MealTableAdapter();
        Type_NomerTableAdapter type_NomerTA = new Type_NomerTableAdapter();

        private StringBuilder sb = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();
            RefreshDB();
        }

        private void ExportToCSV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    sb.Clear();
                    List<string> tablecolumns = new List<string>();
                    tablecolumns.Add("ID_User");
                    tablecolumns.Add("login");
                    tablecolumns.Add("Sotrudnik_ID");
                    tablecolumns.Add("Role_ID");
                    tablecolumns.Add("password");
                    FillCSVTable(tablecolumns, "Users", dataset.Users.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Role");
                    tablecolumns.Add("Name");
                    FillCSVTable(tablecolumns, "Role", dataset.Role.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Doljnost");
                    tablecolumns.Add("Name");
                    tablecolumns.Add("Salary");
                    FillCSVTable(tablecolumns, "Doljnost", dataset.Doljnost.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Sotrudnik_Doljnost");
                    tablecolumns.Add("Doljnost_ID");
                    tablecolumns.Add("Sotrudnik_ID");
                    FillCSVTable(tablecolumns, "Sotrudnik_Doljnost", dataset.Sotrudnik_Doljnost.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Sotrudnik");
                    tablecolumns.Add("Surname");
                    tablecolumns.Add("Name");
                    tablecolumns.Add("Otchestvo");
                    tablecolumns.Add("Date_Rozhdenia");
                    FillCSVTable(tablecolumns, "Sotrudnik", dataset.Sotrudnik.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Cleaning");
                    tablecolumns.Add("Date");
                    tablecolumns.Add("Sotrudnik_ID");
                    tablecolumns.Add("Nomer_ID");
                    FillCSVTable(tablecolumns, "Cleaning", dataset.Cleaning.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Cleaning_Equipment");
                    tablecolumns.Add("Equipment_ID");
                    tablecolumns.Add("Cleaning_ID");
                    FillCSVTable(tablecolumns, "Cleaning_Equipment", dataset.Cleaning_Equipment.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Equipment");
                    tablecolumns.Add("Name");
                    tablecolumns.Add("Sklad_ID");
                    FillCSVTable(tablecolumns, "Equipment", dataset.Equipment.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Sklad");
                    tablecolumns.Add("Name");
                    FillCSVTable(tablecolumns, "Sklad", dataset.Sklad.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Nomer");
                    tablecolumns.Add("Number");
                    tablecolumns.Add("Type_ID");
                    FillCSVTable(tablecolumns, "Nomer", dataset.Nomer.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Type");
                    tablecolumns.Add("Name");
                    FillCSVTable(tablecolumns, "Type_Nomer", dataset.Type_Nomer.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Book");
                    tablecolumns.Add("DateStart");
                    tablecolumns.Add("Nomer_ID");
                    tablecolumns.Add("Client_ID");
                    tablecolumns.Add("DateEnd");
                    FillCSVTable(tablecolumns, "Book", dataset.Book.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Client");
                    tablecolumns.Add("Surname");
                    tablecolumns.Add("Name");
                    tablecolumns.Add("Otchestvo");
                    tablecolumns.Add("Date_Rozhdeniya");
                    tablecolumns.Add("SeriaPas");
                    tablecolumns.Add("NumberPas");
                    FillCSVTable(tablecolumns, "Client", dataset.Client.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Priem");
                    tablecolumns.Add("Date");
                    tablecolumns.Add("Menu_Date_ID");
                    tablecolumns.Add("Nomer_ID");
                    FillCSVTable(tablecolumns, "Priem_Pitaniya", dataset.Priem_Pitaniya.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Menu_Date");
                    tablecolumns.Add("Date");
                    tablecolumns.Add("Type_ID");
                    tablecolumns.Add("Menu_ID");
                    FillCSVTable(tablecolumns, "Menu_Date", dataset.Menu_Date.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Type");
                    tablecolumns.Add("MealName");
                    tablecolumns.Add("TimeEnd");
                    tablecolumns.Add("TimeStart");
                    FillCSVTable(tablecolumns, "Type_Meal", dataset.Type_Meal.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Menu");
                    tablecolumns.Add("Name");
                    FillCSVTable(tablecolumns, "Menu", dataset.Menu.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Menu_Dish");
                    tablecolumns.Add("Dish_ID");
                    tablecolumns.Add("Menu_ID");
                    FillCSVTable(tablecolumns, "Menu_Dish", dataset.Menu_Dish.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Dish");
                    tablecolumns.Add("Name");
                    tablecolumns.Add("Weight");
                    FillCSVTable(tablecolumns, "Dish", dataset.Dish.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Dish_Food");
                    tablecolumns.Add("Weight");
                    tablecolumns.Add("Dish_ID");
                    tablecolumns.Add("Food_ID");
                    FillCSVTable(tablecolumns, "Dish_Food", dataset.Dish_Food.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Food");
                    tablecolumns.Add("Name");
                    tablecolumns.Add("Fridge_ID");
                    FillCSVTable(tablecolumns, "Food", dataset.Food.Rows);
                    sb.Append("\r\n");
                    tablecolumns.Clear();
                    tablecolumns.Add("ID_Fridge");
                    tablecolumns.Add("Name");
                    FillCSVTable(tablecolumns, "Fridge", dataset.Fridge.Rows);

                    File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка при сохранении! Проверьте, что выбранный файл закрыт");
            }
        }

        private void FillCSVTable(List<string> tablecolumns, string tablename, DataRowCollection data)
        {
            sb.Append(tablename);
            sb.Append("\r\n");
            for (int i = 0; i < tablecolumns.Count; i++)
            {
                if (i != tablecolumns.Count - 1)
                    sb.Append(tablecolumns[i] + ",");
                else
                    sb.Append(tablecolumns[i]);
            }
            sb.Append("\r\n");
            foreach (DataRow dr in data)
            {
                int rowId = Convert.ToInt32(dr[tablecolumns[0]]);
                sb.Append(rowId.ToString() + ",");
                for (int i=1;i<tablecolumns.Count;i++)
                {
                    if (i != tablecolumns.Count - 1)
                        sb.Append(dr[tablecolumns[i]].ToString() + ",");
                    else
                        sb.Append(dr[tablecolumns[i]]);
                }
                sb.Append("\r\n");
            }
        }

        private void ReserveCopy_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.bak)|*.bak";
            if (saveFileDialog.ShowDialog() == true)
            {
                //File.Create(saveFileDialog.FileName);
                ProceduresDB pr = new ProceduresDB();
                pr.CreateBackup(saveFileDialog.FileName);
            }
        }

        private void Restoration_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.Filter = "Text files (*.bak)|*.bak";
            if (saveFileDialog.ShowDialog() == true)
            {
                ProceduresDB pr = new ProceduresDB();
                pr.RestoreDataBase(saveFileDialog.FileName);
            }
            RefreshDB();
        }

        private void FiltrUsers_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FiltrUsers.Text != "")
            {
                ProceduresDB pr = new ProceduresDB();
                pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Users] WHERE [Логин] = '" + FiltrUsers.Text + "' OR [Роль] = '" + FiltrUsers.Text + "' OR [ФИО] = '" + FiltrUsers.Text + "'");
                UsersData.ItemsSource = pr.FilterSQL.DefaultView;
            }
            else
            {
                UsersData.ItemsSource = dataset.View_Users.DefaultView;
                UsersData.SelectedValuePath = "ID_User";
            }
            UsersData.Columns[0].Visibility = Visibility.Hidden;
            UsersData.Columns[1].Visibility = Visibility.Hidden;
            UsersData.Columns[2].Visibility = Visibility.Hidden;
            UsersData.Columns[4].Visibility = Visibility.Hidden;
        }

        private void UsersData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            UsersData.Columns[0].Visibility = Visibility.Hidden;
            UsersData.Columns[1].Visibility = Visibility.Hidden;
            UsersData.Columns[2].Visibility = Visibility.Hidden;
            UsersData.Columns[4].Visibility = Visibility.Hidden;
        }

        private void UsersData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)UsersData.SelectedItem;
            if (dataRowView != null)
            {
                LoginUsers.Text = dataRowView.Row.Field<String>("Логин");
                SotrudnikUsers.SelectedValue = dataRowView.Row.Field<int>("ID_Sotrudnik");
                RoleUsers.SelectedValue = dataRowView.Row.Field<int>("ID_Role");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            List<String> names = new List<String>();
            names.Add("login");
            names.Add("password");
            names.Add("Role_ID");
            names.Add("Sotrudnik_ID");

            List<String> parametrs = new List<String>();
            parametrs.Add("'" + LoginUsers.Text + "'");
            parametrs.Add("'" + GetHash(PasswordUsers.Text) + "'");
            parametrs.Add(RoleUsers.SelectedValue.ToString());
            parametrs.Add(SotrudnikUsers.SelectedValue.ToString());

            ProceduresDB procedure = new ProceduresDB();
            procedure.Insert("Users", names, parametrs, "ID_User");
            RefreshDB();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersData.SelectedValue != null)
            {
                List<String> names = new List<String>();
                names.Add("ID_User");
                names.Add("login");
                names.Add("password");
                names.Add("Role_ID");
                names.Add("Sotrudnik_ID");

                List<String> parametrs = new List<String>();
                parametrs.Add(UsersData.SelectedValue.ToString());
                parametrs.Add("'" + LoginUsers.Text + "'");
                parametrs.Add("'" + GetHash(PasswordUsers.Text) + "'");
                parametrs.Add(RoleUsers.SelectedValue.ToString());
                parametrs.Add(SotrudnikUsers.SelectedValue.ToString());

                ProceduresDB procedure = new ProceduresDB();
                procedure.Update("Users", names, parametrs);
                RefreshDB();
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersData.SelectedValue != null)
            {
                ProceduresDB procedure = new ProceduresDB();
                procedure.Delete("Users", "ID_User", UsersData.SelectedValue.ToString());
                RefreshDB();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UsersData.ItemsSource = dataset.View_Users.DefaultView;
            UsersData.SelectedValuePath = "ID_User";

            SotrudnikUsers.ItemsSource = dataset.Sotrudnik.DefaultView;
            SotrudnikUsers.DisplayMemberPath = "Surname";
            SotrudnikUsers.SelectedValuePath = "ID_Sotrudnik";
            SotrudnikUsers.SelectedIndex = 0;


            RoleUsers.ItemsSource = dataset.Role.DefaultView;
            RoleUsers.DisplayMemberPath = "Name";
            RoleUsers.SelectedValuePath = "ID_Role";
            RoleUsers.SelectedIndex = 0;
        }

        private void RefreshDB()
        {
            sotrudnikTA.Fill(dataset.Sotrudnik);
            doljnostTA.Fill(dataset.Doljnost);
            usersTA.Fill(dataset.Users);
            roleTA.Fill(dataset.Role);
            bookTA.Fill(dataset.Book);
            cleaningTA.Fill(dataset.Cleaning);
            cleaning_EquipmentTA.Fill(dataset.Cleaning_Equipment);
            clientTA.Fill(dataset.Client);
            dishTA.Fill(dataset.Dish);
            dish_FoodTA.Fill(dataset.Dish_Food);
            equipmentTA.Fill(dataset.Equipment);
            foodTA.Fill(dataset.Food);
            fridgeTA.Fill(dataset.Fridge);
            menuTA.Fill(dataset.Menu);
            menu_DateTA.Fill(dataset.Menu_Date);
            menu_DishTA.Fill(dataset.Menu_Dish);
            nomerTA.Fill(dataset.Nomer);
            priemTA.Fill(dataset.Priem_Pitaniya);
            skladTA.Fill(dataset.Sklad);
            sotrudnik_DoljnostTA.Fill(dataset.Sotrudnik_Doljnost);
            type_MealTA.Fill(dataset.Type_Meal);
            type_NomerTA.Fill(dataset.Type_Nomer);

            view_userTA.Fill(dataset.View_Users);
        }
        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
