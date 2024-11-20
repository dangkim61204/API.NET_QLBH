namespace Clothing_storeAPI.Models.Request
{
    public class LoginRequest
    {
        public string? userName { get; set; }
        public string password { get; set; }
        public string? email { get; set; }
    }
}
