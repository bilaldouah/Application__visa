using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System.Runtime.ConstrainedExecution;

namespace Application_visa.Models
{
    public class Tourism : Files
    {
        public Agence agence { get; set;}

     public void Add()
        {
            Service sr = Service.getService("Tourism");
            Fournisseur f = Fournisseur.GetFournisseur("Tourism");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO files (nom, prenom, tele,cin,prix,charge,total,scan,date,id_service,ami_khalid,id_fourn1,id_user) VALUES (@nom, @prenom, @tele,@cin,@prix,@charge,@total,@scan,Now(),@id_service,@ami_khalid,@id_fourn1,@id_user)";
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
            cmd.Parameters.Add(new MySqlParameter("@id_user", this.user.id));
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
        public static List<Tourism> GetAll(int id)
        {
           
            Service ser = Service.getService("Tourism");
            MySqlConnection con = connexion();
           
           
            con.Open();
           

            String query = "SELECT * FROM files where id_service= @id and id_user= @idUser";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", ser.id));
            cmd.Parameters.Add(new MySqlParameter("@idUser", id));
            List<Tourism> list = new List<Tourism>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Tourism app = new Tourism();
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
        public static List<Tourism> getAllbyAgence()
        {
        Service s = new Service();
        List<Service> listS = s.getAllServices();
            List<Tourism> list = new List<Tourism>();
            MySqlConnection con = connexion();
            foreach(Service l in listS)
            {

                con.Open();
                String query = "SELECT * FROM files INNER JOIN user on files.id_user=user.id INNER JOIN agence on user.id_agence=agence.id INNER JOIN service ON files.id_service=service.id WHERE service.id=@idS";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.Add(new MySqlParameter("@idS", l.id));
               
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Tourism app = new Tourism();
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
                    app.agence= new Agence();
                    app.agence.nom= rd[28].ToString();
                    list.Add(app);
                }
                con.Close();
            }
            return list;
        }
    }
}
