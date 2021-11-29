namespace DistantLearning.Models
{
    public class AnswerComplete
    {
        public int AnswerCompleteId { get; set; }
        public string? Answer { get; set; }
        public int TestCompleteID { get; set; }
        public int QuestionID { get; set; }
        public string? RightAnswer { get; set; }
        public virtual TestComplete? TestComplete { get; set; } 
        public virtual Question? Question { get; set; }  
    }
}
