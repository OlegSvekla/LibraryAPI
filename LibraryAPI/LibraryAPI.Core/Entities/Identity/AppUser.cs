using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
