using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DistantLearning.Models
{
    public class User:IdentityUser
    {
        public virtual Teacher? Teacher { get; set; }
        public virtual Student? Student { get; set; }
    }
}
