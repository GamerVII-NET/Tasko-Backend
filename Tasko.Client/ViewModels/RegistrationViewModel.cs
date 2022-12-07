using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Tasko.Domains.Models.DTO.User;

namespace Tasko.Client.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Отчество обязательно для заполнения")]
        public string Patronymic { get; set; }

        public bool LoginFailureHidden { get; set; } = true;

        public async Task<string> RegistrationAsync()
        {

            var user = new UserCreate
            {
                Login = Username,
                Password = Password,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Patronymic = Patronymic
            };

            JsonContent content = JsonContent.Create(user);

            using (var client = new HttpClient())
            {

                var response = await client.PostAsync("http://87.249.49.56:7177/api/users", content);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
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
