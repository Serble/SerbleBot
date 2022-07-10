using System.Xml.Serialization;
using Discord.WebSocket;
using GeneralPurposeLib;

namespace SerbleBot.Commands.ExecutionFunctions; 

public class LinkShortenCommand : ICommandExecutionHandler {
    public async void Execute(SocketSlashCommand cmd, DiscordSocketClient client) {
        // Get args
        SocketSlashCommandDataOption[] linkArray = cmd.Data.Options.Where(option => option.Name == "link" ).ToArray();
        if (linkArray.Length != 1) throw new Exception("Invalid number of arguments named link");
        string link = (string) linkArray[0].Value;
        
        SocketSlashCommandDataOption[] nameArray = cmd.Data.Options.Where(option => option.Name == "name" ).ToArray();
        if (nameArray.Length is not (1 or 0)) throw new Exception("Invalid number of arguments named name");
        string name = nameArray.Length == 1 ? (string) nameArray[0].Value : RandomString(10);

        // Send a http request to link.serble.net
        HttpClient http = new ();
        HttpResponseMessage response = await http.PostAsync("https://link.serble.net", new FormUrlEncodedContent(new[] {
            new KeyValuePair<string, string>("location", link),
            new KeyValuePair<string, string>("name", name)
        }));
        string content = await response.Content.ReadAsStringAsync();
        
        // Send the response to the discord channel
        await cmd.RespondAsync(content);
    }
    
    // Function to generate a random string of length n
    private static string RandomString(int length) {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
}