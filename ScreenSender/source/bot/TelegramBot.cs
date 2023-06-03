using Telegram.Bot;
using Telegram.Bot.Types;

namespace ScreenSender.Bot;

public class TelegramBot
{
    private static readonly Lazy<TelegramBot> _instance;

    private TelegramBotClient? _client;

    public static TelegramBot Instance => _instance.Value;

    static TelegramBot()
    {
        _instance = new Lazy<TelegramBot>(() => new TelegramBot());
    }

    private TelegramBot()
    {
    }

    public void Init(string token)
    {
        _client = new TelegramBotClient(token);
    }

    public async Task SendImageAsync(string filepath, long receiver)
    {
        using var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        var photo = new InputFileStream(stream);

        await _client!.SendPhotoAsync(chatId: receiver, photo: photo, cancellationToken: CancellationToken.None);
    }
}
