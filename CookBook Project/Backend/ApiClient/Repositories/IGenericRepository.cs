using CookBook.ApiClient.Models;
namespace CookBook.ApiClient.Repositories
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Frissíti a Tokent.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        Task<ActualUser> GetRefreshTokenAsync(JwtToken actual);

        /// <summary>
        ///  Létrehoz egy új elemet
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> NewItemAsync(T entity,JwtToken actual);

        /// <summary>
        /// Egy adott elemet megváltoztat        /// </summary>
        /// <param name="entity"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> ChangeItemAsync(T entity,JwtToken actual);

        /// <summary>
        /// Bejelentkeztet egy felhasználót
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
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
        /// <param name="actual"></param>
        /// <returns></returns>
        Task<bool> ExistsByIdAsync(int id,JwtToken actual=null);
        /// <summary>
        /// Beilleszt egy új elemet
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> InsertAsync(T entity,JwtToken actual);

        /// <summary>
        /// Módosít egy meglévő elemet azonosító alapján
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdateAsync( T entity, JwtToken actual);

        /// <summary>
        /// Törli a megadott elemet azonosító alapján
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(int id, JwtToken actual);

    }
}
