namespace DistantLearning.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public string? QuestionAnswer { get; set; }
        public int TestId { get; set; }
        public virtual Test? Test { get; set; } 

    }
}
