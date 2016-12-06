using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MovieProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsBlock { get; set; }
    }
}
