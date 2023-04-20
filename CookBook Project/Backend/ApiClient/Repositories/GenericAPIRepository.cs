using System.Net.Http.Json;
using System.Text.Json;
using CookBook.ApiClient.Models;
using CookBook.Models.Models;

namespace CookBook.ApiClient.Repositories
{
    public class GenericAPIRepository<T> : BaseAPIRepository, IGenericRepository<T>
    {
        public GenericAPIRepository(string path, string? baseUrl = null, DelegatingHandler? handler = null) : base(baseUrl, path, handler)
        {
        }
        public async Task<ActualUser> GetRefreshTokenAsync(JwtToken actual )
        {
            var response = await client.PostAsJsonAsync(_path + "/Refresh",actual);
            if (response.IsSuccessStatusCode)
            {
                ActualUser freshToken =new ActualUser();
                var content = response.Content.ReadAsStringAsync();
                content.Wait();
                string result = content.Result;
                // result.Replace("access_Token", "accessToken");
                //result.Replace("refresh_Token", "refreshToken");
                freshToken =JsonSerializer.Deserialize<ActualUser>(result);
                //TODOO itt nem tölti fel a tokent.
                return freshToken;
            }
            return null;
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

        public async Task<bool> ExistsByIdAsync(int id,JwtToken actual=null)
        {
            var old = _path;
            if (_path.Contains("ser"))
            {
                _path = "api/Admin/user";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
                HttpResponseMessage responseMessage = await client.GetAsync(_path +"/"+ id);
                _path= old;
                return responseMessage.IsSuccessStatusCode;
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(_path + "/" + id);
             
                return responseMessage.IsSuccessStatusCode;

            }

        }
        public async Task<HttpResponseMessage> ChangeItemAsync(T entity, JwtToken actual)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
            var old = _path;
            Mealtype type = new Mealtype();
            Measures measure = new Measures();
            HttpResponseMessage responseMessage= new HttpResponseMessage();
            if (entity.GetType() == type.GetType())
            {
                _path = "api/admin/mealtype";
               responseMessage = await client.PutAsJsonAsync(_path + "/Modify", entity);

            }

            if (entity.GetType() == measure.GetType())
            {
                _path = "api/admin/measure";
              responseMessage = await client.PostAsJsonAsync(_path + "/Modify", entity);
            }
            _path = old;
            return responseMessage;
        }
        public async Task<HttpResponseMessage> NewItemAsync(T entity,JwtToken actual)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
            var old=_path;
            Mealtype type = new Mealtype();
            Measures measure = new Measures();
            if (entity.GetType()==type.GetType())
            {
                _path = "api/admin/mealtype";
            }

            if (entity.GetType()==measure.GetType())
            {
                _path = "api/admin/measure";
            }
            var responseMessage = await client.PostAsJsonAsync(_path + "/add", entity);
            _path= old;
            return responseMessage;
        }
        public async Task<HttpResponseMessage> InsertAsync(T entity,JwtToken actual)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
           var responseMessage= await client.PostAsJsonAsync(_path+"/add",entity);
            return responseMessage;
        }
        public async Task<HttpResponseMessage> UpdateAsync( T entity, JwtToken actual)
        {
            var old = _path;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
            if (_path.Contains("ser"))
            {
                _path = "api/Admin/User";
            }
           var responseMessage= await client.PutAsJsonAsync(_path + "/Modify", entity);
            _path = old;

            return responseMessage;
        }
        public async Task<HttpResponseMessage> DeleteAsync(int id, JwtToken actual)
        {
            var old = _path;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", actual.Access_Token);
            if (_path.Contains("ser"))
            {
                _path = "api/Admin/user";
               HttpResponseMessage responseMessage= await client.DeleteAsync(_path + "/Delete/" + id);
                _path = old;
                return responseMessage;

            }          
            if (_path.Contains("Measure"))
            {
                _path = "api/Admin/measure";
               HttpResponseMessage responseMessage= await client.DeleteAsync(_path + "/Delete/"+id);
                _path = old;
                return responseMessage;

            }    
            if (_path.Contains("MealType"))
            {
                _path = "api/Admin/mealtype";
               HttpResponseMessage responseMessage= await client.DeleteAsync(_path + "/Delete/" + id);
                _path = old;
                return responseMessage;

            }
            else
            {
            HttpResponseMessage responseMessage= await client.DeleteAsync(_path + "/Delete/" +id);
                return responseMessage;
            }
        }
    }
}
