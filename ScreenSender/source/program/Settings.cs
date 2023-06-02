using System.Collections.Immutable;

namespace ScreenSender;

public static class Settings
{
    public static readonly string BotToken;

    public static readonly ImmutableHashSet<string> AllowUpdatesReceiveUsernames;

    public static readonly ImmutableHashSet<string> ReceiversUsernames;

    public static readonly ImmutableArray<Keys> ScreenshotTakeCombination;

    public static readonly IReadOnlyDictionary<Keys, Keys> KeysAliases;

    static Settings()
    {
        BotToken = Secret.BotToken;
        AllowUpdatesReceiveUsernames = Secret.AllowUpdatesReceiveUsernames;
        ReceiversUsernames = Secret.ReceiversUsernames;

        ScreenshotTakeCombination = new List<Keys> { Keys.ControlKey, Keys.Menu, }.ToImmutableArray();
        KeysAliases = GetKeysAliases();
    }

    private static IReadOnlyDictionary<Keys, Keys> GetKeysAliases()
    {
        return new Dictionary<Keys, Keys>
        {
            { Keys.MButton, Keys.LButton },
            { Keys.RButton, Keys.LButton },
            { Keys.XButton1, Keys.LButton },
            { Keys.XButton2, Keys.LButton },
            { Keys.LMenu, Keys.Menu },
            { Keys.RMenu, Keys.Menu },
            { Keys.LControlKey, Keys.ControlKey},
            { Keys.RControlKey, Keys.ControlKey },
            { Keys.LShiftKey, Keys.ShiftKey },
            { Keys.RShiftKey, Keys.ShiftKey },
        }
        .AsReadOnly();
    }
}
