using System.ComponentModel.DataAnnotations;
namespace DistantLearning.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Display(Name = "Инициалы")]
        public string Name { get; set; }
        [Display(Name = "Возраст")]
        public int Age { get; set; }

    }
}
