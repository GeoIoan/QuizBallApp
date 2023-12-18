///<summary>
///This interface contains the basic CRUD operations. The repositories 
///of all the the entities can extend the impementation of this repository
///and use any method of the forementioned ones if needed.
///<summary>
namespace QuizBall.Repositories
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Runs asychronally and returns all the registries 
        /// of the respectable entity.
        /// </summary>
        /// <returns>(IEnumerable<T>)All the registries of an entity</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Runs asychronally and returns a specific registry 
        /// of an entity based on the provided id.
        /// </summary>
        /// <param name="id">(int) The id that represents the unique identifier of a registry</param>
        /// <returns><T?> The registry if found or else null</returns>
        Task<T?> GetAsync(int id);

        /// <summary>
        /// Runs asychronally and adds a registry of a specific entity
        /// in the database.
        /// </summary>
        /// <param name="entity">(T)The registry that is going to be added</param>
        /// <returns>(T?)The added registry or null if the registry was not added</returns>
        Task<T?> AddAsync(T entity);

        /// <summary>
        /// Runs asychronally and updates a registry of a specific entity
        /// after finding it by using the provided id.
        /// </summary>
        /// <param name="id">(int) The id that represents the unique identifier of a registry</param>
        /// <param name="entity">(T) The new data that are going to be used for the update operation</param>
        /// <returns>(T?)The updated registry or null if the registry was not updated</returns>
        Task<T?> UpdateAsync(int id, T entity);

        /// <summary>
        /// Runs asychronally and deletes a specific registry of an entity
        /// defined by the provided id.
        /// </summary>
        /// <param name="id">(int) The id that represents the unique identifier of a registry</param>
        /// <returns>(bool)True if the deletion was successfull and false if not</returns>
        Task<bool> DeleteAsync(int id);
    }
}
