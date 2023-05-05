﻿using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Application_visa.Models
{
    public abstract class Files
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "Nom est obligatoire")]

        public string nom { get; set; }
        [Required(ErrorMessage = "Prenom est obligatoire")]

        public string prenom { get; set; }
        [Required(ErrorMessage = "Telephone est obligatoire")]

        public String tele { get; set; }
        [Required(ErrorMessage = "CIN est obligatoire")]

        public string cin { get; set; }
        [Required(ErrorMessage = "Charge est obligatoire")]

        public float charge { get; set; }

        public float prix { get; set; }
        [Required(ErrorMessage = "Total est obligatoire")]

        public float total { get; set;}

        public IFormFile? file { get; set; }
        public string? scan { get; set;}
        public Boolean ami_khaled { get; set; }
        public DateTime date { get; set; }
        public Service service { get; set; }
        public Fournisseur fournisseur { get; set; }
        public User user { get; set; }
    

        public static MySqlConnection connexion()
        {
            using MySqlConnection connection = new MySqlConnection("server=localhost;database=apk_visa;uid=root;password=;");
            return connection;
        }

    }
}