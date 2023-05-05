﻿using MySql.Data.MySqlClient;
using System.Web;

namespace Application_visa.Models
{
    public class assurance:Files
    {
        public void add()
        {
            Service sr =Service.getService("assurance");
            MySqlConnection con = connexion();
            con.Open();
            String query = "INSERT INTO files (nom, prenom, tele,cin,prix,charge,total,scan,date,id_service,ami_khalid,id_user) VALUES (@nom, @prenom, @tele,@cin,@prix,@charge,@total,@scan,Now(),@id_service,@ami_khalid,@id_user)";
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
            //cmd.Parameters.Add(new MySqlParameter("@id_user", HttpContext.Current.Session["userId"]));

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public assurance getAssurance(int id)
        {
            MySqlConnection con = connexion();
            con.Open();
            string query = "SELECT * FROM files where id= @id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            assurance ass = new assurance();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ass.id =  int.Parse(dr["id"].ToString());
                ass.prix =float.Parse(dr["prix"].ToString());
                ass.charge =float.Parse (dr["charge"].ToString());
                ass.total = float.Parse( dr["total"].ToString());
                ass.nom = dr["nom"].ToString();
                ass.prenom = dr["prenom"].ToString();
                ass.tele = dr["tele"].ToString();
                ass.cin = dr["cin"].ToString();
                ass.scan = dr["scan"].ToString();
            }
            dr.Close();
            con.Close();
            return ass;
        }
        public void modifier()
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