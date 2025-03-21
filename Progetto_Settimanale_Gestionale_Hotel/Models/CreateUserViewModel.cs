namespace Progetto_Settimanale_Gestionale_Hotel.Models
{
    public class CreateUserViewModel
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfermaPassword { get; set; }
        public string Role { get; set; }
    }
}
