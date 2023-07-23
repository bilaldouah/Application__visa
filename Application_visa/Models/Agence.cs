using MySql.Data.MySqlClient;
using System.Runtime.ConstrainedExecution;

namespace Application_visa.Models
{
    public class Agence:Files
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string ville { get; set; }
        public  List<Agence> getAgences()
        {
            MySqlConnection conn = connexion();
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from agence";
            MySqlDataReader reader = command.ExecuteReader();
            List<Agence> agences = new List<Agence>();
            while (reader.Read())
            {
                Agence agence = new Agence();
                agence.id = int.Parse(reader["id"].ToString());
                agence.nom = reader["nom"].ToString();
                agence.ville = reader["ville"].ToString();
                agences.Add(agence);
            }
            conn.Close();
            return agences;
        }
        public static Agence getAgence(int id)
        {
            MySqlConnection conn = connexion();
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from agence WHERE id=@id";
            command.Parameters.Add(new MySqlParameter("@id",id));
            Agence agence = new Agence();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                agence.id = int.Parse(reader["id"].ToString());
                agence.nom = reader["nom"].ToString();
                agence.ville = reader["ville"].ToString();
            }
            conn.Close();
            return agence;
        }

    }
    
}
