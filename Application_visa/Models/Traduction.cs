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
        public static List<Traduction> GetAll(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            Service ser = Service.getService("Traduction");

            String query = "SELECT * FROM files where id_service= @id and id_user= @idUser";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", ser.id));
            cmd.Parameters.Add(new MySqlParameter("@idUser", id));
            List<Traduction> list = new List<Traduction>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Traduction app = new Traduction();
                app.id = int.Parse(rd["id"].ToString());
                app.nom = rd["nom"].ToString();
                app.prenom = rd["prenom"].ToString();
                app.tele = rd["tele"].ToString();
                app.cin = rd["cin"].ToString();
                app.prix = float.Parse(rd["prix"].ToString());
                app.charge = float.Parse(rd["charge"].ToString());
                app.total = float.Parse(rd["total"].ToString());
                app.scan = rd["scan"].ToString();
                app.scan1 = rd["scan1"].ToString();
                app.fournisseur = Fournisseur.getFourn(int.Parse(rd["id_fourn1"].ToString()));
                app.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                app.date = (DateTime)rd["date"];
                app.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                list.Add(app);
            }
            con.Close();
            return list;
        }
    }
}
