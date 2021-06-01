using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abiturients
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
            string command = String.Empty;
            if(school == "")
            {
                command = $"SELECT * FROM abiturients WHERE (mark1 + mark2 + mark3) >= {mark}";
            }
            else
            {
                command = $"SELECT * FROM abiturients WHERE (mark1 + mark2 + mark3) >= {mark} AND school = '{school}'";
            }
            
            return GetList(command);
        }

        public int AddToBase(Student abiturient)
        {
            try
            {
                command.CommandText = $"SELECT * FROM abiturients WHERE fullName = '{abiturient.fullName}' AND school = '{abiturient.school}';";

                reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();
                    command.CommandText = "INSERT INTO abiturients(fullName,mark1,mark2,mark3,school) VALUES(@fullName, @mark1, @mark2, @mark3, @school)";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@fullName", abiturient.fullName);
                    command.Parameters.AddWithValue("@mark1", abiturient.marks[0]);
                    command.Parameters.AddWithValue("@mark2", abiturient.marks[1]);
                    command.Parameters.AddWithValue("@mark3", abiturient.marks[2]);
                    command.Parameters.AddWithValue("@school", abiturient.school);

                    command.ExecuteNonQuery();

                    return 1;
                }
                else
                {
                    reader.Close();
                    return -1;
                }

            }
            catch
            {
                reader.Close();
                return 0;
            }
        }

        public bool DeleteFromBase(Student abiturient)
        {
            try
            {
                command.CommandText = $"DELETE FROM abiturients WHERE fullName = '{abiturient.fullName}' AND mark1 = {abiturient.marks[0]} " +
                    $"AND mark2 = {abiturient.marks[1]} AND mark3 = {abiturient.marks[2]} AND school = '{abiturient.school}';";


                if (command.ExecuteNonQuery() == 0)
                {
                    throw new Exception();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
