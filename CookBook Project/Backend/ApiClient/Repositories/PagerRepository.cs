using System.Globalization;
using System.Net.Http.Json;
using CookBook.ApiClient.Models;
using CookBook.ApiClient.Models.DTO;
namespace CookBook.ApiClient.Repositories
{
    public class PagerRepository<T> : GenericAPIRepository<T>, IPagerRepository<T>
    {
        public PagerRepository(string path, string? baseUrl = null, DelegatingHandler? handler = null) : base(path, baseUrl, handler)
        {
        }

        public async Task<TableDTO<T>> GetAllAsync(
            int page = 0,
            int itemsPerPage = 0,
            string? search = null,
            string? sortBy = null,
            bool ascending = true,
            JwtToken actual=null

            )
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
            var queryParameters = new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "itemsPerPage", itemsPerPage.ToString() }
            };
            if (!string.IsNullOrWhiteSpace(search))
            {
                queryParameters.Add("search", search);
            }
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                queryParameters.Add("sortBy", sortBy);
                queryParameters.Add("ascending", ascending.ToString());
            }
          
            // URL kódolásúvá alakítja a szöveget
            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            // Átalakítja egy teljes string formátummá, hogy hozzáfűzze az URL-hez
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            return await client.GetFromJsonAsync<TableDTO<T>>($"{_path}?{queryString}");
        }

    }
}
