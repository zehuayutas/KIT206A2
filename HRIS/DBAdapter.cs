using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Model;
using MySql.Data.MySqlClient;
using Type = HRIS.Model.Type;

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
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}; SslMode=none; Convert Zero Datetime=True; Allow User Variables=True", db, server, user, pass);
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
        public static Staff GetStaffByID(int id)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            Staff s = new Staff();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, " +
                                                    "phone, room, photo, email, campus, category" +
                                                    " from staff where id = ?id ", conn);

                cmd.Parameters.AddWithValue("id", id);
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


        //Get One Staff Info
        public static void UpdateStaffDetails(Staff s)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("update staff set title = ?title, " +
                                                    "phone = ?phone, room = ?room, photo = ?photo, email = ?email, campus= ?campus, category= ?category" +
                                                    " where id = ?id ", conn);

                cmd.Parameters.AddWithValue("title", s.Title);
                cmd.Parameters.AddWithValue("phone", s.Phone);
                cmd.Parameters.AddWithValue("room", s.Room);
                cmd.Parameters.AddWithValue("photo", s.Photo);
                cmd.Parameters.AddWithValue("email", s.Email);
                cmd.Parameters.AddWithValue("campus", s.Campus.ToString());
                cmd.Parameters.AddWithValue("category", s.Category.ToString());
                cmd.Parameters.AddWithValue("id", s.Id);
                rdr = cmd.ExecuteReader();

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

        }

        //Get all Units
        public static List<Unit> GetAllUnits()
        {
            List<Unit> units = new List<Unit>();


  

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select coordinator, code, title from unit order by code", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Create new Unit and set the value of each variable in Unit list
                    units.Add(new Unit
                    {
                        Coordinator = rdr.GetInt32(0),
                        Code = rdr.GetString(1),
                        Title = rdr.GetString(2)
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

            return units;
        }

        //Get all Units
        public static void AddNewUnit(Unit u)
        {

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into unit(code, title, coordinator) values(?code, ?title, ?coordinator)", conn);
 

                cmd.Parameters.AddWithValue("code", u.Code);
                cmd.Parameters.AddWithValue("title", u.Title);
                cmd.Parameters.AddWithValue("coordinator", u.Coordinator);

                rdr = cmd.ExecuteReader();
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

        }


        //Get all Units
        public static void UpdateUnit(Unit u)
        {

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("update unit setvalue set coordinator = ?coordinator where code = ?code)", conn);


                cmd.Parameters.AddWithValue("code", u.Code);
                cmd.Parameters.AddWithValue("coordinator", u.Coordinator);

                rdr = cmd.ExecuteReader();
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

        }


        //Get all one staff's consultations
        public static List<Consultation> GetConsultationsByStaff(int staffId)
        {
            List<Consultation> consultations = new List<Consultation>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();


                //Sql to search events of staff
                MySqlCommand cmd = new MySqlCommand("select staff_id, day, start, end from consultation " +
                                                    "where staff_id=?id", conn);

                cmd.Parameters.AddWithValue("id", staffId);
                rdr = cmd.ExecuteReader();

                //Loop to store events
                while (rdr.Read())
                {
                    consultations.Add(new Consultation
                    {
                        StaffID = rdr.GetInt32(0),
                        Day = ParseEnum<Day>(rdr.GetString(1)),
                        StartTime = rdr.GetTimeSpan(2),
                        EndTime = rdr.GetTimeSpan(3)
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

            return consultations;
        }

        //Get all one unit's classes
        public static List<Class> GetUnitClasses(string unitCode)
        {
            List<Class> classes = new List<Class>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select staff, unit_code, campus, day, start, end, type, room" +
                                                      " from class where unit_code = ?unitCode", conn);

                cmd.Parameters.AddWithValue("unitCode", unitCode);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Create new Unit and set the value of each variable in Unit list
                    classes.Add(new Class
                    {
                        Staff = rdr.GetInt32(0),
                        UnitCode = rdr.GetString(1),
                        Campus = ParseEnum<Campus>(rdr.GetString(2)),
                        Day = ParseEnum<Day>(rdr.GetString(3)),
                        StartTime = rdr.GetTimeSpan(4),
                        EndTime = rdr.GetTimeSpan(5),
                        Type = ParseEnum<Type>(rdr.GetString(6)),
                        Room = rdr.GetString(7)
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

            return classes;
        }

        //Add new Class
        public static void AddNewClass(Class c)
        {

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into class(unit_code, campus, day, start, end, type, room, staff) values(?unit_code, ?campus, ?day, ?start, ?end, ?type, ?room, ?staff)", conn);


                cmd.Parameters.AddWithValue("unit_code", c.UnitCode);
                cmd.Parameters.AddWithValue("campus", c.Campus.ToString());
                cmd.Parameters.AddWithValue("day", c.Day.ToString());
                cmd.Parameters.AddWithValue("start", c.StartTime);
                cmd.Parameters.AddWithValue("end", c.EndTime);
                cmd.Parameters.AddWithValue("type", c.Type.ToString());
                cmd.Parameters.AddWithValue("room", c.Room.ToString());
                cmd.Parameters.AddWithValue("staff", c.Staff);

                rdr = cmd.ExecuteReader();
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

        }
    }
}
