using Discord.WebSocket;
using SerbleBot.Data;

namespace SerbleBot; 

public static class DmHandler {
    
    private static readonly Dictionary<ulong, ElizaWrapper> ElizaWrappers = new();

    public static async void Run(SocketMessage msg) {
        if (!ElizaWrappers.ContainsKey(msg.Channel.Id)) {
            ElizaWrapper newWrapper = new("Data/RawData/eliza-doctor.json");
            await msg.Channel.SendMessageAsync(newWrapper.Start());
            ElizaWrappers.Add(msg.Channel.Id, newWrapper);
            return;
        }
        
        ElizaWrapper elizaWrapper = ElizaWrappers[msg.Channel.Id];
        await msg.Channel.SendMessageAsync(elizaWrapper.Query(msg.Content));
    }
    
}