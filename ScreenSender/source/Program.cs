using ScreenSender.Bot;
using ScreenSender.Managers;
using ScreenSender.Native;
using System.Drawing.Imaging;

namespace ScreenSender;

public class Program
{
    static void Main(string[] args)
    {
#if DEBUG
        NativeMethods.AllocConsole();
#endif

        var token = Settings.BotToken;
        var keyAliases = Settings.KeysAliases;
        var screenshotDirInfo = Settings.ScreenshotSaveDir;
        var receiverId = Settings.ReceiverId;
        var applicationExitCombination = Settings.ApplicationExitCombination;
        var screenshotTakeCombination = Settings.ScreenshotTakeCombination;

        KeyboardManager.SetAliases(keyAliases);

        CreateScreenshotsDirectory(screenshotDirInfo);

        var bot = GetBot(token);

        var screenshotCombination = new KeyboardManager.KeysCombination(
            screenshotTakeCombination, () => TakeAndSendScreenshot(screenshotDirInfo, bot, receiverId));

        var exitCombination = new KeyboardManager.KeysCombination(
            applicationExitCombination, () => Exit(screenshotDirInfo));

        KeyboardManager.StartReadKeysAsync();

        screenshotCombination.StartReadingAsync();
        exitCombination.StartReadingAsync();

        Wait();
    }

    private static void CreateScreenshotsDirectory(DirectoryInfo dirInfo)
    {
        var isDirCreated = FileManager.CreateDirectory(dirInfo.FullName);

        if (!isDirCreated)
        {
            FileManager.CleanDirectory(dirInfo);
        }
    }

    private static TelegramBot GetBot(string token)
    {
        var bot = TelegramBot.Instance;

        bot.Init(token);

        return bot;
    }

    private async static void TakeAndSendScreenshot(DirectoryInfo dirInfo, TelegramBot bot, long receiverId)
    {
        var filepath = ScreenManager.TakeScreenshot(ImageFormat.Jpeg, dirInfo);

        await bot.SendImageAsync(filepath, receiverId);
    }

    private static void Exit(DirectoryInfo screenshotsDirInfo)
    {
        FileManager.CleanDirectory(screenshotsDirInfo);
        Environment.Exit(0);
    }

    private static void Wait()
    {
        while (true)
        {
            Thread.Sleep(int.MaxValue);
        }
    }
}