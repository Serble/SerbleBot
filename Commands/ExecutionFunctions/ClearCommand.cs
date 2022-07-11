using Discord;
using Discord.Net;
using Discord.WebSocket;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class ClearCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        int messageCount = (int)cmd.GetArgument<long?>("count")!;

        await cmd.DeferAsync();
        
        if (messageCount > 1000)
            await cmd.RespondAsync("You cannot delete more than 1000 messages at once.");

        try {
            IEnumerable<IMessage> messages = 
                (await cmd.Channel.GetMessagesAsync(messageCount + 1).FlattenAsync()).Skip(1);
            foreach (var message in messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14)) {
                await message.DeleteAsync();
            }
        }
        catch (HttpException) {
            await cmd.ModifyOriginalResponseAsync(msg => msg.Content = 
                "An Error Occured, do I have permission to delete messages?");
            return;
        }

        await cmd.ModifyOriginalResponseAsync(msg => msg.Content = $"Deleted {messageCount} messages.");
    }
}