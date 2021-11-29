using System.ComponentModel.DataAnnotations;
namespace DistantLearning.Models
{
    public class Teacher : Person  
    {
        [Display(Name = "Должность")]
        public int DegreeId { get; set; }
        public virtual Degree? Degree { get; set; }
    }
}
