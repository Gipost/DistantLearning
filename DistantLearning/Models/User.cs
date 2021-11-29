using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DistantLearning.Models
{
    public class User:IdentityUser
    {
        int? StudentId;
        int? TeacherId;
        public virtual Teacher? Teacher { get; set; }
        public virtual Student? Student { get; set; }
    }
}
