using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace Application_visa.Models
{
    public class Transaction_Dargent : Files
    {
        public void Add()
        {
            Service sr = Service.getService("Transaction_Dargent");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO files (nom, prenom, tele,cin,prix,charge,total,scan,date,id_service) VALUES (@nom, @prenom, @tele,@cin,@prix,@charge,@total,@scan,Now(),@id_service)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@nom", this.nom));
            cmd.Parameters.Add(new MySqlParameter("@prenom", this.prenom));
            cmd.Parameters.Add(new MySqlParameter("@tele", this.tele));
            cmd.Parameters.Add(new MySqlParameter("@cin", this.cin));
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@charge", this.charge));
            cmd.Parameters.Add(new MySqlParameter("@total", this.total));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));
            cmd.Parameters.Add(new MySqlParameter("@id_service", sr.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static Transaction_Dargent getTransaction_Dargent(int id) 
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "select * from files where id= @id";
            MySqlCommand cmd = new MySqlCommand(query,con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            Transaction_Dargent t=new Transaction_Dargent();
            MySqlDataReader dr=cmd.ExecuteReader();
            while (dr.Read()) 
            {
                t.id = int.Parse ( dr["id"].ToString());
                t.prix = float.Parse(dr["prix"].ToString());
                t.charge = float.Parse(dr["charge"].ToString());
                t.total = float.Parse(dr["total"].ToString());
                t.nom = dr["nom"].ToString();
                t.prenom = dr["prenom"].ToString();
                t.tele = dr["tele"].ToString();
                t.cin = dr["cin"].ToString();
                t.scan = dr["scan"].ToString();
            }
            return t;
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
        public static List<Transaction_Dargent> GetAll(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            Service ser = Service.getService("Transaction_Dargent");

            String query = "SELECT * FROM files where id_service= @id and id_user= @idUser";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", ser.id));
            cmd.Parameters.Add(new MySqlParameter("@idUser", id));
            List<Transaction_Dargent> list = new List<Transaction_Dargent>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Transaction_Dargent app = new Transaction_Dargent();
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
                app.user = User.getUser(int.Parse(rd["id_user"].ToString()));
                list.Add(app);
            }
            con.Close();
            return list;
        }
    }
}
