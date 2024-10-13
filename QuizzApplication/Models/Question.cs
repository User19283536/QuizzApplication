namespace QuizzApplication.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }
        public int? AnswerText { get; set; }

        public Question() { }
            
    }
}
