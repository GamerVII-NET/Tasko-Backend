using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using Tasko.Client.Models;
using Tasko.Client.Services;
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
        string Password { get; set; }
        string BoardsSearchText { get; set; }
        Guid UserId { get; set; }
        UserAuthRead UserInfo { get; set; }
        bool LoginFailureHidden { get; set; }
        IBoardsService BoardsService { get; set; }
        IUserService UserService { get; set; }
        bool IsLoadingBoards { get; set; }
        string ErrorMessage { get; set; }
        ObservableCollection<BoardRead> Boards { get; set; }
        #endregion

        #region Methods
        Task<string> ValidateLoginAsync();
        Task GetProfileAsync();
        Task GetBoards();
        #endregion
    }
    #endregion

    #region Abstracts
    public abstract class LoginViewModelBase : ILoginViewModel, IDisposable
    {
        #region Consturctors
        public LoginViewModelBase(IHttpClientFactory clientFactory, IBoardsService boardsService, IUserService userService)
        {
            HttpClient = clientFactory.CreateClient("base");
            BoardsService = boardsService;
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
        public virtual IBoardsService BoardsService { get; set; }
        public virtual IUserService UserService { get; set; }
        public virtual string BoardsSearchText { get; set; }
        public virtual bool LoginFailureHidden { get; set; }
        public virtual bool IsLoadingBoards { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual UserAuthRead UserInfo { get; set; }
        public virtual string ErrorMessage { get; set; }
        public virtual ObservableCollection<BoardRead> Boards { get; set; } = new ObservableCollection<BoardRead>();
        #endregion

        #region Methods
        public abstract Task<string> ValidateLoginAsync();

        public abstract Task GetProfileAsync();
        public abstract Task GetBoards();

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
    public class IndexViewModel : LoginViewModelBase
    {
        public IndexViewModel(IHttpClientFactory clientFactory, IBoardsService boardsService, IUserService userService) : base(clientFactory, boardsService, userService)
        {
        }


        #region Properties
        [Display(Name = "Логин")]
        [MinLength(4, ErrorMessage = "Минимальная длина логина 4 символа")]
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public override string Username { get; set; }

        [Display(Name = "Пароль")]
        [MinLength(6, ErrorMessage = "Минимальная длина логина 6 символов")]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public override string Password { get; set; }

        public override bool LoginFailureHidden { get; set; } = true;
        public override string ErrorMessage { get; set; }
        public override string BoardsSearchText { get; set; } = string.Empty;
        public override UserAuthRead UserInfo { get; set; }
        public override bool IsLoadingBoards { get; set; } = true;
        public override ObservableCollection<BoardRead> Boards { get => base.Boards; set => base.Boards = value; }


        #endregion

        #region Methods
        public override async Task GetBoards()
        {
            IsLoadingBoards = true;

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));

            Boards = new ObservableCollection<BoardRead>(await BoardsService.GetBoardsAsync(UserInfo.Token));

            IsLoadingBoards = false;
        }

        public override async Task GetProfileAsync()
        {
            var value = await SecureStorage.GetAsync("UserInfo");
            if(value != null)
            {
                UserInfo = JsonConvert.DeserializeObject<UserAuthRead>(value);
            }
        }

        public async override Task<string> ValidateLoginAsync()
        {

            LoginFailureHidden = true;

            var authResult = await UserService.AuthUser(Username, Password);

            if (authResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return (authResult.Response as RequestResponse<UserAuthRead>).Response.Token;
            }
            else if (authResult.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                LoginFailureHidden = false;
                ErrorMessage = "Неверный логин или пароль!";
            }
            else
            {
                LoginFailureHidden = false;
                var errors = authResult.Response as BadRequestResponse<IEnumerable<ValidationFailure>>;
                ErrorMessage = errors?.Message;
            }

            return string.Empty;
        }
        #endregion
    }
    #endregion

}
