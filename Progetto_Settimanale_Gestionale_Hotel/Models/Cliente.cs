namespace Progetto_Settimanale_Gestionale_Hotel.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<Prenotazione> Prenotazioni { get; set; } = new();
    }

}
