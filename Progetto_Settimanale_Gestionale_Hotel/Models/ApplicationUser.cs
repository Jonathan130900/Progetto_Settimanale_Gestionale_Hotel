using Microsoft.AspNetCore.Identity;

namespace Progetto_Settimanale_Gestionale_Hotel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }

}
