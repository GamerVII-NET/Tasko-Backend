using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Tasko.Domains.Models.DTO.User;

namespace Tasko.Client.ViewModels
{
    #region Interfaces
    public interface ILoginViewModel
    {
        #region Properties
        string Username { get; set; }
        string Password { get; set; }
        bool LoginFailureHidden { get; set; }
        #endregion

        #region Methods
        Task<string> ValidateLoginAsync();
        #endregion
    }
    #endregion

    #region Abstracts
    public abstract class LoginViewModelBase : ILoginViewModel, IDisposable
    {
        #region Consturctors
        public LoginViewModelBase(IHttpClientFactory clientFactory) => HttpClient = clientFactory.CreateClient("base");

        #endregion

        #region Fields
        internal readonly HttpClient HttpClient;
        private bool _disposed;
        #endregion

        #region Properties
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual bool LoginFailureHidden { get; set; }
        #endregion

        #region Methods
        public abstract Task<string> ValidateLoginAsync();
        protected virtual void Dispose(bool dispoing)
        {
            if(!_disposed)
            {
                if(dispoing)
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
        #endregion

        
    }
    #endregion

    #region Providers
    public class LoginViewModel : LoginViewModelBase
    {
        public LoginViewModel(IHttpClientFactory clientFactory) : base(clientFactory) { }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public override string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public override string Password { get; set; }

        public override bool LoginFailureHidden { get; set; } = true;

        public async override Task<string> ValidateLoginAsync()
        {
            var user = new UserAuth
            {
                Login = Username,
                Password = Password
            };
            var content = JsonContent.Create(user);
            var response = await HttpClient.PostAsync("/api/authorization", content);
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                Dispose(true);
                return await response.Content.ReadAsStringAsync();
            }   
            LoginFailureHidden = false;
            return string.Empty;
        }
    }
    #endregion


}
