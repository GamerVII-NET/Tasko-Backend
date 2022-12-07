using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Tasko.Domains.Models.DTO.User;

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

        public async Task<string> ValidateLoginAsync()
        {

            var user = new UserAuth
            {
                Login = Username,
                Password = Password
            };

            JsonContent content = JsonContent.Create(user);

            using (var client = new HttpClient())
            {

                var response = await client.PostAsync("http://87.249.49.56:7177/api/authorization", content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)    
                {
                    var jwtToken = await response.Content.ReadAsStringAsync();

                    return jwtToken;
                }


            }

            LoginFailureHidden = false;
            return string.Empty;
        }
    }
}
