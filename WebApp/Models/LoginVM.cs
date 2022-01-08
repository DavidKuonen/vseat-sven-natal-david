using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please Enter a Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter a Password")]
        public string Password { get; set; }
    }
}
