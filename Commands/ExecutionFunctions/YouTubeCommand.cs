using Discord.WebSocket;
using GeneralPurposeLib;
using Google;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class YouTubeCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        string searchTerm = cmd.GetArgument<string>("query") ?? "";
        string result;
        try {
            result = await GetVideoLink(searchTerm);
        }
        catch (GoogleApiException) {
            await cmd.RespondWithEmbedAsync("Error", "Communication with the YouTube API failed", ResponseType.Error);
            return;
        }
        await cmd.RespondAsync(result);
    }
    
    private static async Task<string> GetVideoLink(string search) {
        YouTubeService youtubeService = new(new BaseClientService.Initializer {
            ApiKey = Program.Config!["youtube-api-key"],
            ApplicationName = "Serble Bot"
        });

        SearchResource.ListRequest? searchListRequest = youtubeService.Search.List("snippet");
        searchListRequest.Q = search; // Query
        searchListRequest.MaxResults = 5;

        SearchListResponse? searchListResponse;
        try {
            searchListResponse = await searchListRequest.ExecuteAsync();
        }
        catch (GoogleApiException e) {
            Logger.Error("YouTube API Error: " + e.Message);
            Logger.Error(e);
            throw;
        }

        if (searchListResponse.Items.Count == 0) {
            return "No results.";
        }

        foreach (SearchResult? item in searchListResponse.Items) {
            if (item.Id.Kind != "youtube#video") continue;
            return "https://youtube.com/watch?v=" + item.Id.VideoId;
        }
            
        return "No video results.";

    }
}