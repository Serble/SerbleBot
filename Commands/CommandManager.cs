using Discord;
using Discord.WebSocket;
using GeneralPurposeLib;

namespace SerbleBot.Commands; 

public static class CommandManager {

    public static async void Invoke(SocketSlashCommand cmdArgs, DiscordSocketClient client) {
        Logger.Debug("Attempting to invoke command: " + cmdArgs.CommandName);
        try {
            await Commands.SlashCommands.Single(slashCmd => slashCmd.Name == cmdArgs.CommandName).OnExecute.Execute(cmdArgs, client);
        }
        catch (Exception e) {
            Logger.Error(e);
            Logger.Error("Execution of command " + cmdArgs.CommandName + " failed.");
            
            // Dump info
            Logger.Debug("Command name: " + cmdArgs.CommandName);
            Logger.Debug($"Command executor: {cmdArgs.User.Username}#{cmdArgs.User.Discriminator}");
            string argsString = cmdArgs.Data.Options.Aggregate("\n", (current, option) => current + $"{option.Name} ({option.Type.ToString()}) = {option.Value}\n");
            Logger.Debug($"Arguments: {argsString}");
            
            await cmdArgs.RespondAsync("Sorry but I couldn't execute that command ¯\\_(ツ)_/¯");
        }
    }
    
    public static void UpdateCommands(DiscordSocketClient client) {
        DateTime startTime = DateTime.Now;
        Logger.Info("Commencing command update");

        foreach (SlashCommand cmd in Commands.SlashCommands) {
            Logger.Debug("Updating command: " + cmd.Name);
            Logger.Debug("Sending command update request");
            if (cmd.TestingCommand) {
                Logger.Debug("Testing command, sending as guild specific");
                SocketGuild testingGuid = client.GetGuild(ulong.Parse(Program.Config!["testing_server_id"]));
                testingGuid.CreateApplicationCommandAsync(cmd.Build()).Wait();
                Logger.Debug("Request finished");
            }
            else {
                Logger.Debug("Sending command as global");
                client.CreateGlobalApplicationCommandAsync(cmd.Build()).Wait();
                Logger.Debug("Request finished");
            }
            Logger.Info("Created command: " + cmd.Name);
        }
        
        Logger.Info("Command update completed in " + (DateTime.Now - startTime).TotalSeconds + " seconds");
        
    }

    public static void UpdateCommand(DiscordSocketClient client, string cmdName) {
        SlashCommand cmd = Commands.SlashCommands.Single(slashCmd => slashCmd.Name == cmdName);
        if (cmd.TestingCommand) {
            Logger.Debug("Testing command, sending as guild specific");
            SocketGuild testingGuid = client.GetGuild(ulong.Parse(Program.Config!["testing_server_id"]));
            testingGuid.CreateApplicationCommandAsync(cmd.Build()).Wait();
            Logger.Debug("Request finished");
        }
        else {
            Logger.Debug("Sending command as global");
            client.CreateGlobalApplicationCommandAsync(cmd.Build()).Wait();
            Logger.Debug("Request finished");
        }
        Logger.Info("Created command: " + cmd.Name);
    }

    public static T? GetArgument<T>(this SocketSlashCommand self, string name) {
        IEnumerable<SocketSlashCommandDataOption> args = self.Data.Options.Where(option => option.Name == name);
        if (!args.Any()) {
            return default;
        }
        return (T) self.Data.Options.Single(option => option.Name == name).Value;
    }
    
    private static Embed GetEmbed(string title, string body, ResponseType type) {
        Color color = type switch {
            ResponseType.Success => Color.Green,
            ResponseType.Error => Color.Red,
            ResponseType.Info => Color.Blue,
            _ => Color.Blue
        };
        
        return new EmbedBuilder()
            .WithTitle(title)
            .WithDescription(body)
            .WithColor(color)
            .Build();
    }

    public static async Task RespondWithEmbedAsync(this SocketSlashCommand self, string title, string body, ResponseType type = ResponseType.Info) {
        await self.RespondAsync(embed: GetEmbed(title, body, type));
    }
    
    public static async Task ModifyWithEmbedAsync(this SocketSlashCommand self, string title, string body, ResponseType type = ResponseType.Info) {
        await self.ModifyOriginalResponseAsync(msg => msg.Embed = GetEmbed(title, body, type));
    }

    public static async Task ModifyBodyTextAsync(this SocketSlashCommand self, string body) {
        await self.ModifyOriginalResponseAsync(msg => msg.Content = body);
    }

    public static async Task RespondWithUsageAsync(this SocketSlashCommand self, string usage) {
        await self.RespondWithEmbedAsync("Usage", usage, ResponseType.Error);
    }
}

public enum ResponseType {
    Success,
    Error,
    Info
}