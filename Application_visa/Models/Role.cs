using MySql.Data.MySqlClient;

namespace Application_visa.Models
{
    public class Role
    {
        public int ? id { get; set; }
        public string nom { get; set; }
   
        public List<Role> getRoles()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;");
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from role";
            MySqlDataReader reader = command.ExecuteReader();
            List<Role> roles = new List<Role>();
            while (reader.Read())
            {
                Role role = new Role();
                role.id = int.Parse(reader["id"].ToString());
                role.nom = reader["role"].ToString();
                roles.Add(role);
           
            }
            conn.Close();
            return roles;
        }
    }
}
