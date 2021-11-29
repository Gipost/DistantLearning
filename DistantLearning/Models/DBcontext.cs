using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DistantLearning.Models
{
    public class DBcontext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Test> tests { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<TestComplete> testsCompleted { get; set; }
        public DbSet<AnswerComplete> answersCompleted { get; set; }
        public DBcontext (DbContextOptions<DBcontext> options)
            : base (options)
        {
            Database.EnsureCreated ();
        }
    }
}
