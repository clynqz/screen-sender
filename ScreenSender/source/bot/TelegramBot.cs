using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace ScreenSender.Bot;

public class TelegramBot
{
    private static readonly Lazy<TelegramBot> _instance;

    private readonly TelegramBotClient _client;

    private readonly ReceiverOptions _receiverOptions;

    private readonly CancellationTokenSource _cancellationTokenSource;

    public static TelegramBot Instance => _instance.Value;

    static TelegramBot()
    {
        _instance = new Lazy<TelegramBot>(() => new TelegramBot());
    }

    public TelegramBot()
    {
        _client = new TelegramBotClient(Settings.BotToken);
        _receiverOptions = new ReceiverOptions();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void StartReceiving()
    {
        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: _receiverOptions,
            cancellationToken: _cancellationTokenSource.Token);
    }

    public void StopReceiving()
    {

    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine(update.Message.From.Username);
    }

    private async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {

    }

    private async Task SendTextMessageAsync(ChatId chatId, string text)
    {
        await _client.SendTextMessageAsync(chatId, text, cancellationToken: CancellationToken.None);
    }
}
