using System;
using System.ComponentModel.DataAnnotations;
namespace DistantLearning.Models
{
    public class Student : Person
    {
        [Display(Name = "Группа")]
        public int? groupID { get; set; }
        public virtual Group? Group { get; set; }

    }
}
