using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using CookBook.API.Controllers.DTO;
using CookBook.ApiClient.Models;
namespace CookBook.ApiClient.Repositories
{
    public class GenericAPIRepository<T> : BaseAPIRepository, IGenericRepository<T>
    {
        public GenericAPIRepository(string path, string? baseUrl = null, DelegatingHandler? handler = null) : base(baseUrl, path, handler)
        {
        }
        public async Task<ActualUser> GetLoginAsync(UserLogins log)
        {
           var response= await client.PostAsJsonAsync(_path + "/login", log);
           
            if (response.IsSuccessStatusCode)
            {

            var content = response.Content.ReadAsStringAsync();
            content.Wait();
            string result = content.Result;
                
            ActualUser actual= JsonSerializer.Deserialize<ActualUser>(result); 
             
            return  actual;
            }
            return null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            
            return await client.GetFromJsonAsync<List<T>>(_path);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await client.GetFromJsonAsync<T>(_path + "/" + id);
        }

        public async Task<bool> ExistsByIdAsync(int id,JwtToken actual=null)
        {
            if (_path.Contains("ser"))
            {
                _path = "api/Admin/user";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
                HttpResponseMessage responseMessage = await client.GetAsync(_path +"/"+ id);
                return responseMessage.IsSuccessStatusCode;
            }
            else
            {
            HttpResponseMessage responseMessage = await client.GetAsync(_path + "/id?id=" +id);
            return responseMessage.IsSuccessStatusCode;

            }
        }

        public async Task InsertAsync(T entity,JwtToken actual)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
            await client.PostAsJsonAsync(_path+"/add",entity);
        }

        public async Task UpdateAsync( T entity, JwtToken actual)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
   
            await client.PutAsJsonAsync(_path + "/modify", entity);

        }

        public async Task DeleteAsync(int id, JwtToken actual)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
            if (_path.Contains("ser"))
            {
                _path = "api/Admin/user";
                await client.DeleteAsync(_path + "/delete/" + id);

            }
            else
            {
            await client.DeleteAsync(_path + "/delete" + id);
            }
        }
    }
}
