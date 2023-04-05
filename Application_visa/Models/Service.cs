using MySql.Data.MySqlClient;

namespace Application_visa.Models
{
    public class Service
    {
        public int ? id { get; set; }
        public string nom { get; set; }
        public static MySqlConnection connexion()
        {
            using MySqlConnection connection = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;");
            return connection;
        }

        public List<Service> getAllServices()
        {
            MySqlConnection conn = connexion();
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from service";
            MySqlDataReader reader = command.ExecuteReader();
            List<Service> services = new List<Service>();
            while (reader.Read())
            {
                Service service = new Service();
                service.id = int.Parse(reader["id"].ToString());
                service.nom = reader["nom"].ToString();
                services.Add(service);
            }
            conn.Close();
            return services;
        }
        public static Service getService(string nom)
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "select id from service where nom= @nom";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@nom",nom));
            MySqlDataReader dr = cmd.ExecuteReader();
            Service service = new Service();
            while (dr.Read())
            {
                service.id = int.Parse(dr["id"].ToString());
            }
            return service;
        }
    }

}
