using CookBook.ApiClient.Models;
namespace CookBook.ApiClient.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<ActualUser> GetLoginAsync(UserLogins log);
        /// <summary>
        /// Lekérdezi az összes elemet
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// Lekérdez egy elemet azonosító alapján
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// Létezik-e az elem, azonosító alapján
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsByIdAsync(int id,JwtToken actual=null);
        /// <summary>
        /// Beilleszt egy új elemet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(T entity,JwtToken actual);
        /// <summary>
        /// Módosít egy meglévő elemet azonosító alapján
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync( T entity, JwtToken actual);
        /// <summary>
        /// Törli a megadott elemet azonosító alapján
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id, JwtToken actual);
    }
}
