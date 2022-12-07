using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Tasko.Domains.Models.DTO.User;

namespace Tasko.Client.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "�����")]
        [Required(ErrorMessage = "����� ���������� ��� ����������")]
        public string Username { get; set; }

        [Display(Name = "������")]
        [Required(ErrorMessage = "������ ���������� ��� ����������")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email ���������� ��� ����������")]
        public string Email { get; set; }

        [Display(Name = "���")]
        [Required(ErrorMessage = "��� ����������� ��� ����������")]
        public string FirstName { get; set; }

        [Display(Name = "�������")]
        [Required(ErrorMessage = "������� ����������� ��� ����������")]
        public string LastName { get; set; }

        [Display(Name = "��������")]
        [Required(ErrorMessage = "�������� ����������� ��� ����������")]
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
