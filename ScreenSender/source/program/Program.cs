using ScreenSender.Bot;
using ScreenSender.Managers;
using ScreenSender.Native;

namespace ScreenSender;

public class Program
{
    static void Main(string[] args)
    {
#if DEBUG
        NativeMethods.AllocConsole();
#endif

        KeyboardManager.SetAliases(Settings.KeysAliases);

        var path = Path.Combine(Environment.CurrentDirectory, Settings.ScreenshotFilename);

        var combination = new KeyboardManager.KeysCombination(Settings.ScreenshotTakeCombination, () => ScreenManager.TakeScreenshot(path));

        var bot = TelegramBot.Instance;

        bot.Init(Settings.BotToken);

        ScreenManager.ScreenshotTaken += async () => await bot.SendImageAsync(path, Settings.ReceiversUsernames);

        KeyboardManager.StartReadKeysAsync();
        combination.StartReadingAsync();

        Wait();
    }

    private static void Wait()
    {
        while (true)
        {
            Thread.Sleep(int.MaxValue);
        }
    }
}