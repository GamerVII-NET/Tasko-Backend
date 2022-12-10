using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Tasko.Domains.Models.DTO.User;

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
        #endregion

        #region Methods
        Task<string> RegistrationAsync();
        #endregion
    }
    #endregion

    #region Abstracts   
    public abstract class IRegsiterViewModelBase : IRegisterViewModel, IDisposable
    {
        #region Consturctors
        public IRegsiterViewModelBase(IHttpClientFactory clientFactory) => HttpClient = clientFactory.CreateClient("base");
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

        public abstract Task<string> RegistrationAsync();
        #endregion
    }
    #endregion

    #region Providers
    public class RegisterViewModel : IRegsiterViewModelBase
    {
        #region Constructors
        public RegisterViewModel(IHttpClientFactory clientFactory) : base(clientFactory) { }
        #endregion

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
        #endregion

        #region Methods
        public async override Task<string> RegistrationAsync()
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
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Dispose(true);
                return await response.Content.ReadAsStringAsync();
            }
            LoginFailureHidden = false;
            return string.Empty;
        }

        #endregion
    }
    #endregion
}
