using MySql.Data.MySqlClient;

namespace Application_visa.Models
{
    public class Fournisseur
    {
        public int? id { get; set; }
        public string nom { get; set; }

        public static MySqlConnection connexion()
        {
            using MySqlConnection connection = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;");
            return connection;
        }
        public static Fournisseur GetFournisseur(string nom)
        {
            MySqlConnection conn = connexion();
            conn.Open();
            string query = "SELECT id from fournisseur where nom=@nom";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.Add(new MySqlParameter("@nom", nom));
            MySqlDataReader dr = cmd.ExecuteReader();
            Fournisseur fournisseur = new Fournisseur();
            while (dr.Read())
            {
                fournisseur.id = int.Parse(dr["id"].ToString());
            }
            return fournisseur;
        }
        public static List<Fournisseur> getTraduction()
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "SELECT * FROM fournisseur  where nom like 'traduction%'";
            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader dr = cmd.ExecuteReader();
            List<Fournisseur> list = new List<Fournisseur>();
            while (dr.Read())
            {
                Fournisseur fournisseur = new Fournisseur();
                fournisseur.id = int.Parse(dr["id"].ToString());
                fournisseur.nom = dr["nom"].ToString();
               list.Add(fournisseur);

            }
            return list;   
        }

        public static Fournisseur getLabo()
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "SELECT * FROM fournisseur  where nom like 'laboratoire%'";
            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader dr = cmd.ExecuteReader();
            Fournisseur fournisseur = new Fournisseur();

            while (dr.Read())
            {
                fournisseur.id = int.Parse(dr["id"].ToString());
                fournisseur.nom = dr["nom"].ToString();


            }
            return fournisseur;
        }

        public static Fournisseur getMedecin()
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "SELECT * FROM fournisseur  where nom like 'Certificat medical%'";
            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader dr = cmd.ExecuteReader();
            Fournisseur fournisseur = new Fournisseur();

            while (dr.Read())
            {
                fournisseur.id = int.Parse(dr["id"].ToString());
                fournisseur.nom = dr["nom"].ToString();
            }
            return fournisseur;
        }

        public static Fournisseur getFourn(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "SELECT * FROM fournisseur  where id = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id",id));

            MySqlDataReader dr = cmd.ExecuteReader();
            Fournisseur fournisseur = new Fournisseur();

            while (dr.Read())
            {
                fournisseur.id = int.Parse(dr["id"].ToString());
                fournisseur.nom = dr["nom"].ToString();
            }
            return fournisseur;
        }
        public static  List<Fournisseur> getAllFournisseurs()
        {
            List<Fournisseur> fournisseurs = new List<Fournisseur>();
            MySqlConnection conn = connexion();
            conn.Open();
            string query = "SELECT * from fournisseur";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Fournisseur fournisseur = new Fournisseur();
                fournisseur.id = int.Parse(dr["id"].ToString());
                fournisseur.nom = dr["nom"].ToString();
                fournisseurs.Add(fournisseur);
            }
            return fournisseurs;
        }
    }
}
