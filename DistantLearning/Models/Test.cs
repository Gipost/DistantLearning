using System.ComponentModel.DataAnnotations;
namespace DistantLearning.Models
{
    public class Test
    {
        public int TestId  { get; set; }
        [Display(Name = "Название теста")]
        public string TestName { get; set; }
        [Display(Name = "Предмет")]
        public int SubjectId { get; set; }
        
        public virtual Subject? Subject { get; set; }
        public virtual Question? Question { get; set; }

    }
}
