using Discord.WebSocket;
using SerbleBot.Data;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class DareCommand : ICommandExecutionHandler {
    public async Task Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        await cmd.RespondWithEmbedAsync("Dare", TruthOrDareService.RandomDare);
    }
}