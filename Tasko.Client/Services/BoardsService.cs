using Newtonsoft.Json;
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

}