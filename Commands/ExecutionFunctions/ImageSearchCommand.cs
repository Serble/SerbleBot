using System.Text.Json;
using Discord.WebSocket;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class ImageSearchCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        string query = cmd.GetArgument<string>("query")!;

        await cmd.DeferAsync();

        try {
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.PostAsync("https://search.serble.net/", new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("category_images", "1"),
                new KeyValuePair<string, string>("q", query),
                new KeyValuePair<string, string>("format", "json"),
                new KeyValuePair<string, string>("engines", "google")
            }));
            
            JsonDocument content = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            string url = content.RootElement.GetProperty("results")[0].GetProperty("img_src").GetString()!;
            await cmd.ModifyOriginalResponseAsync(msg => msg.Content = url);
        }
        catch (Exception) {
            await cmd.ModifyOriginalResponseAsync(msg => msg.Content = "Error: Could not find image.");
        }
    }
}