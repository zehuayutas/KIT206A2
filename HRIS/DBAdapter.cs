using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Model;
using MySql.Data.MySqlClient;


namespace HRIS.Database
{
    class DBAdapter
    {
        //Database connection info
        private const string db = "hris";
        private const string user = "kit206g13a";
        private const string pass = "group13a";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Deal with enum variable
        public static T ParseEnum<T>(string value)
        {
            if (value != "") {
                return (T)Enum.Parse(typeof(T), value);
            }
            else { return default(T); }

        }

        //Connect and returen MySQL database
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}; SslMode=none; Convert Zero Datetime=True", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        //Get all staff
        public static List<Staff> GetAllStaff()
        {
            List<Staff> staffs = new List<Staff>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, " +
                                                    "phone, room, photo, email, campus, category" +
                                                    " from staff order by family_name ", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Create new Staff and set the value of each variable in Staff list
                    staffs.Add(new Staff
                    {
                        Id = rdr.GetInt32(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        Title = rdr.GetString(3),
                        Phone = rdr.GetString(4),
                        Room = rdr.GetString(5),
                        Photo = (rdr[6] as string) ?? "",
                        Email = rdr.GetString(7),
                        Campus = ParseEnum<Campus>(rdr.GetString(8)),        
                        Category = ParseEnum<Category>(rdr.GetString(9))
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return staffs;
        }

        //Get One Staff Info
        public static Staff GetStaffDetails(Staff s)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, " +
                                                    "phone, room, photo, email, campus, category" +
                                                    " from staff where id = ?id ", conn);

                cmd.Parameters.AddWithValue("id", s.Id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Create new Staff and set the value of each variable in Staff list
                    s.Id = rdr.GetInt32(0);
                    s.GivenName = rdr.GetString(1);
                    s.FamilyName = rdr.GetString(2);
                    s.Title = rdr.GetString(3);
                    s.Phone = rdr.GetString(4);
                    s.Room = rdr.GetString(5);
                    s.Photo = (rdr[6] as string) ?? "";
                    s.Email = rdr.GetString(7);
                    s.Campus = ParseEnum<Campus>(rdr.GetString(8));
                    s.Category = ParseEnum<Category>(rdr.GetString(9));

                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return s;
        }
    }
}
