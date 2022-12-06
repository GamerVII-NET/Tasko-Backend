using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace Tasko.Client.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }

        public bool LoginFailureHidden { get; set; } = true;

        public async Task<bool> ValidateLoginAsync(out string jwtToken)
        {
            

            jwtToken = null;
            LoginFailureHidden = false;
            return false;
        }
    }
}
