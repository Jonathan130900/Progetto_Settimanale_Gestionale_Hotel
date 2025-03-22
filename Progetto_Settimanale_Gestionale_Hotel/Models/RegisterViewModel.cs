namespace Progetto_Settimanale_Gestionale_Hotel.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Le password non coincidono!")]
        public string ConfirmPassword { get; set; }
    }

}
