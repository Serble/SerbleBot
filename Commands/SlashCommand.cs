using Discord;

namespace SerbleBot.Commands; 

public class SlashCommand {
    
    public string Name { get; set; }
    public string Description { get; set; }
    public SlashCommandArgument[] Arguments { get; set; }
    public bool TestingCommand { get; set; }
    public ICommandExecutionHandler OnExecute { get; set; }
    public GuildPermission? RequiredPermissions { get; set; }
    
    public SlashCommand(
        string name, 
        string description, 
        SlashCommandArgument[] args, 
        ICommandExecutionHandler onExecute, 
        GuildPermission? perms = null, 
        bool testing = false) {
        Name = name;
        Description = description;
        OnExecute = onExecute;
        TestingCommand = testing;
        RequiredPermissions = perms;
        Arguments = args;
    }

}