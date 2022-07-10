using Discord;
using SerbleBot.Commands.ExecutionFunctions;

namespace SerbleBot.Commands; 

public static class Commands {

    public static readonly SlashCommand[] SlashCommands = {
        
        new (
            "info", 
            "Get information about the bot", 
            Array.Empty<SlashCommandArgument>(),
            new InfoCommand(),
            null,
            false),
        
        new ("shorten-link",
            "Generates a shortened link using link.serble.net",
            new [] {
                new SlashCommandArgument("link", "The link to shorten", true, ApplicationCommandOptionType.String),
                new SlashCommandArgument("name", "The custom text to be in the shortened link", false, ApplicationCommandOptionType.String)
            },
            new LinkShortenCommand(),
            null,
            false
            ),
        
        new ("rock-paper-scissors", "Play a game of rock paper scissors",
            new [] {
                new SlashCommandArgument("choice", "Rock paper or scissors?", true, ApplicationCommandOptionType.String)
            },
            new RockPaperScissorsCommand(),
            null,
            false
            ),
        
        new ("8ball", "Ask the magic 8 ball a question",
            new [] {
                new SlashCommandArgument("question", "The question to ask the 8 ball", false, ApplicationCommandOptionType.String)
            },
            new EightBallCommand(),
            null,
            false
            ),
        
        new ("roll", "Roll dice",
            new [] {
                new SlashCommandArgument("sides", "The number of sides on the dice", false, ApplicationCommandOptionType.Integer),
                new SlashCommandArgument("rolls", "The number of dice to roll", false, ApplicationCommandOptionType.Integer)
            },
            new DiceCommand(),
            null,
            false
            ),
        
    };

}