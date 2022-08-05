using ELIZA.NET;

namespace SerbleBot.Data;

public class ElizaWrapper {
    private readonly ELIZALib _eliza;

    /// <summary>
    /// An example ELIZA wrapper class.
    /// </summary>
    /// <param name="scriptFile">The path to the JSON file containing the ELIZA script.</param>
    public ElizaWrapper(string scriptFile) {
        _eliza = new ELIZALib(File.ReadAllText(scriptFile));
    }

    public string Start() {
        return _eliza.Session.GetGreeting();
    }

    public string Stop() {
        return _eliza.Session.GetGoodbye();
    }

    public string Query(string q) {
        return _eliza.GetResponse(q);
    }
}