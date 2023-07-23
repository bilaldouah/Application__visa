using Application_visa.Controllers;
using MySql.Data.MySqlClient;

namespace Application_visa.Models
{
    public class Virment
    {
        public int? id { get; set; }
        public DateTime date { get; set; }
        public int montant { get; set; }
        public Boolean statut { get; set; }
        public User user_receiver { get; set; }
        public User user_sender { get; set; }
        public string scan { get; set; }
        public Fournisseur fournisseur { get; set; }
        public Agence agent { get; set; }
        public IFormFile? file { get; set; }


        public static MySqlConnection connexion()
        {
            using MySqlConnection connection = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;");
            return connection;
        }

        public void VirmentToUser(int id_sender)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO virment (date, montant, statut,id_user_sender,id_user_receiver,scan) VALUES (Now(),@montant,0,@sender,@reciver,@scan)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@montant", this.montant));
            cmd.Parameters.Add(new MySqlParameter("@sender", id_sender));
            cmd.Parameters.Add(new MySqlParameter("@reciver", this.user_receiver.id));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));


            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void VirmentToFourn(int id_sender)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO virment (date, montant, statut,id_user_sender,id_fourn,scan) VALUES (Now(),@montant,0,@sender,@fourn,@scan)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@montant", this.montant));
            cmd.Parameters.Add(new MySqlParameter("@sender", id_sender));
            cmd.Parameters.Add(new MySqlParameter("@fourn", this.fournisseur.id));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));


            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void VirmentToAgence(int id_sender)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO virment (date, montant, statut,id_user_sender,id_agent,scan) VALUES (Now(),@montant,0,@sender,@agence,@scan)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@montant", this.montant));
            cmd.Parameters.Add(new MySqlParameter("@sender", id_sender));
            cmd.Parameters.Add(new MySqlParameter("@agence", this.agent.id));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));


            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<Virment> getAllFourn(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Select * from virment  where id_user_sender= @id and id_fourn is not null";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            List < Virment > virments = new List<Virment>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Virment virement = new Virment();

                virement.id = int.Parse(rd["id"].ToString());
                virement.date = (DateTime)rd["date"];
                virement.montant = int.Parse(rd["montant"].ToString());
                virement.statut =Convert.ToBoolean(rd["statut"]);
                //virement.user_receiver = User.getUser(int.Parse(rd["id_user_receiver"].ToString()));
                virement.scan = rd["scan"].ToString();
                virement.fournisseur =Fournisseur.getFourn(int.Parse(rd["id_fourn"].ToString()));
                virments.Add(virement);
            }
            con.Close();
            return virments;
        }

        public static List<Virment> getAllEmploye(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Select * from virment  where id_user_sender= @id and id_fourn is null";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            List<Virment> virments = new List<Virment>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Virment virement = new Virment();

                virement.id = int.Parse(rd["id"].ToString());
                virement.date = (DateTime)rd["date"];
                virement.montant = int.Parse(rd["montant"].ToString());
                virement.statut = virement.statut = Convert.ToBoolean(rd["statut"]);
                virement.user_receiver = User.getUser(int.Parse(rd["id_user_receiver"].ToString()));
                virement.scan = rd["scan"].ToString();
                virments.Add(virement);
            }
            con.Close();
            return virments;
        }
        public static List<Virment> getAllAgence(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Select * from virment  where id_user_sender= @id and id_agent is not null";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            List<Virment> virments = new List<Virment>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Virment virement = new Virment();

                virement.id = int.Parse(rd["id"].ToString());
                virement.date = (DateTime)rd["date"];
                virement.montant = int.Parse(rd["montant"].ToString());
                virement.statut = Convert.ToBoolean(rd["statut"]);
                virement.agent = Agence.getAgence(int.Parse(rd["id_agent"].ToString()));
                virement.scan = rd["scan"].ToString();
                virments.Add(virement);
            }
            con.Close();
            return virments;
        }
        public static List<Virment> getVirments(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Select * from virment  where id_agent = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            List<Virment> virments = new List<Virment>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Virment virement = new Virment();

                virement.id = int.Parse(rd["id"].ToString());
                virement.date = (DateTime)rd["date"];
                virement.montant = int.Parse(rd["montant"].ToString());
                virement.statut = Convert.ToBoolean(rd["statut"]);
                virement.agent = Agence.getAgence(int.Parse(rd["id_agent"].ToString()));
                virement.scan = rd["scan"].ToString();
                virement.user_sender = User.getUser(int.Parse(rd["id_user_sender"].ToString()));
                virments.Add(virement);
            }
            con.Close();
            return virments;
        }

        public static void Accepter(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Update virment set statut = 1 where id =@id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));



            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static Virment getVirment(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Select * from virment  where id = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            Virment virement = new Virment();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {

                virement.id = int.Parse(rd["id"].ToString());
                virement.date = (DateTime)rd["date"];
                virement.montant = int.Parse(rd["montant"].ToString());
                virement.statut = Convert.ToBoolean(rd["statut"]);
                virement.user_sender = User.getUser(int.Parse(rd["id_user_sender"].ToString()));
                if (rd["id_user_receiver"].ToString() != ""){
                    virement.user_receiver = User.getUser(int.Parse(rd["id_user_receiver"].ToString()));
                }
                if (rd["id_agent"].ToString() != "")
                {
                    virement.agent = Agence.getAgence(int.Parse(rd["id_agent"].ToString()));
                }
                if (rd["id_fourn"].ToString() != "")
                {
                    virement.fournisseur = Fournisseur.getFourn(int.Parse(rd["id_fourn"].ToString()));
                }
                virement.scan = rd["scan"].ToString();
            }
            con.Close();
            return virement;
        }

        public static void Modifier(Virment v)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Update virment set scan = @scan where id =@id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", v.id));
            cmd.Parameters.Add(new MySqlParameter("@scan", v.scan));

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static List<Virment> MesVirement(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Select * from virment  where id_user_receiver = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            List<Virment> virments = new List<Virment>();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Virment virement = new Virment();

                virement.id = int.Parse(rd["id"].ToString());
                virement.date = (DateTime)rd["date"];
                virement.montant = int.Parse(rd["montant"].ToString());
                virement.statut = Convert.ToBoolean(rd["statut"]);
                virement.scan = rd["scan"].ToString();
                virement.user_sender = User.getUser(int.Parse(rd["id_user_sender"].ToString()));
                virments.Add(virement);
            }
            con.Close();
            return virments;
        }
    }
}
