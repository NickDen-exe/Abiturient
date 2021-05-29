using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abiturient
{
    public class DataAccess
    {
        MySqlConnection conn;
        MySqlCommand command;
        MySqlDataReader reader;

        public DataAccess()
        {
            string connStr = "server=localhost; user=admin; password=; database=abiturients; port=3306;";
            conn = new MySqlConnection(connStr);
            command = new MySqlCommand();
            command.Connection = conn;

            command.Connection.Open();
        }

        private List<Student> GetList(string commandText)
        {
            try
            {
                command.CommandText = commandText;

                reader = command.ExecuteReader();

                List<Student> result = new List<Student> { };

                while (reader.Read())
                {
                    result.Add(new Student(reader.GetString(0), new int[3] { reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3) }, reader.GetString(4)));
                }

                reader.Close();
                if (result.Count == 0)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Student> GetStudents()
        {
            return GetList("SELECT * FROM abiturients;");
        }

        public bool CheckLogin(string login, string password)
        {
            command.CommandText = $"SELECT * FROM editors WHERE login = '{login}' AND password = '{password}'";
            reader = command.ExecuteReader();

            bool result = reader.HasRows;
            reader.Close();

            return result;
        }

        public List<Student> SelectData(string mark, string school = "")
        {
            if(school == "")
            {
                command.CommandText = $"SELECT * FROM abiturients WHERE (mark1 + mark2 + mark3) >= {mark}";
            }
            else
            {
                command.CommandText = $"SELECT * FROM abiturients WHERE (mark1 + mark2 + mark3) >= {mark} AND school = '{school}'";
            }

            List<Student> result = new List<Student>();

            reader = command.ExecuteReader();
            while(reader.Read())
            {
                result.Add(new Student(reader.GetString(0), new int[3] { reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3) }, reader.GetString(4)));
            }
            reader.Close();

            return result;
        }

        //public int AddToBase(Abiturient abiturient)
        //{
        //    try
        //    {
        //        command.CommandText = $"SELECT * FROM abiturients WHERE city = '{weather.city}' AND date = {weather.day} " +
        //            $"AND month = '{weather.month}';";

        //        reader = command.ExecuteReader();

        //        if (!reader.HasRows)
        //        {
        //            reader.Close();
        //            command.CommandText = "INSERT INTO cloudy(city,date,month,temperature,precipitation,pressure) VALUES(@city, @date, @month, @temperature, @precipitation, @pressure)";

        //            command.Parameters.Clear();

        //            command.Parameters.AddWithValue("@city", weather.city);
        //            command.Parameters.AddWithValue("@date", weather.day);
        //            command.Parameters.AddWithValue("@month", weather.month);
        //            command.Parameters.AddWithValue("@temperature", weather.temperature);
        //            command.Parameters.AddWithValue("@precipitation", weather.precipitation == "Є" ? true : false);
        //            command.Parameters.AddWithValue("@pressure", weather.pressure);

        //            command.ExecuteNonQuery();

        //            return 1;
        //        }
        //        else
        //        {
        //            reader.Close();
        //            return -1;
        //        }

        //    }
        //    catch
        //    {
        //        reader.Close();
        //        return 0;
        //    }
        //}

        //public bool DeleteFromBase(Abiturient weather)
        //{
        //    try
        //    {
        //        command.CommandText = $"DELETE FROM cloudy WHERE city = '{weather.city}' AND date = {weather.day} " +
        //            $"AND month = '{weather.month}' AND temperature = {weather.temperature} AND precipitation = {(weather.precipitation == "Є" ? true : false)} " +
        //            $"AND pressure = {weather.pressure};";

        //        if (command.ExecuteNonQuery() == 0)
        //        {
        //            throw new Exception();
        //        }

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public List<Abiturient> SelectXY(string city, string month)
        //{
        //    try
        //    {
        //        string commandText;
        //        if (city != String.Empty && month != String.Empty)
        //        {
        //            commandText = $"SELECT * FROM abiturients WHERE city = '{city}' AND month = '{month}';";
        //        }
        //        else if (city != String.Empty)
        //        {
        //            commandText = $"SELECT * FROM abiturients WHERE city = '{city}';";
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }

        //        return GetList(commandText);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public List<Abiturient> GetRainData()
        //{
        //    return GetList($"SELECT * FROM abiturients WHERE temperature > 0 AND precipitation = 1;");
        //}
    }
}
