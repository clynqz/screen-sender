using System.Drawing.Imaging;

namespace ScreenSender.Managers;

public static class ScreenManager
{
    public static string TakeScreenshot(ImageFormat format, DirectoryInfo saveDirInfo)
    {
        var screen = Screen.PrimaryScreen!;

        using var bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppArgb);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);

        var filename = FileManager.GetScreenshotFilename(format);
        var path = Path.Combine(saveDirInfo.FullName, filename);
        var saveFileInfo = new FileInfo(path);

        SaveScreenshot(bitmap, format, saveFileInfo);

        return path;
    }

    private static void SaveScreenshot(Bitmap bitmap, ImageFormat format, FileInfo saveFileInfo)
    {
        bitmap.Save(saveFileInfo.FullName, format);
    }
}
