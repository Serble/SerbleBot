using Discord.WebSocket;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class InfoCommand : ICommandExecutionHandler {
    
    public async Task Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        await cmd.RespondAsync($"I am SerbleBot {Program.Version}");
    }
    
}