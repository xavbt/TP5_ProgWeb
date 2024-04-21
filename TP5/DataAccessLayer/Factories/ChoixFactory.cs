using TP5.Models;
using MySql.Data.MySqlClient;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Events;
using Microsoft.AspNetCore.Mvc;
using TP5.Areas.Admin.ViewModels;

namespace TP5.DataAccessLayer.Factories
{
    public class ChoixFactory : Controller
    {
        private Choix CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string description = mySqlDataReader["Description"].ToString() ?? string.Empty;

            return new Choix(id, description);
        }

        public Choix CreateEmpty()
        {
            return new Choix(0, string.Empty);
        }
        public List<Choix> GetAll()
        {
            List<Choix> list = new List<Choix>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_menuchoices ORDER BY Id";
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Choix choix = CreateFromReader(mySqlDataReader);
                    list.Add(choix);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }
            return list;
        }
        public Choix? Get(int id)
        {
            Choix? choix = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_menuchoices WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    choix = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return choix;
        }
        public void Save(Choix choix)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "INSERT INTO tp5_menuchoices(Description)" + "VALUES (@Description)";
                mySqlCmd.Parameters.AddWithValue("@Description", choix.Description?.Trim());
                mySqlCmd.ExecuteNonQuery();
                if (choix.Id == 0)
                {
                    choix.Id = (int)mySqlCmd.LastInsertedId;
                }
            }
            finally
            {
                mySqlCnn.Close();
            }
        }

        public void Edit(Choix choix)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "UPDATE tp5_menuchoices " +
                                           "SET Description=@Description " +
                                           "WHERE Id=@Id";
                mySqlCmd.Parameters.AddWithValue("@Id", choix.Id);
                mySqlCmd.Parameters.AddWithValue("@Description", choix.Description?.Trim());
                mySqlCmd.ExecuteNonQuery();
                if (choix.Id == 0)
                {
                    choix.Id = (int)mySqlCmd.LastInsertedId;
                }
            }
            finally
            {
                mySqlCnn.Close();
            }
        }

        public void Delete(int id)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "DELETE FROM tp5_menuchoices WHERE Id=@Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);
                mySqlCmd.ExecuteNonQuery();
            }
            finally
            {
                mySqlCnn?.Close();
            }
        }
    }
}
