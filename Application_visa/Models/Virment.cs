using MySql.Data.MySqlClient;

namespace Application_visa.Models
{
    public class Virment : Files
    {
        public int? id { get; set; }
        public DateTime date { get; set; }
        public int montant { get; set; }
        public Boolean statut { get; set; }
        public User user_receiver { get; set;}
        public User user_sender { get; set;}
        public string scan { get; set;}
        public Fournisseur fournisseur { get; set;}
        public User agent { get; set;}

        public void VirmentToUser(int id_sender)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO virment (date, montant, statut,id_user_sender,id_user_receiver) VALUES (Now(),@montant,FALSE,@sender,@reciver)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@montant", this.montant));
            cmd.Parameters.Add(new MySqlParameter("@sender",id_sender));
            cmd.Parameters.Add(new MySqlParameter("@reciver", this.user_receiver.id));

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void VirmentToFourn(int id_sender)
        {
            Service ser = Service.getService("aposstille");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO virment (date, montant, statut,id_user_sender,id_fourn,scan) VALUES (Now(),@montant,FALSE,@sender,@fourn,@scan)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@montant", this.montant));
            cmd.Parameters.Add(new MySqlParameter("@sender", id_sender));
            cmd.Parameters.Add(new MySqlParameter("@fourn", this.fournisseur.id));
            cmd.Parameters.Add(new MySqlParameter("@scan", this.scan));


            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void getAll(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "Select * from Service where id_user_sender= @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            aposstille virement = new aposstille();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                virement.id = int.Parse(rd["id"].ToString());
                virement.nom = rd["nom"].ToString();
                virement.prenom = rd["prenom"].ToString();
                virement.tele = rd["tele"].ToString();
                virement.cin = rd["cin"].ToString();
                virement.prix = float.Parse(rd["prix"].ToString());
                virement.charge = float.Parse(rd["charge"].ToString());
                virement.total = float.Parse(rd["total"].ToString());
                virement.scan = rd["scan"].ToString();
                virement.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                virement.destination = rd["destination"].ToString();
            }
            con.Close();
            return virement;


            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
