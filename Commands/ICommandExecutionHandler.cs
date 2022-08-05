using Discord.WebSocket;

namespace SerbleBot.Commands; 

public interface ICommandExecutionHandler {
    public Task Execute(SocketSlashCommand cmd, DiscordSocketClient client);
}