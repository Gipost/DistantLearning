namespace DistantLearning.Models
{
    public class TestComplete
    {
        public int TestCompleteId { get; set; }
        public int Studentid { get; set; }
        public int Subjectid { get; set; }
        
        public int Testid { get; set; }
        public double Mark { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Student? Student { get; set; }   
        
        public virtual Test? Test { get; set; }
    }
}
