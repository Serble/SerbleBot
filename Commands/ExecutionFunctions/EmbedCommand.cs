using Discord;
using Discord.WebSocket;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class EmbedCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {

        await cmd.DeferAsync();
        
        string title = cmd.GetArgument<string>("title") ?? "";
        string body = cmd.GetArgument<string>("body") ?? "";
        string colour = cmd.GetArgument<string>("colour") ?? "Blue";

        Color color;
        switch (colour.ToLower()) {
            case "blue":
                color = Color.Blue;
                break;
            case "green":
                color = Color.Green;
                break;
            case "red":
                color = Color.Red;
                break;
            case "gold":
            case "yellow":
                color = Color.Gold;
                break;
            case "purple":
                color = Color.Purple;
                break;
            case "orange":
                color = Color.Orange;
                break;
            case "magenta":
                color = Color.Magenta;
                break;
            case "dark blue":
            case "darkblue":
                color = Color.DarkBlue;
                break;
            case "gray":
            case "grey":
                color = Color.LightGrey;
                break;
            case "black":
                color = Color.DarkerGrey;
                break;
            default:
                await cmd.RespondWithUsageAsync("Invalid colour");
                return;
        }

        EmbedBuilder embed = new EmbedBuilder()
            .WithTitle(title)
            .WithDescription(body)
            .WithColor(color);

        await cmd.Channel.SendMessageAsync(embed: embed.Build());
        await cmd.DeleteOriginalResponseAsync();
    }
}