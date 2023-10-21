using Hotel.DataSet1TableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.IO;
using Microsoft.Reporting.WinForms;
using Word = Microsoft.Office.Interop.Word;
using System.Globalization;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> idforlists = new List<int>();

        DataSet1 dataset = new DataSet1();
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


        View_UsersTableAdapter view_userTA = new View_UsersTableAdapter();
        View_BooksTableAdapter view_booksTA = new View_BooksTableAdapter();
        View_CleaningsTableAdapter view_cleaningsTA = new View_CleaningsTableAdapter();
        View_EquipmentTableAdapter view_equipmentTA = new View_EquipmentTableAdapter();
        View_FoodTableAdapter view_foodTA = new View_FoodTableAdapter();
        View_NomerTableAdapter view_nomerTA = new View_NomerTableAdapter();
        View_PriemsTableAdapter view_priemsTA = new View_PriemsTableAdapter();
        View_SotrudnikTableAdapter view_sotrudnikTA = new View_SotrudnikTableAdapter();
        View_MenuTableAdapter view_MenuTableTA = new View_MenuTableAdapter();
        View_CleanMenTableAdapter cleanMenTA = new View_CleanMenTableAdapter();


        public MainWindow()
        {
            InitializeComponent();
            AuthorizationGrid.Visibility = Visibility.Visible;

            Admin.Visibility = Visibility.Hidden;
            Administrator.Visibility = Visibility.Hidden;
            Chief.Visibility = Visibility.Hidden;
            SkladMan.Visibility = Visibility.Hidden;
            CleanMan.Visibility = Visibility.Hidden;
            Kadrovik.Visibility = Visibility.Hidden;

            RefreshDB();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.Authorization(LoginTB.Text, PasswordTB.Password);
                if (result)
                {
                    foreach (var row in dataset.Users)
                    {
                        if (row.login == LoginTB.Text)
                        {
                            if (row.password == GetHash(PasswordTB.Password))
                            {
                                AuthorizationGrid.Visibility = Visibility.Hidden;
                                switch (row.Role_ID)
                                {
                                    case 1:
                                        Admin.Visibility = Visibility.Visible;
                                        break;
                                    case 2:
                                        Administrator.Visibility = Visibility.Visible;
                                        break;
                                    case 3:
                                        Chief.Visibility = Visibility.Visible;
                                        break;
                                    case 4:
                                        CleanMan.Visibility = Visibility.Visible;
                                        break;
                                    case 5:
                                        SkladMan.Visibility = Visibility.Visible;
                                        break;
                                    case 6:
                                        Kadrovik.Visibility = Visibility.Visible;
                                        break;
                                }
                            }
                        }
                    }
                    if (AuthorizationGrid.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                    MessageBox.Show("Некоректно заполнен логин и пароль. Логин и пароль должны содержать латинские символы, не должны содержать специальные символы и должны бать длиной от 1 до 15 символов");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UsersData.ItemsSource = dataset.View_Users.DefaultView;
            UsersData.SelectedValuePath = "ID_User";

            RoomsData.ItemsSource = dataset.View_Nomer.DefaultView;
            RoomsData.SelectedValuePath = "ID_Nomer";

            BooksData.ItemsSource = dataset.View_Books.DefaultView;
            BooksData.SelectedValuePath = "ID_Book";

            CleaningsData2.ItemsSource = dataset.View_Cleanings.DefaultView;
            CleaningsData2.SelectedValuePath = "ID_Cleaning";

            RoomTypesData.ItemsSource = dataset.Type_Nomer.DefaultView;
            RoomTypesData.SelectedValuePath = "ID_Type";

            PriemsData.ItemsSource = dataset.View_Priems.DefaultView;
            PriemsData.SelectedValuePath = "ID_Priem";

            ClientsData.ItemsSource = dataset.Client.DefaultView;
            ClientsData.SelectedValuePath = "ID_Client";

            FoodsData.ItemsSource = dataset.View_Food.DefaultView;
            FoodsData.SelectedValuePath = "ID_Food";

            DishesData.ItemsSource = dataset.Dish.DefaultView;
            DishesData.SelectedValuePath = "ID_Dish";

            MenusData.ItemsSource = dataset.View_Menu.DefaultView;
            MenusData.SelectedValuePath = "ID_Menu";

            FridgesData.ItemsSource = dataset.Fridge.DefaultView;
            FridgesData.SelectedValuePath = "ID_Fridge";

            CleaningsData.ItemsSource = dataset.View_Cleanings.DefaultView;
            CleaningsData.SelectedValuePath = "ID_Cleaning";

            EquipmentData2.ItemsSource = dataset.View_Equipment.DefaultView;
            EquipmentData2.SelectedValuePath = "ID_Equipment";

            EquipmentData.ItemsSource = dataset.View_Equipment.DefaultView;
            EquipmentData.SelectedValuePath = "ID_Equipment";

            RoomsData2.ItemsSource = dataset.View_Nomer.DefaultView;
            RoomsData2.SelectedValuePath = "ID_Nomer";

            SotrudniksData.ItemsSource = dataset.View_Sotrudnik.DefaultView;
            SotrudniksData.SelectedValuePath = "ID_Sotrudnik";

            DoljnostsData.ItemsSource = dataset.Doljnost.DefaultView;
            DoljnostsData.SelectedValuePath = "ID_Doljnost";

            SkladsData.ItemsSource = dataset.Sklad.DefaultView;
            SkladsData.SelectedValuePath = "ID_Sklad";

            SotrudnikUsers.ItemsSource = dataset.Sotrudnik.DefaultView;
            SotrudnikUsers.DisplayMemberPath = "Surname";
            SotrudnikUsers.SelectedValuePath = "ID_Sotrudnik";
            SotrudnikUsers.SelectedIndex = 0;


            RoleUsers.ItemsSource = dataset.Role.DefaultView;
            RoleUsers.DisplayMemberPath = "Name";
            RoleUsers.SelectedValuePath = "ID_Role";
            RoleUsers.SelectedIndex = 0;

            TypeNumber.ItemsSource = dataset.Type_Nomer.DefaultView;
            TypeNumber.DisplayMemberPath = "Name";
            TypeNumber.SelectedValuePath = "ID_Type";
            TypeNumber.SelectedIndex = 0;

            NomerBook.ItemsSource = dataset.View_Nomer.DefaultView;
            NomerBook.DisplayMemberPath = "Номер";
            NomerBook.SelectedValuePath = "ID_Nomer";
            NomerBook.SelectedIndex = 0;

            ClientBook.ItemsSource = dataset.Client.DefaultView;
            ClientBook.DisplayMemberPath = "Surname";
            ClientBook.SelectedValuePath = "ID_Client";
            ClientBook.SelectedIndex = 0;

            NomerBook.ItemsSource = dataset.View_Nomer.DefaultView;
            NomerBook.DisplayMemberPath = "Номер";
            NomerBook.SelectedValuePath = "ID_Nomer";
            NomerBook.SelectedIndex = 0;

            MenuPriem.ItemsSource = dataset.View_Menu.DefaultView;
            MenuPriem.DisplayMemberPath = "Полное имя";
            MenuPriem.SelectedValuePath = "ID_Menu_Date";
            MenuPriem.SelectedIndex = 0;

            NomerPriem.ItemsSource = dataset.View_Nomer.DefaultView;
            NomerPriem.DisplayMemberPath = "Номер";
            NomerPriem.SelectedValuePath = "ID_Nomer";
            NomerPriem.SelectedIndex = 0;

            FridgeFood.ItemsSource = dataset.Fridge.DefaultView;
            FridgeFood.DisplayMemberPath = "Name";
            FridgeFood.SelectedValuePath = "ID_Fridge";
            FridgeFood.SelectedIndex = 0;

            NomerCleaning.ItemsSource = dataset.View_Nomer.DefaultView;
            NomerCleaning.DisplayMemberPath = "Номер";
            NomerCleaning.SelectedValuePath = "ID_Nomer";
            NomerCleaning.SelectedIndex = 0;

            SotrudnikCleaning.ItemsSource = dataset.View_CleanMen.DefaultView;
            SotrudnikCleaning.DisplayMemberPath = "ФИО";
            SotrudnikCleaning.SelectedValuePath = "ID_Sotrudnik";
            SotrudnikCleaning.SelectedIndex = 0;

            SkladEquipment.ItemsSource = dataset.Sklad.DefaultView;
            SkladEquipment.DisplayMemberPath = "Name";
            SkladEquipment.SelectedValuePath = "ID_Sklad";
            SkladEquipment.SelectedIndex = 0;

            TypeMenu.ItemsSource = dataset.Type_Meal.DefaultView;
            TypeMenu.DisplayMemberPath = "MealName";
            TypeMenu.SelectedValuePath = "ID_Type";
            TypeMenu.SelectedIndex = 0;

            DoljnostsLB.ItemsSource = dataset.Doljnost.DefaultView;
            DoljnostsLB.DisplayMemberPath = "Name";
            DoljnostsLB.SelectedValuePath = "ID_Doljnost";

            EquipmentLB.ItemsSource = dataset.View_Equipment.DefaultView;
            EquipmentLB.DisplayMemberPath = "Наименование";
            EquipmentLB.SelectedValuePath = "ID_Equipment";

            DishesLB.ItemsSource = dataset.Dish.DefaultView;
            DishesLB.DisplayMemberPath = "Name";
            DishesLB.SelectedValuePath = "ID_Dish";

            ProductsLB.ItemsSource = dataset.Food.DefaultView;
            ProductsLB.DisplayMemberPath = "Name";
            ProductsLB.SelectedValuePath = "ID_Food";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginTB.Text = "";
            PasswordTB.Password = "";
            AuthorizationGrid.Visibility = Visibility.Visible;

            Admin.Visibility = Visibility.Hidden;
            Administrator.Visibility = Visibility.Hidden;
            Chief.Visibility = Visibility.Hidden;
            SkladMan.Visibility = Visibility.Hidden;
            CleanMan.Visibility = Visibility.Hidden;
            Kadrovik.Visibility = Visibility.Hidden;
        }

        private void UsersData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            UsersData.Columns[0].Visibility = Visibility.Hidden;
            UsersData.Columns[1].Visibility = Visibility.Hidden;
            UsersData.Columns[2].Visibility = Visibility.Hidden;
            UsersData.Columns[4].Visibility = Visibility.Hidden;
        }

        private void RoomsData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            RoomsData.Columns[0].Visibility = Visibility.Hidden;
            RoomsData.Columns[1].Visibility = Visibility.Hidden;
        }

        private void BooksData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            BooksData.Columns[0].Visibility = Visibility.Hidden;
            BooksData.Columns[1].Visibility = Visibility.Hidden;
            BooksData.Columns[2].Visibility = Visibility.Hidden;
            BooksData.Columns[3].Visibility = Visibility.Hidden;
        }

        private void CleaningsData2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            CleaningsData2.Columns[0].Visibility = Visibility.Hidden;
            CleaningsData2.Columns[1].Visibility = Visibility.Hidden;
            CleaningsData2.Columns[2].Visibility = Visibility.Hidden;
            CleaningsData2.Columns[3].Visibility = Visibility.Hidden;
        }

        private void RoomTypesData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            RoomTypesData.Columns[0].Visibility = Visibility.Hidden;
            RoomTypesData.Columns[1].Header = "Наименование";
            RoomTypesData.Columns[RoomTypesData.Columns.Count - 1].Visibility = Visibility.Hidden;

        }

        private void PriemsData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            PriemsData.Columns[0].Visibility = Visibility.Hidden;
            PriemsData.Columns[1].Visibility = Visibility.Hidden;
            PriemsData.Columns[2].Visibility = Visibility.Hidden;
            PriemsData.Columns[3].Visibility = Visibility.Hidden;
            PriemsData.Columns[4].Visibility = Visibility.Hidden;
        }

        private void ClientsData_LoadingRow(object sender, DataGridRowEventArgs e)
        {

            ClientsData.Columns[0].Visibility = Visibility.Hidden;
            ClientsData.Columns[1].Header = "Фамилия";
            ClientsData.Columns[2].Header = "Имя";
            ClientsData.Columns[3].Header = "Отчество";
            ClientsData.Columns[4].Header = "Дата рождения";
            ClientsData.Columns[5].Visibility = Visibility.Hidden;
            ClientsData.Columns[6].Visibility = Visibility.Hidden;
            ClientsData.Columns[ClientsData.Columns.Count - 1].Visibility = Visibility.Hidden;
        }

        private void FoodsData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            FoodsData.Columns[0].Visibility = Visibility.Hidden;
            FoodsData.Columns[1].Visibility = Visibility.Hidden;
        }

        private void DishesData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (FiltrDish.Text == "")
            {
                DishesData.Columns[0].Visibility = Visibility.Hidden;
                DishesData.Columns[1].Header = "Наименование";
                DishesData.Columns[2].Header = "Вес";
                DishesData.Columns[DishesData.Columns.Count - 1].Visibility = Visibility.Hidden;
                DishesData.Columns[DishesData.Columns.Count - 2].Visibility = Visibility.Hidden;
            }
        }

        private void MenusData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            MenusData.Columns[0].Visibility = Visibility.Hidden;
            MenusData.Columns[1].Visibility = Visibility.Hidden;
            MenusData.Columns[2].Visibility = Visibility.Hidden;
        }

        private void FridgesData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            FridgesData.Columns[0].Visibility = Visibility.Hidden;
            FridgesData.Columns[1].Header = "Наименование";
            FridgesData.Columns[FridgesData.Columns.Count - 1].Visibility = Visibility.Hidden;
        }

        private void CleaningsData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            CleaningsData.Columns[0].Visibility = Visibility.Hidden;
            CleaningsData.Columns[1].Visibility = Visibility.Hidden;
            CleaningsData.Columns[2].Visibility = Visibility.Hidden;
            CleaningsData.Columns[3].Visibility = Visibility.Hidden;
        }

        private void EquipmentData2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            EquipmentData2.Columns[0].Visibility = Visibility.Hidden;
            EquipmentData2.Columns[1].Visibility = Visibility.Hidden;
        }

        private void RoomsData2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            RoomsData2.Columns[0].Visibility = Visibility.Hidden;
            RoomsData2.Columns[1].Visibility = Visibility.Hidden;
        }

        private void SotrudniksData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            SotrudniksData.Columns[0].Visibility = Visibility.Hidden;
            SotrudniksData.Columns[1].Visibility = Visibility.Hidden;
            SotrudniksData.Columns[2].Visibility = Visibility.Hidden;
        }

        private void DoljnostsData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DoljnostsData.Columns[0].Visibility = Visibility.Hidden;
            DoljnostsData.Columns[1].Header = "Наименование";
            DoljnostsData.Columns[DoljnostsData.Columns.Count - 1].Visibility = Visibility.Hidden;
        }

        private void SkladsData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            SkladsData.Columns[0].Visibility = Visibility.Hidden;
            SkladsData.Columns[1].Header = "Наименование";
            SkladsData.Columns[SkladsData.Columns.Count - 1].Visibility = Visibility.Hidden;
        }

        private void EquipmentData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            EquipmentData.Columns[0].Visibility = Visibility.Hidden;
            EquipmentData.Columns[1].Visibility = Visibility.Hidden;
        }

        private void UsersData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)UsersData.SelectedItem;
                if (dataRowView != null)
                {
                    LoginUsers.Text = dataRowView.Row.Field<String>("Логин");
                    SotrudnikUsers.SelectedValue = dataRowView.Row.Field<int>("ID_Sotrudnik");
                    RoleUsers.SelectedValue = dataRowView.Row.Field<int>("ID_Role");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void RoomsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)RoomsData.SelectedItem;
                if (dataRowView != null)
                {
                    NumberRoom.Text = dataRowView.Row.Field<int>("Номер").ToString();
                    TypeNumber.SelectedValue = dataRowView.Row.Field<int>("ID_Type");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void BooksData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)BooksData.SelectedItem;
                if (dataRowView != null)
                {
                    DateStartBook.SelectedDate = dataRowView.Row.Field<DateTime>("Дата начала");
                    NomerBook.SelectedValue = dataRowView.Row.Field<int>("ID_Nomer");
                    DateEndBook.SelectedDate = dataRowView.Row.Field<DateTime>("Дата окончания");
                    ClientBook.SelectedValue = dataRowView.Row.Field<int>("ID_Client");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void RoomTypesData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)RoomTypesData.SelectedItem;
                if (dataRowView != null)
                {
                    NameRoomType.Text = dataRowView.Row.Field<String>("Name");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void PriemsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)PriemsData.SelectedItem;
                if (dataRowView != null)
                {
                    DatePriem.SelectedDate = dataRowView.Row.Field<DateTime>("Дата");
                    NomerPriem.SelectedValue = dataRowView.Row.Field<int>("ID_Nomer");
                    MenuPriem.SelectedValue = dataRowView.Row.Field<int>("ID_Menu_Date");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void ClientsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)ClientsData.SelectedItem;
                if (dataRowView != null)
                {
                    DateBirthClient.SelectedDate = dataRowView.Row.Field<DateTime>("Date_Rozhdeniya");
                    SurnameClient.Text = dataRowView.Row.Field<String>("Surname");
                    NameClient.Text = dataRowView.Row.Field<String>("Name");
                    SecondNameClient.Text = dataRowView.Row.Field<String>("Otchestvo");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FoodsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)FoodsData.SelectedItem;
                if (dataRowView != null)
                {
                    NameFood.Text = dataRowView.Row.Field<String>("Наименование");
                    FridgeFood.SelectedValue = dataRowView.Row.Field<int>("ID_Fridge");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DishesData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)DishesData.SelectedItem;
                if (dataRowView != null)
                {
                    idforlists.Clear();
                    Products_DishesLB.Items.Clear();
                    NameDish.Text = dataRowView.Row.Field<String>("Name");
                    WeightDish.Text = dataRowView.Row.Field<int>("Weight").ToString();
                    int iddish = dataRowView.Row.Field<int>("ID_Dish");
                    for (int i = 0; i < dataset.Dish_Food.Count; i++)
                    {
                        if (iddish == dataset.Dish_Food[i].Dish_ID)
                        {
                            idforlists.Add(dataset.Dish_Food[i].Food_ID);
                            for (int j = 0; j < dataset.Food.Count; j++)
                            {
                                if (dataset.Dish_Food[i].Food_ID == dataset.Food[j].ID_Food)
                                {
                                    Products_DishesLB.Items.Add(dataset.Food[j].Name);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void MenusData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)MenusData.SelectedItem;
                if (dataRowView != null)
                {
                    idforlists.Clear();
                    Dishes_MenuLB.Items.Clear();
                    NameMenu.Text = dataRowView.Row.Field<String>("Наименование");
                    TypeMenu.SelectedValue = dataRowView.Row.Field<int>("ID_Type");
                    DateMenu.SelectedDate = dataRowView.Row.Field<DateTime>("Дата");
                    int idmenu = dataRowView.Row.Field<int>("ID_Menu");
                    for (int i = 0; i < dataset.Menu_Dish.Count; i++)
                    {
                        if (idmenu == dataset.Menu_Dish[i].Menu_ID)
                        {
                            idforlists.Add(dataset.Menu_Dish[i].Dish_ID);
                            for (int j = 0; j < dataset.Dish.Count; j++)
                            {
                                if (dataset.Menu_Dish[i].Dish_ID == dataset.Dish[j].ID_Dish)
                                {
                                    Dishes_MenuLB.Items.Add(dataset.Dish[j].Name);
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FridgesData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)FridgesData.SelectedItem;
                if (dataRowView != null)
                {
                    NameFridge.Text = dataRowView.Row.Field<String>("Name");
                }

            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void CleaningsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)CleaningsData.SelectedItem;
                if (dataRowView != null)
                {
                    idforlists.Clear();
                    Equipment_CleaningLB.Items.Clear();
                    DateCleaning.SelectedDate = dataRowView.Row.Field<DateTime>("Дата");
                    NomerCleaning.SelectedValue = dataRowView.Row.Field<int>("ID_Nomer");
                    SotrudnikCleaning.SelectedValue = dataRowView.Row.Field<int>("ID_Sotrudnik");
                    int idcleaning = dataRowView.Row.Field<int>("ID_Cleaning");
                    for (int i = 0; i < dataset.Cleaning_Equipment.Count; i++)
                    {
                        if (idcleaning == dataset.Cleaning_Equipment[i].Cleaning_ID)
                        {
                            idforlists.Add(dataset.Cleaning_Equipment[i].Equipment_ID);
                            for (int j = 0; j < dataset.Equipment.Count; j++)
                            {
                                if (dataset.Cleaning_Equipment[i].Equipment_ID == dataset.Equipment[j].ID_Equipment)
                                {
                                    Equipment_CleaningLB.Items.Add(dataset.Equipment[j].Name);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void SotrudniksData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)SotrudniksData.SelectedItem;
                if (dataRowView != null)
                {
                    idforlists.Clear();
                    Doljnost_SotrudnikLB.Items.Clear();
                    DateBirthSotrudnik.SelectedDate = dataRowView.Row.Field<DateTime>("Дата рождения");
                    SurnameSotrudnik.Text = dataRowView.Row.Field<String>("Фамилия");
                    NameSotrudnik.Text = dataRowView.Row.Field<String>("Имя");
                    SecondNameSotrudnik.Text = dataRowView.Row.Field<String>("Отчество");
                    int idsotr = dataRowView.Row.Field<int>("ID_Sotrudnik");
                    for (int i = 0; i < dataset.Sotrudnik_Doljnost.Count; i++)
                    {
                        if (idsotr == dataset.Sotrudnik_Doljnost[i].Sotrudnik_ID)
                        {
                            idforlists.Add(dataset.Sotrudnik_Doljnost[i].Doljnost_ID);
                            for (int j = 0; j < dataset.Doljnost.Count; j++)
                            {
                                if (dataset.Sotrudnik_Doljnost[i].Doljnost_ID == dataset.Doljnost[j].ID_Doljnost)
                                {
                                    Doljnost_SotrudnikLB.Items.Add(dataset.Doljnost[j].Name);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DoljnostsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)DoljnostsData.SelectedItem;
                if (dataRowView != null)
                {
                    NameDoljnost.Text = dataRowView.Row.Field<String>("Name");
                    SalaryDoljnost.Text = dataRowView.Row.Field<decimal>("Salary").ToString();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void SkladsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)SkladsData.SelectedItem;
                if (dataRowView != null)
                {
                    NameSklad.Text = dataRowView.Row.Field<String>("Name");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EquipmentData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)EquipmentData.SelectedItem;
                if (dataRowView != null)
                {
                    NameEquipment.Text = dataRowView.Row.Field<String>("Наименование");
                    SkladEquipment.SelectedValue = dataRowView.Row.Field<int>("ID_Sklad");
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void RefreshDB()
        {
            try
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
                view_booksTA.Fill(dataset.View_Books);
                view_cleaningsTA.Fill(dataset.View_Cleanings);
                view_equipmentTA.Fill(dataset.View_Equipment);
                view_foodTA.Fill(dataset.View_Food);
                view_nomerTA.Fill(dataset.View_Nomer);
                view_priemsTA.Fill(dataset.View_Priems);
                view_sotrudnikTA.Fill(dataset.View_Sotrudnik);
                view_MenuTableTA.Fill(dataset.View_Menu);
                cleanMenTA.Fill(dataset.View_CleanMen);
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditUserTest(LoginUsers.Text, PasswordUsers.Text, (int)RoleUsers.SelectedValue, (int)SotrudnikUsers.SelectedValue);
                if (result)
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
                else
                    MessageBox.Show("Неверно заполнены поля");

            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddNomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditNomerTest(NumberRoom.Text, (int)TypeNumber.SelectedValue);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Number");
                    names.Add("Type_ID");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + NumberRoom.Text + "'");
                    parametrs.Add(TypeNumber.SelectedValue.ToString());

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Nomer", names, parametrs, "ID_Nomer");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditBookTest(DateStartBook.SelectedDate.Value.Date, DateEndBook.SelectedDate.Value.Date, (int)NomerBook.SelectedValue, (int)ClientBook.SelectedValue);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("DateStart");
                    names.Add("DateEnd");
                    names.Add("Nomer_ID");
                    names.Add("Client_ID");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + DateStartBook.SelectedDate.Value.Date.ToShortDateString() + "'");
                    parametrs.Add("'" + DateEndBook.SelectedDate.Value.Date.ToShortDateString() + "'");
                    parametrs.Add(NomerBook.SelectedValue.ToString());
                    parametrs.Add(ClientBook.SelectedValue.ToString());

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Book", names, parametrs, "ID_Book");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Ошибка заполнения полей");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditRoomTypeTest(NameRoomType.Text);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Name");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + NameRoomType.Text + "'");

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Type_Nomer", names, parametrs, "ID_Type");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddPriem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditPriemTest(DatePriem.SelectedDate.Value.Date, (int)MenuPriem.SelectedValue, (int)NomerPriem.SelectedValue);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Date");
                    names.Add("Menu_Date_ID");
                    names.Add("Nomer_ID");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + DatePriem.SelectedDate.Value.Date.ToShortDateString() + "'");
                    parametrs.Add(MenuPriem.SelectedValue.ToString());
                    parametrs.Add(NomerPriem.SelectedValue.ToString());

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Priem_Pitaniya", names, parametrs, "ID_Priem");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditClientTest(SurnameClient.Text, NameClient.Text, SecondNameClient.Text, DateBirthClient.SelectedDate.Value.Date, SeriasPassportClient.Text, NumberPassportClient.Text);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Surname");
                    names.Add("Name");
                    names.Add("Otchestvo");
                    names.Add("Date_Rozhdeniya");
                    names.Add("SeriaPas");
                    names.Add("NumberPas");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + SurnameClient.Text + "'");
                    parametrs.Add("'" + NameClient.Text + "'");
                    parametrs.Add("'" + SecondNameClient.Text + "'");
                    parametrs.Add("'" + DateBirthClient.SelectedDate.Value.Date.ToShortDateString() + "'");
                    parametrs.Add("'" + GetHash(SeriasPassportClient.Text) + "'");
                    parametrs.Add("'" + GetHash(NumberPassportClient.Text) + "'");

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Client", names, parametrs, "ID_Client");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditFoodTest(NameFood.Text, (int)FridgeFood.SelectedValue);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Name");
                    names.Add("Fridge_ID");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + NameFood.Text + "'");
                    parametrs.Add(FridgeFood.SelectedValue.ToString());

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Food", names, parametrs, "ID_Food");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");

            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddDish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditDishTest(NameDish.Text, WeightDish.Text);
                if (result)
                {
                    if (idforlists.Count > 0)
                    {
                        List<String> names = new List<String>();
                        names.Add("Name");
                        names.Add("Weight");

                        List<String> parametrs = new List<String>();
                        parametrs.Add("'" + NameDish.Text + "'");
                        parametrs.Add(WeightDish.Text);

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Insert("Dish", names, parametrs, "ID_Dish");

                        int idsotr = procedure.lastid;
                        names.Clear();
                        names.Add("Dish_ID");
                        names.Add("Weight");
                        names.Add("Food_ID");

                        parametrs.Clear();
                        parametrs.Add(idsotr.ToString());
                        parametrs.Add(WeightDish.Text);

                        for (int i = 0; i < idforlists.Count; i++)
                        {
                            if (parametrs.Count >= 3)
                                parametrs.RemoveAt(parametrs.Count - 1);
                            parametrs.Add(idforlists[i].ToString());
                            procedure.Insert("Dish_Food", names, parametrs, "ID_Dish_Food");
                        }
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Заполните список продуктов!");
                }
                else
                    MessageBox.Show("Неверно заполнены поля");

            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddMenuType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditMenuTypeTest(NameMenu.Text, DateMenu.SelectedDate.Value.Date, (int)TypeMenu.SelectedValue);
                if (result)
                {
                    if (idforlists.Count > 0)
                    {
                        List<String> names = new List<String>();
                        names.Add("Name");

                        List<String> parametrs = new List<String>();
                        parametrs.Add("'" + NameMenu.Text + "'");

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Insert("Menu", names, parametrs, "ID_Menu");

                        names.Clear();
                        parametrs.Clear();

                        names.Add("Date");
                        names.Add("Type_ID");
                        names.Add("Menu_ID");

                        parametrs.Add("'" + DateMenu.SelectedDate.Value.Date.ToShortDateString() + "'");
                        parametrs.Add(TypeMenu.SelectedValue.ToString());
                        parametrs.Add(procedure.lastid.ToString());

                        procedure.Insert("Menu_Date", names, parametrs, "ID_Menu_Date");

                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Заполните список блюд");
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddFridge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditFridgeTest(NameFridge.Text);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Name");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + NameFridge.Text + "'");

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Fridge", names, parametrs, "ID_Fridge");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");

            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddCleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditCleaningTest(DateCleaning.SelectedDate.Value.Date, (int)SotrudnikCleaning.SelectedValue, (int)NomerCleaning.SelectedValue);
                if (result)
                {
                    if (idforlists.Count > 0)
                    {
                        List<String> names = new List<String>();
                        names.Add("Date");
                        names.Add("Sotrudnik_ID");
                        names.Add("Nomer_ID");

                        List<String> parametrs = new List<String>();
                        parametrs.Add("'" + DateCleaning.SelectedDate.Value.Date.ToShortDateString() + "'");
                        parametrs.Add(SotrudnikCleaning.SelectedValue.ToString());
                        parametrs.Add(NomerCleaning.SelectedValue.ToString());

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Insert("Cleaning", names, parametrs, "ID_Cleaning");
                        int idsotr = procedure.lastid;
                        names.Clear();
                        names.Add("Cleaning_ID");
                        names.Add("Equipment_ID");

                        parametrs.Clear();
                        parametrs.Add(idsotr.ToString());

                        for (int i = 0; i < idforlists.Count; i++)
                        {
                            if (parametrs.Count >= 2)
                                parametrs.RemoveAt(parametrs.Count - 1);
                            parametrs.Add(idforlists[i].ToString());
                            procedure.Insert("Cleaning_Equipment", names, parametrs, "ID_Cleaning_Equipment");
                        }
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Заполните список использованного инвентаря");
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddSotrudnik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditSotrudnikTest(SurnameSotrudnik.Text, NameSotrudnik.Text, SecondNameSotrudnik.Text, DateBirthSotrudnik.SelectedDate.Value.Date);
                if (result)
                {
                    if (idforlists.Count > 0)
                    {
                        List<String> names = new List<String>();
                        names.Add("Surname");
                        names.Add("Name");
                        if (SecondNameSotrudnik.Text != "")
                            names.Add("Otchestvo");
                        names.Add("Date_Rozhdenia");

                        List<String> parametrs = new List<String>();
                        parametrs.Add("'" + SurnameSotrudnik.Text + "'");
                        parametrs.Add("'" + NameSotrudnik.Text + "'");
                        if (SecondNameSotrudnik.Text != "")
                            parametrs.Add("'" + SecondNameSotrudnik.Text + "'");
                        parametrs.Add("'" + DateBirthSotrudnik.SelectedDate.Value.Date.ToShortDateString() + "'");

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Insert("Sotrudnik", names, parametrs, "ID_Sotrudnik");

                        int idsotr = procedure.lastid;
                        names.Clear();
                        names.Add("Sotrudnik_ID");
                        names.Add("Doljnost_ID");

                        parametrs.Clear();
                        parametrs.Add(idsotr.ToString());

                        for (int i = 0; i < idforlists.Count; i++)
                        {
                            if (parametrs.Count >= 2)
                                parametrs.RemoveAt(parametrs.Count - 1);
                            parametrs.Add(idforlists[i].ToString());
                            procedure.Insert("Sotrudnik_Doljnost", names, parametrs, "ID_Sotrudnik_Doljnost");
                        }
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Зполните список должностей");
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddDoljnost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditDoljnostTest(NameDoljnost.Text, SalaryDoljnost.Text);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Name");
                    names.Add("Salary");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + NameDoljnost.Text + "'");
                    parametrs.Add(SalaryDoljnost.Text);

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Doljnost", names, parametrs, "ID_Doljnost");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddSklad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditSkladTest(NameSklad.Text);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Name");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + NameSklad.Text + "'");

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Sklad", names, parametrs, "ID_Sklad");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestClass test = new TestClass();
                bool result = test.AddEditEquipmentTest(NameEquipment.Text, (int)SkladEquipment.SelectedValue);
                if (result)
                {
                    List<String> names = new List<String>();
                    names.Add("Name");
                    names.Add("Sklad_ID");

                    List<String> parametrs = new List<String>();
                    parametrs.Add("'" + NameEquipment.Text + "'");
                    parametrs.Add(SkladEquipment.SelectedValue.ToString());

                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Insert("Equipment", names, parametrs, "ID_Equipment");
                    RefreshDB();
                }
                else
                    MessageBox.Show("Неверно заполнены поля");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditUserTest(LoginUsers.Text, PasswordUsers.Text, (int)RoleUsers.SelectedValue, (int)SotrudnikUsers.SelectedValue);
                    if (result)
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
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditNomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoomsData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditNomerTest(NumberRoom.Text, (int)TypeNumber.SelectedValue);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Nomer");
                        names.Add("Number");
                        names.Add("Type_ID");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(RoomsData.SelectedValue.ToString());
                        parametrs.Add("'" + NumberRoom.Text + "'");
                        parametrs.Add(TypeNumber.SelectedValue.ToString());

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Nomer", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditBookTest(DateStartBook.SelectedDate.Value.Date, DateEndBook.SelectedDate.Value.Date, (int)NomerBook.SelectedValue, (int)ClientBook.SelectedValue);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Book");
                        names.Add("DateStart");
                        names.Add("DateEnd");
                        names.Add("Nomer_ID");
                        names.Add("Client_ID");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(BooksData.SelectedValue.ToString());
                        parametrs.Add("'" + DateStartBook.SelectedDate.Value.Date.ToShortDateString() + "'");
                        parametrs.Add("'" + DateEndBook.SelectedDate.Value.Date.ToShortDateString() + "'");
                        parametrs.Add(NomerBook.SelectedValue.ToString());
                        parametrs.Add(ClientBook.SelectedValue.ToString());

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Book", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoomTypesData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditRoomTypeTest(NameRoomType.Text);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Type");
                        names.Add("Name");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(RoomTypesData.SelectedValue.ToString());
                        parametrs.Add("'" + NameRoomType.Text + "'");

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Type_Nomer", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditPriem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PriemsData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditPriemTest(DatePriem.SelectedDate.Value.Date, (int)MenuPriem.SelectedValue, (int)NomerPriem.SelectedValue);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Priem");
                        names.Add("Date");
                        names.Add("Menu_Date_ID");
                        names.Add("Nomer_ID");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(PriemsData.SelectedValue.ToString());
                        parametrs.Add("'" + DatePriem.SelectedDate.Value.Date.ToShortDateString() + "'");
                        parametrs.Add(MenuPriem.SelectedValue.ToString());
                        parametrs.Add(NomerPriem.SelectedValue.ToString());

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Priem_Pitaniya", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientsData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditClientTest(SurnameClient.Text, NameClient.Text, SecondNameClient.Text, DateBirthClient.SelectedDate.Value.Date, SeriasPassportClient.Text, NumberPassportClient.Text);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Client");
                        names.Add("Surname");
                        names.Add("Name");
                        names.Add("Otchestvo");
                        names.Add("Date_Rozhdeniya");
                        names.Add("SeriaPas");
                        names.Add("NumberPas");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(ClientsData.SelectedValue.ToString());
                        parametrs.Add("'" + SurnameClient.Text + "'");
                        parametrs.Add("'" + NameClient.Text + "'");
                        parametrs.Add("'" + SecondNameClient.Text + "'");
                        parametrs.Add("'" + DateBirthClient.SelectedDate.Value.Date.ToShortDateString() + "'");
                        parametrs.Add("'" + GetHash(SeriasPassportClient.Text) + "'");
                        parametrs.Add("'" + GetHash(NumberPassportClient.Text) + "'");

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Client", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditFood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FoodsData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditFoodTest(NameFood.Text, (int)FridgeFood.SelectedValue);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Food");
                        names.Add("Name");
                        names.Add("Fridge_ID");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(FoodsData.SelectedValue.ToString());
                        parametrs.Add("'" + NameFood.Text + "'");
                        parametrs.Add(FridgeFood.SelectedValue.ToString());

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Food", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditDish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DishesData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditDishTest(NameDish.Text, WeightDish.Text);
                    if (result)
                    {
                        if (idforlists.Count > 0)
                        {
                            List<String> names = new List<String>();
                            names.Add("ID_Dish");
                            names.Add("Name");
                            names.Add("Weight");

                            List<String> parametrs = new List<String>();
                            parametrs.Add(DishesData.SelectedValue.ToString());
                            parametrs.Add("'" + NameDish.Text + "'");
                            parametrs.Add(WeightDish.Text);

                            ProceduresDB procedure = new ProceduresDB();
                            procedure.Update("Dish", names, parametrs);

                            names.Clear();
                            names.Add("Dish_ID");

                            parametrs.Clear();
                            parametrs.Add(DishesData.SelectedValue.ToString());

                            procedure.Delete("Dish_Food", "Dish_ID", parametrs[0].ToString());

                            int idsotr = Convert.ToInt32(DishesData.SelectedValue.ToString());
                            names.Clear();
                            names.Add("Dish_ID");
                            names.Add("Weight");
                            names.Add("Food_ID");

                            parametrs.Clear();
                            parametrs.Add(idsotr.ToString());
                            parametrs.Add(WeightDish.Text);

                            for (int i = 0; i < idforlists.Count; i++)
                            {
                                if (parametrs.Count >= 3)
                                    parametrs.RemoveAt(parametrs.Count - 1);
                                parametrs.Add(idforlists[i].ToString());
                                procedure.Insert("Dish_Food", names, parametrs, "ID_Dish_Food");
                            }
                            RefreshDB();
                        }
                        else
                            MessageBox.Show("Заполните список продуктов!");
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditMenuType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MenusData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditMenuTypeTest(NameMenu.Text, DateMenu.SelectedDate.Value.Date, (int)TypeMenu.SelectedValue);
                    if (result)
                    {
                        if (idforlists.Count > 0)
                        {
                            List<String> names = new List<String>();
                            names.Add("ID_Menu");
                            names.Add("Name");

                            List<String> parametrs = new List<String>();
                            parametrs.Add(MenusData.SelectedValue.ToString());
                            parametrs.Add("'" + NameMenu.Text + "'");

                            ProceduresDB procedure = new ProceduresDB();
                            procedure.Update("Menu", names, parametrs);

                            names.Clear();
                            names.Add("Menu_ID");

                            parametrs.Clear();
                            parametrs.Add(MenusData.SelectedValue.ToString());

                            procedure.Delete("Menu_Dish", "Menu_ID", parametrs[0].ToString());

                            int idsotr = Convert.ToInt32(MenusData.SelectedValue.ToString());
                            names.Clear();
                            names.Add("Menu_ID");
                            names.Add("Dish_ID");

                            parametrs.Clear();
                            parametrs.Add(idsotr.ToString());

                            for (int i = 0; i < idforlists.Count; i++)
                            {
                                if (parametrs.Count >= 2)
                                    parametrs.RemoveAt(parametrs.Count - 1);
                                parametrs.Add(idforlists[i].ToString());
                                procedure.Insert("Menu_Dish", names, parametrs, "ID_Menu_Dish");
                            }
                            RefreshDB();
                        }
                        else
                            MessageBox.Show("Заполните список блюд");
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditFridge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FridgesData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditFridgeTest(NameFridge.Text);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Fridge");
                        names.Add("Name");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(FridgesData.SelectedValue.ToString());
                        parametrs.Add("'" + NameFridge.Text + "'");

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Fridge", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditCleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CleaningsData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditCleaningTest(DateCleaning.SelectedDate.Value.Date, (int)SotrudnikCleaning.SelectedValue, (int)NomerCleaning.SelectedValue);
                    if (result)
                    {
                        if (idforlists.Count > 0)
                        {
                            List<String> names = new List<String>();
                            names.Add("ID_Cleaning");
                            names.Add("Date");
                            names.Add("Sotrudnik_ID");
                            names.Add("Nomer_ID");

                            List<String> parametrs = new List<String>();
                            parametrs.Add(CleaningsData.SelectedValue.ToString());
                            parametrs.Add("'" + DateCleaning.SelectedDate.Value.Date.ToShortDateString() + "'");
                            parametrs.Add(SotrudnikCleaning.SelectedValue.ToString());
                            parametrs.Add(NomerCleaning.SelectedValue.ToString());

                            ProceduresDB procedure = new ProceduresDB();
                            procedure.Update("Cleaning", names, parametrs);

                            names.Clear();
                            names.Add("Cleaning_ID");

                            parametrs.Clear();
                            parametrs.Add(CleaningsData.SelectedValue.ToString());

                            procedure.Delete("Cleaning_Equipment", "Cleaning_ID", parametrs[0].ToString());

                            int idsotr = Convert.ToInt32(CleaningsData.SelectedValue.ToString());
                            names.Clear();
                            names.Add("Cleaning_ID");
                            names.Add("Equipment_ID");

                            parametrs.Clear();
                            parametrs.Add(idsotr.ToString());

                            for (int i = 0; i < idforlists.Count; i++)
                            {
                                if (parametrs.Count >= 2)
                                    parametrs.RemoveAt(parametrs.Count - 1);
                                parametrs.Add(idforlists[i].ToString());
                                procedure.Insert("Cleaning_Equipment", names, parametrs, "ID_Cleaning_Equipment");
                            }
                            RefreshDB();
                        }
                        else
                            MessageBox.Show("Заполните список использованного инвентаря");
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditSotrudnik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SotrudniksData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditSotrudnikTest(SurnameSotrudnik.Text, NameSotrudnik.Text, SecondNameSotrudnik.Text, DateBirthSotrudnik.SelectedDate.Value.Date);
                    if (result)
                    {
                        if (idforlists.Count > 0)
                        {
                            List<String> names = new List<String>();
                            names.Add("ID_Sotrudnik");
                            names.Add("Surname");
                            names.Add("Name");
                            names.Add("Otchestvo");
                            names.Add("Date_Rozhdenia");

                            List<String> parametrs = new List<String>();
                            parametrs.Add(SotrudniksData.SelectedValue.ToString());
                            parametrs.Add("'" + SurnameSotrudnik.Text + "'");
                            parametrs.Add("'" + NameSotrudnik.Text + "'");
                            parametrs.Add("'" + SecondNameSotrudnik.Text + "'");
                            parametrs.Add("'" + DateBirthSotrudnik.SelectedDate.Value.Date.ToShortDateString() + "'");

                            ProceduresDB procedure = new ProceduresDB();
                            procedure.Update("Sotrudnik", names, parametrs);

                            names.Clear();
                            names.Add("Sotrudnik_ID");

                            parametrs.Clear();
                            parametrs.Add(SotrudniksData.SelectedValue.ToString());

                            procedure.Delete("Sotrudnik_Doljnost", "Sotrudnik_ID", parametrs[0].ToString());

                            int idsotr = Convert.ToInt32(SotrudniksData.SelectedValue.ToString());
                            names.Clear();
                            names.Add("Sotrudnik_ID");
                            names.Add("Doljnost_ID");

                            parametrs.Clear();
                            parametrs.Add(idsotr.ToString());

                            for (int i = 0; i < idforlists.Count; i++)
                            {
                                if (parametrs.Count >= 2)
                                    parametrs.RemoveAt(parametrs.Count - 1);
                                parametrs.Add(idforlists[i].ToString());
                                procedure.Insert("Sotrudnik_Doljnost", names, parametrs, "ID_Sotrudnik_Doljnost");
                            }
                            RefreshDB();
                        }
                        else
                            MessageBox.Show("Зполните список должностей");
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditDoljnost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DoljnostsData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditDoljnostTest(NameDoljnost.Text, SalaryDoljnost.Text);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Doljnost");
                        names.Add("Name");
                        names.Add("Salary");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(DoljnostsData.SelectedValue.ToString());
                        parametrs.Add("'" + NameDoljnost.Text + "'");
                        parametrs.Add(SalaryDoljnost.Text);

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Doljnost", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditSklad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkladsData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditSkladTest(NameSklad.Text);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Sklad");
                        names.Add("Name");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(SkladsData.SelectedValue.ToString());
                        parametrs.Add("'" + NameSklad.Text + "'");

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Sklad", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void EditEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EquipmentData.SelectedValue != null)
                {
                    TestClass test = new TestClass();
                    bool result = test.AddEditEquipmentTest(NameEquipment.Text, (int)SkladEquipment.SelectedValue);
                    if (result)
                    {
                        List<String> names = new List<String>();
                        names.Add("ID_Equipment");
                        names.Add("Name");
                        names.Add("Sklad_ID");

                        List<String> parametrs = new List<String>();
                        parametrs.Add(EquipmentData.SelectedValue.ToString());
                        parametrs.Add("'" + NameEquipment.Text + "'");
                        parametrs.Add(SkladEquipment.SelectedValue.ToString());

                        ProceduresDB procedure = new ProceduresDB();
                        procedure.Update("Equipment", names, parametrs);
                        RefreshDB();
                    }
                    else
                        MessageBox.Show("Неверно заполнены поля");
                }
                else
                    MessageBox.Show("Выберите поле для редактирования");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Users", "ID_User", UsersData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteNomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoomsData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Nomer", "ID_Nomer", RoomsData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Book", "ID_Book", BooksData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoomTypesData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Type_Nomer", "ID_Type", RoomTypesData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeletePriem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PriemsData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Priem_Pitaniya", "ID_Priem", PriemsData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientsData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Client", "ID_Client", ClientsData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteFood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FoodsData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Food", "ID_Food", FoodsData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteDish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DishesData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Dish", "ID_Dish", DishesData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteMenuType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MenusData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Menu", "ID_Menu", MenusData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteFridge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FridgesData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Fridge", "ID_Fridge", FridgesData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteCleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CleaningsData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Cleaning", "ID_Cleaning", CleaningsData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteSotrudnik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SotrudniksData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Sotrudnik", "ID_Sotrudnik", SotrudniksData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteDoljnost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DoljnostsData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Doljnost", "ID_Doljnost", DoljnostsData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteSklad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkladsData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Sklad", "ID_Sklad", SkladsData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EquipmentData.SelectedValue != null)
                {
                    ProceduresDB procedure = new ProceduresDB();
                    procedure.Delete("Equipment", "ID_Equipment", EquipmentData.SelectedValue.ToString());
                    RefreshDB();
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddDoljnost_Sotrudnik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DoljnostsLB.SelectedIndex != -1)
                {
                    bool check = true;
                    string temp = "";
                    idforlists.Add(Convert.ToInt32(DoljnostsLB.SelectedValue));
                    for (int i = 0; i < dataset.Doljnost.Count; i++)
                    {
                        if (idforlists[idforlists.Count - 1].ToString() == dataset.Doljnost[i].ID_Doljnost.ToString())
                        {
                            temp = dataset.Doljnost[i].Name.ToString();
                            break;
                        }
                    }
                    for (int i = 0; i < Doljnost_SotrudnikLB.Items.Count; i++)
                    {
                        if (Doljnost_SotrudnikLB.Items[i].ToString() == temp)
                        {
                            idforlists.RemoveAt(idforlists.Count - 1);
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        Doljnost_SotrudnikLB.Items.Add(temp);
                    }

                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteDoljnost_Sotrudnik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Doljnost_SotrudnikLB.SelectedIndex != -1)
                {
                    idforlists.RemoveAt(Doljnost_SotrudnikLB.SelectedIndex);
                    Doljnost_SotrudnikLB.Items.Remove(Doljnost_SotrudnikLB.SelectedItem);
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddDishes_Menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DishesLB.SelectedIndex != -1)
                {
                    bool check = true;
                    string temp = "";
                    idforlists.Add(Convert.ToInt32(DishesLB.SelectedValue));
                    for (int i = 0; i < dataset.Dish.Count; i++)
                    {
                        if (idforlists[idforlists.Count - 1].ToString() == dataset.Dish[i].ID_Dish.ToString())
                        {
                            temp = dataset.Dish[i].Name.ToString();
                            break;
                        }
                    }
                    for (int i = 0; i < Dishes_MenuLB.Items.Count; i++)
                    {
                        if (Dishes_MenuLB.Items[i].ToString() == temp)
                        {
                            idforlists.RemoveAt(idforlists.Count - 1);
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        Dishes_MenuLB.Items.Add(temp);
                    }

                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteDishes_Menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dishes_MenuLB.SelectedIndex != -1)
                {
                    idforlists.RemoveAt(Dishes_MenuLB.SelectedIndex);
                    Dishes_MenuLB.Items.Remove(Dishes_MenuLB.SelectedItem);
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddEquipment_Cleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EquipmentLB.SelectedIndex != -1)
                {
                    bool check = true;
                    string temp = "";
                    idforlists.Add(Convert.ToInt32(EquipmentLB.SelectedValue));
                    for (int i = 0; i < dataset.Equipment.Count; i++)
                    {
                        if (idforlists[idforlists.Count - 1].ToString() == dataset.Equipment[i].ID_Equipment.ToString())
                        {
                            temp = dataset.Equipment[i].Name.ToString();
                            break;
                        }
                    }
                    for (int i = 0; i < Equipment_CleaningLB.Items.Count; i++)
                    {
                        if (Equipment_CleaningLB.Items[i].ToString() == temp)
                        {
                            idforlists.RemoveAt(idforlists.Count - 1);
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        Equipment_CleaningLB.Items.Add(temp);
                    }

                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteEquipment_Cleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Equipment_CleaningLB.SelectedIndex != -1)
                {
                    idforlists.RemoveAt(Equipment_CleaningLB.SelectedIndex);
                    Equipment_CleaningLB.Items.Remove(Equipment_CleaningLB.SelectedItem);
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void AddProducts_Dishes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductsLB.SelectedIndex != -1)
                {
                    bool check = true;
                    string temp = "";
                    idforlists.Add(Convert.ToInt32(ProductsLB.SelectedValue));
                    for (int i = 0; i < dataset.Food.Count; i++)
                    {
                        if (idforlists[idforlists.Count - 1].ToString() == dataset.Food[i].ID_Food.ToString())
                        {
                            temp = dataset.Food[i].Name.ToString();
                            break;
                        }
                    }
                    for (int i = 0; i < Products_DishesLB.Items.Count; i++)
                    {
                        if (Products_DishesLB.Items[i].ToString() == temp)
                        {
                            idforlists.RemoveAt(idforlists.Count - 1);
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        Products_DishesLB.Items.Add(temp);
                    }

                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DeleteProducts_Dishes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Products_DishesLB.SelectedIndex != -1)
                {
                    idforlists.RemoveAt(Products_DishesLB.SelectedIndex);
                    Products_DishesLB.Items.Remove(Products_DishesLB.SelectedItem);
                }
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void ClearList(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                idforlists.Clear();
            }
        }

        public string GetHash(string input)
        {
            try
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                return Convert.ToBase64String(hash);
            }
            catch
            {
                MessageBox.Show("Неизвестная ошибка");
                return "";
            }
        }

        private void FiltrUsers_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
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
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrRooms_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrRooms.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    try
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Nomer] WHERE [Номер] = '" + Convert.ToInt32(FiltrRooms.Text) + "' OR [Тип номера] = '" + FiltrRooms.Text + "'");
                    }
                    catch
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Nomer] WHERE [Тип номера] = '" + FiltrRooms.Text + "'");
                    }
                    RoomsData.ItemsSource = pr.FilterSQL.DefaultView;
                }
                else
                {
                    RoomsData.ItemsSource = dataset.View_Nomer.DefaultView;
                    RoomsData.SelectedValuePath = "ID_Nomer";
                }
                RoomsData.Columns[0].Visibility = Visibility.Hidden;
                RoomsData.Columns[1].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrBooks_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrBooks.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    try
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Books] WHERE [Номер] = '" + Convert.ToInt32(FiltrBooks.Text) + "' OR [Тип номера] = '" + FiltrBooks.Text + "' OR [ФИО] = '" + FiltrBooks.Text + "'");
                    }
                    catch
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Books] WHERE [Тип номера] = '" + FiltrBooks.Text + "' OR [ФИО] = '" + FiltrBooks.Text + "'");
                    }
                    BooksData.ItemsSource = pr.FilterSQL.DefaultView;
                }
                else
                {
                    BooksData.ItemsSource = dataset.View_Books.DefaultView;
                    BooksData.SelectedValuePath = "ID_Book";
                }
                BooksData.Columns[0].Visibility = Visibility.Hidden;
                BooksData.Columns[1].Visibility = Visibility.Hidden;
                BooksData.Columns[2].Visibility = Visibility.Hidden;
                BooksData.Columns[3].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrCleanings2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrCleanings2.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    try
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Cleanings] WHERE [Номер] = '" + Convert.ToInt32(FiltrCleanings2.Text) + "' OR [ФИО] = '" + FiltrCleanings2.Text + "' OR [Тип номера] = '" + FiltrCleanings2.Text + "'");
                    }
                    catch
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Cleanings] WHERE [ФИО] = '" + FiltrCleanings2.Text + "' OR [Тип номера] = '" + FiltrCleanings2.Text + "'");
                    }
                    CleaningsData2.ItemsSource = pr.FilterSQL.DefaultView;

                }
                else
                {
                    CleaningsData2.ItemsSource = dataset.View_Cleanings.DefaultView;
                    CleaningsData2.SelectedValuePath = "ID_Cleaning";
                }
                CleaningsData2.Columns[0].Visibility = Visibility.Hidden;
                CleaningsData2.Columns[1].Visibility = Visibility.Hidden;
                CleaningsData2.Columns[2].Visibility = Visibility.Hidden;
                CleaningsData2.Columns[3].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrPriems_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrPriems.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    try
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Priems] WHERE [Номер] = '" + Convert.ToInt32(FiltrPriems.Text) + "' OR [Прием питания] = '" + FiltrPriems.Text + "' OR [Название меню] = '" + FiltrPriems.Text + "'");
                    }
                    catch
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Priems] WHERE [Прием питания] = '" + FiltrPriems.Text + "' OR [Название меню] = '" + FiltrPriems.Text + "'");
                    }
                    PriemsData.ItemsSource = pr.FilterSQL.DefaultView;
                }
                else
                {
                    PriemsData.ItemsSource = dataset.View_Priems.DefaultView;
                    PriemsData.SelectedValuePath = "ID_Priem";
                }
                PriemsData.Columns[0].Visibility = Visibility.Hidden;
                PriemsData.Columns[1].Visibility = Visibility.Hidden;
                PriemsData.Columns[2].Visibility = Visibility.Hidden;
                PriemsData.Columns[3].Visibility = Visibility.Hidden;
                PriemsData.Columns[4].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrClients_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrClients.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[Client] WHERE [Surname] = '" + FiltrClients.Text + "' OR [Name] = '" + FiltrClients.Text + "' OR [Otchestvo] = '" + FiltrUsers.Text + "'");
                    ClientsData.ItemsSource = pr.FilterSQL.DefaultView;
                }
                else
                {
                    ClientsData.ItemsSource = dataset.Client.DefaultView;
                    ClientsData.SelectedValuePath = "ID_Client";
                }
                ClientsData.Columns[0].Visibility = Visibility.Hidden;
                ClientsData.Columns[1].Header = "Фамилия";
                ClientsData.Columns[2].Header = "Имя";
                ClientsData.Columns[3].Header = "Отчество";
                ClientsData.Columns[4].Header = "Дата рождения";
                ClientsData.Columns[5].Visibility = Visibility.Hidden;
                ClientsData.Columns[6].Visibility = Visibility.Hidden;
                ClientsData.Columns[ClientsData.Columns.Count - 1].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrFood_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrFood.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Food] WHERE [Наименование] = '" + FiltrFood.Text + "' OR [Место хранения] = '" + FiltrFood.Text + "'");
                    FoodsData.ItemsSource = pr.FilterSQL.DefaultView;
                }
                else
                {
                    FoodsData.ItemsSource = dataset.View_Food.DefaultView;
                    FoodsData.SelectedValuePath = "ID_Food";
                }
                FoodsData.Columns[0].Visibility = Visibility.Hidden;
                FoodsData.Columns[1].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrDish_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrDish.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[Dish] WHERE [Name] = '" + FiltrDish.Text + "'");
                    DishesData.ItemsSource = pr.FilterSQL.DefaultView;

                }
                else
                {
                    DishesData.ItemsSource = dataset.Dish.DefaultView;
                    DishesData.SelectedValuePath = "ID_Dish";
                }
                DishesData.Columns[0].Visibility = Visibility.Hidden;
                DishesData.Columns[1].Header = "Наименование";
                DishesData.Columns[2].Header = "Вес";
                DishesData.Columns[1].Visibility = Visibility.Visible;
                DishesData.Columns[2].Visibility = Visibility.Visible;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrCleanings_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrCleanings.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    try
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Cleanings] WHERE [Номер] = '" + Convert.ToInt32(FiltrCleanings.Text) + "' OR [ФИО] = '" + FiltrCleanings.Text + "' OR [Тип номера] = '" + FiltrCleanings.Text + "'");
                    }
                    catch
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Cleanings] WHERE [ФИО] = '" + FiltrCleanings.Text + "' OR [Тип номера] = '" + FiltrCleanings.Text + "'");
                    }
                    CleaningsData.ItemsSource = pr.FilterSQL.DefaultView;
                }
                else
                {
                    CleaningsData.ItemsSource = dataset.View_Cleanings.DefaultView;
                    CleaningsData.SelectedValuePath = "ID_Cleaning";
                }
                CleaningsData.Columns[0].Visibility = Visibility.Hidden;
                CleaningsData.Columns[1].Visibility = Visibility.Hidden;
                CleaningsData.Columns[2].Visibility = Visibility.Hidden;
                CleaningsData.Columns[3].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrRooms2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrRooms2.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    try
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Nomer] WHERE [Номер] = '" + Convert.ToInt32(FiltrRooms2.Text) + "' OR [Тип номера] = '" + FiltrRooms2.Text + "'");
                    }
                    catch
                    {
                        pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Nomer] WHERE [Тип номера] = '" + FiltrRooms2.Text + "'");
                    }
                    RoomsData2.ItemsSource = pr.FilterSQL.DefaultView;

                }
                else
                {
                    RoomsData2.ItemsSource = dataset.View_Nomer.DefaultView;
                    RoomsData2.SelectedValuePath = "ID_Nomer";
                }
                RoomsData2.Columns[0].Visibility = Visibility.Hidden;
                RoomsData2.Columns[1].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void FiltrSotrudniks_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (FiltrSotrudniks.Text != "")
                {
                    ProceduresDB pr = new ProceduresDB();
                    pr.FilterSQL = pr.Filter("SELECT * FROM [dbo].[View_Sotrudnik] WHERE [Фамилия] = '" + FiltrSotrudniks.Text + "' OR [Имя] = '" + FiltrSotrudniks.Text + "' OR [Отчество] = '" + FiltrSotrudniks.Text + "' OR [Должность] = '" + FiltrSotrudniks.Text + "'");
                    SotrudniksData.ItemsSource = pr.FilterSQL.DefaultView;
                }
                else
                {
                    SotrudniksData.ItemsSource = dataset.View_Sotrudnik.DefaultView;
                    SotrudniksData.SelectedValuePath = "ID_Sotrudnik";
                }
                SotrudniksData.Columns[0].Visibility = Visibility.Hidden;
                SotrudniksData.Columns[1].Visibility = Visibility.Hidden;
                SotrudniksData.Columns[2].Visibility = Visibility.Hidden;
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DoADiagrammDoljnost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> name = new List<string>();
                List<string> param = new List<string>();

                for (int i = 0; i < dataset.View_Sotrudnik.Count; i++)
                {
                    bool proverka = true;
                    for (int j = 0; j < name.Count; j++)
                    {
                        if (name[j] == dataset.View_Sotrudnik[i].Должность)
                        {
                            proverka = false;
                            double temp = Convert.ToDouble(param[j]);
                            param[j] = (temp + Convert.ToDouble(dataset.View_Sotrudnik[i].Зарплата)).ToString();
                        }
                    }
                    if (proverka)
                    {
                        name.Add(dataset.View_Sotrudnik[i].Должность);
                        param.Add(dataset.View_Sotrudnik[i].Зарплата.ToString());
                    }
                }
                Diagram diagram = new Diagram(name, param);
                diagram.ShowDialog();
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DoADiagrammBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> name = new List<string>();
                List<string> param = new List<string>();

                for (int i = 0; i < dataset.Book.Count; i++)
                {
                    bool proverka = true;
                    string dates = dataset.Book[i].DateStart.ToString() + "-" + dataset.Book[i].DateEnd.ToString();
                    for (int j = 0; j < name.Count; j++)
                    {
                        if (name[j] == dates)
                        {
                            proverka = false;
                            int temp = Convert.ToInt32(param[j]);
                            param[j] = (temp + 1).ToString();
                        }
                    }
                    if (proverka)
                    {
                        name.Add(dates);
                        param.Add("1");
                    }
                }
                Diagram diagram = new Diagram(name, param);
                diagram.ShowDialog();
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void DoADiagrammSotrudnik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> name = new List<string>();
                List<string> param = new List<string>();

                for (int i = 0; i < dataset.View_Sotrudnik.Count; i++)
                {
                    bool proverka = true;
                    for (int j = 0; j < name.Count; j++)
                    {
                        if (name[j] == dataset.View_Sotrudnik[i].Должность)
                        {
                            proverka = false;
                            int temp = Convert.ToInt32(param[j]);
                            param[j] = (temp + 1).ToString();
                        }
                    }
                    if (proverka)
                    {
                        name.Add(dataset.View_Sotrudnik[i].Должность);
                        param.Add("1");
                    }
                }
                Diagram diagram = new Diagram(name, param);
                diagram.ShowDialog();
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void ConnectionSetting_Click(object sender, RoutedEventArgs e)
        {
            ConnectDB connect = new ConnectDB();
            connect.Show();
            this.Close();
        }

        private void DoADocumentBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksData.SelectedIndex != -1)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "docx file (*.docx)|*.docx";
                    if (save.ShowDialog() == true)
                    {
                        int index = BooksData.SelectedIndex;
                        BooksData.SelectedIndex = -1;
                        BooksData.SelectedIndex = index;
                        List<string> param = new List<string>();
                        param.Add(DateStartBook.SelectedDate.Value.Year.ToString());
                        param.Add(DateStartBook.SelectedDate.Value.Day.ToString());
                        string monthandyear = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateStartBook.SelectedDate.Value.Month) + " " + DateStartBook.SelectedDate.Value.Year.ToString();
                        param.Add(monthandyear);
                        int clientid = (int)ClientBook.SelectedValue;
                        string fioclient = "";
                        for (int i = 0; i < dataset.Client.Count; i++)
                        {
                            if (clientid == dataset.Client[i].ID_Client)
                            {
                                fioclient = dataset.Client[i].Surname + " " + dataset.Client[i].Name.Substring(0, 1) + ".";
                                if (dataset.Client[i].Otchestvo != null)
                                    fioclient += dataset.Client[i].Otchestvo.Substring(0, 1) + ".";
                            }
                        }
                        param.Add(fioclient);

                        ExportBookToWord(param, save.FileName);
                    }
                }
                else
                    MessageBox.Show("Выберите поле для экспорта!");
            }
            catch { MessageBox.Show("Неизвестная ошибка"); }
        }

        private void ExportBookToWord(List<string> param, string filepath)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                var wordDocument = wordApp.Documents.Open(Directory.GetCurrentDirectory() + @"\TemplatesForDocuments\bookdoc.docx");

                ReplaceWordStub("{yearofbook}", param[0], wordDocument);
                ReplaceWordStub("{dateofbookDAY}", param[1], wordDocument);
                ReplaceWordStub("{dateofbookMONTHANDYEAR}", param[2], wordDocument);
                ReplaceWordStub("{clientfio}", param[3], wordDocument);
                wordDocument.SaveAs2(FileName: filepath);
                wordApp.Visible = true;

                MessageBox.Show("Успешное сохранение!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при сохранении!");
            }
        }

        private void ReplaceWordStub(string stub, string text, Word.Document document)
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stub, ReplaceWith: text);
        }
    }
}