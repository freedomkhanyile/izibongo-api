using System.ComponentModel.DataAnnotations;

namespace izibongo.api.DAL.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; } 
    }

    public class TokenResponse
    {
        public string Token { get; set; }    
        public string UserName { get; set; }    
    }
}