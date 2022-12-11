using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.Client.ViewModels
{
    #region Interfaces
    public interface ILoginViewModel
    {
        #region Properties
        string Username { get; set; }
        string Initials { get; set; }
        string Photo { get; set; }
        string UserAbout { get; set; }
        Guid UserId { get; set; }
        string Password { get; set; }
        bool LoginFailureHidden { get; set; }
        string ErrorMessage { get; set; }
        #endregion

        #region Methods
        Task<string> ValidateLoginAsync();
        Task<IUser> GetProfile();
        Task UpdateUserStorageParams(IUser user);
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
        public string Initials { get; set; }
        public string Photo { get; set; }
        public string UserAbout { get; set; }
        public Guid UserId { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Methods
        public abstract Task<string> ValidateLoginAsync();

        public abstract Task<IUser> GetProfile();
        public abstract Task UpdateUserStorageParams(IUser user);

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

        #endregion

    }
    #endregion

    #region Providers
    public class LoginViewModel : LoginViewModelBase
    {
        #region Constructors
        public LoginViewModel(IHttpClientFactory clientFactory) : base(clientFactory) { } 
        #endregion

        #region Properties
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public override string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public override string Password { get; set; }

        public override bool LoginFailureHidden { get; set; } = true;
        public string ErrorMessage { get; set; }
        #endregion

        #region Methods
        public override async Task<IUser> GetProfile()
        {
            var userGuid = await SecureStorage.GetAsync("UserId");

            if (string.IsNullOrEmpty(userGuid) == false)
            {
                var token = await SecureStorage.GetAsync("Token");

                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.GetAsync($"/api/users/{userGuid}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var user = JsonConvert.DeserializeObject<IUser>(result);

                    UpdateUserStorageParams(user);
                }

            }

            return null;
        }

        public override async Task UpdateUserStorageParams(IUser user)
        {
            Initials = $"{user.LastName} {user.FirstName[0]}. {user.Patronymic[0]}.";
            Photo = user.Photo;
            UserId = user.Id;
            UserAbout = user.About;

            await SecureStorage.SetAsync("Initials", Initials);
            await SecureStorage.SetAsync("Photo", Photo);
            await SecureStorage.SetAsync("UserId", UserId.ToString());
            await SecureStorage.SetAsync("UserAbout", UserAbout);
        }

        public async override Task<string> ValidateLoginAsync()
        {
            var user = new UserAuth
            {
                Login = Username,
                Password = Password
            };
            var content = JsonContent.Create(user);
            var response = await HttpClient.PostAsync("/api/authorization", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            LoginFailureHidden = false;
            return string.Empty;
        } 
        #endregion
    }
    #endregion

}
