using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;

namespace Application_visa.Models
{
    public class Tourism : Files
    {
        private Agence agence { get; set;}

     public void Add()
        {
            Service sr = Service.getService("Tourism");
            Fournisseur f = Fournisseur.GetFournisseur("Tourism");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO files (nom, prenom, tele,cin,prix,charge,total,scan,date,id_service,ami_khalid,id_fourn1) VALUES (@nom, @prenom, @tele,@cin,@prix,@charge,@total,@scan,Now(),@id_service,@ami_khalid,@id_fourn1)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@nom", this.nom));
            cmd.Parameters.Add(new MySqlParameter("@prenom", this.prenom));
            cmd.Parameters.Add(new MySqlParameter("@tele", this.tele));
            cmd.Parameters.Add(new MySqlParameter("@cin", this.cin));
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@charge", this.charge));
            cmd.Parameters.Add(new MySqlParameter("@total", this.total));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));
            cmd.Parameters.Add(new MySqlParameter("@id_service",sr.id));
            cmd.Parameters.Add(new MySqlParameter("@ami_khalid", this.ami_khaled));
            cmd.Parameters.Add(new MySqlParameter("@id_fourn1",f.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Tourism getTourism(int id) 
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "SELECT * FROM files where id= @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            Tourism tourism = new Tourism();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tourism.id = int.Parse(dr["id"].ToString());
                tourism.prix = float.Parse(dr["prix"].ToString());
                tourism.charge = float.Parse(dr["charge"].ToString());
                tourism.total = float.Parse(dr["total"].ToString());
                tourism.nom = dr["nom"].ToString();
                tourism.prenom = dr["prenom"].ToString();
                tourism.tele = dr["tele"].ToString();
                tourism.cin = dr["cin"].ToString();
                tourism.scan = dr["scan"].ToString();
            }
            dr.Close();
            con.Close();
            return tourism;
        }
        public void Update() 
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = " UPDATE files set prix = @prix , scan = @scan , date = Now() where id = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));
            cmd.Parameters.Add(new MySqlParameter("@id", this.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
