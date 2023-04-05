using Application_visa.Models;
using MySql.Data.MySqlClient;
using Mysqlx;
using System;

namespace Application_visa.Models
{
    public class Statistique : Files
    {
        public List<Application_visa.Models.Service> services { get; set; }
        public List<Application_visa.Models.Files> files { get; set; }
        public List<Application_visa.Models.Agence> agences { get; set; }
        public List<Application_visa.Models.dateRepository> years { get; set; }
        public List<Application_visa.Models.dateRepository> monthsNM { get; set; }
        public List<Application_visa.Models.Statistique> nbrFilles { get; set; }
        public List<Application_visa.Models.User> users { get; set; }
        public List<Application_visa.Models.Statistique> countSumFourniseurs { get; set; }
        public List<Application_visa.Models.Fournisseur> fournisseureName { get; set; }
        public string getYears { get; set; }
        public string monthName { get; set; }
        public int countNbrFillesAdded { get; set; }
        public int countNbrFilles { get; set; }
        public int nbrFilesAvance { get; set; }
        public int nbrFilesDone { get; set; }
        public float nbrSalaireFour { get; set; }

        public List<Files> filterQeury(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            string service = "true";
            string agence = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (agenceId != -1)
            {
                agence = "agence.id= " + agenceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            List<Files> files = new List<Files>();
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT * from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user inner join service on files.id_service=service.id where {service} and  {agence} and  {dateCompare}  and  {Years} and {amieKh}";

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Statistique file = new Statistique();
                    file.nom = reader[9].ToString();
                    file.prenom = reader[10].ToString();
                    file.tele = reader[11].ToString();
                    file.prix = float.Parse(reader[13].ToString());
                    file.charge = float.Parse(reader[14].ToString());
                    file.total = float.Parse(reader[15].ToString());
                    file.date = DateTime.Parse(reader[29].ToString());
                    file.service = new Service();
                    file.service.nom = reader[31].ToString();
                    file.user = new User();
                    file.user.agence = new Agence();
                    file.user.agence.nom = reader[1].ToString();

                    files.Add(file);
                }
            }
            conn.Close();
            return files;
        }
        public int countAddedFiles(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            string service = "true";
            string agence = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (agenceId != -1)
            {
                agence = "agence.id= " + agenceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where {service} and  {agence} and  {dateCompare}  and  {Years} and {amieKh} ";

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countNbrFillesAdded = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return countNbrFillesAdded;
        }
        public Tuple<List<Statistique>, List<dateRepository>> countFiles(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            List<dateRepository> monthNames = new List<dateRepository>();
            List<Statistique> listF = new List<Statistique>();
            string service = "true";
            string agence = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (agenceId != -1)
            {
                agence = "agence.id= " + agenceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }

            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            for (int i = 1; i <= 12; i++)
            {
                dateRepository.getMonthNames(i, monthNames);
                conn.Open();
                using MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where month(files.date)={i}  and  {service} and  {agence} and  {dateCompare}  and  {Years} and {amieKh} ";
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Statistique s = new Statistique();
                        s.countNbrFilles = int.Parse(reader[0].ToString());
                        listF.Add(s);
                    }
                }

                conn.Close();
            }
            return Tuple.Create(listF, monthNames);
        }
        public List<dateRepository> getAllYears()
        {
            List<dateRepository> listYears = new List<dateRepository>();
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = "select DISTINCT Year(date) from files order by year(date) asc";
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dateRepository getYears = new dateRepository();
                    getYears.getAllyears = int.Parse(reader[0].ToString());
                    listYears.Add(getYears);
                }
            }
            conn.Close();
            return listYears;
        }
        public int getCountAvance(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            string service = "true";
            string agence = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (agenceId != -1)
            {
                agence = "agence.id= " + agenceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where files.prix<files.total and {service} and  {agence} and  {dateCompare}  and  {Years} and {amieKh} ";
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nbrFilesAvance = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return nbrFilesAvance;
        }
        public int getCountFilesDone(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {
            string service = "true";
            string agence = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (agenceId != -1)
            {
                agence = "agence.id= " + agenceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where files.prix=files.total and {service} and  {agence} and  {dateCompare}  and  {Years} and   {amieKh} ";
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nbrFilesDone = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return nbrFilesDone;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int filterQeuryByAgennce(int serviceId, string dateD, string dateF, int year, string kh, int agId)
        {
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }

            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user inner join service on files.id_service=service.id where agence.id=@idAg and {service}  and  {dateCompare}  and  {Years} and {amieKh}";
            command.Parameters.Add(new MySqlParameter("@idAg", agId));
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countNbrFillesAdded = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return countNbrFillesAdded;
        }
        public int getCountFilesDoneByAg(int serviceId, string dateD, string dateF, int year, string kh, int agId)
        {
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user inner join service on files.id_service=service.id where agence.id=@idAg and  files.prix=files.total and {service} and  {dateCompare}  and  {Years} and {amieKh}";
            command.Parameters.Add(new MySqlParameter("@idAg", agId));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nbrFilesDone = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return nbrFilesDone;
        }
        public int getCountAvanceByAg(int serviceId, string dateD, string dateF, int year, string kh, int agId)
        {
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }

            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where files.prix<files.total and agence.id=@idAg and {service} and  {dateCompare}  and  {Years} and {amieKh} ";
            command.Parameters.Add(new MySqlParameter("@idAg", agId));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nbrFilesAvance = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return nbrFilesAvance;
        }
        public Tuple<List<Statistique>, List<dateRepository>> countFilesByAg(int serviceId, string dateD, string dateF, int year, string kh, int agId)
        {
            List<dateRepository> monthNames = new List<dateRepository>();
            List<Statistique> listF = new List<Statistique>();
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }

            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }

            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            for (int i = 1; i <= 12; i++)
            {
                dateRepository.getMonthNames(i, monthNames);
                conn.Open();
                using MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where agence.id=@idAg and month(files.date)={i}  and  {service}  and  {dateCompare}  and  {Years} and {amieKh} ";
                command.Parameters.Add(new MySqlParameter("@idAg", agId));
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Statistique s = new Statistique();
                        s.countNbrFilles = int.Parse(reader[0].ToString());
                        listF.Add(s);
                    }
                }

                conn.Close();
            }
            return Tuple.Create(listF, monthNames);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int filterQeuryByEmp(int serviceId, string dateD, string dateF, int year, string kh, int agId, int empId)
        {
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }

            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user inner join service on files.id_service=service.id where user.id=@userId and agence.id=@idAg and {service}  and  {dateCompare}  and  {Years} and {amieKh}";
            command.Parameters.Add(new MySqlParameter("@idAg", agId));
            command.Parameters.Add(new MySqlParameter("@userId", empId));
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countNbrFillesAdded = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return countNbrFillesAdded;
        }
        public int getCountFilesDoneByEmp(int serviceId, string dateD, string dateF, int year, string kh, int agId, int empId)
        {
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user inner join service on files.id_service=service.id where user.id=@userId and agence.id=@idAg and  files.prix=files.total and {service} and  {dateCompare}  and  {Years} and {amieKh}";
            command.Parameters.Add(new MySqlParameter("@idAg", agId));
            command.Parameters.Add(new MySqlParameter("@userId", empId));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nbrFilesDone = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return nbrFilesDone;
        }
        public int getCountAvanceByEmp(int serviceId, string dateD, string dateF, int year, string kh, int agId, int empId)
        {
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }

            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            conn.Open();
            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where user.id=@userId and files.prix<files.total and agence.id=@idAg and {service} and  {dateCompare}  and  {Years} and {amieKh} ";
            command.Parameters.Add(new MySqlParameter("@idAg", agId));
            command.Parameters.Add(new MySqlParameter("@userId", empId));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nbrFilesAvance = int.Parse(reader[0].ToString());
                }
            }
            conn.Close();
            return nbrFilesAvance;
        }
        public Tuple<List<Statistique>, List<dateRepository>> countFilesByEmp(int serviceId, string dateD, string dateF, int year, string kh, int agId, int empId)
        {
            List<dateRepository> monthNames = new List<dateRepository>();
            List<Statistique> listF = new List<Statistique>();
            string service = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }

            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }

            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True"); ;
            for (int i = 1; i <= 12; i++)
            {
                dateRepository.getMonthNames(i, monthNames);
                conn.Open();
                using MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT count(*) from agence inner join user on agence.id=user.id_agence inner join files on user.id=files.id_user where user.id=@userId and agence.id=@idAg and month(files.date)={i}  and  {service}  and  {dateCompare}  and  {Years} and {amieKh} ";
                command.Parameters.Add(new MySqlParameter("@idAg", agId));
                command.Parameters.Add(new MySqlParameter("@userId", empId));
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Statistique s = new Statistique();
                        s.countNbrFilles = int.Parse(reader[0].ToString());
                        listF.Add(s);
                    }
                }

                conn.Close();
            }
            return Tuple.Create(listF, monthNames);
        }
        public List<Statistique> getCountCharge(int serviceId, int agenceId, string dateD, string dateF, int year, string kh)
        {            
            List<Fournisseur> fournisseurs = Fournisseur.getAllFournisseurs();
            List<Statistique> countVirments = new List<Statistique>();
            string service = "true";
            string agence = "true";
            string dateCompare = "true";
            string Years = "true";
            string amieKh = "true";
         
            if (kh != null)
            {
                amieKh = "files.ami_khalid= " + bool.Parse(kh) + "";

            }
            if (serviceId != -1)
            {
                service = "files.id_service= " + serviceId + "";

            }
            if (agenceId != -1)
            {
                agence = "agence.id= " + agenceId + "";

            }
            if (dateD != null)
            {
                dateCompare = "DATE(files.date) between '" + dateD + "' and '" + dateF + "'";
            }

            if (year != -1)
            {
                Years = "Year(files.date)= " + year + "";

            }
            
            MySqlConnection conn = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;convert zero datetime=True");
            foreach(Fournisseur f in fournisseurs)
             {             
                conn.Open();
                using MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT sum(virment.montant) FROM virment inner join fournisseur on virment.id_fourn=fournisseur.id  where virment.id_fourn=@idF";
                command.Parameters.Add(new MySqlParameter("@idF", f.id));
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Statistique s = new Statistique();
                        try
                        {
                            s.nbrSalaireFour = float.Parse(reader[0].ToString());
                        }
                        catch (FormatException ex)
                        {
                            // Handle the error here
                            Console.WriteLine("Error parsing value: " + reader[0].ToString());
                            Console.WriteLine(ex.Message);
                        }
                           countVirments.Add(s);
                    }
                }
                conn.Close();
            }
            return countVirments;
        }               
        
    }
}
