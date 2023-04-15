namespace Application_visa.Models
{
    public class Virment : Files
    {
        private int? id { get; set; }
        private DateTime date { get; set; }
        private int montant { get; set; }
        private Boolean statut { get; set; }
        private User user_receiver { get; set;}
        private User user_sender { get; set;}
        private string scan { get; set;}
        private Fournisseur fournisseur { get; set;}

        private User agent { get; set;}
    }
}
