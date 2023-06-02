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

        var combination = new KeyboardManager.KeysCombination(Settings.ScreenshotTakeCombination, () => Console.WriteLine("pressed"));

        combination.StartReadingAsync();

        var bot = TelegramBot.Instance;

        bot.StartReceiving();

        KeyboardManager.StartReadKeysAsync();

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