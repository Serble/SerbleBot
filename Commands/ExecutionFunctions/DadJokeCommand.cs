using System.Text.Json;
using Discord.WebSocket;
using GeneralPurposeLib;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class DadJokeCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        await cmd.DeferAsync();
        
        try {
            HttpClient http = new ();
            http.DefaultRequestHeaders.Add("Accept", "text/plain");
            HttpResponseMessage response = await http.GetAsync("https://icanhazdadjoke.com/");

            string joke = await response.Content.ReadAsStringAsync();
            await cmd.ModifyOriginalResponseAsync(msg => msg.Content = joke);
        }
        catch (Exception e) {
            Logger.Error(e);
            await cmd.ModifyOriginalResponseAsync(msg => msg.Content = 
                "Error: Could not think of a dad joke.");
        }
    }
}