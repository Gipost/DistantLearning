using System.ComponentModel.DataAnnotations;
namespace DistantLearning.Models
{
    public class Subject
    {
       public int SubjectId { get; set; }
       public string? SubjectName { get; set; }
        [Display(Name = "Учитель")]
        public int TeacherId { get; set; }
        public virtual Teacher? teacher { get; set; }
    }
}
