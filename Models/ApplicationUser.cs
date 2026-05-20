using Microsoft.AspNetCore.Identity;

namespace Proyecto_Red.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? NombreCompleto { get; set; }
    }
}
