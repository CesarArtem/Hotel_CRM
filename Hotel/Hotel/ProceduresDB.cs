using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel
{
    class ProceduresDB
    {
        SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
        private string connectionString;
        public int lastid;

        public System.Data.DataTable FilterSQL = new System.Data.DataTable("database");

        public void Insert(string tablename, List<String> names, List<String> parametrs, string idname)
        {
            try
            {
                stringBuilder.ConnectionString = Properties.Settings.Default.HotelConnectionString1;
                connectionString = stringBuilder.ConnectionString;
                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();
                
                string part1 = "";
                for (int i = 0; i < names.Count; i++)
                {
                    if (i != names.Count - 1)
                        part1 += names[i] + ", ";
                    else
                        part1 += names[i];
                }

                string part2 = "";
                for (int i = 0; i < parametrs.Count; i++)
                {
                    if (parametrs[i].Contains(","))
                        parametrs[i] = DotsHelper(parametrs[i]);

                    if (i != parametrs.Count - 1)
                        part2 += parametrs[i] + ", ";
                    else
                        part2 += parametrs[i];
                }
                string sql="";
                if (tablename != "Sotrudnik" && tablename != "Client")
                {
                    sql = string.Format($"Insert Into {tablename} " + $"({part1}) " +
                        $"OUTPUT INSERTED.{idname} " +
                        $"Values({part2})");
                }
                else
                {
                    sql = string.Format($"Insert Into {tablename} " + $"({part1}) " +
                       $"Values({part2}) Select SCOPE_IDENTITY()");
                }

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                        lastid = Convert.ToInt32(cmd.ExecuteScalar());
                 }
                    connect.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Update(string tablename, List<String> names, List<String> parametrs)
        {
            try
            {
                stringBuilder.ConnectionString = Properties.Settings.Default.HotelConnectionString1;
                connectionString = stringBuilder.ConnectionString;
                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();

                string part1 = "";
                for (int i=1;i<names.Count;i++)
                {
                    if (parametrs[i].Contains(","))
                        parametrs[i] = DotsHelper(parametrs[i]);
                    part1 += names[i] + "=" + parametrs[i];
                    if (i != names.Count - 1)
                        part1 += ", ";
                }
                string sql = string.Format($"Update {tablename} Set {part1} WHERE {names[0]}={parametrs[0]}");

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.ExecuteNonQuery();
                }
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Delete(string tablename, string idname, string id)
        {
            try
            {
                stringBuilder.ConnectionString = Properties.Settings.Default.HotelConnectionString1;
                connectionString = stringBuilder.ConnectionString;
                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();
                string sql = string.Format($"Delete FROM {tablename} WHERE {idname}={id}");

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.ExecuteNonQuery();
                }
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string DotsHelper(string line)
        {
            string result = line.Replace(",", ".");
            return result;
        }

        public System.Data.DataTable Filter(string SearchSQL)
        {
            stringBuilder.ConnectionString = Properties.Settings.Default.HotelConnectionString1;
            connectionString = stringBuilder.ConnectionString;
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();
            SqlCommand command = connect.CreateCommand();
            command.CommandText = SearchSQL;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(FilterSQL);
            connect.Close();
            return FilterSQL;
        }
    }
}
