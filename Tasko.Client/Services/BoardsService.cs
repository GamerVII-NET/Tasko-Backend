using Newtonsoft.Json;
using System.Net;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;

namespace Tasko.Client.Services;

public static class BoardsService
{


    public static async Task<IEnumerable<BoardRead>> GetBoardsAsync(HttpClient client)
    {
        var response = await client.GetAsync($"/api/boards");

        if (response.IsSuccessStatusCode)
        {
            var boardsContent = await response.Content.ReadAsStringAsync();

            var boards = JsonConvert.DeserializeObject<GetRequestResponse<BoardRead>>(boardsContent);

            return boards.Response.Items;
        }

        return Enumerable.Empty<BoardRead>();
    }


    public static async Task<(IBaseRequestResponse, HttpStatusCode)> GetBoardInfoAsync(HttpClient client, Guid boardGuid)
    {
        var response = await client.GetAsync($"/api/boards/{boardGuid}");
        var result = await response.Content.ReadAsStringAsync();

        RequestResponse<BoardRead> boardModel;

        if (response.IsSuccessStatusCode)
            boardModel = JsonConvert.DeserializeObject<RequestResponse<BoardRead>>(result);
        else
            return (JsonConvert.DeserializeObject<BadRequestResponse<IEnumerable<Models.ValidationFailure>>>(result), response.StatusCode);

        return (boardModel, response.StatusCode);
    }

}