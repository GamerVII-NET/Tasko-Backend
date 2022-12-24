using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using Tasko.Client.Models;
using Tasko.Client.Services;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;

namespace Tasko.Client.ViewModels
{
    #region Interfaces
    public interface IRegisterViewModel
    {
        #region Properties
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Patronymic { get; set; }
        bool LoginFailureHidden { get; set; }
        string ErrorMessage { get; set; }

        IUserService UserService { get; set; }
        #endregion

        #region Methods
        Task<IUserAuthRead> RegistrationAsync();
        #endregion
    }
    #endregion

    #region Abstracts   
    public abstract class IRegsiterViewModelBase : IRegisterViewModel, IDisposable
    {
        #region Consturctors
        public IRegsiterViewModelBase(IHttpClientFactory clientFactory, IUserService userService)
        {
            HttpClient = clientFactory.CreateClient("base");
            UserService = userService;
        }
        #endregion

        #region Fields
        internal readonly HttpClient HttpClient;
        private bool _disposed;
        #endregion

        #region Properties
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Patronymic { get; set; }
        public virtual bool LoginFailureHidden { get; set; }
        public virtual IUserService UserService { get; set; }
        public virtual string ErrorMessage { get; set; }
        #endregion

        #region Methods
        protected virtual void Dispose(bool dispoing)
        {
            if (!_disposed)
            {
                if (dispoing)
                {
                    HttpClient.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract Task<IUserAuthRead> RegistrationAsync();
        #endregion
    }
    #endregion

    #region Providers
    public class RegisterViewModel : IRegsiterViewModelBase
    {
        public RegisterViewModel(IHttpClientFactory clientFactory, IUserService userService) : base(clientFactory, userService)
        {
        }

        #region Propeties
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public override string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public override string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        public override string Email { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public override string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        public override string LastName { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Отчество обязательно для заполнения")]
        public override string Patronymic { get; set; }

        public override bool LoginFailureHidden { get; set; } = true;

        public override string ErrorMessage { get; set; }
        #endregion

        #region Methods
        public async override Task<IUserAuthRead> RegistrationAsync()
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
            var response = await HttpClient.PostAsync("/api/users", content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {

                var createdUser = JsonConvert.DeserializeObject<RequestResponse<UserAuthRead>>(result);

                await UserService.SaveUserInStorageAsync(createdUser.Response);

                return createdUser.Response;
            }
            else
            {
                var request = JsonConvert.DeserializeObject<BadRequestResponse<List<ValidationFailure>>>(result);

                ErrorMessage = $"{request.Message}\n";

                foreach (var item in request.Error.Select(c => $"{c.ErrorMessage}\n"))
                {
                    ErrorMessage += item;
                }
            }
            LoginFailureHidden = false;
            return null;
        }

        #endregion
    }
    #endregion
}
