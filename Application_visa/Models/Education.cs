using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Application_visa.Models
{
    public class Education : Files
    {
        [Required(ErrorMessage = "Charge du analyse est obligatoire")]

        public float charge2 { get; set; }

        [Required(ErrorMessage = "Charge du Certificat médical est obligatoire")]

        public float charge3 { get; set; }

        public IFormFile? file1 { get; set; }
        public string? scan1 { get; set; }

        public IFormFile? file2 { get; set; }
        public string? scan2 { get; set; }


        [Required(ErrorMessage = "Scan du fichier de traduction est obligatoire")]

        public IFormFile file3 { get; set; }
        public string? scan3 { get; set; }

        public Fournisseur fournisseur2 { get; set; }

        public Fournisseur fournisseur3 { get; set; }

        public void add()
        {
            Service ser = Service.getService("education");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO files (nom, prenom, tele,cin,prix,charge,total,scan,scan1,scan2,scan3,date,id_service,ami_khalid,charge2,charge3,id_fourn1,id_fourn2,id_fourn3) VALUES (@nom, @prenom, @tele,@cin,@prix,@charge,@total,@scan,@scan1,@scan2,@scan3,Now(),@id_service,@ami_khalid,@charge2,@charge3,@id_fourn1,@id_fourn2,@id_fourn3)";
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
            cmd.Parameters.Add(new MySqlParameter("@scan2", this.scan2));
            cmd.Parameters.Add(new MySqlParameter("@scan3", this.scan3));
            cmd.Parameters.Add(new MySqlParameter("@id_service", ser.id));
            cmd.Parameters.Add(new MySqlParameter("@ami_khalid", this.ami_khaled));
            cmd.Parameters.Add(new MySqlParameter("@charge2", this.charge2));
            cmd.Parameters.Add(new MySqlParameter("@charge3", this.charge3));
            cmd.Parameters.Add(new MySqlParameter("@id_fourn1", this.fournisseur.id));
            cmd.Parameters.Add(new MySqlParameter("@id_fourn2", Fournisseur.getLabo().id));
            cmd.Parameters.Add(new MySqlParameter("@id_fourn3", Fournisseur.getMedecin().id));


            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static Education getEducation(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "SELECT * FROM files where id= @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            Education edd = new Education();
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                edd.id = int.Parse(rd["id"].ToString());
                edd.nom = rd["nom"].ToString();
                edd.prenom = rd["prenom"].ToString();
                edd.tele = rd["tele"].ToString();
                edd.cin = rd["cin"].ToString();
                edd.prix = float.Parse(rd["prix"].ToString());
                edd.charge = float.Parse(rd["charge"].ToString());
                edd.charge2 = float.Parse(rd["charge2"].ToString());
                edd.charge3 = float.Parse(rd["charge3"].ToString());
                edd.total = float.Parse(rd["total"].ToString());
                edd.scan = rd["scan"].ToString();
                edd.scan1 = rd["scan1"].ToString();
                edd.scan2 = rd["scan2"].ToString();
                edd.scan3 = rd["scan3"].ToString();
                edd.ami_khaled = Convert.ToBoolean(rd["ami_khalid"]);
                edd.fournisseur = Fournisseur.getFourn(int.Parse(rd["id_fourn1"].ToString()));
                edd.fournisseur2 = Fournisseur.getFourn(int.Parse(rd["id_fourn2"].ToString()));
                edd.fournisseur3 = Fournisseur.getFourn(int.Parse(rd["id_fourn3"].ToString()));

            }
            con.Close();
            return edd;
        }
        public void update()
        {
            MySqlConnection con = connexion();
            con.Open();
            String query = "UPDATE files set prix = @prix , scan1 = @scan1 , scan2 = @scan2 , scan3 = @scan3 , date = Now() where id = @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@prix", this.prix));
            cmd.Parameters.Add(new MySqlParameter("@scan1", this.scan1));
            cmd.Parameters.Add(new MySqlParameter("@scan2", this.scan2));
            cmd.Parameters.Add(new MySqlParameter("@scan3", this.scan3));
            cmd.Parameters.Add(new MySqlParameter("@id", this.id));
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}