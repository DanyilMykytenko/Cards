using Microsoft.AspNetCore.Identity;

namespace Cards.Identity.Models
{
    public class AdminRolesModel
    {
        public AppUser User { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<string> UserRoles { get; set; }
    }
}