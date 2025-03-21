using Microsoft.AspNetCore.Identity;
using Progetto_Settimanale_Gestionale_Hotel.Models;

namespace Progetto_Settimanale_Gestionale_Hotel.Services
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roleNames = new string[] { "Admin", "Manager", "Receptionist" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }
        }
    }



}
