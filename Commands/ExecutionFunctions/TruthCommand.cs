using Discord.WebSocket;
using SerbleBot.Data;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class TruthCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        await cmd.RespondWithEmbedAsync("Truth", TruthOrDareService.RandomTruth);
    }
}