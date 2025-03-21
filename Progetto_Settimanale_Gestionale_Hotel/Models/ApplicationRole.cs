using Microsoft.AspNetCore.Identity;

namespace Progetto_Settimanale_Gestionale_Hotel.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }


}
