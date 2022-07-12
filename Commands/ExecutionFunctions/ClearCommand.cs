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

        int messagesCount;
        try {
            IEnumerable<IMessage> messages = 
                (await cmd.Channel
                    .GetMessagesAsync(messageCount + 1)
                    .FlattenAsync())
                    .Skip(1)
                    .Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);
            IEnumerable<IMessage> enumeratedMessages = messages as IMessage[] ?? messages.ToArray();
            messagesCount = enumeratedMessages.Count();
            
            if (cmd.Channel is not ITextChannel textChannel) {
                // Create embed[]
                Embed[] embeds = new Embed[1];
                embeds[0] = new EmbedBuilder()
                    .WithTitle("Usage")
                    .WithDescription("You can only clear messages in non DM text channels.")
                    .WithColor(Color.Red)
                    .Build();
                await cmd.ModifyOriginalResponseAsync(msg => msg.Embeds = new Optional<Embed[]>(embeds));
                return;
            }
            
            // Mass delete
            await textChannel.DeleteMessagesAsync(enumeratedMessages);
        }
        catch (HttpException) {
            await cmd.ModifyOriginalResponseAsync(msg => msg.Content = 
                "An Error Occured, do I have permission to delete messages?");
            return;
        }

        await cmd.ModifyOriginalResponseAsync(msg => 
            msg.Content = $"Deleted {messagesCount} message{(messagesCount == 1 ? "" : "s")}.");
    }
}