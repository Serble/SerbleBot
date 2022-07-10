using Discord.WebSocket;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class WheelOfFortuneCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        string[] options = new string[cmd.Data.Options.Count];

        // Get the options
        {
            int i = 0;
            foreach (var option in cmd.Data.Options) {
                options[i] = (string)option.Value;
                i++;
            }
        }

        await cmd.RespondWithEmbedAsync("Wheel of Fortune", 
            $"**You have rolled:**\n{options[new Random().Next(options.Length)]}");
    }
}