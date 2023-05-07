using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
namespace Application_visa.Models
{
    public class aposstille : Files
    {
        [Required(ErrorMessage = "Destination est obligatoire")]
        public String destination { get; set; }

        public void add()
        {
            Service ser = Service.getService("aposstille");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO files (nom, prenom, tele,cin,prix,charge,total,scan,destination,date,id_service,ami_khalid,id_user) VALUES (@nom, @prenom, @tele,@cin,@prix,@charge,@total,@scan,@destination,Now(),@id_service,@ami_khalid,@id_user)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@nom", this.nom));
            cmd.Parameters.Add(new MySqlParameter("@prenom", this.prenom));
            cmd.Parameters.Add(new MySqlParameter("@tele", this.tele));
            cmd.Parameters.Add(new MySqlParameter("@cin", this.cin));
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@charge", this.charge));
            cmd.Parameters.Add(new MySqlParameter("@total", this.total));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));
            cmd.Parameters.Add(new MySqlParameter("@destination", this.destination));
            cmd.Parameters.Add(new MySqlParameter("@id_service", ser.id));
            cmd.Parameters.Add(new MySqlParameter("@ami_khalid", this.ami_khaled));
            cmd.Parameters.Add(new MySqlParameter("@id_user", this.user.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static aposstille getAposstile(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "SELECT * FROM files where id= @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            aposstille app = new aposstille();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                app.id = int.Parse(rd["id"].ToString());
                app.nom = rd["nom"].ToString();
                app.prenom = rd["prenom"].ToString();
                app.tele = rd["tele"].ToString();
                app.cin = rd["cin"].ToString();
                app.prix = float.Parse(rd["prix"].ToString());
                app.charge = float.Parse(rd["charge"].ToString());
                app.total = float.Parse(rd["total"].ToString());
                app.scan = rd["scan"].ToString();
                app.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                app.destination = rd["destination"].ToString();
            }
            con.Close();
            return app;
        }
        public void update()
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "UPDATE files set prix = @prix , scan = @scan , date = Now() where id = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));
            cmd.Parameters.Add(new MySqlParameter("@id", this.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<aposstille> getAll(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            Service ser = Service.getService("aposstille");

            String query = "SELECT * FROM files where id_service= @id and id_user= @idUser";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", ser.id));
            cmd.Parameters.Add(new MySqlParameter("@idUser", id));
            List<aposstille> list = new List<aposstille>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                aposstille app = new aposstille();
                app.id = int.Parse(rd["id"].ToString());
                app.nom = rd["nom"].ToString();
                app.prenom = rd["prenom"].ToString();
                app.tele = rd["tele"].ToString();
                app.cin = rd["cin"].ToString();
                app.prix = float.Parse(rd["prix"].ToString());
                app.charge = float.Parse(rd["charge"].ToString());
                app.total = float.Parse(rd["total"].ToString());
                app.scan = rd["scan"].ToString();
                app.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                app.date = (DateTime)rd["date"];
                app.destination = rd["destination"].ToString();
                app.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                list.Add(app);
            }
            con.Close();
            return list;

        }
        public static List<aposstille> getAllbyAgence(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "SELECT * FROM files where  idAgence=@id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id",Agence.getAgence(id)));
            List<aposstille> list = new List<aposstille>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                aposstille app = new aposstille();
                app.id = int.Parse(rd["id"].ToString());
                app.nom = rd["nom"].ToString();
                app.prenom = rd["prenom"].ToString();
                app.tele = rd["tele"].ToString();
                app.cin = rd["cin"].ToString();
                app.prix = float.Parse(rd["prix"].ToString());
                app.charge = float.Parse(rd["charge"].ToString());
                app.total = float.Parse(rd["total"].ToString());
                app.scan = rd["scan"].ToString();
                app.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                app.date = (DateTime)rd["date"];
                app.destination = rd["destination"].ToString();
                app.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                list.Add(app);
            }
            con.Close();
            return list;

        }

    }
}
