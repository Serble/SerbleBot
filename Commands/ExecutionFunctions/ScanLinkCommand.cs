using Discord.WebSocket;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Cloud.WebRisk.V1;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class ScanLinkCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        await cmd.RespondWithEmbedAsync("Unimplemented", "This command is not yet implemented.", ResponseType.Error);
        // string link = cmd.GetArgument<string>("link")!;
        //
        // await cmd.DeferAsync();
        //
        // WebRiskServiceClientBuilder builder = new WebRiskServiceClientBuilder {
        //     // TODO: DONT COMMIT UNTIL TOKEN IS REMOVED
        //     GoogleCredential = GoogleCredential.()
        // };
        //
        // WebRiskServiceClient webRiskServiceClient = await builder.BuildAsync();
        //
        // SearchUrisResponse? response = await webRiskServiceClient.SearchUrisAsync(link, new[] {
        //     ThreatType.Malware, 
        //     ThreatType.SocialEngineering
        // });
        //
        // if (response == null) {
        //     await cmd.ModifyWithEmbedAsync("Link Scanner", "No threats found", ResponseType.Success);
        // }
        //
        // await cmd.ModifyWithEmbedAsync("Link Scanner", $"{response!.Threat.ThreatTypes}");
    }
}