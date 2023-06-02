using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

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

    public async Task SendImageAsync(string imagePath, IEnumerable<long> receivers)
    {
        using var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

        var photo = new InputOnlineFile(stream);

        foreach (var receiver in receivers)
        {
            await _client!.SendPhotoAsync(chatId: receiver, photo: photo, cancellationToken: CancellationToken.None);
        }
    }
}
