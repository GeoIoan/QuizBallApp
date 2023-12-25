namespace QuizballApp.DTO.QuestionDTO
{
    ///<summary>
    ///Instances of this class
    ///transfer the data needed to
    ///complete the operation of 
    ///selecting a random question
    ///of a specific category and
    ///difficulty level.
    ///</summary>
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
