using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Application_visa.Models
{
    public abstract class Files
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "Nom est obligatoire")]

        public string nom { get; set; }
        [Required(ErrorMessage = "Prenom est obligatoire")]

        public string prenom { get; set; }
        [Required(ErrorMessage = "Telephone est obligatoire")]

        public String tele { get; set; }
        [Required(ErrorMessage = "CIN est obligatoire")]

        public string cin { get; set; }
        [Required(ErrorMessage = "Charge est obligatoire")]

        public float charge { get; set; }

        public float prix { get; set; }
        [Required(ErrorMessage = "Total est obligatoire")]

        public float total { get; set; }

        public IFormFile? file { get; set; }
        public string? scan { get; set; }
        public Boolean ami_khaled { get; set; }
        public DateTime date { get; set; }
        public Service service { get; set; }
        public Fournisseur fournisseur { get; set; }
        public User user { get; set; }


        public static MySqlConnection connexion()
        {
            using MySqlConnection connection = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;");
            return connection;
        }
        public static List<Files> getAllbyAgence(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            Service ser = Service.getService("assurance");
            String query = "SELECT * FROM files inner join user on user.id=id_user inner join agence on agence.id=id_agence where id_user=@id and id_service= @ids ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.Parameters.Add(new MySqlParameter("@ids", ser.id));
            MySqlDataReader rd = cmd.ExecuteReader();
            List<Files> files = new List<Files>();
            while (rd.Read())
            {
                assurance f = new assurance();
                f.id = int.Parse(rd["id"].ToString());
                f.nom = rd["nom"].ToString();
                f.prenom = rd["prenom"].ToString();
                f.prix = int.Parse(rd["prix"].ToString());
                f.charge = int.Parse(rd["charge"].ToString());
                f.total = int.Parse(rd["total"].ToString());
                f.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                f.date = (DateTime)rd["date"];
                f.scan = rd["scan"].ToString();
                f.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                Service s = new Service();
                f.service = Service.getService(s.nom);
                f.fournisseur = Fournisseur.getFourn(int.Parse(rd["id_fourn1"].ToString()));
                files.Add(f);
            }
            return files;
        }
        public static List<Files> getAllAppostilbyAgence()
        {
            MySqlConnection con = connexion();
            con.Open();
            Service ser = Service.getService("aposstille");
            String query = "SELECT * FROM files inner join user on user.id=id_user inner join agence on agence.id=id_agence where  id_service= @ids group by id_agence ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@ids", ser.id));
            MySqlDataReader rd = cmd.ExecuteReader();
            List<Files> files = new List<Files>();
            while (rd.Read())
            {
                aposstille f = new aposstille();
                f.id = int.Parse(rd["id"].ToString());
                f.nom = rd["nom"].ToString();
                f.prenom = rd["prenom"].ToString();
                f.prix = int.Parse(rd["prix"].ToString());
                f.charge = int.Parse(rd["charge"].ToString());
                f.total = int.Parse(rd["total"].ToString());
                f.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                f.date = (DateTime)rd["date"];
                f.scan = rd["scan"].ToString();
                f.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                Service s = new Service();
                f.service = Service.getService(s.nom);
                files.Add(f);
            }
            return files;
        }
        public static List<Files> getAllTorismByAgence(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            Service ser = Service.getService("Tourism");
            Fournisseur f1 = Fournisseur.GetFournisseur("Tourism");
            String query = "SELECT * FROM files inner join user on user.id=id_user inner join agence on agence.id=id_agence where id_user=@id and id_service= @ids ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.Parameters.Add(new MySqlParameter("@ids", ser.id));
            MySqlDataReader rd = cmd.ExecuteReader();
            List<Files> files = new List<Files>();
            while (rd.Read())
            {
                Tourism f = new Tourism();
                f.id = int.Parse(rd["id"].ToString());
                f.nom = rd["nom"].ToString();
                f.prenom = rd["prenom"].ToString();
                f.prix = int.Parse(rd["prix"].ToString());
                f.charge = int.Parse(rd["charge"].ToString());
                f.total = int.Parse(rd["total"].ToString());
                f.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                f.date = (DateTime)rd["date"];
                f.scan = rd["scan"].ToString();
                f.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                Service s = new Service();
                f.service = Service.getService(s.nom);
                f.fournisseur = Fournisseur.getFourn((int)f1.id);
                files.Add(f);
            }
            return files;
        }
        public static List<Files> getAllTraductionByAgence(int idagence)
        {
            MySqlConnection con = connexion();
            con.Open();
            Service ser = Service.getService("Traduction");
            String query = "SELECT  * FROM files inner join user on user.id=id_user inner join agence on agence.id=id_agence where id_service= @ids and id_agence=@idA ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@ids", ser.id));
            cmd.Parameters.Add(new MySqlParameter("@idA", idagence));
            MySqlDataReader rd = cmd.ExecuteReader();
            List<Files> files = new List<Files>();
            while (rd.Read())
            {
                Traduction f = new Traduction();
                f.id = int.Parse(rd["id"].ToString());
                f.nom = rd["nom"].ToString();
                f.prenom = rd["prenom"].ToString();
                f.prix = int.Parse(rd["prix"].ToString());
                f.charge = int.Parse(rd["charge"].ToString());
                f.total = int.Parse(rd["total"].ToString());
                f.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                f.date = (DateTime)rd["date"];
                f.scan = rd["scan"].ToString();
                f.scan1 = rd["scan1"].ToString();
                f.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                Service s = new Service();
                f.service = Service.getService(s.nom);
                
                files.Add(f);
            }
            return files;

        }
        public static List<Files> GetAllEducationByAgence(int id)
        {
            
            Service ser = Service.getService("Education");
            List<Files> files = new List<Files>();
            MySqlConnection con = connexion();                
                con.Open();
                String query = "SELECT * from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user inner join service on files.id_service=service.id where agence.id=@idA and id_service= @ids";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.Add(new MySqlParameter("@ids", ser.id));
                cmd.Parameters.Add(new MySqlParameter("@idA",id));
            MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Education f = new Education();
                    f.id = int.Parse(rd["id"].ToString());
                    f.nom = rd["nom"].ToString();
                    f.prenom = rd["prenom"].ToString();
                    f.prix = int.Parse(rd["prix"].ToString());
                    f.charge = int.Parse(rd["charge"].ToString());
                    f.total = int.Parse(rd["total"].ToString());
                    f.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                    f.date = (DateTime)rd["date"];
                    f.scan = rd["scan"].ToString();
                    f.scan1 = rd["scan1"].ToString();
                    f.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                    Service s = new Service();
                    f.service = Service.getService(s.nom);
                    files.Add(f);
                }
                con.Close();
                return files;
            }
        }
    }





 