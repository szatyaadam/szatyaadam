namespace CookBook.ApiClient.Models
{
    public class ActualUser
    {
        public ActualUser (int id, string userName, string password, string role, string? access_Token, string? refresh_Token)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.role = role;
            this.access_Token = access_Token;
            this.refresh_Token = refresh_Token;
        }
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string? access_Token { get; set; }
        public string? refresh_Token { get; set; }
        public ActualUser()
        {       
        }
    }
}

