using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Application_visa.Models

{
    public class User
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "Login est obligatoire")]
        public string login { get; set; }
        [Required(ErrorMessage = "le mot de pass est obligatoire")]
        public string pwd { get; set; }
        [Required(ErrorMessage = "password Confirmation est obligatoire")]
        public string passwordConfirm { get; set; }
        [Required(ErrorMessage = "le role est obligatoire")]
        public Role  role { get; set; }
        [Required(ErrorMessage = "l'agence est obligatoire")]
        public Agence  agence { get; set; }

       public List<Application_visa.Models.Role> ? roles { get; set; }
        public List<Application_visa.Models.Agence> ? agences { get; set; }
     

     public string hashPassword(string password)
        {

            if (password != null)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                SHA256 sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
            return null;
        }
        public void addUtilisateure()
        {          
            MySqlConnection con = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;");
            con.Open();
            String query = "INSERT INTO user (login, pwd, id_role,id_agence) VALUES (@login, @pwd, @id_role,@id_agence)";
            MySqlCommand cmd = new MySqlCommand(query, con);         
            cmd.Parameters.Add(new MySqlParameter("@login", this.login));
            cmd.Parameters.Add(new MySqlParameter("@pwd", hashPassword(this.pwd)));
            cmd.Parameters.Add(new MySqlParameter("@id_role", this.role.id));
            cmd.Parameters.Add(new MySqlParameter("@id_agence", this.agence.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public User loginUser()
        {
            User user = new User();
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from user u inner join role r on u.id_role=r.id inner join agence a on u.id_agence=a.id where login=@login and pwd=@pwd";
            command.Parameters.Add(new MySqlParameter("@login", this.login));
            command.Parameters.Add(new MySqlParameter("@pwd",hashPassword(this.pwd)));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.id = int.Parse(reader[0].ToString());
                    user.login = reader[1].ToString();
                    user.pwd = reader[2].ToString();
                    user.role = new Role();
                    user.role.nom = reader[6].ToString();
                      
                }
                
            }
                                    
            conn.Close();
            return user;

        }
        public Boolean searchUser(String login)
        {
            Boolean result = false;
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from user where login=@login";
            command.Parameters.Add(new MySqlParameter("@login",login));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result= true;
                }            
            }
            else
            {
                result= false;
            }
            conn.Close();
            return result;
        }
        public List<User> getUsersByAgence(int idAg)
        {
            List<User> users = new List<User>();
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from user where id_agence=@idAgence and id_role between 2 and 3";
            command.Parameters.Add(new MySqlParameter("@idAgence", idAg));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    User user = new User();
                    user.id= int.Parse(reader["id"].ToString());
                    user.login= reader["login"].ToString();
                    users.Add(user);
                }
            }
            conn.Close();
            return users;
        }
        public List<User> getUsersById(int empId)
        {
            List<User> users = new List<User>();
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from user where id=@idEmp";
            command.Parameters.Add(new MySqlParameter("@idEmp", empId));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    User user = new User();
                    user.id = int.Parse(reader["id"].ToString());
                    user.login = reader["login"].ToString();
                    users.Add(user);
                }
            }
            conn.Close();
            return users;
        }

        public static User getUsersById_(int empId)
        {
            User user = new User();
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from user where id=@idEmp";
            command.Parameters.Add(new MySqlParameter("@idEmp", empId));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.id = int.Parse(reader["id"].ToString());
                    user.login = reader["login"].ToString();
                    user.agence = Agence.getAgence(int.Parse(reader["id_agence"].ToString()));
                }
            }
            conn.Close();
            return user;
        }
        public static User getUser(int empId)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * from user where id=@idEmp";
            command.Parameters.Add(new MySqlParameter("@idEmp", empId));
            User user = new User();
            MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.id = int.Parse(reader["id"].ToString());
                    user.login = reader["login"].ToString();
                    user.agence = Agence.getAgence(int.Parse(reader["id_agence"].ToString()));
                }
            conn.Close();
            return user;
        }

    }
}
