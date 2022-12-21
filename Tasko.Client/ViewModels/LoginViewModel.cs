using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tasko.Client.Models;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;

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
        ObservableCollection<BoardRead> Boards { get; set; }
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
        public virtual string Initials { get; set; }
        public virtual string Photo { get; set; }
        public virtual string UserAbout { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string ErrorMessage { get; set; }
        public virtual ObservableCollection<BoardRead> Boards { get; set; } = new ObservableCollection<BoardRead>();
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
        public override string ErrorMessage { get; set; }

        public override ObservableCollection<BoardRead> Boards { get => base.Boards; set => base.Boards = value; }


        #endregion

        #region Methods
        public override async Task<IUser> GetProfile()
        {

            var token = await SecureStorage.GetAsync("Token");

            if (token != string.Empty)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.GetAsync($"/api/boards");


                if (response.IsSuccessStatusCode)
                {
                    var boardsContent = await response.Content.ReadAsStringAsync();

                    var boards = JsonConvert.DeserializeObject<GetRequestResponse<BoardRead>>(boardsContent);

                    Boards = new ObservableCollection<BoardRead>(boards.Response.Items);
                }
                else
                {
                    var test = response.StatusCode;
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
            var response = await HttpClient.PostAsync("/api/auth", content);

            var result = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                LoginFailureHidden = true;

                var model = JsonConvert.DeserializeObject<RequestResponse<UserAuthRead>>(result);

                return model.Response.Token;
            }
            else
            {

                var model = JsonConvert.DeserializeObject<BadRequestResponse<List<ValidationFailure>>>(result);


                ErrorMessage = model.Error.FirstOrDefault().ErrorMessage;
            }

            LoginFailureHidden = false;

            return string.Empty;
        }
        #endregion
    }
    #endregion

}
