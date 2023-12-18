using QuizballApp.Data;
using QuizballApp.DTO;
using QuizballApp.DTO.QuestionDTO;
///<summary>
///This interface contains all the methods that are needed
///so that the bussiness logic of the question entity can be
///implemented.
///</summary>


namespace QuizballApp.Services
{
    public interface IQuestionService
    {
        /// <summary>
        /// Runs asychronously and is used to create a new custom question.
        /// </summary>
        /// <param name="dto">(CreateQuestionDTO) Contains all the nesseccary data needed for
        ///                                     this operation</param>
        /// <returns>(QuestionReadOnlyDTO) The questions that was added</returns>
        
        Task<QuestionReadOnlyDTO> CreateCustomQuestionAsync(CreateQuestionDTO dto);
        
        /// <summary>
        /// Runs asychronously and is used to update an existing custom question. 
        /// </summary>
        /// <param name="dto">(UpdateQuestionDTO) Contains all the nesseccary data needed for
        ///                                     this operation</param>
        /// <returns>(QuestionReadOnlyDTO) The questions that was updated</returns>
        Task<QuestionReadOnlyDTO> UpdateCustomQuestionAsync(UpdateQuestionDTO dto);
        
        /// <summary>
        /// Runs asychronously and is used to delete an existing custom question.
        /// </summary>
        /// <param name="id">(int) The questions id</param>
        /// <returns>(QuestionReadOnlyDTO)The question that was deleted</returns>
        Task<QuestionReadOnlyDTO> DeleteCustomQuestionAsync(int id);
        
        /// <summary>
        /// Runs asychronously and returns all the custom questions of a gamemaster.
        /// </summary>
        /// <param name="gamemasterId">(int)The gamemasters id</param>
        /// <returns>(IList<QuestionReadOnlyDTO>) The questions that meet the provided criteria</returns>
        Task<IList<QuestionReadOnlyDTO>> GetCustomQuestionsAsync(int gamemasterId);
        
        /// <summary>
        /// Runs asychronously and returns a random question of a specific category 
        /// and difficulty.
        /// </summary>
        /// <param name="dto">(SelectQuestionDTO) Contains all the nessecary data needed
        ///                    for this operation</param>
        /// <returns>(QuestionReadOnlyDTO)The question that meet the provided criteria</returns>
        Task<QuestionReadOnlyDTO> GetRandomQuestionAsync(SelectQuestionDTO dto);

        /// <summary>
        /// This is not used and will be deleted!
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddGameAsync(int questionId, InsertGameToQuestionDTO dto);

        /// <summary>
        /// Runs asychronously and returns all the custom questions of gamemaster
        /// filtered by category
        /// </summary>
        /// <param name="gamemasterId">(int)The gamemasters id</param>
        /// <param name="catId">(int) The categories id</param>
        /// <returns>(IList<QuestionReadOnlyDTO>) The questions that meet the provided
        ///                                        criteria</returns>
        Task<IList<QuestionReadOnlyDTO>> GetCustQuestionsByCatAsync(int gamemasterId, int catId);
    }
}
