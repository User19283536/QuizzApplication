namespace QuizzApplication.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }
        public string? AnswerText { get; set; }
        public string? CreatedBy { get; set; }  
        public DateTime? CreatedAt { get; set; } 
        public string? ModifiedBy { get; set; } 
        public DateTime? ModifiedAt { get; set; } 

        public Question() { }
            
    }
}
