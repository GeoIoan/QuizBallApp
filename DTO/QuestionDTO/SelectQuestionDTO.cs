namespace QuizballApp.DTO.QuestionDTO
{
    public class SelectQuestionDTO
    {
        public int? Gamemaster_id { get; set; }
        public int Category_id { get; set; }
        public int Difficulty_id { get; set; }

        public int LastQuestion { get; set; }

        public override string? ToString()
        {
            return $"gmId: {Gamemaster_id}, catId: {Category_id}, DifId: {Difficulty_id}";
        }
    }
}
