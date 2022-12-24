using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Tasko.Client.Models;
using Tasko.Domains.Models.DTO.User;
using Tasko.General.Models.RequestResponses;

namespace Tasko.Client.Services;

public static class UserService
{


    public static async Task<dynamic> AuthUser(HttpClient client, string login, string password)
    {
        var user = new UserAuth
        {
            Login = login,
            Password = password
        };

        RequestResponse<UserAuthRead> model;

        var content = JsonContent.Create(user);
        var response = await client.PostAsync("/api/auth", content);
        var result = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
            model = JsonConvert.DeserializeObject<RequestResponse<UserAuthRead>>(result);
        else
            return new 
            {
                Response = JsonConvert.DeserializeObject<BadRequestResponse<IEnumerable<ValidationFailure>>>(result), 
                StatusCode = response.StatusCode 
            };

        await SaveUserInStorageAsync(model.Response);

        return new
        {
            Response = model,
            StatusCode = response.StatusCode
        };
    }


    public static async Task SaveUserInStorageAsync(UserAuthRead user)
    {
        await SecureStorage.SetAsync("UserInfo", JsonConvert.SerializeObject(user));
    }

}