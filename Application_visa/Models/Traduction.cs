using MySql.Data.MySqlClient;

namespace Application_visa.Models
{
    public class Traduction:Files
    {
        public IFormFile? file_1 { get; set; }
        public string scan1 { get; set; }
        public void Add()
        {
            Service sr = Service.getService("Traduction");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO files (nom, prenom, tele,cin,prix,charge,total,scan,scan1,date,id_service,ami_khalid,id_fourn1) VALUES (@nom, @prenom, @tele,@cin,@prix,@charge,@total,@scan,@scan1,Now(),@id_service,@ami_khalid,@id_fourn1)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@nom", this.nom));
            cmd.Parameters.Add(new MySqlParameter("@prenom", this.prenom));
            cmd.Parameters.Add(new MySqlParameter("@tele", this.tele));
            cmd.Parameters.Add(new MySqlParameter("@cin", this.cin));
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@charge", this.charge));
            cmd.Parameters.Add(new MySqlParameter("@total", this.total));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));
            cmd.Parameters.Add(new MySqlParameter("@scan1", this.scan1));
            cmd.Parameters.Add(new MySqlParameter("@id_service", sr.id));
            cmd.Parameters.Add(new MySqlParameter("@ami_khalid", this.ami_khaled));
            cmd.Parameters.Add(new MySqlParameter("@id_fourn1", this.fournisseur.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static Traduction getTraduction(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "SELECT * FROM files where id= @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            Traduction traduction = new Traduction();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                traduction.id = int.Parse(dr["id"].ToString());
                traduction.prix = float.Parse(dr["prix"].ToString());
                traduction.charge = float.Parse(dr["charge"].ToString());
                traduction.total = float.Parse(dr["total"].ToString());
                traduction.nom = dr["nom"].ToString();
                traduction.prenom = dr["prenom"].ToString();
                traduction.tele = dr["tele"].ToString();
                traduction.cin = dr["cin"].ToString();
                traduction.scan = dr["scan"].ToString();
                traduction.scan1 = dr["scan1"].ToString();
            }
            dr.Close();
            con.Close();
            return traduction;
        }
        public void Update()
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = " UPDATE files set prix = @prix ,scan1= @scan1, date = Now() where id = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@scan1", this.scan1));
            cmd.Parameters.Add(new MySqlParameter("@id", this.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
