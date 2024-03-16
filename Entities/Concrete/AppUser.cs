using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
