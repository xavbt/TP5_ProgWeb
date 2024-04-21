using TP5.Models;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace TP5.DataAccessLayer.Factories
{
    public class ReservationFactory
    {
        private Reservations CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string nom = mySqlDataReader["Nom"].ToString() ?? string.Empty;
            string courriel = mySqlDataReader["Courriel"].ToString() ?? string.Empty;
            int nbPersonne = (int)mySqlDataReader["NbPersonne"];
            DateTime dateReservation = (DateTime)mySqlDataReader["DateReservation"];
            int menuChoiceId = (int)mySqlDataReader["MenuChoiceId"];

            return new Reservations(id, nom, courriel, nbPersonne, dateReservation, menuChoiceId);
        }
        public Reservations CreateEmpty()
        {
            return new Reservations(0, string.Empty, string.Empty, 0, DateTime.Now, 0);
        }
        public List<Reservations> GetAll()
        {
            List<Reservations> reservations = new List<Reservations>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_reservations ORDER BY Id";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Reservations reservation = CreateFromReader(mySqlDataReader);
                    reservations.Add(reservation);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }
            return reservations;
        }
        public Reservations Get(int id)
        {
            Reservations? reservation = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_reservations WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    reservation = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservation;
        }
        public void Save(Reservations reservation)
        {
            MySqlConnection? mySqlCnn = null;
            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                if (reservation.Id == 0)
                {
                    // On sait que c'est un nouveau produit avec Id == 0,
                    // car c'est ce que nous avons affecter dans la fonction CreateEmpty().
                    mySqlCmd.CommandText = "INSERT INTO tp5_reservations(Nom, Courriel, NbPersonne, DateReservation, MenuChoiceId) " +
                                           "VALUES (@Nom, @Courriel, @NbPersonne, @DateReservation, @MenuChoiceId)";
                }
                else
                {
                    mySqlCmd.CommandText = "UPDATE tp5_reservations " +
                                           "SET Nom=@Nom, Courriel=@Courriel, NbPersonne=@NbPersonne, DateReservation=@DateReservation, MenuChoiceId=@MenuChoiceId " +
                                           "WHERE Id=@Id";
                }
                mySqlCmd.Parameters.AddWithValue("@Id", reservation.Id);
                mySqlCmd.Parameters.AddWithValue("@Nom", reservation.Nom?.Trim());
                mySqlCmd.Parameters.AddWithValue("@Courriel", reservation.Courriel?.Trim());
                mySqlCmd.Parameters.AddWithValue("@NbPersonne", reservation.NbPersonne);
                mySqlCmd.Parameters.AddWithValue("@DateReservation", reservation.DateReservation);
                mySqlCmd.Parameters.AddWithValue("@MenuChoiceId", reservation.MenuChoiceId);

                mySqlCmd.ExecuteNonQuery();
                if (reservation.Id == 0)
                {
                    reservation.Id = (int)mySqlCmd.LastInsertedId;
                }
            }
            finally
            {
                mySqlCnn?.Close();
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
                mySqlCmd.CommandText = "DELETE FROM tp5_reservations WHERE Id=@Id";
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
